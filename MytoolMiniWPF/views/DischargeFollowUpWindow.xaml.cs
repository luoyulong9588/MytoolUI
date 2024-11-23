using Aspose.Words.Tables;
using ExcelDataReader;
using Microsoft.Win32;
using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Excel = Microsoft.Office.Interop.Excel;


namespace MytoolMiniWPF.views
{
    
    /// <summary>
    /// DischargeFollowUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DischargeFollowUpWindow : Window
    {
        DataSet data;
        string currentPath;
        string savePath = @"D:\MytoolDataFiles\出院随访";
        public DischargeFollowUpWindow()
        {
            InitializeComponent();
            string path = new DatabaseUnit().GetFolderPath().followupPath;
            savePath = string.IsNullOrEmpty(path) ? @"D:\MytoolDataFiles\出院随访": path;
            currentPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
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
                    }
                }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
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
        private bool CheckUsersAndFilePath()
        {
            if (string.IsNullOrEmpty(selectUserComboBox.Text))
            {
                UMessageBox.Show("警告！", "请先选择要导出的用户名！");
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPath.Text))
            {
                UMessageBox.Show("警告！", "请先选择要格式化的文件！");
                return false;
            }
            return true;
        }
        private void startFormatExcel()
        {
            if (data is null){return;}

            ChangeStartBtnStyle(true);

            DateTime outDate = new DateTime();
            new DatabaseUnit().ReadLogo(); //数据库读取logo文件;
            DatabaseForPatient db = new DatabaseForPatient();
            db.m_dbConnection.Open();

            int columns = data.Tables[0].Columns.Count;//获取列数
            int rows = data.Tables[0].Rows.Count;//获取行数

            Patient patient = new Patient();

            string selectUser = "";
            App.Current.Dispatcher.Invoke((Action)delegate ()
            {

                selectUser = this.selectUserComboBox.Text;
            });
            

            Excel.Application xlapp = new Excel.Application();
            xlapp.DisplayAlerts = false;
            xlapp.Visible = false;
            Excel.Workbook wb = xlapp.Workbooks.Add();
            Excel.Worksheet ws = (Excel.Worksheet)xlapp.Workbooks[1].Worksheets[1];

            string[] title = { "出院时间", "姓名", "性别","年龄", "出院诊断",
                                "联系电话","随访情况", "患者意见", "随访者",
                                "随访时间", "督查签名"};
            int rowCount = data.Tables[0].Rows.Count;
            int columnCount = data.Tables[0].Columns.Count;
            object[,] objData = new object[data.Tables[0].Rows.Count + 1, data.Tables[0].Columns.Count + 1];
            objData[0, 0] = "出院患者随访登记本";
            for (int i = 0; i < title.Length; i++)
            {
                objData[1, i] = title[i];
            }

            int startRow = 2;
            for (int i = 1; i < rows; i++)
            {
                patient.Id = data.Tables[0].Rows[i][1].ToString();
                patient.Name = data.Tables[0].Rows[i][2].ToString();
                patient.Gender = data.Tables[0].Rows[i][3].ToString();
                patient.Age = data.Tables[0].Rows[i][4].ToString();
                patient.IdCardNumber = data.Tables[0].Rows[i][5].ToString();
                patient.Vocation = data.Tables[0].Rows[i][6].ToString();
                patient.Phone = data.Tables[0].Rows[i][7].ToString();
                patient.HomeAddr = data.Tables[0].Rows[i][8].ToString();
                patient.DoctorName = data.Tables[0].Rows[i][11].ToString();
                patient.InDay = data.Tables[0].Rows[i][12].ToString();
                patient.InDiagnose = data.Tables[0].Rows[i][13].ToString();
                patient.OutDay = data.Tables[0].Rows[i][14].ToString();
                patient.DuringDay = data.Tables[0].Rows[i][15].ToString();
                patient.OutDiagnose = data.Tables[0].Rows[i][16].ToString();
                if (patient.Id != "" && patient.DoctorName == selectUser)
                {
                    outDate = Convert.ToDateTime(patient.OutDay);
                    objData[startRow, 0] = outDate.ToString("yyyy-MM-dd");
                    objData[startRow, 1] = patient.Name;
                    objData[startRow, 2] = patient.Gender;
                    objData[startRow, 3] = patient.Age;
                    objData[startRow, 4] = patient.OutDiagnose;
                    objData[startRow, 5] = patient.Phone;
                    objData[startRow, 8] = patient.DoctorName;
                    startRow++;
                }
                if (!string.IsNullOrEmpty(patient.Id)&&patient.Id !="住院号")
                {
                    
                    db.InsertPatientInfo(patient);

                }

            }
            db.m_dbConnection.Close();

            Excel.Range range = (Excel.Range)ws.Cells[1, 1];
            range = range.get_Resize(startRow, columnCount);
            range.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, objData);
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
            //设置边框
            allRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            Excel.Range firstRow = ws.get_Range("A1", "K1");
            firstRow.Merge(firstRow.MergeCells);
            firstRow.Font.Name = "方正小标宋_GBK";
            firstRow.Font.Size = 20;
            firstRow.Font.Bold = true;
            firstRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            firstRow.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            firstRow.Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            // 设置出院诊断列的字体
            Excel.Range outDiagnose = ((Excel.Range)ws.Columns["E"]);
            outDiagnose.Font.Size = 10;



            ws.get_Range("A2", "K2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.get_Range("A2", "K2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

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
            string saveFileName = string.Format(@"{0}\出院随访.{1}.{2}.xlsx", savePath, selectUser, outDate.ToString("yyyy年MM月dd日"));
            
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
    }
}
