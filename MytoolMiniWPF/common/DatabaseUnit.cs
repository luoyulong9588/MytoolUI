using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MytoolMiniWPF.common;
using MytoolMiniWPF.views;
using WpfToast.Controls;

namespace MytoolMiniWPF
{
    class DatabaseUnit
    {
        private static string dataBasePath = @"Data Source=.\config\data.db";
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\data.db;Version=3;");
        private string catchPath = @".\cache\";

        /// <summary>
        /// 从数据库读取docx文件到cache目录
        /// </summary>
        /// <returns></returns>
        public void GetDocxFilefromDbForChronic()
        {
            
            string sql, writePath;
            m_dbConnection.Open();
            for (int i = 0; i < 7; i++)
            {
                sql = string.Format("select file from docx where id={0}", i);
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] word = reader[0] as byte[];
                    if (word != null)
                    {
                        writePath = string.Format($"{catchPath}mod{i}.docx");
          
                        try
                        {
                            //FileStream fs = new FileStream(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            //fs.Write(word, 0, word.Length);
                            //fs.Close();
                            using (FileStream fs = new FileStream(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {
                                fs.Write(word, 0, word.Length);
                            }

                        }
                        catch (IOException)
                        {
                            bool? select = UMessageBox.Show("警告！", "写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。");

                            if (select == true)
                            {
                                killWinWordProcess();
                            }
                            else
                            {
                                Console.WriteLine("点击了取消~");
                                return;
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("null");
                    }
                }
            }
            m_dbConnection.Close();
        }



        public void GetDocxFilefromDbForTumor()
        {
            string writePath = string.Format($"{catchPath}turmorReportCard.docx");

            if (Directory.Exists(catchPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(catchPath);
            }
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select file from turmor", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    try
                    {
                        FileStream fs = new FileStream(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        UMessageBox.Show("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。");
                    }
                }
            }
            m_dbConnection.Close();

        }



        #region 结束winword进程
        /// <summary>
        /// 杀掉winword.exe进程   
        /// </summary>   
        public void killWinWordProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取随机主诉的List;
        /// </summary>
        /// <param name="checkString">查找关键词</param>
        /// <returns>主诉（List）</returns>
        public List<string> GetrandomChief(string checkString)
        {
            List<string> result = new List<string>();
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from diagnose", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[1].ToString().Contains(checkString))
                {
                    result.Clear();
                    for (int i = 2; i < reader.FieldCount; i++)
                    {
                        if (reader[i].ToString() != "")
                        {
                            result.Add(reader[i].ToString());
                        }
                    }
                }
            }
            m_dbConnection.Close();
            return result;
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns>用户名</returns>
        public string GetuserName()
        {
            //return "未写入userName";
            Console.WriteLine("数据库位置:" + dataBasePath);
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select name from userName where  id=1", m_dbConnection);
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
            return "";
        }

        /// <summary>
        /// 修改用户名
        /// </summary>
        /// <param name="userName">新的用户名</param>
        public void UpdateUserName(string userName)
        {
            m_dbConnection.Open();
            string sql = string.Format("UPDATE userName SET name='{0}' WHERE id=1", userName);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        /// <summary>
        /// 获取打印的偏移参数
        /// </summary>
        /// <returns>x:y:x:y == 感染病例报卡 single_x:感染病例报卡single_y:double_x:double_y</returns>
        public List<string> GetAdjustNumber(string tableName)
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"select * from {tableName}", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<string> result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader.GetString(1).ToString());
            }
            reader.Close();
            m_dbConnection.Close();
            Console.WriteLine("打印参数读取完毕，关闭数据链接");
            return result;
        }

