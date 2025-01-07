using ExcelDataReader;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Action = System.Action;
using Application = System.Windows.Application;
using Excel = Microsoft.Office.Interop.Excel;
using Window = System.Windows.Window;


namespace MytoolMiniWPF.views
{

    /// <summary>
    /// DischargeFollowUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DischargeFollowUpWindow : Window
    {
        private DataSet data;
        private string currentPath;
        private string savePath = @"D:\MytoolDataFiles\出院随访";
        private string query = $" SELECT * FROM inhospitalinfos";
        private DateTime? minDate;
        private DateTime? maxDate;
        private string host;

        public DischargeFollowUpWindow()
        {
            InitializeComponent();
            string path = new DatabaseUnit().GetFolderPath().followupPath;
            savePath = string.IsNullOrEmpty(path) ? @"D:\MytoolDataFiles\出院随访": path;
            currentPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            try
            {
                LoadData();
            }
            catch (MySqlException)
            {
                UMessageBox.Show("MySqlException Error", $"数据库链接出现错误，请检查网络链接和服务器状态。");

            }
            catch (TimeoutException ex)
            {
                UMessageBox.Show("TimeoutException Error", $"链接超时，请检查服务器网络。");
            }
            catch (Exception ex)
            {
                UMessageBox.Show("未知错误", ex.ToString());
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 在多线程中更新主线程控件<button>的样式，循环进度条
        /// </summary>
        /// <param name="isStart"></param>
        private void ChangeStartBtnStyle(bool isStart)
        {
            bool isIndeterminate = false;
            string content = "开  始";
            if (isStart)
            {
                isIndeterminate = true;
                content = "等  待..";
            }

            App.Current.Dispatcher.Invoke((Action)delegate ()
            {

                MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndeterminate(btnStart, isIndeterminate);
                btnStart.Content = content;
            });

        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                textBoxPath.Text = openFileDialog.FileName;
                using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        data = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                        });

                        //startFormatExcel(result);  // 打印信息debug
                    }
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

                        // 将新DataTable绑定到DataGrid
                        dataGridPatiansInfo.ItemsSource = newDataTable.DefaultView;
                        dataGridPatiansInfo.AutoGenerateColumns = true; // 自动生成列
                        var db = new DatabaseForPatient();
                        //db.InsertPatientsInfo(dataGridPatiansInfo);
                        
                        db.InsertPatientsInfoMySQL(dataGridPatiansInfo,Global.mySqlConnectionString);
                        //dataGridPatiansInfo
                        getDate(newDataTable);
                        // 如果没有数据行，弹出提示
                        if (newDataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("数据区为空，请检查文件内容");
                        }

                       

                    }
                }
                textblockInport.Text = "已导入";
            }
            
        }



        private void getDate(System.Data.DataTable dataTable)
        {
            string dischargeColumnName = "出院日期";
            var dischargeTimes = dataTable.AsEnumerable()
                                .Where(row => row[dischargeColumnName] != DBNull.Value)  // 过滤掉空值
                                .Select(row => row[dischargeColumnName].ToString())  // 获取字符串时间
                                .Select(timeStr => DateTime.TryParse(timeStr, out DateTime dt) ? dt : (DateTime?)null)  // 转换为 DateTime 类型
                                .Where(dt => dt.HasValue)  // 过滤掉转换失败的值
                                .ToList();

            // 获取最小时间和最大时间
            minDate = dischargeTimes.Min();
            maxDate = dischargeTimes.Max();

            // 判断并显示结果
            if (minDate.HasValue && maxDate.HasValue)
            {
               StartDatePicker.SelectedDate = minDate.Value;
                EndDatePicker.SelectedDate = maxDate.Value;
                //MessageBox.Show($"最小出院时间: {minDate.Value.ToString("yyyy/M/d H:mm:ss")}\n" +
                //                $"最大出院时间: {maxDate.Value.ToString("yyyy/M/d H:mm:ss")}");
            }
            else
            {
                MessageBox.Show("未能找到有效的出院时间数据");
            }


        }
       
        private bool CheckUsersAndFilePath()
        {

            if (dataGridPatiansInfo.ItemsSource == null || !dataGridPatiansInfo.ItemsSource.Cast<object>().Any())
            {
                
                    UMessageBox.Show("没有数据可以生成，请先筛选数据。");
                    
              
                return false;
            }
            
            return true;
        }

        private int FindColumnIndexByName(DataRow headerRow, string columnName)
        {
            for (int i = 0; i < headerRow.ItemArray.Length; i++)
            {
                if (headerRow[i].ToString().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1; // 未找到
        }
        private void startFormatExcel()
        {
           

            ChangeStartBtnStyle(true);

            DateTime outDate = new DateTime();
            new DatabaseUnit().ReadLogo(); //数据库读取logo文件;
            DatabaseForPatient db = new DatabaseForPatient();
            db.m_dbConnection.Open();

            //int columns = data.Tables[0].Columns.Count;//获取列数
            //int rows = data.Tables[0].Rows.Count;//获取行数

            Patient patient = new Patient();

            string selectUser = "";
            string startDate = "1970年1月1日";
            string endDate = "2050年12月30日";

            Excel.Application xlapp = new Excel.Application();
            xlapp.DisplayAlerts = false;
            xlapp.Visible = false;
            Excel.Workbook wb = xlapp.Workbooks.Add();
            Excel.Worksheet ws = (Excel.Worksheet)xlapp.Workbooks[1].Worksheets[1];
           

         

            ws.Cells[1,1].Value = "出院患者随访登记本"; 
            // 获取DataGrid的列标题
            var columns = dataGridPatiansInfo.Columns;
            
            App.Current.Dispatcher.Invoke((Action)delegate ()
            {
                selectUser = this.selectUserComboBox.Text;
                int columnCount = columns.Count;
                startDate = StartDatePicker.SelectedDate.Value.ToString("yyyy年MM月dd日");
                endDate = StartDatePicker.SelectedDate.Value.ToString("yyyy年MM月dd日");
                
                // 设置表头
                for (int i = 0; i < columnCount; i++)
            {
                ws.Cells[2, i + 1] = columns[i].Header;
            }

            // 填充数据
            int rowIndex = 3;
            foreach (var item in dataGridPatiansInfo.Items)
            {
                dynamic row = item;  // 假设每行数据是一个动态对象
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    var cellValue = row.GetType().GetProperty(columns[colIndex].SortMemberPath)?.GetValue(row, null);

                    // 如果是日期列，设置格式
                    if (colIndex == 1 - 1) // 减1是因为Excel列索引从1开始
                    {
                            DateTime date = DateTime.Now;
                        if (cellValue != null && DateTime.TryParse(cellValue.ToString(), out  date))
                        {
                            ws.Cells[rowIndex, colIndex + 1] = date;
                            ws.Cells[rowIndex, colIndex + 1].NumberFormat = "yyyy/MM/dd"; // 设置日期格式
                        }
                        else
                        {
                            ws.Cells[rowIndex, colIndex + 1] = ""; // 如果无法解析为日期，填充空值
                        }
                    }
                    else
                    {
                        ws.Cells[rowIndex, colIndex + 1] = cellValue ?? ""; // 填充其他数据
                    }
                }
                rowIndex++;
            }

            });


            db.m_dbConnection.Close();

            //Excel.Range range = (Excel.Range)ws.Cells[1, 1];
            //range = range.get_Resize(2, columnCount);
            //range.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, objData);
            // 设置样式;
            ws.get_Range("A1", Missing.Value).ColumnWidth = 11.5;
            ws.get_Range("B1", Missing.Value).ColumnWidth = 7.1;
            ws.get_Range("C1", Missing.Value).ColumnWidth = 4.6;
            ws.get_Range("D1", Missing.Value).ColumnWidth = 5.6;
            ws.get_Range("E1", Missing.Value).ColumnWidth = 27.5;
            ws.get_Range("F1", Missing.Value).ColumnWidth = 12.5;
            ws.get_Range("G1", Missing.Value).ColumnWidth = 22;
            ws.get_Range("H1", Missing.Value).ColumnWidth = 12;
            ws.get_Range("I1", Missing.Value).ColumnWidth = 6.67;
            ws.get_Range("J1", Missing.Value).ColumnWidth = 12.3;
            ws.get_Range("K1", Missing.Value).ColumnWidth = 8.5;
            ((Excel.Range)ws.Columns["A:A"]).RowHeight = 33;  // 行高
            ((Excel.Range)ws.Rows[1, Missing.Value]).RowHeight = 40.1;
            Excel.Range allRange = ((Excel.Range)ws.Columns["A:K"]);
            allRange.Font.Size = 12;
            allRange.Font.Name = "宋体";
            allRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            allRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //设置边框
            allRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            Excel.Range firstRow = ws.get_Range("A1", "K1");
            firstRow.Merge(firstRow.MergeCells);
            firstRow.Font.Name = "方正小标宋_GBK";
            firstRow.Font.Size = 20;
            firstRow.Font.Bold = true;
            firstRow.Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            // 设置出院诊断列的字体
            Excel.Range outDiagnose = ((Excel.Range)ws.Columns["E"]);
            outDiagnose.Font.Size = 10;
            outDiagnose.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;  // 出院诊断列，左对齐
            outDiagnose.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;  // 出院诊断列，上对齐
            outDiagnose.WrapText = true;  // 启用自动换行
            ws.get_Range("E2", "E2").Font.Size = 12;
            ws.get_Range("E2", "E2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;  // 出院诊断列，上对齐
            ws.get_Range("E2", "E2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  // 出院诊断列，左对齐

            ws.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;//页面方向横向
            //ws.PageSetup.Zoom = 100;//打印时页面设置,缩放比例
            ws.PageSetup.TopMargin = xlapp.InchesToPoints(0.5905512); ; //上边距为0
            ws.PageSetup.BottomMargin = xlapp.InchesToPoints(0.5905512); //下边距为0
            ws.PageSetup.LeftMargin = xlapp.InchesToPoints(0.2); //左边距为0.5
            ws.PageSetup.RightMargin = xlapp.InchesToPoints(0.2); //右边距为0.5
            ws.PageSetup.CenterHorizontally = true;//水平居中
            ws.PageSetup.PrintTitleRows = "1:1";

            //加载图片，并设置图片大小
            //添加图片到页眉右边的单元格

            ws.PageSetup.LeftHeaderPicture.Filename = currentPath + "\\cache\\logo.png";
            ws.PageSetup.LeftHeader = "&G";
            ws.PageSetup.LeftHeaderPicture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
            ws.PageSetup.LeftHeaderPicture.Height = (float)xlapp.InchesToPoints(0.5314961);
            ws.PageSetup.LeftHeaderPicture.Width = (float)xlapp.InchesToPoints(2.2047244);
            ws.PageSetup.FooterMargin = xlapp.InchesToPoints(0.2);//页脚1
            ws.PageSetup.CenterFooter = "注：随访过程中患者提出意见随访人员在备注栏注明处置情况，初次、二次随访需在备注栏标明。";
            ws.PageSetup.RightFooter = @"&P/&N";
            
            string saveFileName = $"{savePath}\\出院随访.{selectUser}.{startDate} - {endDate}.xlsx";

            bool saveable = checkFolderAndFile(savePath, saveFileName);
            if (saveable) 
                { 
                wb.SaveAs(saveFileName);
                wb.Saved = true;
                }
            destoryExcelApp(xlapp);
            ChangeStartBtnStyle(false);

            Process.Start("explorer.exe", saveFileName);
            if (!saveable)
            {
                Application.Current.Dispatcher.Invoke(delegate ()
                {
                    UMessageBox.Show("提示", "当前打开的并不是最新生成的文件，请关闭文件占用后重新运行。");
                });
            }

        }

       
        private bool checkFolderAndFile(string folder, string fileName)
        {
            if (Directory.Exists(folder) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(folder);
            }
            if (File.Exists(fileName))//文件存在，删除文件;
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (IOException e)
                {
                    Application.Current.Dispatcher.Invoke(delegate ()
                    {
                        UMessageBox.Show("IOException", e.ToString());
                    });
                    return false;
                }
            }
            return true;
        }

        #region Kill Special Excel Process
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        #endregion
        private void destoryExcelApp(Excel.Application app)
        {

            if (app != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(app.Hwnd), out lpdwProcessId);
                Process.GetProcessById(lpdwProcessId).Kill();
            }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
                textBoxPath.Text = f;
            }
        }

        private void btnOpenDir_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(savePath);
        }

 
       
     

        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterData();
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            FilterData();
            if (!CheckUsersAndFilePath())
            {
                return;
            }
            try
            {
                Thread thread = new Thread(startFormatExcel);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误！", ex.ToString());

            }
        }
        private void FilterData()
        {
            if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                string startDate = StartDatePicker.SelectedDate.Value.ToString("yyyy-M-d H:mm:ss");
                string endDate = EndDatePicker.SelectedDate.Value.ToString("yyyy-M-d H:mm:ss").Replace("0:00:00", "23:59:59");
                //currentStatueTextblock.Text = $"当前时间区间:{startDate} - {endDate}";
                
                string doctor = selectUserComboBox.Text;

                if (string.IsNullOrEmpty(selectUserComboBox.Text))
                {
                    query = $" SELECT * FROM inhospitalinfos where outDate between '{startDate}' and '{endDate}'";
                }
                else
                {
                    query = $" SELECT * FROM inhospitalinfos where (outDate between '{startDate}' and '{endDate}') and Doctor = '{doctor}'";

                }
                Console.Write(query);
                LoadData();

            }
        }


        private void LoadData()
        {
            

            List<PatientsDataModel> data = new List<PatientsDataModel>();
            data.Clear();
            using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
            {
                conn.Open();
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(new PatientsDataModel
                        {

                            姓名 = reader.GetString(1),
                            性别 = reader.GetString(2),
                            年龄 = reader.GetString(3),
                            联系电话 = reader.GetString(9),
                            出院诊断 = reader.GetString(10),
                            出院时间 = reader.GetDateTime(13).ToString("yyyy-MM-dd"),
                            随访情况 = "",
                            患者意见 = "",
                            督查签名 = "",
                            随访者 = reader.GetString(6),
                            随访时间 = ""

                        });
                    }
                }

            }
            dataGridPatiansInfo.ItemsSource = data;
            dataGridOrderBy();


            textblockFilterUser.Text = selectUserComboBox.Text +    $"({dataGridPatiansInfo.Items.Count.ToString()})";
            //// 将查询结果绑定到 DataGrid
            //myDataGrid.ItemsSource = data;
            //texblockShowCount.Text = $"统计:共计 {data.Count} 条数据";
        }
        public void dataGridOrderBy()
        {
            var column = dataGridPatiansInfo.Columns.FirstOrDefault(c => c.Header.ToString() == "出院时间");

            if (column != null)
            {
                // 确保出院时间列按升序排序
                dataGridPatiansInfo.Items.SortDescriptions.Clear(); // 清除任何现有的排序
                dataGridPatiansInfo.Items.SortDescriptions.Add(new SortDescription("出院时间", ListSortDirection.Ascending));
            }

        }
        public class PatientsDataModel
        {
          
            public string 出院时间 { get; set; }
            
            public string 姓名 { get; set; }
            public string 性别 { get; set; }
            public string 年龄 { get; set; }

            public string 出院诊断 { get; set; }
            public string 联系电话 { get; set; }
            
            public string 随访情况 { get; set; }
            public string 患者意见 { get; set; }
            public string 随访者 { get; set; }
            public string 随访时间 { get; set; }
            public string 督查签名 { get; set; }
            
        }

        private void selectUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textblockFilterUser.Text = selectUserComboBox.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem:", "") ;
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            textblockStartDate.Text = StartDatePicker.SelectedDate.ToString();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            textblockEndDate.Text = EndDatePicker.SelectedDate.ToString().Replace("0:00:00","23:59:59");
        }
    }
}
