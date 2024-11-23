using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Drawing;

namespace MytoolMiniWPF.common
{
    internal class AddressData
    {

        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\address.db;Version=3;");

        public string GetDistrictName(string idCard)
        {
            int districtId = 500222;
            try
            {
                districtId = int.Parse(idCard.Substring(0, 6));
            }
            catch (Exception ex)
            {

                Console.WriteLine($"idCard截取失败,{ex}");
            }

            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"select district_name from district_id where  district_id = {districtId}", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var result = reader[0];
                reader.Close();
                m_dbConnection.Close();
                return result.ToString();
            }
            reader.Close();
            m_dbConnection.Close();
            Console.WriteLine("行政区域数据库中未检索到户籍地址");
            return null;

        }
    }
}
