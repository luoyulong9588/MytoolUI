using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Sunny.UI;

namespace MytoolUI
{
    public partial class ChangeDocx : UIForm
    {
        public ChangeDocx(Color color)
        {
            
            InitializeComponent();
            //SetBtnColor(color);

        }
        private void SetBtnColor(Color color)
        {
            List<UIButton> btnList = new List<UIButton>()
            {
            ubtnBrosweDb,butnOpen0,ubtnOpen1,ubtnOpen2,ubtnOpen3,ubtnOpen5,ubtnOpen4,ubtnOpen6,
            usbtnReead0,usbtnReead1,usbtnReead2,usbtnReead3,usbtnReead5,usbtnReead4,usbtnReead6,
            usbtnSave0,usbtnSave1,usbtnSave2,usbtnSave3,usbtnSave5,usbtnSave4,usbtnSave6,
            ubtnBrowseInvegation,uiSymbolbtnRead,uiSymbolbtnSave,
            uBtnBrowserHCard,uBtnHCardRead,uBtnHCardSave,uBtnTurmorRead,uBtnTurmorSave,uBtnBrowserTurmor
            };
            foreach (UIButton btn in btnList)
            {
                btn.ForeHoverColor = color;
                btn.ForePressColor = color;
                btn.ForeSelectedColor = color;
                btn.FillHoverColor = Color.FromArgb(39, 39, 58);
                btn.FillPressColor = Color.FromArgb(39, 39, 58);
                btn.FillSelectedColor = Color.FromArgb(39, 39, 58);
            }
        }


        private void uiSymbolbtnRead_Click(object sender, EventArgs e)
        {
            string sqlString = "select file from investigation where normal=1";
            string saveName = "\\invetigation_normal.docx";
            SaveFileToLocal(sqlString, saveName);

/*
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select file from investigation", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    FolderBrowserDialog savePathSelect = new FolderBrowserDialog();
                    savePathSelect.ShowDialog();
                    string savePath = savePathSelect.SelectedPath + "\\invetigation.docx";
                    Console.WriteLine(savePath);
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                        MessageBox.Show("Success！", "info");
                    }
                    catch (System.IO.IOException)
                    {
                        DialogResult select = MessageBox.Show("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }
            }
            m_dbConnection.Close();*/
        }

        private void uiSymbolbtnSave_Click(object sender, EventArgs e)
        {

            var buffer = File.ReadAllBytes(textBoxFilepath.Text);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update investigation set file =@newFile";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("关闭链接");
            MessageBox.Show("Success!", "info");
        }

        private void ChangeDocx_Load(object sender, EventArgs e)
        {
            uiTextBoxDbPath.Text = Application.StartupPath + "\\config\\data.db";
        }

        private void usbtnReead0_Click(object sender, EventArgs e)
        {



            this.ReadFile(uTextBox0.Text, 0);
        }
        
        private void usbtnReead1_Click(object sender, EventArgs e)
        {
            this.ReadFile(uTextBox1.Text, 1);
        }

        private void usbtnReead2_Click(object sender, EventArgs e)
        {
            this.ReadFile(uTextBox2.Text, 2);
        }

        private void usbtnReead3_Click(object sender, EventArgs e)
        {
            this.ReadFile(uTextBox3.Text, 3);
        }

        private void usbtnReead4_Click(object sender, EventArgs e)
        {
            this.ReadFile(uTextBox4.Text, 4);
            
        }

        private void usbtnReead5_Click(object sender, EventArgs e)
        {
            string sqlString = "select file from investigation where normal=0";
            string saveName = "\\invetigation_withPermitPage.docx";
            SaveFileToLocal(sqlString, saveName);
        }

        private void usbtnReead6_Click(object sender, EventArgs e)
        {
            this.ReadFile(uTextBox6.Text, 6);
        }

        private void usbtnSave0_Click(object sender, EventArgs e)
        {
            SaveFile(uTextBox0.Text, 0);
        }
        private void usbtnSave1_Click(object sender, EventArgs e)
        {
            SaveFile(uTextBox1.Text, 1);
        }

        private void usbtnSave2_Click(object sender, EventArgs e)
        {
            SaveFile(uTextBox2.Text, 2);
        }

        private void usbtnSave3_Click(object sender, EventArgs e)
        {
            SaveFile(uTextBox3.Text, 3);
        }

        private void usbtnSave4_Click(object sender, EventArgs e)
        {
            SaveFile(uTextBox4.Text, 4);
            
        }

        private void usbtnSave5_Click(object sender, EventArgs e)
        {
            //SaveFile(uTextBox5.Text, 5);
            var buffer = File.ReadAllBytes(uTextBox5.Text);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update investigation set file=@newFile where normal=0";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("关闭链接");
            MessageBox.Show("Success!", "info");


        }

        private void usbtnSave6_Click(object sender, EventArgs e)
        {
            SaveFile(uTextBox6.Text, 6);
        }
        

        private void butnOpen0_Click(object sender, EventArgs e)
        {
            uTextBox0.Text = OpenFile();
        }

