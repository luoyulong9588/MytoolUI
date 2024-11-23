using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Sunny.UI;


namespace MytoolUI
{
    public partial class DischargeFollowUpUI : Form
    {

        DataTableCollection tableCollection;
        string savePath = @"D:\出院随访";
        DataSet data;
        private UIMessageForm message = new UIMessageForm();

        public DischargeFollowUpUI(Color color,string currentUserName)
        {
            InitializeComponent();
            SetBtnColor(color);
            InitDataGridView();
            this.comboBoxSelectUser.Text = currentUserName;
        }

        private void SetBtnColor(Color color)
        {
            this.uBtnOpendir.ForeHoverColor = color;
            this.uBtnOpendir.ForePressColor = color;
            this.uBtnOpendir.ForeSelectedColor = color;
            this.uBtnStart.ForeHoverColor = color;
            this.uBtnStart.ForePressColor = color;
            this.uBtnStart.ForeSelectedColor = color;

            this.uBtnBrowse.ForeHoverColor = color;
            this.uBtnBrowse.ForePressColor = color;
            this.uBtnBrowse.ForeSelectedColor = color;

            this.comboBoxSelectUser.ItemSelectForeColor = Color.White;
            this.comboBoxSelectUser.ItemSelectBackColor = color;
            this.comboBoxSheets.ItemSelectForeColor = Color.White;
            this.comboBoxSheets.ItemSelectBackColor = color;

            this.uiProcessBar.RectColor = color;
        }

        private void InitDataGridView()
        {
          uiDataGridViewTable.AllowUserToAddRows = false;
            for (int i = 0; i < 20; i++) // 假定需要显示20行空表格,具体根据需要修改此处
            {
                uiDataGridViewTable.Columns.Add(new DataGridViewTextBoxColumn());
                this.uiDataGridViewTable.Rows.Add(new DataGridViewRow());
            }
            this.uiDataGridViewTable.ColumnHeadersVisible = false;
            this.uiDataGridViewTable.RowHeadersVisible = false;

        }

