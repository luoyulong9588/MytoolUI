using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Drawing;
using Sunny.UI;

namespace MytoolUI.common
{
    class DatabaseForOutpatient
    {
        private static string dataBasePath = @"Data Source=.\config\out_patient_infomations.db";
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\out_patient_infomations.db;Version=3;");
        private SQLiteTransaction _SQLiteTrans = null;
        private UIMessageForm message = new UIMessageForm();

        /// <summary>
        /// 保存门诊日志患者信息到数据库;
        /// </summary>
        /// <param name="sql">sql的insert语句(传值太麻烦了)</param>
        public void SaveInfoToDb(string doctorName,string painName,string gender,string age,string phone,string vocation,string idCard,string workAddress,string nowAddress,string comeDate,string diaseDate,string bloodPressure,string mainChef,string diagMemory,string mainDrug)
        {
            string sql;
            bool exist = QueryDb(doctorName, painName, gender, age, comeDate);
            if (exist)
            {
                sql = $@"update informations set vocation = ""{vocation}"" ,work_addr = ""{workAddress}"" ,chief = ""{mainChef}"" ,diag = ""{diagMemory}"" ,drug = ""{mainDrug}"" where ( doctor = ""{doctorName}""  and patient =""{painName}"" and gender =""{gender}"" and age = ""{age}"" and visit_date = ""{comeDate}"") ";
            }
            else
            {
                sql = $@"insert into informations VALUES(""{doctorName}"",""{painName}"",""{gender}"",""{age}"",""{phone}"",""{vocation}"",""{idCard}"",""{workAddress}"",""{nowAddress}"",""{comeDate}"",""{diaseDate}"",""{bloodPressure}"",""{mainChef}"",""{diagMemory}"",""{mainDrug}"")";
            }
           // string sql = $@"insert into informations VALUES(""{doctorName}"",""{painName}"",""{gender}"",""{age}"",""{phone}"",""{vocation}"",""{idCard}"",""{workAddress}"",""{nowAddress}"",""{comeDate}"",""{diaseDate}"",""{bloodPressure}"",""{mainChef}"",""{diagMemory}"",""{mainDrug}"")";
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Connection = m_dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            
        }

        public bool QueryDb(string doctorName, string painName, string gender, string age, string comeDate)
        {
            string sql = $@"select * from informations where doctor = ""{doctorName}""  and patient =""{painName}"" and gender =""{gender}"" and age = ""{age}"" and visit_date = ""{comeDate}""";
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


        public Pain QueryInfo(string name, string idCard,DateTime onsetDate)
        {
            Pain pain = new Pain();
            m_dbConnection.Open();
             
            SQLiteCommand command = new SQLiteCommand($@"select * from informations where id_card=""{idCard}"" and onset_date = ""{onsetDate.ToString("yyyy-MM-dd")}""", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                pain.DoctorName = reader[0].ToString();
                pain.Name = reader[1].ToString();
                pain.Gender = reader[2].ToString();
                pain.Age = reader[3].ToString();
                pain.Phone = reader[4].ToString();
                pain.Vocation = reader[5].ToString();
                pain.IdCardNumber = reader[6].ToString();
                pain.WorkAddr = reader[7].ToString();
                pain.HomeAddr = reader[8].ToString();
                pain.BloodPresure = reader[11].ToString();
                pain.MainChief = reader[12].ToString();
                pain.MainDiagnose = reader[13].ToString();
                reader.Close();
                m_dbConnection.Close();
                return pain;
            }
            command = new SQLiteCommand($@"select * from informations where patient=""{name}""  and onset_date = ""{onsetDate.ToString("yyyy-MM-dd")}""", m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                pain.DoctorName = reader[0].ToString();
                pain.Name = reader[1].ToString();
                pain.Gender = reader[2].ToString();
                pain.Age = reader[3].ToString();
                pain.Phone = reader[4].ToString();
                pain.Vocation = reader[5].ToString();
                pain.IdCardNumber = reader[6].ToString();
                pain.WorkAddr = reader[7].ToString();
                pain.HomeAddr = reader[8].ToString();
                pain.BloodPresure = reader[11].ToString();
                pain.MainChief = reader[12].ToString();
                pain.MainDiagnose = reader[13].ToString();
                reader.Close();
                m_dbConnection.Close();
                return pain;
            }

            reader.Close();
            m_dbConnection.Close();
            return pain;

        }
    }
}