        private void ubtnOpen1_Click(object sender, EventArgs e)
        {
            uTextBox1.Text = OpenFile();
        }

        private void ubtnOpen2_Click(object sender, EventArgs e)
        {
            uTextBox2.Text = OpenFile();
        }

        private void ubtnOpen3_Click(object sender, EventArgs e)
        {
            uTextBox3.Text = OpenFile();
        }

        private void ubtnOpen4_Click(object sender, EventArgs e)
        {
            uTextBox4.Text = OpenFile();
            
        }

        private void ubtnOpen5_Click(object sender, EventArgs e)
        {
            uTextBox5.Text = OpenFile();
        }

        private void ubtnOpen6_Click(object sender, EventArgs e)
        {
            uTextBox6.Text = OpenFile();
        }

        private void ReadFile(string path,int id_)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            string sql = "select file from docx where id = " + id_.ToString();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    FolderBrowserDialog savePathSelect = new FolderBrowserDialog();
                    savePathSelect.ShowDialog();
                    string savePath = savePathSelect.SelectedPath + $"\\mod{id_}.docx";
                    Console.WriteLine(savePath);
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                        MessageBox.Show("Success！", "info");
                    }
                    catch (System.IO.IOException)
                    {
                        DialogResult select = MessageBox.Show("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }

            }
            m_dbConnection.Close();


        }
        private void SaveFile(string path, int id_)
        {
            var buffer = File.ReadAllBytes(path);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update docx set file =@newFile where id = @id_";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer), new SQLiteParameter("@id_", id_) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("关闭链接");
            MessageBox.Show("Success!", "info");

        }
        private string OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Word Workbook|*.docx|All Files|*.*" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        private void iconButtonBrosweDb_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Sqlite DataBase|*.db|All Files|*.*" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    uiTextBoxDbPath.Text = openFileDialog.FileName;
                }
            }
        }

        private void iconbtnBrowse_Click(object sender, EventArgs e)
        {
            string result = OpenFile();

            if (result != null)
            {
                textBoxFilepath.Text = result;
            }
        }

        private void test()
        {
            // save  logo
            var buffer = File.ReadAllBytes(Application.StartupPath + "\\logo_blank.png");
            string path = Application.StartupPath + "\\config\\data.db";
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={path};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update logo set file =@newFile";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer), new SQLiteParameter("@id_", buffer) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("关闭链接");
            MessageBox.Show("Success!", "info");

        }

        private void uBtnHCardSave_Click(object sender, EventArgs e)
        {
            var buffer = File.ReadAllBytes(uiTextBoxInHCard.Text);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update inHospitalCard set file =@newFile";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("关闭链接");
            MessageBox.Show("Success!", "info");
        }

        private void uBtnBrowserHCard_Click(object sender, EventArgs e)
        {
            uiTextBoxInHCard.Text = OpenFile();
        }

        private void uBtnHCardRead_Click(object sender, EventArgs e)
        {
            string sqlString = "select file from inHospitalCard";
            string saveName = "\\inHospitalCard.docx";
            SaveFileToLocal(sqlString, saveName);

/*
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select file from inHospitalCard", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    FolderBrowserDialog savePathSelect = new FolderBrowserDialog();
                    savePathSelect.ShowDialog();
                    string savePath = savePathSelect.SelectedPath + "\\inHospitalCard.docx";
                    Console.WriteLine(savePath);
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                        MessageBox.Show("Success！", "info");
                    }
                    catch (System.IO.IOException)
                    {
                        DialogResult select = MessageBox.Show("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }
            }
            m_dbConnection.Close();*/
        }

        private void uBtnBrowserTurmor_Click(object sender, EventArgs e)
        {
            uiTextBoxTurmor.Text = OpenFile();
        }

        private void uBtnTurmorSave_Click(object sender, EventArgs e)
        {
            var buffer = File.ReadAllBytes(uiTextBoxTurmor.Text);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            //command.CommandText = "create table inHospitalCard (file BLOD)";
            //command.ExecuteNonQuery();
            command.CommandText = "update turmor set file =@newFile";
            //command.CommandText = "insert into turmor values(@newFile)";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            Console.WriteLine("关闭链接");
            MessageBox.Show("Success!", "info");
        }

        private void uBtnTurmorRead_Click(object sender, EventArgs e)
        {
            string sqlString = "select file from turmor";
            string saveName = "\\turmorReportCard.docx";
            SaveFileToLocal(sqlString,saveName);
        }


        private void SaveFileToLocal(string sql,string saveName)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={uiTextBoxDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    FolderBrowserDialog savePathSelect = new FolderBrowserDialog();
                    savePathSelect.ShowDialog();
                    string savePath = savePathSelect.SelectedPath + $"\\{saveName}";
                    Console.WriteLine(savePath);
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                        MessageBox.Show($"Success！\r\n{savePath}", "info");
                    }
                    catch (System.IO.IOException)
                    {
                        DialogResult select = MessageBox.Show("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }
            }
            m_dbConnection.Close();



        }
    }

}