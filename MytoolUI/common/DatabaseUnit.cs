using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing;
using Sunny.UI;

namespace MytoolUI
{
    class DatabaseUnit
    {
        private static string dataBasePath = @"Data Source=.\config\data.db";
        private SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\data.db;Version=3;");
        private SQLiteTransaction _SQLiteTrans = null;
        private UIMessageForm message= new UIMessageForm();
        /// <summary>
        /// 从数据库读取docx文件到cache目录
        /// </summary>
        /// <returns></returns>
        public void GetwordfromDb()
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
                        writePath = string.Format(@"cache\mod{0}.docx", i);
                        try
                        {
                            FileStream fs = new FileStream(writePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            fs.Write(word, 0, word.Length);
                            fs.Close();
                        }
                        catch (System.IO.IOException)
                        {
                            bool select = this.message.ShowAskDialog("警告！", "写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。",UIStyle.LightRed);
                            if (select)
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
            return "未写入userName";
        }

        /// <summary>
        /// 获取所有的关键词及主诉，以DataTable的格式返回;
        /// </summary>
        /// <returns>所有的关键词及主诉</returns>
        public DataTable GetAllChief()
        {
            DataTable dt = new DataTable();
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from diagnose", m_dbConnection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);
            var re = dt.Rows;
            return dt;
        }

        /// <summary>
        /// 创建用户名
        /// </summary>
        /// <param name="userName">设置的用户名</param>
        public void CreateUserName(string userName)
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("create table userName (id varchar(20) primary key , name varchar (20))", m_dbConnection);
            command.ExecuteNonQuery();
            string sql = string.Format("insert into userName(id, name) values({0}, 1)", userName);
            command.CommandText = sql;
            command.ExecuteNonQuery();
            m_dbConnection.Close();

        }

        /// <summary>
        /// 修改用户名
        /// </summary>
        /// <param name="userName">新的用户名</param>
        public void UpdateUserName(string userName)
        {
            m_dbConnection.Open();
            string sql = string.Format("update userName set name='{0}' where id=1", userName);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Connection = m_dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("姓名更新成功,关闭数据链接");
        }

        /// <summary>
        /// 获取打印的偏移参数
        /// </summary>
        /// <returns>x:y:x:y == 感染病例报卡x:感染病例报卡y:病历质量x:病历质量y</returns>
        public List<string> GetAdjustNum(string tableName)
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
        public void SetAdjustNum(string[] newString,string tableName)
        {

            m_dbConnection.Open();

            for (int i = 0; i < newString.Length; i++)
            {
                string sql = string.Format("update {0} set adjust='{1}' where id_ = '{2}'",tableName, newString[i], i);
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
                        this.message.ShowErrorDialog("读取logo失败!" );
                    }
                }
            }
            m_dbConnection.Close();


        }

        /// <summary>
        /// 保存血气分析结果到数据库;
        /// </summary>
        /// <param name="sql">sql的insert语句(传值太麻烦了)</param>
        public void SaveBloodGasResult(string sql)
        {
            m_dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Connection = m_dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("血气分析结果已成功保存，关闭数据链接");
        }

        /// <summary>
        /// 获取数据库保存的注册码
        /// </summary>
        /// <returns>注册码</returns>
        public string GetRegisterCode()
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from register", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string result = "";
            while (reader.Read())
            {
                result = reader[0].ToString();
            }
            reader.Close();
            m_dbConnection.Close();
            Console.WriteLine("注册码获取成功，关闭链接");
            return result;
        }
        /// <summary>
        /// 将新的授权注册码保存到数据库
        /// </summary>
        /// <param name="code">新的注册码</param>
        public void SaveRegisterCode(string code)
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"update register set code = '{code}'", m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("机器码保存成功,关闭数据连接");
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

        public void UpdateColorStyle(string color)
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"update style set color = '{color}'", m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("主题保存完毕!");
        }

        public string GetColorStyle()
        {
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from style", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string result = "red";
            while (reader.Read())
            {
                result = reader[0].ToString();
            }
            reader.Close();
            m_dbConnection.Close();
            Console.WriteLine($"主题获取完毕,color={result}");
            return result;

        }
    }

}
