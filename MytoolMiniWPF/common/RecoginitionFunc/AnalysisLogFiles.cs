using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MytoolMiniWPF.common
{
    
    internal class AnalysisLogFiles
    {
        string directory = @"D:\Log_医检互认";
        string connectionString = "server=localhost;port=3306;user=mytool;password=123321;database=mytooldata";

        public  AnalysisLogFiles(string dir,string connstr)
                {
                directory = dir;
                connectionString = connstr;
                }
        // 计算年龄
        public  int CalculateAge(string birthDateStr)
        {
            DateTime birthDate = DateTime.ParseExact(birthDateStr, "yyyyMMdd", null);
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }

        // 读取文件内容并返回患者列表
        public  List<Dictionary<string, string>> GetFileItems(string filePath)
        {
            List<Dictionary<string, string>> patientsList = new List<Dictionary<string, string>>();
            string[] lines = File.ReadAllLines(filePath);
            var patientsItem = new FixedSizeDeque<string>();
            foreach (var line in lines)
            {
                if (line.Contains("显示互认提示框")) continue;
                string patientName = "", patientId = "", patientBirthday = "", patientGender = "", patientDiagnose = "", patientNewItem = "";
                string patientPhone = "", patientAddress = "", doctorName = "", logDate = "", age = "";

                bool type1 = false, type2 = false;
                JObject entryInfo = null, leaveInfo = null;
                patientsItem.Add(line);
                if (line.Contains("要打开"))
                {
                    // 处理日志信息（模拟）
                    string[] separators = { "入参:" };
                    string[] secondSeparators = { "出参:" };
                    var entryLine = patientsItem.GetAll()[0];
                    var leaveLine = patientsItem.GetAll()[1];
                    logDate = entryLine.Split(new string[] { "====>" }, StringSplitOptions.None)[0];
                    // 解析 JSON 数据
                    try
                    {
                        entryInfo = JObject.Parse(entryLine.Split(separators, StringSplitOptions.None)[1]);
                        leaveInfo = JObject.Parse(leaveLine.Split(secondSeparators, StringSplitOptions.None)[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }
                    if (entryInfo == null || leaveInfo == null)
                    {
                        continue;
                    }

                    if (entryLine.Contains("患者近期相似检查项目查询接口"))
                    {
                        Console.WriteLine("患者近期相似检查项目查询接口");
                        type1 = true;

                        patientName = entryInfo["patience"]["name"].ToString();

                        patientId = entryInfo["patience"]["id_no"].ToString();
                        patientBirthday = entryInfo["patience"]["birthday"].ToString().Replace("-", "");

                        patientGender = entryInfo["patience"]["gender"].ToString() == "1" ? "男" : "女";

                        //patientDiagnose = query_from_local_db(patient_id, QueryType.DIAGNOSE)

                        patientPhone = entryInfo["patience"]["phone"].ToString();
                        patientAddress = entryInfo["patience"]["address"].ToString();
                        doctorName = entryInfo["doctor"]["name"].ToString();
                        var projectList = entryInfo["visit_order"]["project_list"];
                        var listNewItem = projectList
                                .Select(item => $"{item["project_name"]}:{item["study_body_part"]}")
                                .ToList();
                        patientNewItem = String.Join(" ", listNewItem);





                    }

                    if (leaveLine.Contains("您开立的项目有可互认的结果，请确认是否互认"))
                    {
                        type2 = true;
                        patientId = entryInfo["messages"]["PID"]["identityNo"].ToString();
                        patientName = entryInfo["messages"]["PID"]["personalName"].ToString();
                        patientGender = entryInfo["messages"]["PID"]["sexCode"].ToString() == "1" ? "男" : "女";
                        patientBirthday = entryInfo["messages"]["PID"]["birthDate"].ToString();
                        patientDiagnose = entryInfo["messages"]["DG1"]["diagName"].ToString();
                        doctorName = entryInfo["messages"]["PV1"]["doctorName"].ToString();
                        patientNewItem = entryInfo["messages"]["OBR"]["lab"][0]["labItemName"].ToString();


                        patientPhone = "";
                        patientAddress = "";

                    }

                    if (type1 || type2)
                    {
                        age = CalculateAge(patientBirthday).ToString();
                        Dictionary<string, string> patientItemToSave = new Dictionary<string, string>
                             {
                    { "patient_id", patientId },
                    { "name", patientName },
                    { "doctor", doctorName },
                    { "birthday", patientBirthday },
                    { "gender", patientGender },
                    { "age", $"{age}岁" },
                    { "diagnose", patientDiagnose },
                    { "patient_phone", patientPhone },
                    { "patient_address", patientAddress },
                    { "new_item", patientNewItem },
                    { "log_date", logDate }
                          };
                        patientsList.Add(patientItemToSave);
                    }

                }
            }

            return patientsList;
        }

        // 过滤文件并插入数据库
        public  void FilterFilesAndInsert(string directory, MySqlConnection connection)
        {
            var files = Directory.GetFiles(directory, "*.txt");
            foreach (var file in files)
            {
                var patientsList = GetFileItems(file);
                if (patientsList.Count == 0)
                { continue; }
                InsertDb(patientsList, connection);
            }
        }

        // 查询数据库是否存在记录
        public  bool QueryDb(string logDate, MySqlConnection connection)
        {
            string sql = $"SELECT * FROM recognition WHERE LogDate = @logDate";
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@logDate", logDate);
                var result = cmd.ExecuteScalar();
                return result != null;
            }
        }

        // 插入数据库
        public  void InsertDb(List<Dictionary<string, string>> patientsList, MySqlConnection connection)
        {
            string sql = @"INSERT INTO recognition 
                        (LogDate, Name, PatientID, Birthday, Gender, Age, Diagnose, Address, NewItem, Phone, Doctor) 
                        VALUES (@log_date, @name, @patient_id, @birthday, @gender, @age, @diagnose, @address, @new_item, @phone, @doctor)";

            foreach (var patient in patientsList)
            {
                if (QueryDb(patient["log_date"], connection))
                {
                    Console.WriteLine("Data already exists in recognition.");
                    continue;
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@log_date", patient["log_date"]);
                    cmd.Parameters.AddWithValue("@name", patient["name"]);
                    cmd.Parameters.AddWithValue("@patient_id", patient["patient_id"]);
                    cmd.Parameters.AddWithValue("@birthday", patient["birthday"]);
                    cmd.Parameters.AddWithValue("@gender", patient["gender"]);
                    cmd.Parameters.AddWithValue("@age", patient["age"]);
                    cmd.Parameters.AddWithValue("@diagnose", patient["diagnose"]);
                    cmd.Parameters.AddWithValue("@address", patient["patient_address"]);
                    cmd.Parameters.AddWithValue("@new_item", string.Join(",", patient["new_item"])); // 假设是一个字符串数组
                    cmd.Parameters.AddWithValue("@phone", patient["patient_phone"]);
                    cmd.Parameters.AddWithValue("@doctor", patient["doctor"]);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data inserted successfully!");
                }
            }
        }

        // 创建数据库
        public static void CreateDb(MySqlConnection connection)
        {
            string createTableSql = @"
            CREATE TABLE IF NOT EXISTS recognition (
                LogDate VARCHAR(255)  PRIMARY KEY,
                Name VARCHAR(255),
                PatientID VARCHAR(255),
                Birthday VARCHAR(255),
                Gender VARCHAR(255),
                Age VARCHAR(255),
                Diagnose VARCHAR(255),
                Address VARCHAR(255),
                NewItem VARCHAR(255),
                Phone VARCHAR(255),
                Doctor VARCHAR(255)
            );
        ";

            using (MySqlCommand cmd = new MySqlCommand(createTableSql, connection))
            {
                cmd.ExecuteNonQuery();
            }

            string createTableSql2 = @"
            CREATE TABLE IF NOT EXISTS zlbhPath (
                directory VARCHAR(255) PRIMARY KEY
            );
        ";

            using (MySqlCommand cmd = new MySqlCommand(createTableSql2, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // 主函数
        public void  Start()
        {

            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();
            CreateDb(conn);
            FilterFilesAndInsert(directory, conn);
            conn.Close();
        }

        public class FixedSizeDeque<T>
        {
            private LinkedList<T> deque;
            private int maxSize;

            public FixedSizeDeque(int maxSize = 3)
            {
                this.maxSize = maxSize;
                this.deque = new LinkedList<T>();
            }

            public void Add(T item)
            {
                if (deque.Count == maxSize)
                {
                    deque.RemoveFirst(); // 删除队列的第一个元素
                }
                deque.AddLast(item); // 添加新元素
            }

            public List<T> GetAll()
            {
                return new List<T>(deque); // 返回队列中的所有元素
            }
        }
    }
}
