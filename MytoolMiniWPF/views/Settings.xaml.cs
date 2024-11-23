using ExcelDataReader;
using Microsoft.Win32;
using MytoolMiniWPF.common;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfToast.Controls;
using MytoolMiniWPF.common;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : System.Windows.Window
    {
        public string VerSion { get; set; }
        public Settings()
        {
            InitializeComponent();
            SetDeafultUserName();
            SetDefaultFolderPath();
            LoadVersion();
            // 加载数据  
            LoadData();
            // 将数据绑定到TreeView  
            //seedTreeView.ItemsSource = DataItems;
            seedTreeView.DisplayMemberPath = "Key"; // 设置TreeView的显示属性为Key 
            SetPersonInfos();
        }
        private void LoadVersion()
        {
            try
            {
                TextBlockverSion.Text = "Public Version: " +  System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (Exception)
            {
                TextBlockverSion.Text = "Not Network deployed.";
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SavePersonInfos();
            UpdatePath();
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUserName.IsReadOnly)
            {
                textBoxUserName.IsReadOnly = false;
                textBoxUserName.Text = string.Empty;
                textBlockuserName.Text = "请键入用户名:";
                textBlockuserName.Foreground = Brushes.Orange;
                textBlocxEditUser.Text = "更 新";
                logo.Foreground = Brushes.Orange;
                
            }
            else
            {
                UpdateUserName();
                
            }
            
        }

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            foreach (string f in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                textBoxOutPatient.Text = f;
                LoadXls();
                break;
            }
        }
        private bool UpdateUserName()
        {
            string inputUserName = textBoxUserName.Text;


            if (inputUserName.Length > 1 && "罗玉龙王雪玲刘益宏彭育欢朱庆霞李小琴userName张李张  李".Contains(inputUserName))
            {
                
                new DatabaseUnit().UpdateUserName(inputUserName);

                textBoxUserName.IsReadOnly = true;
                //textBoxUserName.Text = string.Empty;
                textBlockuserName.Text = "当前用户:";
                textBlocxEditUser.Text = "修 改";
                logo.Foreground = Brushes.LightSeaGreen;
                textBlockuserName.Foreground = Brushes.DarkCyan;
                Toast.Show(this,$"新用户名“{inputUserName}”已保存!", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(2), Time = 2000, Location = ToastLocation.OwnerCenter });
                //UMessageBox.Show("提示", $"新用户名“{inputUserName}”已保存!");
                RWRegistry.WriteRegistryValue(inputUserName);
                return true;
            }
            else
            {
                UMessageBox.Show("错误！", "字符非法!");
                return false;
            }
        }
        private void SetDeafultUserName()
        {
            textBoxUserName.Text = new DatabaseUnit().GetuserName();
        }
        private void SetDefaultFolderPath()
        {
            string path_ = @"D:\MytoolDataFiles\";
            var folderPath = new DatabaseUnit().GetFolderPath();
            textBlockChronicPath.Text = string.IsNullOrEmpty(folderPath.chronicPath)? path_ + "慢病报卡": folderPath.chronicPath;
            textBlockAdmissionCertificatePath.Text = string.IsNullOrEmpty(folderPath.admissionCertificatePath) ? path_ + "住院证" :folderPath.admissionCertificatePath ;
            textBlockFollowUpPath.Text = string.IsNullOrEmpty(folderPath.followupPath)? path_ + "出院随访": folderPath.followupPath;
            CheckFolderExists(textBlockChronicPath.Text);
            CheckFolderExists(textBlockAdmissionCertificatePath.Text);
            CheckFolderExists(textBlockFollowUpPath.Text);
        }
        private void CheckFolderExists(string path)
        {
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }
        }



        private void BtnBrowseOutPatient_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx"
            };
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    textBoxOutPatient.Text = openFileDialog.FileName;
                    LoadXls();


                }

            }
        }
        private void LoadXls()
        {
            using (var stream = File.Open(textBoxOutPatient.Text, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                    });
                    DataTableCollection tableCollection = result.Tables;

                    DataTable dt = tableCollection[0];
                    //StartFormatFile(dt);
                    new FormatXlsDataForOutPatients().StartFormatFile(dt);
                }
                Toast.Show(this, "文件读取完成！", new ToastOptions { Icon = ToastIcons.Information, ToastMargin = new Thickness(2), Time = 2000, Location = ToastLocation.OwnerCenter });
            }
        }

        private void textBlockFollowUpPathBrowse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(textBlockFollowUpPath.Text);

        }

        private void textBlockChronicPathBrowse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(textBlockChronicPath.Text);
        }

        private void textBlockAdmissionCertificatePathBrowse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(textBlockAdmissionCertificatePath.Text);
        }

        private void btnStartCapture_Click(object sender, RoutedEventArgs e)
        {
           new ScreenShootWindow().Show();
        }

        private void btnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            startAnalysis();
        }
        async Task startAnalysis()
        {
            string path_ = Environment.CurrentDirectory + "\\Analysis\\Analysis.exe";
            await Task.Run(() => StartExe(path_));
        }
        void StartExe(string filePath) {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = filePath;
            info.Arguments = "";
            info.WindowStyle = ProcessWindowStyle.Normal;
            Process pro = Process.Start(info);
            pro.WaitForExit();
        }
    }

    public class DataItem
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Chief1 { get; set; }
        public string Chief2 { get; set; }
        public string Chief3 { get; set; }
        public string Chief4 { get; set; }
        public string Chief5 { get; set; }
        public string Chief6 { get; set; }
        public string Chief7 { get; set; }
        public string Chief8 { get; set; }
        public string Chief9 { get; set; }
        public string Chief10 { get; set; }
    }
}
