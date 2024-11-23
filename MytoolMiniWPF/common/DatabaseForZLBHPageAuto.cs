using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MytoolMiniWPF.common
{
    class DatabaseForZLBHPageAuto
    {
        private string dbName = "D:\\MytoolDataFiles\\data\\person_infomations.db";
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=D:\MytoolDataFiles\data\person_infomations.db;Version=3;");

        public DatabaseForZLBHPageAuto() {
            CheckAndCreateDirectory("D:\\MytoolDataFiles\\data\\");
            CheckAndCreateDb();
        }

        public bool CheckAndCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CheckAndCreateDb()
        {
            if (!File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);  // 创建一个新的SQLite数据库文件  
                m_dbConnection.Open();
                using (var command = new SQLiteCommand(m_dbConnection))
                {
                    command.CommandText = @"CREATE TABLE main_page_person_infos (  
                        residentPhysician TEXT,  
                        attendingPhysician TEXT,  
                        associateChiefPhysician TEXT,  
                        qualityControlDoctor TEXT,  
                        qualityControlNurse TEXT,  
                        headOfDepartment TEXT)";
                    command.ExecuteNonQuery();  // 执行SQL命令，创建表  
                }
                m_dbConnection.Close();
            }
        }

        public void SaveInfoToDb(string residentPhysician, string attendingPhysician, string associateChiefPhysician, string qualityControlDoctor, string qualityControlNurse, string headOfDepartment)
        
        {
            string sql;
            bool exist = QueryDb(residentPhysician);
            if (exist)
            {
                sql = $@"update main_page_person_infos set residentPhysician = ""{residentPhysician}"" ,attendingPhysician = ""{attendingPhysician}"" ,associateChiefPhysician = ""{associateChiefPhysician}"" ,qualityControlDoctor = ""{qualityControlDoctor}"" ,qualityControlNurse = ""{qualityControlNurse}"",headOfDepartment=""{headOfDepartment}"" where ( residentPhysician = ""{residentPhysician}"") ";
            }
            else
            {
                sql = $@"insert into main_page_person_infos VALUES(""{residentPhysician}"",""{attendingPhysician}"",""{associateChiefPhysician}"",""{qualityControlDoctor}"",""{qualityControlNurse}"",""{headOfDepartment}"")";
            }
            // string sql = $@"insert into informations VALUES(""{doctorName}"",""{painName}"",""{gender}"",""{age}"",""{phone}"",""{vocation}"",""{idCard}"",""{workAddress}"",""{nowAddress}"",""{comeDate}"",""{diaseDate}"",""{bloodPressure}"",""{mainChef}"",""{diagMemory}"",""{mainDrug}"")";
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Connection = m_dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public bool QueryDb(string residentPhysician)
        {
             string sql = $@"select * from main_page_person_infos where residentPhysician = ""{residentPhysician}""";
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var result = reader[0];
                reader.Close();
                m_dbConnection.Close();
                return true;
            }
            reader.Close();
            m_dbConnection.Close();
            return false;
        }
        public Dictionary<string, string> QueryInfo(string residentPhysician)
        {
            Dictionary<string, string> personInfos = new Dictionary<string, string>();
            m_dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand($@"select * from main_page_person_infos where residentPhysician=""{residentPhysician}""", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                personInfos.Add("residentPhysician", reader[0].ToString().Trim());
                personInfos.Add("attendingPhysician", reader[1].ToString().Trim());
                personInfos.Add("associateChiefPhysician", reader[2].ToString().Trim());
                personInfos.Add("qualityControlDoctor", reader[3].ToString().Trim());
                personInfos.Add("qualityControlNurse", reader[4].ToString().Trim());
                personInfos.Add("headOfDepartment", reader[5].ToString().Trim());
                reader.Close();
                m_dbConnection.Close();
                break;
            }
            return personInfos;
        }

    }
}
