using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using WpfToast.Controls;

namespace MytoolMiniWPF.common
{
    class DatabaseForPatient
    {
        private string dbName = "D:\\MytoolDataFiles\\data\\patient_infomations.db";
        public SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=D:\MytoolDataFiles\data\patient_infomations.db;Version=3;");


        public DatabaseForPatient()
        {
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
                        command.CommandText = @"CREATE TABLE out_patient_infos (  
                        patient_id TEXT,  
                        doctor TEXT,  
                        patient TEXT,  
                        gender TEXT,  
                        age TEXT,  
                        phone TEXT,  
                        vocation TEXT,  
                        id_card TEXT,  
                        work_addr TEXT,  
                        addr_now TEXT,  
                        visit_date TEXT,  
                        onset_date TEXT,  
                        blood_presure TEXT,  
                        chief TEXT,  
                        diag TEXT,  
                        drug TEXT)";
                        command.ExecuteNonQuery();  // 执行SQL命令，创建表  

                    command.CommandText = @"CREATE TABLE in_hospital_infos (  
                        patient_id TEXT PRIMARY KEY,  
                        doctor TEXT,  
                        patient TEXT,  
                        gender TEXT,  
                        age TEXT,  
                        phone TEXT,   
                        id_card TEXT,  
                        addr_now TEXT,  
                        in_date TEXT,  
                        out_date TEXT,
                        during_date TEXT,
                        diagnose TEXT)";
                    command.ExecuteNonQuery();  // 执行SQL命令，创建表  
                }
                    m_dbConnection.Close();
            }
        }
        public void InsertPatientInfo(Patient patient)
        {
            //m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"insert into in_hospital_infos values('{patient.Id}','{patient.DoctorName}','{patient.Name}','{patient.Gender}','{patient.Age}','{patient.Phone}','{patient.IdCardNumber}','{patient.HomeAddr}','{patient.InDay}','{patient.OutDay}','{patient.DuringDay}','{patient.OutDiagnose}')", m_dbConnection);
            try
            {
                command.ExecuteNonQuery();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Toast.Show($"{patient.Name}:{patient.Id} 记录为空！插入数据。", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 1000, Location = ToastLocation.ScreenCenter });
                    
                });
            }
            catch (SQLiteException)
            {
                command.CommandText = $"update in_hospital_infos set doctor = '{patient.DoctorName}', patient = '{patient.DoctorName}',gender = '{patient.Gender}',age = '{patient.Age}',phone = '{patient.Phone}',id_card = '{patient.IdCardNumber}',addr_now = '{patient.HomeAddr}',in_date = '{patient.InDay}',out_date = '{patient.OutDay}',during_date = '{patient.DuringDay}',diagnose='{patient.OutDiagnose}' where patient_id = '{patient.Id}'";
                command.ExecuteNonQuery();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Toast.Show($"{patient.Name}:{patient.Id} 记录已存在！更新数据。", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 1000, Location = ToastLocation.ScreenCenter });

                });
            }

            //m_dbConnection.Close();
        }

        /// <summary>
        /// 保存门诊日志患者信息到数据库;
        /// </summary>
        /// <param name="sql">sql的insert语句(传值太麻烦了)</param>
        public void SaveInfoToDb(string doctorName, string patientName, string gender, string age, string phone, string vocation, string idCard, string workAddress, string nowAddress, string comeDate, string diaseDate, string bloodPressure, string mainChef, string diagMemory, string mainDrug)
        {
            string sql;
            bool exist = QueryDb(doctorName, patientName, gender, age, comeDate);
            if (exist)
            {
                sql = $@"update out_patient_infos set vocation = ""{vocation}"" ,work_addr = ""{workAddress}"" ,chief = ""{mainChef}"" ,diag = ""{diagMemory}"" ,drug = ""{mainDrug}"" where ( doctor = ""{doctorName}""  and patient =""{patientName}"" and gender =""{gender}"" and age = ""{age}"" and visit_date = ""{comeDate}"") ";
            }
            else
            {
                sql = $@"insert into out_patient_infos VALUES(""{doctorName}"",""{patientName}"",""{gender}"",""{age}"",""{phone}"",""{vocation}"",""{idCard}"",""{workAddress}"",""{nowAddress}"",""{comeDate}"",""{diaseDate}"",""{bloodPressure}"",""{mainChef}"",""{diagMemory}"",""{mainDrug}"")";
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
            string sql = $@"select * from out_patient_infos where doctor = ""{doctorName}""  and patient =""{painName}"" and gender =""{gender}"" and age = ""{age}"" and visit_date = ""{comeDate}""";
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


        public Patient QueryInfo(string name, string idCard, DateTime onsetDate)
        {
            Patient patient = new Patient();
            m_dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand($@"select * from out_patient_infos where id_card=""{idCard}"" and onset_date = ""{onsetDate.ToString("yyyy-MM-dd")}""", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                patient.DoctorName = reader[0].ToString();
                patient.Name = reader[1].ToString();
                patient.Gender = reader[2].ToString();
                patient.Age = reader[3].ToString();
                patient.Phone = reader[4].ToString();
                patient.Vocation = reader[5].ToString();
                patient.IdCardNumber = reader[6].ToString();
                patient.WorkAddr = reader[7].ToString();
                patient.HomeAddr = reader[8].ToString();
                patient.BloodPresure = reader[11].ToString();
                patient.MainChief = reader[12].ToString();
                patient.MainDiagnose = reader[13].ToString();
                reader.Close();
                m_dbConnection.Close();
                return patient;
            }
            command = new SQLiteCommand($@"select * from out_patient_infos where patient=""{name}""  and onset_date = ""{onsetDate.ToString("yyyy-MM-dd")}""", m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                patient.DoctorName = reader[0].ToString();
                patient.Name = reader[1].ToString();
                patient.Gender = reader[2].ToString();
                patient.Age = reader[3].ToString();
                patient.Phone = reader[4].ToString();
                patient.Vocation = reader[5].ToString();
                patient.IdCardNumber = reader[6].ToString();
                patient.WorkAddr = reader[7].ToString();
                patient.HomeAddr = reader[8].ToString();
                patient.BloodPresure = reader[11].ToString();
                patient.MainChief = reader[12].ToString();
                patient.MainDiagnose = reader[13].ToString();
                reader.Close();
                m_dbConnection.Close();
                return patient;
            }

            reader.Close();
            m_dbConnection.Close();
            return patient;

        }
    }
}
