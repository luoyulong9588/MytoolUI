using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// 目录设置页面
    /// </summary>
    public partial class Settings
    {
        private string SelectFolder()
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            // 设置对话框的属性  
            folderBrowserDialog.Description = "请选择文件夹";
            folderBrowserDialog.ShowNewFolderButton = true; // 隐藏"新建文件夹"按钮  

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 获取选定的路径  
                string selectedPath = folderBrowserDialog.SelectedPath;
                return selectedPath;
                // 在你的应用程序中使用选定的路径  
                // ...  
            }
            return null;
        }

        private void textBlockChronicPath_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newPath = SelectFolder();

            textBlockChronicPath.Text = newPath == null ? textBlockChronicPath.Text : newPath;
        }


        private void textBlockAdmissionCertificatePath_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newPath = SelectFolder();

            textBlockAdmissionCertificatePath.Text = newPath == null ? textBlockAdmissionCertificatePath.Text : newPath;
        }

        private void textBlockFollowUpPath_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newPath = SelectFolder();

            textBlockFollowUpPath.Text = newPath == null ? textBlockFollowUpPath.Text : newPath;
        }

        private void UpdatePath()
        {

            if (string.IsNullOrEmpty(textBlockChronicPath.Text) || string.IsNullOrEmpty(textBlockAdmissionCertificatePath.Text) || string.IsNullOrEmpty(textBlockFollowUpPath.Text))
            {
                UMessageBox.Show("目录地址设置不规范，取消更新！");
                return;
            }

            string connectionString = "Data Source=./config/data.db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = string.Format("update folderPath set chronicPath='{0}',admissionCertificatePath='{1}',followupPath='{2}'", textBlockChronicPath.Text, textBlockAdmissionCertificatePath.Text, textBlockFollowUpPath.Text);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Connection = connection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void SetDefaultPath()
        { 
        
        
        
        }
    }
}