        /// <summary>
        /// 更新打印偏移参数
        /// </summary>
        /// <param name="newString">List,x:y:x:y == 感染病例报卡x:感染病例报卡y:病历质量x:病历质量y</param>
        public void SetAdjustNum(string[] newString, string tableName)
        {

            m_dbConnection.Open();

            for (int i = 0; i < newString.Length; i++)
            {
                string sql = string.Format("update {0} set adjust='{1}' where id_ = '{2}'", tableName, newString[i], i);
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.Connection = m_dbConnection;
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            m_dbConnection.Close();
            Console.WriteLine("打印参保存成功，关闭数据链接");
        }

        /// <summary>
        /// 读取Logo,出院随访表需要使用;
        /// </summary>
        public void ReadLogo()
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select file from logo", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] logo = reader[0] as byte[];
                if (logo != null)
                {
                    string savePath = ".\\cache\\logo.png";
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(logo, 0, logo.Length);
                        fs.Close();
                    }
                    catch (System.IO.IOException)
                    {
                        UMessageBox.Show("警告","读取logo失败!");
                    }
                }
            }
            m_dbConnection.Close();


        }

        /// <summary>
        /// 在数据库检索icdO3编码;
        /// 首先在病理诊断中检索,如没有,则换用医院诊断检索;
        /// </summary>
        /// <param name="checkString">检索的诊断字符串</param>
        /// <returns>一个字典对象，包含诊断编码,如果为null则检索失败</returns>
        public Dictionary<string, string> GetIcdO3Numbers(string checkString)
        {
            checkString = checkString.Replace("?", "").Replace("？", "").Replace(" ", "");
            m_dbConnection.Open();
            Dictionary<string, string> icdo3 = new Dictionary<string, string>();
            SQLiteCommand command = new SQLiteCommand(m_dbConnection);
            command.CommandText = $"select * from icdo3 where pathologicalDiagnose = '{checkString}'";
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                command.CommandText = $"select * from icdo3 where hospitalDiagnose = '{checkString}'";
                reader = command.ExecuteReader();
            }
            if (!reader.HasRows)
            {
                m_dbConnection.Close();
                return null;
            }
            else
            {
                reader.Read();
                string mainDiagnose = reader[0].ToString();
                string pathological = reader[1].ToString();
                string anatomicalLocation = reader[2].ToString();
                string phologyDiagnose = reader[3].ToString();
                string action = reader[4].ToString();
                string leveal = reader[5].ToString();
                string icd10 = reader[6].ToString();
                string anatomicalLocationName = reader[9].ToString();
                string anatomicalLocationPinyin = reader[10].ToString();
                icdo3.Add("mainDiagnose", mainDiagnose);
                icdo3.Add("anatomicalLocation", anatomicalLocation);
                icdo3.Add("pathological", pathological);
                icdo3.Add("phologyDiagnose", phologyDiagnose);
                icdo3.Add("action", action);
                icdo3.Add("level", leveal);
                icdo3.Add("icd10", icd10);
                icdo3.Add("anatomicalLocationName", anatomicalLocationName);
                icdo3.Add("anatomicalLocationPinyin", anatomicalLocationPinyin);
                reader.Close();
                m_dbConnection.Close();
            }
            return icdo3;
        }

        public FolderPath GetFolderPath()
        {
            
            m_dbConnection.Open();
            var folderPath = new FolderPath();
            string cmdText = "SELECT chronicPath, admissionCertificatePath, followupPath FROM folderPath";
            var cmd = new SQLiteCommand(cmdText, m_dbConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                 folderPath.chronicPath = reader.GetString(0);
                 folderPath.admissionCertificatePath = reader.GetString(1);
                 folderPath.followupPath = reader.GetString(2); 
            }
            m_dbConnection.Close();
            return folderPath;
        }
        public void InsertPatientInfo(Patient patient)
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"insert into patient_infos values('{patient.Name}','{patient.Gender}','{patient.Age}','{patient.Id}','{patient.IdCardNumber}','{patient.InDay}','{patient.OutDay}','{patient.OutDiagnose}')", m_dbConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                Toast.Show($"Id已存在！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 3000, Location = ToastLocation.ScreenTopCenter })
                ));


            }

            m_dbConnection.Close();
        }

        public MyFontSyle GetFontStyleForReportCard()
        {
            var fontStyle = new MyFontSyle();
           
                m_dbConnection.Open();
                SQLiteCommand command = new SQLiteCommand("select fontSize,fontFamily from reportCardFontStyle where id = 1", m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fontStyle.fontSize = int.Parse(reader[0].ToString());
                    fontStyle.fontFamily = reader[1].ToString();
                }
                reader.Close();
                m_dbConnection.Close();
                return fontStyle;
        }
        public void UpdateFontStyleForReportCard(MyFontSyle fontSyle)
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"update reportCardFontStyle set fontSize='{fontSyle.fontSize}',fontFamily='{fontSyle.fontFamily}' where id = 1", m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
    }
    public class FolderPath
    {
        public string chronicPath { get; set; }
        public string admissionCertificatePath { get; set; }
        public string followupPath { get; set; }
    }

    public class MyFontSyle
    {
        public int fontSize { get; set; }
        public string fontFamily { get; set; }
    }
}
