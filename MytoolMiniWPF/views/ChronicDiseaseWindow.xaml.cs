using ExcelDataReader;
using FlaUI.Core.AutomationElements;
using Google.Protobuf.Compiler;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MytoolMiniWPF.views.DischargeFollowUpWindow;
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
        List<Patient> patientList = new List<Patient>();
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

                            if (data.Tables.Count > 0)
                            {
                                System.Data.DataTable dataTable = data.Tables[0];

                                // 查找 headerRow 和 footerRow
                                int headerRowIndex = 2;
                                int footerRowIndex = -1;

                                for (int i = 0; i < dataTable.Rows.Count; i++)
                                {
                                    var firstColumnValue = dataTable.Rows[i][0].ToString().Trim();

                                    // 假设第一列的值为序号时，该行为表头
                                    if (firstColumnValue == "序号")
                                    {
                                        headerRowIndex = i;  // 找到表头行
                                        break;
                                    }
                                }

                                var headerRow = dataTable.Rows[headerRowIndex];

                                // 创建新的DataTable，用于保存正确的列名和数据
                                System.Data.DataTable newDataTable = dataTable.Clone();  // 复制结构

                                for (int i = 0; i < headerRow.ItemArray.Length; i++)
                                {
                                    newDataTable.Columns[i].ColumnName = headerRow[i].ToString();
                                }

                                for (int i = 3; i < dataTable.Rows.Count + footerRowIndex; i++)
                                {
                                    DataRow dataRow = dataTable.Rows[i];

                                    if (dataRow.ItemArray.Any(item => item != DBNull.Value && item != null))
                                    {
                                        // 手动创建新的 DataRow
                                        DataRow newRow = newDataTable.NewRow();

                                        // 将原始数据行的每列值手动赋值给新行
                                        for (int colIndex = 0; colIndex < dataRow.ItemArray.Length; colIndex++)
                                        {
                                            newRow[colIndex] = dataRow[colIndex];
                                        }

                                        // 将新行添加到新表中
                                        newDataTable.Rows.Add(newRow);
                                    }
                                }
                                DataGrid dataGrid = new DataGrid();

                                // 将新DataTable绑定到DataGrid
                                dataGrid.ItemsSource = newDataTable.DefaultView;
                                dataGrid.AutoGenerateColumns = true; // 自动生成列
                                var db = new DatabaseForPatient();
                                //db.InsertPatientsInfo(dataGrid);

                                db.InsertPatientsInfoMySQL(dataGrid, Global.mySqlConnectionString);

                                // 如果没有数据行，弹出提示
                                if (newDataTable.Rows.Count == 0)
                                {
                                    MessageBox.Show("数据区为空，请检查文件内容");
                                }



                            }


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

            var a = patientList;
            FillCardToWordForChornicDiseases app;
      
            string userName = selectUserComboBox.Text;


            Thread thread = new Thread(Start);
            thread.Start();
            void Start()
            {
                app = new FillCardToWordForChornicDiseases(patientList,this);
                app.ChangeStartBtnStyle(btnStart, true);
                pathList = app.startProgram(userName);
               
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
    
            return true;
        }



        private void btnOpenDir_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new DatabaseUnit().GetFolderPath().chronicPath);
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

        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            

            List<PatientsChronicData> data = new List<PatientsChronicData>();
            if ((bool)checkBoxCOPD.IsChecked)
            {
                string[] searchTerms = { "慢性阻塞性", "慢性支气管炎", "哮喘", "肺气肿", "急性支气管炎" };
                FillDataGrid(dataGridCopd, searchTerms);
                countCOPD.Text = $"共获取{dataGridCopd.Items.Count - 1}条慢性肺病数据";
            }
            else
            {
                dataGridCopd.ItemsSource = data;
                countCOPD.Text = "未选择";
            }

            if ((bool)chkBoxCheckAMI.IsChecked)
            {
                string[] searchTerms = { "急性心肌梗", "急性冠脉综合" };
                FillDataGrid(dataGridAMI, searchTerms);
                countAMI.Text = $"共获取{dataGridAMI.Items.Count - 1}条心血管事件数据";
            }
            else
            {
                dataGridAMI.ItemsSource = data;
                countAMI.Text = "未选择";
            }
            if ((bool)chkBoxCheckapoplexy.IsChecked)
            {
                string[] searchTerms = { "脑梗" };
                FillDataGrid(dataGridApoplexy, searchTerms);
                countApoplexy.Text = $"共获取{dataGridApoplexy.Items.Count -1}条脑血管事件数据";
            }
            else
            {
                dataGridApoplexy.ItemsSource = data;
                countApoplexy.Text = "未选择";
            }
        }

        private void FillDataGrid(DataGrid dataGrid,string[] searchTerms)
        {
            
            string query = $" SELECT * FROM inhospitalinfos WHERE ";
            var likeClauses = searchTerms.Select((term, index) => $"Diagnose LIKE @term{index}");
            query += "(" + string.Join(" OR ", likeClauses) + ")";  // 使用括号确保优先级

            // 添加日期范围条件
            List<string> dateClauses = new List<string>();

            // 检查是否选择了开始日期
            if (StartDatePicker.SelectedDate.HasValue)
            {
               
                dateClauses.Add($"outDate >= @startDate");
            }

            // 检查是否选择了结束日期
            if (EndDatePicker.SelectedDate.HasValue)
            {
                
                dateClauses.Add($"outDate <= @endDate");
            }

            // 如果有日期筛选条件，添加到查询语句
            if (dateClauses.Any())
            {
                query += " AND " + string.Join(" AND ", dateClauses);
            }


            // 添加 Doctor 筛选条件
            if (selectUserComboBox.SelectedItem != null)
            {
                // 获取选中的 Doctor
                var selectedDoctor = selectUserComboBox.SelectedItem.ToString();
                query += " AND Doctor = @doctor";
            }

            List<PatientsChronicData> data = new List<PatientsChronicData>();
            data.Clear();
            using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    for (int i = 0; i < searchTerms.Length; i++)
                    {
                        cmd.Parameters.AddWithValue($"@term{i}", $"%{searchTerms[i]}%");
                    }

                    // 如果有开始日期筛选条件，添加对应的参数
                    if (StartDatePicker.SelectedDate.HasValue)
                    {
                        var startDate = StartDatePicker.SelectedDate.Value.Date; // 获取日期，不包含时间部分
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                    }

                    // 如果有结束日期筛选条件，添加对应的参数
                    if (EndDatePicker.SelectedDate.HasValue)
                    {
                        var endDate = EndDatePicker.SelectedDate.Value.Date.AddDays(1).AddTicks(-1); // 设置时间为 23:59:59
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                    }

                    // 如果选择了 Doctor，添加对应的参数
                    if (selectUserComboBox.SelectedItem != null)
                    {
                        var selectedDoctor = (selectUserComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
                        cmd.Parameters.AddWithValue("@doctor", selectedDoctor);
                    }
                    Console.WriteLine(cmd.CommandText.ToString());
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string outDiagnose = reader.GetString(10);
                            data.Add(new PatientsChronicData
                            {
                                姓名 = reader.GetString(1),
                                性别 = reader.GetString(2),
                                年龄 = reader.GetString(3),
                                身份证号 = reader.GetString(4),
                                住院号 = reader.GetString(5),
                                主治医生 = reader.GetString(6),
                                现住址 = reader.GetString(7),
                                工作单位 = reader.GetString(8),
                                联系电话 = reader.GetString(9),
                                出院诊断 = outDiagnose,
                                职业 =  reader.GetString(11),
                                出院日期 = reader.GetDateTime(12).ToString("yyyy-MM-dd"),
                                入院日期 = (reader.GetDateTime(13).ToString())
                            });
                            string mainDiagnose;
                            if (outDiagnose.Contains("慢性阻塞性"))
                            {
                                mainDiagnose = "慢性阻塞性肺疾病";
                            }
                            else if (outDiagnose.Contains("脑梗"))
                            {
                                mainDiagnose = "脑梗死";
                            }
                            else if (outDiagnose.Contains("慢性支气管炎"))
                            {
                                mainDiagnose = "慢性支气管炎";
                            } else if (outDiagnose.Contains("急性支气管炎"))
                            {
                                mainDiagnose = "急性支气管炎";
                            }
                            else if (outDiagnose.Contains("哮喘"))
                            {
                                mainDiagnose = "支气管哮喘";
                            }
                            else  // 包含肺气肿
                            {
                                mainDiagnose = "慢性阻塞性肺疾病";
                            }

                            patientList.Add(new Patient
                            {

                                Name = reader.GetString(1),
                                Gender = reader.GetString(2),
                                Age = reader.GetString(3),
                                Id = reader.GetString(5),
                                IdCardNumber = reader.GetString(4),
                                Vocation = reader.GetString(11),
                                Phone = reader.GetString(9),
                                HomeAddr = reader.GetString(7),
                                WorkAddr = reader.GetString(8),
                                DoctorName = reader.GetString(6),
                                InDay = reader.GetDateTime(13).ToString(),
                                OutDay = reader.GetDateTime(12).ToString("yyyy-MM-dd"),
                                OutDiagnose = reader.GetString(10),
                                MainDiagnose = mainDiagnose
                            });
                        };
                    }
                }

            }
            dataGrid.ItemsSource = data;
        }

    }
    public class PatientsChronicData
    {


        public string 姓名 { get; set; }

        public string 住院号 { get; set; }
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string 身份证号 { get; set; }
        public string 职业 { get; set; }
        public string 联系电话 { get; set; }
        public string 现住址 { get; set; }
        public string 主治医生 { get; set; }
        public string 入院日期 { get; set; }
        public string 入院诊断 { get; set; }
        public string 出院诊断 { get; set; }
    
        public string 工作单位 { get; set; }
        public string 出院日期 { get; set; }


    }
}