        private void uBtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilepath.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                            });
                            data = result;
                            //startFormatExcel(result);  // 打印信息debug
                            tableCollection = result.Tables;
                            comboBoxSheets.Items.Clear();
                            foreach (DataTable table in tableCollection)
                            {
                                comboBoxSheets.Items.Add(table.TableName);
                            }
                            try
                            {
                                DataTable dt = tableCollection[0];
                                comboBoxSheets.Text = "任意表-出入院登记表";
                                uiDataGridViewTable.ClearAll();
                                uiDataGridViewTable.DataSource = dt;
                                uiDataGridViewTable.Font = new Font("微软雅黑", 10, FontStyle.Regular);
                                uiDataGridViewTable.ColumnHeadersVisible = false;
                                uiDataGridViewTable.RowHeadersVisible = false;
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine(ex.ToString());
                            }

                        }
                    }
                }

            }
        }
        private void startFormatExcel(DataSet data)
        {
            int columns, rows;
            DateTime outDate=new DateTime();
            new DatabaseUnit().ReadLogo(); //数据库读取logo文件;
            if (this.textBoxFilepath.Text == "")
            {
                message.ShowWarningDialog("未选择文件!");
                return;
            }
            if (comboBoxSelectUser.SelectedItem == null)
            {
                message.ShowErrorDialog("selectUserValueError,未选择姓名!");
                return;
            }
            
            string selectUser = this.comboBoxSelectUser.SelectedItem.ToString();
            columns = data.Tables[0].Columns.Count;//获取列数
            rows = data.Tables[0].Rows.Count;//获取行数
            this.uiProcessBar.Maximum = rows+26;
            this.uiProcessBar.Value = 0;
            string painId,
                painName,
                painGender,
                painAge,
                painIdCard,
                painVocation,
                painPhone,
                painAddrNow,
                doctorName,
                inDay,
                inDiagnose,
                outDay,
                duringDay,
                outDiagnose;

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
                painId = data.Tables[0].Rows[i][1].ToString();
                painName = data.Tables[0].Rows[i][2].ToString();
                painGender = data.Tables[0].Rows[i][3].ToString();
                painAge = data.Tables[0].Rows[i][4].ToString();
                painIdCard = data.Tables[0].Rows[i][5].ToString();
                painVocation = data.Tables[0].Rows[i][6].ToString();
                painPhone = data.Tables[0].Rows[i][7].ToString();
                painAddrNow = data.Tables[0].Rows[i][8].ToString();
                doctorName = data.Tables[0].Rows[i][11].ToString();
                inDay = data.Tables[0].Rows[i][12].ToString();
                inDiagnose = data.Tables[0].Rows[i][13].ToString();
                outDay = data.Tables[0].Rows[i][14].ToString();
                duringDay = data.Tables[0].Rows[i][15].ToString();
                outDiagnose = data.Tables[0].Rows[i][16].ToString();
                if (painId != "" && doctorName == selectUser)
                {
                    outDate = Convert.ToDateTime(outDay);
                    objData[startRow, 0] = outDate.ToString("yyyy-MM-dd");
                    objData[startRow, 1] = painName;
                    objData[startRow, 2] = painGender;
                    objData[startRow, 3] = painAge;
                    objData[startRow, 4] = outDiagnose;
                    objData[startRow, 5] = painPhone;
                    objData[startRow, 8] = doctorName;
                    startRow++;
                }
                this.uiProcessBar.Value += 1;
            }
            Excel.Range range = (Excel.Range)ws.Cells[1, 1];
            range = range.get_Resize(startRow, columnCount);
            range.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, objData);
            this.uiProcessBar.Value += 1;
            // 设置样式;
            ws.get_Range("A1", Missing.Value).ColumnWidth = 11.5;
            this.uiProcessBar.Value += 1;
            ws.get_Range("B1", Missing.Value).ColumnWidth = 7.1;
            this.uiProcessBar.Value += 1;
            ws.get_Range("C1", Missing.Value).ColumnWidth = 4.6;
            this.uiProcessBar.Value += 1;
            ws.get_Range("D1", Missing.Value).ColumnWidth = 5.6;
            this.uiProcessBar.Value += 1;
            ws.get_Range("E1", Missing.Value).ColumnWidth = 27.5;
            this.uiProcessBar.Value += 1;
            ws.get_Range("F1", Missing.Value).ColumnWidth = 12.5;
            this.uiProcessBar.Value += 1;
            ws.get_Range("G1", Missing.Value).ColumnWidth = 22;
            this.uiProcessBar.Value += 1;
            ws.get_Range("H1", Missing.Value).ColumnWidth = 12;
            this.uiProcessBar.Value += 1;
            ws.get_Range("I1", Missing.Value).ColumnWidth = 6.67;
            this.uiProcessBar.Value += 1;
            ws.get_Range("J1", Missing.Value).ColumnWidth = 12.3;
            this.uiProcessBar.Value += 1;
            ws.get_Range("K1", Missing.Value).ColumnWidth = 8.5;
            this.uiProcessBar.Value += 1;
            ((Excel.Range)ws.Columns["A:A"]).RowHeight = 33;  // 行高
            this.uiProcessBar.Value += 1;
            ((Excel.Range)ws.Rows[1, Missing.Value]).RowHeight = 40.1;
            this.uiProcessBar.Value += 1;
            Excel.Range allRange = ((Excel.Range)ws.Columns["A:K"]);
            this.uiProcessBar.Value += 1;
            allRange.Font.Size = 12;
            this.uiProcessBar.Value += 1;
            allRange.Font.Name = "宋体";
            this.uiProcessBar.Value += 1;
            //设置边框
            allRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            this.uiProcessBar.Value += 1;
            Excel.Range firstRow = ws.get_Range("A1", "K1");
            this.uiProcessBar.Value += 1;
            firstRow.Merge(firstRow.MergeCells);
            this.uiProcessBar.Value += 1;
            firstRow.Font.Name = "方正小标宋_GBK";
            firstRow.Font.Size = 20;
            firstRow.Font.Bold = true;
            firstRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            firstRow.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            firstRow.Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            ws.get_Range("A2", "K2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            this.uiProcessBar.Value += 1;
            ws.get_Range("A2", "K2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            this.uiProcessBar.Value += 1;
            ws.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;//页面方向横向
            //ws.PageSetup.Zoom = 100;//打印时页面设置,缩放比例
            ws.PageSetup.TopMargin = xlapp.InchesToPoints(0.5905512); ; //上边距为0
            ws.PageSetup.BottomMargin = xlapp.InchesToPoints(0.5905512); //下边距为0
            ws.PageSetup.LeftMargin = xlapp.InchesToPoints(0.2); //左边距为0.5
            ws.PageSetup.RightMargin = xlapp.InchesToPoints(0.2); //右边距为0.5
            ws.PageSetup.CenterHorizontally = true;//水平居中
            ws.PageSetup.PrintTitleRows = "1:1";
            this.uiProcessBar.Value += 1;


            //加载图片，并设置图片大小


            //添加图片到页眉右边的单元格

            ws.PageSetup.LeftHeaderPicture.Filename =Application.StartupPath + "\\cache\\logo.png";
            ws.PageSetup.LeftHeader = "&G";
            ws.PageSetup.LeftHeaderPicture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
            ws.PageSetup.LeftHeaderPicture.Height = (float)xlapp.InchesToPoints(0.5314961);
            ws.PageSetup.LeftHeaderPicture.Width = (float)xlapp.InchesToPoints(2.2047244);
            this.uiProcessBar.Value += 1;
            ws.PageSetup.FooterMargin = xlapp.InchesToPoints(0.2);//页脚1
            ws.PageSetup.CenterFooter = "注：随访过程中患者提出意见随访人员在备注栏注明处置情况，初次、二次随访需在备注栏标明。";
            ws.PageSetup.RightFooter = @"&P/&N" ;
            this.uiProcessBar.Value += 1;
            string saveFileName = string.Format(@"{0}\出院随访.{1}.{2}.xlsx",savePath, selectUser, outDate.ToString("yyyy年MM月dd日"));
            checkFolderAndFile(savePath, saveFileName);
            this.uiProcessBar.Value += 1;
            wb.SaveAs(saveFileName);
            wb.Saved = true;
            destoryExcelApp(xlapp);
            this.uiProcessBar.Value += 1;
            Process.Start("explorer.exe", saveFileName);
        }

        private void checkFolderAndFile(string folder,string fileName) {

            if (Directory.Exists(folder)==false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(folder);
            }
            if (File.Exists(fileName) == true)//如果不存在就创建file文件夹
            {
                File.Delete(fileName);
            }
        }

        #region Kill Special Excel Process
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        #endregion
        private void destoryExcelApp(Excel.Application app) {
            
            if (app != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(app.Hwnd), out lpdwProcessId);
                Process.GetProcessById(lpdwProcessId).Kill();
            }

        }

        private void comboBoxSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[comboBoxSheets.SelectedItem.ToString()];
            uiDataGridViewTable.DataSource = dt;
            uiDataGridViewTable.Font = new Font("微软雅黑", 10, FontStyle.Regular);
            uiDataGridViewTable.ColumnHeadersVisible = false;
            uiDataGridViewTable.RowHeadersVisible = false;
        }

        private void uBtnStart_Click(object sender, EventArgs e)
        {
            startFormatExcel(data);
        }

        private void uBtnOpendir_Click(object sender, EventArgs e)
        {
            string savePath = @"D:\出院随访";
            Process.Start("explorer.exe", savePath);
        }

        
    }
}
