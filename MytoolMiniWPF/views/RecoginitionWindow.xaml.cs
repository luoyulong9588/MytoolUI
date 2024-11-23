using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using Path = System.IO.Path;
using System.Globalization;
using WPFDevelopers.Helpers;
using MytoolMiniWPF.common;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Window = System.Windows.Window;
using Action = System.Action;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// RecoginitionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RecoginitionWindow : Window
    {
        private string connectionString = "Data Source=D:\\MytoolDataFiles\\data\\recognition_data.db;Version=3;";
        private string query = "SELECT * FROM recognition";  // 替换为你的实际查询
        private string query_directory = "SELECT * FROM zlbh_path";
        private string insert_directory = "INSERT INTO zlbh_path (directory) VALUES (@zlbh_directory)";
        private string deleteQuery = "DELETE FROM zlbh_path";  // 删除表中现有的所有数据
        string startDate;
        string endDate;
        string currentPath;
        string saveDirectory = @"D:\MytoolDataFiles\医检互认\";
        public RecoginitionWindow()
        {
            InitializeComponent();
            currentPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            LoadData();
        }
        private void LoadData()
        {
            if (!File.Exists("D:\\MytoolDataFiles\\data\\recognition_data.db"))
            {
                return;
            }
            textBoxDir.Text = GetDirectoryFromDatabase();
            texBlockShowDoctor.Text = string.IsNullOrEmpty(selectUserComboBox.Text) ? "当前医生:未筛选" : $"当前医生:{selectUserComboBox.Text}";

            List<MyDataModel> data = new List<MyDataModel>();
            data.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(new MyDataModel
                        {
                            LogDate = reader.GetString(0),
                            Name = reader.GetString(1),
                            Id = reader.GetString(2),
                            Gender = reader.GetString(4),
                            Age = reader.GetString(5),
                            Diagnose = reader.GetString(6),
                            Address = reader.GetString(7),
                            Item = reader.GetString(8),
                            Phone = reader.GetString(9),
                            Doctor = reader.GetString(10),

                        });
                    }
                }

            }

            // 将查询结果绑定到 DataGrid
            myDataGrid.ItemsSource = data;
            texblockShowCount.Text = $"统计:共计 {data.Count} 条数据";
        }


        // 从数据库查询directory字段值
        private string GetDirectoryFromDatabase()
        {
            string directory = string.Empty;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open(); // 打开数据库连接
                    using (SQLiteCommand cmd = new SQLiteCommand(query_directory, conn))
                    {
                        // 执行查询并获取结果
                        object result = cmd.ExecuteScalar();

                        // 检查查询结果并赋值给 directory
                        if (result != null)
                        {
                            directory = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    UMessageBox.Show("查询错误: " + ex.Message);
                }
            }

            return directory;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                // 打开文件夹选择对话框
                var result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // 将选择的文件夹路径显示到 TextBox
                    textBoxDir.Text = Path.Combine(folderDialog.SelectedPath, "Log_医检互认");

                }
            }

            if (!string.IsNullOrEmpty(textBoxDir.Text))
            {
                if (!Directory.Exists(textBoxDir.Text))
                {
                    UMessageBox.Show("选择的路径不存在！");
                    return;
                }
                ExecuteTestExe();
                UpdateRecoginitionDb.Update(processName: "update_recognition_db.exe");
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    // 开始事务
                    using (var transaction = conn.BeginTransaction())
                    {
                        // 1. 删除现有数据
                        using (SQLiteCommand deleteCmd = new SQLiteCommand(deleteQuery, conn))
                        {
                            deleteCmd.ExecuteNonQuery();
                        }

                        // 2. 插入新数据
                        using (SQLiteCommand insertCmd = new SQLiteCommand(insert_directory, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@zlbh_directory", textBoxDir.Text);
                            insertCmd.ExecuteNonQuery();
                        }

                        // 提交事务
                        transaction.Commit();
                    }
                }
                    // 创建SQLite命令

            }
        }
        public void ExecuteTestExe()
        {
            // 获取当前目录
            string currentDirectory = Directory.GetCurrentDirectory();
            string subfolder = "医检互认日志处理";

            // 定义可执行文件路径
            string exePath = Path.Combine(currentDirectory, subfolder, "analysis_log_files.exe");

            // 定义参数
            string arguments = textBoxDir.Text;

            // 创建并配置进程启动信息
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,  // 指定要执行的程序路径
                Arguments = arguments,  // 传递的参数
                UseShellExecute = false,  // 如果你不想使用外壳来启动进程
                CreateNoWindow = true  // 不显示命令行窗口
            };

            try
            {
                // 启动进程
                Process process = Process.Start(startInfo);
                // 等待进程执行完毕（如果需要）
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                UMessageBox.Show($"Error: {ex.Message}");
                // 错误处理

            }
        }

        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                startDate = StartDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                endDate = EndDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss").Replace("00:00:00", "23:59:59");
                currentStatueTextblock.Text = $"当前时间区间:{startDate} - {endDate}";
                string doctor = selectUserComboBox.Text;
                if (string.IsNullOrEmpty(selectUserComboBox.Text))
                {
                    query = $" SELECT * FROM recognition where log_date between '{startDate}' and '{endDate}'";
                }
                else
                {
                    query = $" SELECT * FROM recognition where log_date between '{startDate}' and '{endDate}' and doctor = '{doctor}'";

                }
                LoadData();

            }
        }



        // 数据模型
        public class MyDataModel
        {
            public string LogDate { get; set; }
            public string Doctor { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }

            public string Item { get; set; }

            public string Address { get; set; }

            public string Diagnose { get; set; }

            public string Id { get; set; }

            public string Phone { get; set; }

        }

        private void buttonBuild_Click(object sender, RoutedEventArgs e)
        {
            // 获取 DataGrid 中绑定的数据
            var patients = myDataGrid.ItemsSource.Cast<MyDataModel>().ToList();
            
            // 导出数据到 Excel
            //ExportToExcel(patients);


            // 启动后台任务
            Task.Run(() =>
            {
                // 在后台线程中执行导出操作
                ExportToExcel(patients);

                // 在操作完成后，通过 Dispatcher 更新 UI
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    // 这里可以更新 UI，例如提示导出完成
                    
                });
            });



        }

        private void ExportToExcel(List<MyDataModel> patients)
        {
            ChangeStartBtnStyle(true);
            EnsureDirectoryExists(saveDirectory);
            new DatabaseUnit().ReadLogo(); //数据库读取logo文件;

            // 创建 Excel 应用程序实例
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                UMessageBox.Show("Excel 未能启动!");
                return;
            }
            // 创建一个新的工作簿
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
            worksheet.Cells[1, 1] = "医检互认登记本";
            // 设置表头 (根据需要导出的列设置)

            worksheet.Cells[2, 1] = "日期";
            worksheet.Cells[2, 2] = "姓名";
            worksheet.Cells[2, 3] = "年龄";
            worksheet.Cells[2, 4] = "性别";
            worksheet.Cells[2, 5] = "电话"; ;
            worksheet.Cells[2, 6] = "身份证";
            worksheet.Cells[2, 7] = "住址";
            worksheet.Cells[2, 8] = "项目";
            worksheet.Cells[2, 9] = "原因";
            worksheet.Cells[2, 10] = "医生";

            ((Excel.Range)worksheet.Columns["E:F"]).NumberFormat = "@";  // 电话、身份证为字符
           
            
            // 填充数据 (这里只填充需要的列)
            for (int i = 1; i < patients.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = ConvertToCustomFormat(patients[i].LogDate);
                worksheet.Cells[i + 2, 2] = patients[i].Name;
                worksheet.Cells[i + 2, 3] = patients[i].Age;
                worksheet.Cells[i + 2, 4] = patients[i].Gender;
                worksheet.Cells[i + 2, 5] = patients[i].Phone;
                worksheet.Cells[i + 2, 6] = patients[i].Id;
                worksheet.Cells[i + 2, 7] = patients[i].Address;
                worksheet.Cells[i + 2, 8] = patients[i].Item;
                worksheet.Cells[i + 2, 9] = "病情变化";
                worksheet.Cells[i + 2, 10] = patients[i].Doctor;
                 }

            //全局字体（除了标题）
            Excel.Range allRange = ((Excel.Range)worksheet.Columns["A:J"]);
            allRange.Font.Size = 12;
            allRange.Font.Name = "宋体";
            allRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


            //标题
            Excel.Range firstRow = worksheet.get_Range("A1", "J1");
            firstRow.Merge(firstRow.MergeCells);
            firstRow.Font.Name = "方正小标宋_GBK";
            firstRow.Font.Size = 20;
            firstRow.Font.Bold = true;
            firstRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            firstRow.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            firstRow.Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            ((Excel.Range)worksheet.Columns["B:E"]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ((Excel.Range)worksheet.Columns["B:E"]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            // 地址和项目，字体缩小，自动换行，
            Excel.Range addrAndItem = ((Excel.Range)worksheet.Columns["G:H"]);
            addrAndItem.WrapText = true;
            addrAndItem.Font.Size = 10;
            //把标题的字体设置回12
            worksheet.get_Range("G2:H2").Font.Size = 12;
                
            // 列宽
            worksheet.get_Range("A1", Missing.Value).ColumnWidth = 16.5;
            worksheet.get_Range("B1", Missing.Value).ColumnWidth = 7.5;
            worksheet.get_Range("C1", Missing.Value).ColumnWidth = 6.0;
            worksheet.get_Range("D1", Missing.Value).ColumnWidth = 4.6;
            worksheet.get_Range("E1", Missing.Value).ColumnWidth = 12.5; // phone
            worksheet.get_Range("F1", Missing.Value).ColumnWidth = 20.0; //id
            worksheet.get_Range("G1", Missing.Value).ColumnWidth = 25; //addr
            worksheet.get_Range("H1", Missing.Value).ColumnWidth = 23; //item
            worksheet.get_Range("I1", Missing.Value).ColumnWidth = 10;
            worksheet.get_Range("J1", Missing.Value).ColumnWidth = 6.67;



            double minRowHeight = 40.1;
            //表头
            worksheet.get_Range("A2", "J2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.get_Range("A2", "J2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


            worksheet.Rows.AutoFit();
            //foreach (Excel.Range row in worksheet.Rows)
            //{
            //    if (row.RowHeight < minRowHeight)
            //    {
            //        row.RowHeight = minRowHeight;  // 设置行高为最小值
            //    }
            //}
            //标题栏行高
            ((Excel.Range)worksheet.Columns["A:A"]).RowHeight = 33;  // 行高
            ((Excel.Range)worksheet.Rows[1, Missing.Value]).RowHeight = 40.1;


            worksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;//页面方向横向
            worksheet.PageSetup.Zoom = 100;//打印时页面设置,缩放比例
            worksheet.PageSetup.TopMargin = excelApp.InchesToPoints(0.5905512); ; //上边距为0
            worksheet.PageSetup.BottomMargin = excelApp.InchesToPoints(0.5905512); //下边距为0
            worksheet.PageSetup.LeftMargin = excelApp.InchesToPoints(0.2); //左边距为0.5
            worksheet.PageSetup.RightMargin = excelApp.InchesToPoints(0.2); //右边距为0.5
            worksheet.PageSetup.CenterHorizontally = true;//水平居中
            worksheet.PageSetup.PrintTitleRows = "1:1";
            //加载图片，并设置图片大小
            //添加图片到页眉右边的单元格

            worksheet.PageSetup.LeftHeaderPicture.Filename = currentPath + "\\cache\\logo.png";
            worksheet.PageSetup.LeftHeader = "&G";
            worksheet.PageSetup.LeftHeaderPicture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
            worksheet.PageSetup.LeftHeaderPicture.Height = (float)excelApp.InchesToPoints(0.5314961);
            worksheet.PageSetup.LeftHeaderPicture.Width = (float)excelApp.InchesToPoints(2.2047244);
            worksheet.PageSetup.FooterMargin = excelApp.InchesToPoints(0.2);//页脚1
            worksheet.PageSetup.CenterFooter = "注：";
            worksheet.PageSetup.RightFooter = @"&P/&N";
            string savePath = "";
            // 显示 Excel 窗口
            excelApp.Visible = true;

            // 使用 Dispatcher.Invoke 来确保 UI 操作在主线程中执行
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                // 在 UI 线程中执行对控件的操作
                string currentUser = string.IsNullOrEmpty(selectUserComboBox.Text) ? "None" : selectUserComboBox.Text;
                string dateDuration = "全部时长";
                if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
                {
                    dateDuration = StartDatePicker.SelectedDate.Value.ToString("yyyyMMdd") + "-" + EndDatePicker.SelectedDate.Value.ToString("yyyyMMdd");
                }

                savePath = $"{saveDirectory}{currentUser}.医检互认.{dateDuration}.xlsx";
            });
            ChangeStartBtnStyle(false);

            // 保存 Excel 文件
            //string filePath = @"D:\MytoolDataFiles\patients_filtered.xlsx";
            workbook.SaveAs(savePath);

            // 关闭工作簿并释放资源
            //workbook.Close(0);
            //excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    //UMessageBox.Show("数据已成功导出到 Excel 文件: " + filePath);
                }




        // 检查路径是否存在，如果不存在则创建
        public static void EnsureDirectoryExists(string path)
        {
            // 检查路径是否存在
            if (!Directory.Exists(path))
            {
                // 如果不存在，创建目录
                Directory.CreateDirectory(path);
                Console.WriteLine("目录已创建: " + path);
            }
            else
            {
                Console.WriteLine("目录已存在: " + path);
            }
        }
        public static string ConvertToCustomFormat(string originalDateTime)
        {
            // 尝试将字符串解析为 DateTime 对象
            DateTime parsedDateTime;
            if (DateTime.TryParse(originalDateTime, out parsedDateTime))
            {
                // 将 DateTime 按指定格式转换为字符串
                return parsedDateTime.ToString("yyyy.MM.dd HH:mm");
            }
            else
            {
                // 解析失败时返回一个默认值或错误信息
                return "Invalid Date Format";
            }
        }
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

                MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndeterminate(buttonBuild, isIndeterminate);
                buttonBuild.Content = content;
            });

        }

    }
        

}


