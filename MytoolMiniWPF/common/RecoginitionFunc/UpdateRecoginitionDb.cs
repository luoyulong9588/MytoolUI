using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MytoolMiniWPF.common.RecoginitionFunc
{
    internal class UpdateRecoginitionDb
    {
        string connectionString;
        public UpdateRecoginitionDb(string connString) { 
            connectionString = connString;
        }

        public void UpdateAddressAndPhone()
        {
            

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 查询 recognition 表中 Address 或 Phone 为空的记录的 PatientID
                List<string> patientIdsToUpdate = new List<string>();
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT PatientID FROM recognition WHERE Address = '' OR Phone = ''", conn))
                {
                    using (MySqlDataReader reader1 = cmd1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            patientIdsToUpdate.Add(reader1.GetString("PatientID"));
                        }
                    }
                }

                // 如果没有需要更新的记录，直接返回
                if (patientIdsToUpdate.Count == 0)
                {
                    Console.WriteLine("No patients with missing Address or Phone found.");
                    return;
                }

                // 查询这些 PatientID 对应的 Address 和 Phone 信息，并批量更新 recognition 表
                foreach (string patientId in patientIdsToUpdate)
                {
                    // 查询 inhospitalinfo 表获取 Address 和 Phone
                    string address = null;
                    string phone = null;

                    using (MySqlCommand cmd2 = new MySqlCommand("SELECT Address, Phone FROM inhospitalinfos WHERE PatientID = @PatientID", conn))
                    {
                        cmd2.Parameters.AddWithValue("@PatientID", patientId);
                        using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                address = reader2.IsDBNull(0) ? null : reader2.GetString(0);
                                phone = reader2.IsDBNull(1) ? null : reader2.GetString(1);
                            }
                        }
                    }

                    // 如果找到 Address 或 Phone，更新 recognition 表中的记录
                    if (address != null || phone != null)
                    {
                        string updateSql = "UPDATE recognition SET ";
                        List<MySqlParameter> parameters = new List<MySqlParameter>();

                        // 仅更新非空的字段
                        if (address != null)
                        {
                            updateSql += "Address = @Address";
                            parameters.Add(new MySqlParameter("@Address", address));
                        }

                        if (phone != null)
                        {
                            if (parameters.Count > 0)
                            {
                                updateSql += ", ";
                            }
                            updateSql += "Phone = @Phone";
                            parameters.Add(new MySqlParameter("@Phone", phone));
                        }

                        updateSql += " WHERE PatientID = @PatientID";
                        parameters.Add(new MySqlParameter("@PatientID", patientId));

                        // 执行更新操作
                        using (MySqlCommand cmd3 = new MySqlCommand(updateSql, conn))
                        {
                            cmd3.Parameters.AddRange(parameters.ToArray());
                            cmd3.ExecuteNonQuery();
                            Console.WriteLine($"Updated Address and/or Phone for PatientID: {patientId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No Address or Phone found for PatientID: {patientId}");
                    }
                }
            }
        }

        // 主函数
        public void Start()
        {
            UpdateAddressAndPhone();
        }
    }
}
