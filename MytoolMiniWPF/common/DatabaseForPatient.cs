using MytoolMiniWPF.views;
using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WpfToast.Controls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void InsertPatientsInfo(System.Windows.Controls.DataGrid dataGridPatientsInfo)
        {
            using (m_dbConnection)
            {
                m_dbConnection.Open();

                // 创建表（如果不存在）
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS InHospitalInfos (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    PatientName TEXT NOT NULL,
                    Gender TEXT,
                    Age TEXT,                    
                    PatientID TEXT NOT NULL,  
                    HospitalID TEXT NOT NULL,
                    Doctor TEXT NOT NULL,  
                    Address TEXT,
                    WorkAddress TEXT,
                    Phone TEXT,
                    Diagnose TEXT,
                    InDate DATETIME,
                    OutDate DATETIME,
                    DuratingDate INT,
                    UNIQUE(HospitalID, InDate) -- 联合主键，确保身份证号码和住院号唯一
                );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, m_dbConnection))
                {
                    cmd.ExecuteNonQuery(); // 执行创建表的命令
                }
                // 获取 DataGrid 的数据源（假设数据源是 DataTable）
                //DataTable dataTable = (DataTable)dataGridPatientsInfo.ItemsSource;

                if (dataGridPatientsInfo.ItemsSource is DataView dataView)
                {
                    // 获取底层的 DataTable
                    DataTable dataTable = dataView.Table;

                    // 继续处理 dataTable
                    // 遍历 DataTable 的每一行并插入数据库
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // 获取你想要存储的列数据
                        string patientID = row["身份证号"].ToString();  // 身份证号码
                        string hospitalID = row["住院号"].ToString(); // 住院号
                        string name = row["姓名"].ToString();
                        string age = row["年龄"].ToString();
                        string gender = row["性别"].ToString();
                        string address = row["现住址"].ToString();
                        string workAddress = row["工作单位"].ToString();
                        string phone = row["联系电话"].ToString();
                        string diagnose = row["出院诊断"].ToString();
                        string inDate = row["入院日期"].ToString();
                        string outDate = row["出院日期"].ToString();
                        string doctor = row["主治医生"].ToString();
                        DateTime admissionDate = DateTime.ParseExact(inDate, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
                        DateTime dischargeDate = DateTime.ParseExact(outDate, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
                        int duratingDate = Convert.ToInt32(row["天数"]);

                        // 检查数据库中是否已有相同的身份证号码和住院号
                        string checkQuery = "SELECT COUNT(*) FROM InHospitalInfos WHERE PatientID = @PatientID AND HospitalID = @HospitalID";

                        using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, m_dbConnection))
                        {
                            checkCmd.Parameters.AddWithValue("@PatientID", patientID);
                            checkCmd.Parameters.AddWithValue("@HospitalID", hospitalID);

                            long count = (long)checkCmd.ExecuteScalar();  // 获取记录数

                            if (count > 0)
                            {
                                Console.WriteLine($"记录已存在：{patientID}, {hospitalID}"); // 如果记录已存在，打印提示信息
                                continue;
                            }

                            // 如果没有记录，执行插入操作

                            // 创建 SQL 插入命令
                            string insertQuery = "INSERT INTO InHospitalInfos (PatientName, Gender, Age,PatientID,HospitalID, Doctor, Address, WorkAddress,Phone,Diagnose,InDate,OutDate,DuratingDate) " +
                                "VALUES (@PatientName, @Gender, @Age, @PatientID, @HospitalID, @Doctor, @Address, @WorkAddress, @Phone, @Diagnose, @InDate, @OutDate, @DuratingDate)";

                            using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, m_dbConnection))
                            {
                                insertCmd.Parameters.AddWithValue("@PatientName", name);
                                insertCmd.Parameters.AddWithValue("@Gender", gender);
                                insertCmd.Parameters.AddWithValue("@Age", age);
                                insertCmd.Parameters.AddWithValue("@PatientID", patientID);
                                insertCmd.Parameters.AddWithValue("@HospitalID", hospitalID);
                                insertCmd.Parameters.AddWithValue("@Address", address);
                                insertCmd.Parameters.AddWithValue("@WorkAddress", workAddress);
                                insertCmd.Parameters.AddWithValue("@Phone", phone);
                                insertCmd.Parameters.AddWithValue("@Diagnose", diagnose);
                                insertCmd.Parameters.AddWithValue("@InDate", admissionDate);
                                insertCmd.Parameters.AddWithValue("@OutDate", dischargeDate);
                                insertCmd.Parameters.AddWithValue("@DuratingDate", duratingDate);
                                insertCmd.Parameters.AddWithValue("@Doctor", doctor);
                                insertCmd.ExecuteNonQuery(); // 执行插入
                            }
                        }

                       
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Toast.Show($" 已导入数据库。", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 1000, Location = ToastLocation.ScreenCenter });
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Toast.Show($" 导入数据库出现未知错误。", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 1000, Location = ToastLocation.ScreenCenter });
                    });
                }


                
            }

        }

        public void InsertPatientsInfoMySQL(System.Windows.Controls.DataGrid dataGridPatientsInfo,string connString)
        {
            string connectionString = connString;

            using (var m_mysqldbConnection = new MySqlConnection(connectionString))
            {
                m_mysqldbConnection.Open();

                // 使用事务开始批量插入
                using (var transaction = m_mysqldbConnection.BeginTransaction())
                {
                    try
                    {
                        // 创建表（如果不存在）
                        string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS InHospitalInfos (
                        ID INT AUTO_INCREMENT PRIMARY KEY,
                        PatientName VARCHAR(255) NOT NULL,
                        Gender VARCHAR(10),
                        Age VARCHAR(10),
                        PatientID VARCHAR(255) NOT NULL,
                        HospitalID VARCHAR(255) NOT NULL,
                        Doctor VARCHAR(255) NOT NULL,
                        Address VARCHAR(255),
                        WorkAddress VARCHAR(255),
                        Phone VARCHAR(20),
                        Diagnose TEXT,
                        Vocation TEXT,
                        InDate DATETIME,
                        OutDate DATETIME,
                        DuratingDate INT,
                        Department TEXT,
                        UNIQUE KEY(HospitalID)  -- 只确保 HospitalID 唯一
                    );";

                        using (var cmd = new MySqlCommand(createTableQuery, m_mysqldbConnection, transaction))
                        {
                            cmd.ExecuteNonQuery(); // 执行创建表的命令
                        }

                        // 获取 DataGrid 的数据源（假设数据源是 DataTable）
                        if (dataGridPatientsInfo.ItemsSource is DataView dataView)
                        {
                            DataTable dataTable = dataView.Table;

                            // 获取所有待插入的住院号
                            var hospitalIDs = dataTable.AsEnumerable()
                                .Select(row => row["住院号"].ToString())
                                .Distinct()
                                .ToArray();

                            // 动态生成 IN 子句的参数
                            var parametersList = new List<MySqlParameter>();
                            var placeholders = new List<string>(); // 用于存储 IN 子句的占位符

                            for (int i = 0; i < hospitalIDs.Length; i++)
                            {
                                var param = new MySqlParameter($"@HospitalID{i}", MySqlDbType.VarChar) { Value = hospitalIDs[i] };
                                parametersList.Add(param);
                                placeholders.Add($"@HospitalID{i}");
                            }

                            // 查询数据库中已存在的住院号
                            string checkQuery = $@"
                        SELECT HospitalID FROM InHospitalInfos 
                        WHERE HospitalID IN ({string.Join(",", placeholders)})";

                            using (var checkCmd = new MySqlCommand(checkQuery, m_mysqldbConnection, transaction))
                            {
                                checkCmd.Parameters.AddRange(parametersList.ToArray());

                                var reader = checkCmd.ExecuteReader();
                                var existingHospitalIDs = new HashSet<string>();

                                while (reader.Read())
                                {
                                    existingHospitalIDs.Add(reader.GetString(0)); // 收集已存在的住院号
                                }

                                reader.Close();

                                // 创建批量插入的 SQL 语句
                                var insertQuery =  new StringBuilder("INSERT INTO InHospitalInfos (PatientName, Gender, Age, PatientID, HospitalID, Doctor, Address, WorkAddress, Phone, Diagnose, Vocation, InDate, OutDate, DuratingDate, Department) VALUES ");
                                bool isFirst = true;
                                var insertValuesList = new List<string>();  // 用于存储每一条记录的值
                                var insertParametersList = new List<MySqlParameter>(); // 用于存储参数对象
                                int cycleCount = 0 ;
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    string hospitalID = row["住院号"].ToString();

                                    // 如果住院号已存在，跳过当前行
                                    if (existingHospitalIDs.Contains(hospitalID))
                                    {
                                        Console.WriteLine($"记录已存在：{hospitalID}");
                                        continue;
                                    }

                                    // 获取其他数据
                                    string patientID = row["身份证号"].ToString();
                                    string name = row["姓名"].ToString();
                                    string age = row["年龄"].ToString();
                                    string gender = row["性别"].ToString();
                                    string address = row["现住址"].ToString();
                                    string workAddress = row["工作单位"].ToString();
                                    string phone = row["联系电话"].ToString();
                                    string diagnose = row["出院诊断"].ToString();
                                    string vocation = row["职业"].ToString();
                                    string inDate = row["入院日期"].ToString();
                                    string outDate = row["出院日期"].ToString();
                                    string doctor = row["主治医生"].ToString();
                                    DateTime admissionDate = DateTime.ParseExact(inDate, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
                                    DateTime dischargeDate = DateTime.ParseExact(outDate, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
                                    int duratingDate = Convert.ToInt32(row["天数"]);
                                    string department = row["科室"].ToString();
                                    if (!isFirst)
                                    {
                                        insertQuery.Append(", ");
                                    }
                                    insertQuery.Append($"('{name}', '{gender}', '{age}', '{patientID}', '{hospitalID}', '{doctor}', '{address}', '{workAddress}', '{phone}', '{diagnose}', '{vocation}', '{admissionDate:yyyy-MM-dd HH:mm:ss}', '{dischargeDate:yyyy-MM-dd HH:mm:ss}', {duratingDate}, '{department}')");
                                    isFirst = false;
                                    cycleCount++;
                                }

                                if (cycleCount>0)
                                {
                                    // 执行批量插入
                                    using (var insertCmd = new MySqlCommand(insertQuery.ToString(), m_mysqldbConnection, transaction))
                                    {
                                        insertCmd.ExecuteNonQuery();  // 执行插入操作
                                    }
                                }

                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    Toast.Show($"共插入{cycleCount}条数据，有{existingHospitalIDs.Count}条数据已存在。", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 4000, Location = ToastLocation.ScreenCenter });
                                });
                                // 提交事务
                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();  // 回滚事务
                        Console.WriteLine($"发生错误: {ex.Message}");
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Toast.Show($"发生错误:{ex.Message}", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 5000, Location = ToastLocation.ScreenCenter });

                        });
                    }
                }
            }
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
