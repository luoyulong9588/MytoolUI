using ExcelDataReader;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Markup;

namespace MytoolMiniWPF.views
{
    //模板更新页面的功能设置;
    public partial class Settings
    {
        #region //所有浏览按钮功能（选择文件）

        /// <summary>
        /// 数据库浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseDb_Click(object sender, RoutedEventArgs e)
        {
            textBlockDbPath.Text = OpenFile("Sqlite DataBase|*.db|All Files|*.*");
        }
        private void btnBrowseCopd_Click(object sender, RoutedEventArgs e)
        {
            textBlockCopd.Text = OpenFile();
        }
        private void btnBrowseChronicBronchitis_Click(object sender, RoutedEventArgs e)
        {
            textBlockChronicBronchitis.Text = OpenFile();
        }

        private void btnBrowseAsthma_Click(object sender, RoutedEventArgs e)
        {
            textBlockAsthma.Text = OpenFile();
        }

        private void btnBrowseEmphysema_Click(object sender, RoutedEventArgs e)
        {
            textBlockEmphysema.Text = OpenFile();
        }

        private void btnBrowseAcuteBronchitis_Click(object sender, RoutedEventArgs e)
        {
            textBlockAcuteBronchitis.Text = OpenFile();
        }

        private void btnBrowseApoplexy_Click(object sender, RoutedEventArgs e)
        {
            textBlockApoplexy.Text = OpenFile();
        }

        private void btnBrowseAdmissionCertificate_Click(object sender, RoutedEventArgs e)
        {
            textBlockAdmissionCertificate.Text = OpenFile();

        }
        private void btnBrowseTumor_Click(object sender, RoutedEventArgs e)
        {
            textBlockTumor.Text = OpenFile();
        }

        #endregion

        #region  // 所有读取按钮功能(Read)
        private void btnReadCopd_Click(object sender, RoutedEventArgs e)
        {

            this.ReadFile(textBlockCopd.Text, Disease.COPD);
        }

        private void btnReadAsthma_Click(object sender, RoutedEventArgs e)
        {
            this.ReadFile(textBlockAsthma.Text, Disease.Asthma);
        }

        private void btnReadChronicBronchitis_Click(object sender, RoutedEventArgs e)
        {
            this.ReadFile(textBlockChronicBronchitis.Text, Disease.ChronicBronchitis);
        }

        private void btnReadEmphysema_Click(object sender, RoutedEventArgs e)
        {
            this.ReadFile(textBlockEmphysema.Text, Disease.Emphysema);
        }

        private void btnReadAcuteBronchitis_Click(object sender, RoutedEventArgs e)
        {
            this.ReadFile(textBlockAcuteBronchitis.Text, Disease.AcuteBronchitis);
        }
        private void btnReadApoplexy_Click(object sender, RoutedEventArgs e)
        {
            this.ReadFile(textBlockApoplexy.Text, Disease.Apoplexy);
        }

        private void btnReadAdmissionCertificate_Click(object sender, RoutedEventArgs e)
        {
            ReadFile("select file from inHospitalCard");
        }

        private void btnReadTumor_Click(object sender, RoutedEventArgs e)
        {
            ReadFile("select file from turmor");
        }
        #endregion

        #region // 所有保存按钮功能（Save）
        private void btnSaveCopd_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(this.textBlockCopd.Text, Disease.COPD);
        }

        private void btnSaveChronicBronchitis_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(textBlockChronicBronchitis.Text, Disease.ChronicBronchitis);
        }

        private void btnSaveAsthma_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(textBlockAsthma.Text, Disease.Asthma);
        }

        private void btnSaveEmphysema_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(textBlockEmphysema.Text, Disease.Emphysema);

        }

        private void btnSaveAcuteBronchitis_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(textBlockAcuteBronchitis.Text, Disease.AcuteBronchitis);
        }

        private void btnSaveApoplexy_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(textBlockApoplexy.Text, Disease.Apoplexy);
        }

        private void btnSaveAdmissionCertificate_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(textBlockAdmissionCertificate.Text, "update inHospitalCard set file =@newFile");
        }

        private void btnSaveTumor_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(textBlockTumor.Text, "update turmor set file =@newFile");
        }
        #endregion

        /// <summary>
        /// 选择文件并返回路径
        /// </summary>
        /// <param name="fileter"></param>
        /// <returns></returns>
        private string OpenFile(string fileter = "Word Workbook|*.docx|All Files|*.*")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = fileter;
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    return openFileDialog.FileName;
                }
                return null;
            }
        }

        /// <summary>
        /// 从数据库读取文件转为docx
        /// </summary>
        /// <param name="path"></param>
        /// <param name="DiseaseId"></param>
        private void ReadFile(string path, object DiseaseId)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={textBlockDbPath.Text};Version=3;");
            m_dbConnection.Open();
            string sql = $"select file from docx where id = " + ((int)DiseaseId).ToString();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    WriteFile(word);
                }

            }
            m_dbConnection.Close();
        }

        private void ReadFile(string sql) {

            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source=./config/data.db;Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    WriteFile(word);
                }
            }
            m_dbConnection.Close();
        }

        private void WriteFile(byte[] fileByte) {
            SaveFileDialog saveDialog = new SaveFileDialog();
            // 设置保存对话框的属性  
            saveDialog.Title = "请选择保存位置";
            saveDialog.Filter = "Word Workbook|*.docx|Word 97-2003|*.doc|All Files|*.*";
            if (saveDialog.ShowDialog() != true) { return; }
            try
            {
                FileStream fs = new FileStream(saveDialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Write(fileByte, 0, fileByte.Length);
                fs.Close();
                UMessageBox.Show("Success！");
            }
            catch (IOException)
            {
                UMessageBox.Show("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。");
            }
        }
      

        /// <summary>
        /// 把docx保存到数据库
        /// </summary>
        /// <param name="path"></param>
        /// <param name="DiseaseId"></param>
        private void SaveFile(string path, object DiseaseId)
        {
            var buffer = File.ReadAllBytes(path);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={textBlockDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update docx set file =@newFile where id = @id_";
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer), new SQLiteParameter("@id_", (int)DiseaseId) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            UMessageBox.Show("Success!");

        }

        /// <summary>
        /// 保存文件对住院证和肿瘤报卡的重载；table不同
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sqlCommand"></param>
        private void SaveFile(string filePath,string sqlCommand)
        {
            var buffer = File.ReadAllBytes(filePath);
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={textBlockDbPath.Text};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = sqlCommand;
            command.Parameters.AddRange(new[] { new SQLiteParameter("@newFile", buffer) });
            command.ExecuteNonQuery();
            m_dbConnection.Close();
            MessageBox.Show("Success!", "info");
        }
    }

    public enum Disease
    {
        COPD = 0,
        ChronicBronchitis = 1,//慢性支气管炎
        Asthma = 2, // 哮喘
        Emphysema = 3, // 肺气肿
        AcuteBronchitis = 4,//急性支气管炎
        Apoplexy = 6, //卒中
        AdmissionCertificate = 7, // 住院证
        Tumor=8
    }
}
