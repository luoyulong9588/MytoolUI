using ExcelDataReader;
using FlaUI.Core.AutomationElements;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace MytoolMiniWPF.views
{

    public class ClassInfo
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
    /// <summary>
    /// ChronicDiseaseWindow.xaml 的交互逻辑
    /// </summary>

    
    public partial class ChronicDiseaseWindow : System.Windows.Window
    {
        private List<string> pathList;
        DataSet data;

        public ChronicDiseaseWindow()
        {
            InitializeComponent();
            new DatabaseUnit().GetDocxFilefromDbForChronic();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            CleanCache();
            this.Close();
        }

        private void CleanCache()
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\cache";
                var files = Directory.GetFiles(path, "*.*");
                foreach (var file in files)
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        UMessageBox.Show($"缓存文件清除失败!",$"{ex.ToString()}");
                    }
            }
            catch
            {
            }

        }

        private void min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState= WindowState.Minimized;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx";
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    txtBoxfilePath.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                            });
                            data = result;
                        }
                    }
                }

            }
        }
        
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckUsersAndFilePath())
            {
                return;
            }

            List<Patient> patientList = new List<Patient>();
            FillCardToWordForChornicDiseases app;
            try
            {
                patientList = getBasicInformation();
            }
            catch (Exception ex)
            {
                UMessageBox.Show("尝试读取文件时发生错误","可能的原因为未选择导出文件或选择文件错误！\r\n" + ex);
            }
            string userName = selectUserComboBox.Text;


            Thread thread = new Thread(Start);
            thread.Start();
            void Start()
            {
                app = new FillCardToWordForChornicDiseases(patientList,this);
                app.ChangeStartBtnStyle(btnStart, true);
                this.pathList = app.startProgram(userName, listViewCopd, listViewAMI, listViewApoplexy);
                UpdateTitles(true);
                app.ChangeStartBtnStyle(btnStart, false);
            }
        }

        

        private bool CheckUsersAndFilePath()
        {
            if (string.IsNullOrEmpty(selectUserComboBox.Text))
            {
                UMessageBox.Show("警告！", "请先选择要导出的用户名！");

                return false;
            }
            if (string.IsNullOrEmpty(txtBoxfilePath.Text))
            {
                UMessageBox.Show("警告！", "请先选择要格式化的文件！");
                return false;
            }
            return true;
        }

        private List<Patient> getBasicInformation()
        {
            int rows, columns;
            List<Patient> patientInfoList = new List<Patient>();
            List<string> diag_select = new List<string>
            //{ "慢性阻塞性", "慢性支气管炎", "哮喘", "肺气肿", "支气管扩张", "急性支气管炎" };
            { "慢性阻塞性", "慢性支气管炎", "哮喘", "肺气肿",  "急性支气管炎" }; // 2022年1月8日 16:08  移除支气管扩张
            if (chkBoxCheckAMI.IsChecked == true)
            {
                diag_select.Add("心肌梗死");
                diag_select.Add("急性冠脉综合征");
            }
            if (chkBoxCheckapoplexy.IsChecked == true)
            {
                diag_select.Add("脑梗死");
                diag_select.Add("脑出血");
                diag_select.Add("脑梗塞");
            }


            rows = data.Tables[0].Rows.Count;//获取行数
            for (int i = 0; i < rows; i++)
            {

                string outDiagnose = data.Tables[0].Rows[i][16].ToString();
                foreach (string item in diag_select)
                {
                    if (outDiagnose.Contains(item))
                    {
                        Patient person = new Patient();
                        person.Name = data.Tables[0].Rows[i][2].ToString();
                        person.Id = data.Tables[0].Rows[i][1].ToString();
                        person.Gender = data.Tables[0].Rows[i][3].ToString();
                        person.Age = data.Tables[0].Rows[i][4].ToString();
                        person.IdCardNumber = data.Tables[0].Rows[i][5].ToString();
                        person.Vocation = data.Tables[0].Rows[i][6].ToString();
                        person.Phone = data.Tables[0].Rows[i][7].ToString();
                        person.HomeAddr = data.Tables[0].Rows[i][8].ToString();
                        person.WorkAddr = data.Tables[0].Rows[i][9].ToString();
                        person.DoctorName = data.Tables[0].Rows[i][11].ToString();
                        person.InDay = data.Tables[0].Rows[i][12].ToString();
                        person.InDiagnose = data.Tables[0].Rows[i][13].ToString();
                        person.OutDay = data.Tables[0].Rows[i][14].ToString();
                        person.DuringDay = data.Tables[0].Rows[i][15].ToString();
                        person.OutDiagnose = data.Tables[0].Rows[i][16].ToString();
                        if (item == "慢性阻塞性")
                        {
                            person.MainDiagnose = "慢性阻塞性肺疾病";
                        }
                        else if (item == "脑梗塞")
                        {
                            person.MainDiagnose = "脑梗死";
                        }
                        else
                        {
                            person.MainDiagnose = item;
                        }
                        patientInfoList.Add(person);
                    }
                }
            }
            return patientInfoList;

        }

        private void UpdateTitles(bool isFinished)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new System.Action(() =>
            {

            if (isFinished)
            {
                this.labelTitleCOPD.Content = $"慢性肺病({this.listViewCopd.Items.Count})";
                this.labelTitleAMI.Content = $"心血管事件({this.listViewAMI.Items.Count})";
                this.labelTitleAPoplexy.Content = $"脑卒中({this.listViewApoplexy.Items.Count})";
            }
            else
            {
                this.labelTitleCOPD.Content = $"慢性肺病";
                this.labelTitleAMI.Content = $"心血管事件";
                this.labelTitleAPoplexy.Content = $"脑卒中";
                listViewCopd.Items.Clear();
                listViewAMI.Items.Clear();
                listViewApoplexy.Items.Clear();
            }
               
            }));
        }

        private void btnOpenDir_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new DatabaseUnit().GetFolderPath().chronicPath);
        }

        private void cleanBtn_Click(object sender, RoutedEventArgs e)
        {
          
                UpdateTitles(false);

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrinterWindow printerWindow = new PrinterWindow(pathList);
            printerWindow.Show();
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
                txtBoxfilePath.Text = f;
            }
        }
    }
}
