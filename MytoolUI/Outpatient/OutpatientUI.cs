using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Excel = Microsoft.Office.Interop.Excel;
using MytoolUI.common;


namespace MytoolUI
{

    public partial class OutpatientUI : Form
    {
        public Random rd = new Random();
        private string programDir = @"D:\门诊日志\";
        private string saveName;
        DataTable dataTable;
        DataTableCollection tableCollection;
        Excel.Application xlapp;
        Excel.Workbook workbook;
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\address.db;Version=3;");
        DatabaseForOutpatient db_info = new DatabaseForOutpatient();
        public OutpatientUI(Color color)
        {
            InitializeComponent();
            SetBtnColor(color);
            InitDataGridView();
            m_dbConnection.Open();

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

            this.comboBoxSheets.ItemSelectBackColor = color;
            this.comboBoxSheets.ItemSelectForeColor = Color.White;
        }

        private void InitDataGridView()
        {
            uiDataGridViewDesktop.AllowUserToAddRows = false;
            for (int i = 0; i < 20; i++) // 假定需要显示20行空表格,具体根据需要修改此处
            {
                uiDataGridViewDesktop.Columns.Add(new DataGridViewTextBoxColumn());
                this.uiDataGridViewDesktop.Rows.Add(new DataGridViewRow());
            }
            this.uiDataGridViewDesktop.ColumnHeadersVisible = false;
            this.uiDataGridViewDesktop.RowHeadersVisible = false;

        }


        /// <summary>
        /// 获取文件，把xls写入到dataTable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uBtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilepath.Text = openFileDialog.FileName;
                    try
                    {
            
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                            });
                            tableCollection = result.Tables;
                            comboBoxSheets.Items.Clear();
                            foreach (DataTable table in tableCollection)
                            {
                                comboBoxSheets.Items.Add(table.TableName);
                            }
                            try
                            {
                                DataTable dt = tableCollection[0];
                                this.dataTable = dt;
                                comboBoxSheets.Text = dt.TableName;
                                uiDataGridViewDesktop.ClearAll();
                                uiDataGridViewDesktop.DataSource = dt;
                                uiDataGridViewDesktop.Font = new Font("微软雅黑", 10, FontStyle.Regular);
                                uiDataGridViewDesktop.ColumnHeadersVisible = false;
                                uiDataGridViewDesktop.RowHeadersVisible = false;
                            }
                            catch (Exception ex)
                            {
                                Sunny.UI.UIMessageDialog.ShowErrorDialog(this,ex.ToString());
                            }

                        }
                    }
                    }
                    catch (System.IO.IOException)
                    {

                        MessageBox.Show("文件正在被使用，请关闭使用该文件的程序后重试！","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }

            }
        }
        private void start()
        {
            if (this.dataTable == null || this.textBoxFilepath.Text == "")
            {
                Sunny.UI.UIMessageDialog.ShowErrorDialog(this, "没有选择正确的文件！");
                return;
            }
            int rows, columns;
            string doctorName, painName, male, female, age, phone, vocation, idCard, workAddress, nowAddress, comeDate, diaseDate, isFirst, notFirst, bloodPressure, mainChef, blank, diagMemory, mainDrug;
            rows = dataTable.Rows.Count;//获取行数
            columns = dataTable.Columns.Count;
            string userName;
            userName = new DatabaseUnit().GetuserName();
            string outportDate = Convert.ToDateTime(dataTable.Rows[1][0].ToString().Split(' ')[0].Split(':')[1]).ToString("yyyy年MM月");
            this.saveName = string.Format(@"{0}{1}门诊日志.{2}{3}", programDir, outportDate, userName, Path.GetExtension(this.textBoxFilepath.Text));
            object[,] arrycell = new object[rows - 7, 19];

            Console.WriteLine(rows - 7);
            //int[,] array = new int[5, 3]
            for (int i = 4; i < rows - 3; i++)
            {
                doctorName = dataTable.Rows[i][0].ToString();
                painName = dataTable.Rows[i][1].ToString();
                male = dataTable.Rows[i][2].ToString();
                female = dataTable.Rows[i][3].ToString();
                age = dataTable.Rows[i][4].ToString();
                phone = dataTable.Rows[i][5].ToString();
                vocation = dataTable.Rows[i][6].ToString();
                idCard = dataTable.Rows[i][7].ToString();
                workAddress = dataTable.Rows[i][8].ToString();
                nowAddress = dataTable.Rows[i][9].ToString();
                comeDate = dataTable.Rows[i][10].ToString();
                diaseDate = dataTable.Rows[i][11].ToString();
                isFirst = dataTable.Rows[i][12].ToString();
                notFirst = dataTable.Rows[i][13].ToString();
                bloodPressure = dataTable.Rows[i][14].ToString();
                mainChef = dataTable.Rows[i][15].ToString();
                diagMemory = dataTable.Rows[i][17].ToString();
                blank = dataTable.Rows[i][16].ToString();
                mainDrug = dataTable.Rows[i][18].ToString();
                if (doctorName != userName)
                {
                    MessageBox.Show("doctorName dose not match the userName in the database;", "SQL/SQLite Error");
                    return;
                }

                if (phone == "")
                {
                    phone = randomPhone();
                    //Console.WriteLine(string.Format("phpone-{0}- name{1}", phone, painName));
                }
                if (vocation == "" || vocation == "农民" || vocation == "无职业" || vocation == "其他" || workAddress == "" || workAddress == "无" || workAddress == "-"||workAddress == "_") //农民不存在工作地址  // 没有工作地址的只配当农民
                {
                    vocation = "农民";
                    workAddress = "无";
                }
                if (idCard == "")
                {
                    idCard = "未带";
                //    continue;  //  未带身份证的患者作移除操作；
                }

                nowAddress = FormatAddress(nowAddress);

                bloodPressure = randomBloodPressure();

                if (diaseDate == "")
                {
                    diaseDate = comeDate;
                }
                if (mainDrug == "") // 首先处理无医嘱的;
                {
                    mainDrug = "未就诊";
                    mainChef = "未就诊";
                    diagMemory = "未就诊";
                }

                if (mainDrug.Contains("新型冠状病毒核酸")) // 再处理核酸检测的;
                {
                    mainChef = "新型冠状病毒核酸筛查";
                }

                if (diagMemory == "")
                {
                    diagMemory = mainDrug;
                    if (mainChef == "")
                    {
                        mainChef = mainDrug;
                    }
                }
                if (mainChef == "")
                {
                    mainChef = randomChief(diagMemory);
                }
                string[] arryeachInfo = { doctorName, painName, male, female, age, phone, vocation, idCard, workAddress, nowAddress, comeDate, diaseDate, isFirst, notFirst, bloodPressure, mainChef, blank, diagMemory, mainDrug };


                string gender = male.Length > 0 ? "男" : "女";
               // string sql = $@"insert into informations VALUES(""{doctorName}"",""{painName}"",""{gender}"",""{age}"",""{phone}"",""{vocation}"",""{idCard}"",""{workAddress}"",""{nowAddress}"",""{comeDate}"",""{diaseDate}"",""{bloodPressure}"",""{mainChef}"",""{diagMemory}"",""{mainDrug}"")";
                db_info.SaveInfoToDb(doctorName, painName, gender, age, phone, vocation, idCard, workAddress, nowAddress, comeDate, diaseDate, bloodPressure, mainChef, diagMemory, mainDrug);


                //Console.WriteLine(arryeachInfo.ToString());
                for (int j = 0; j < arryeachInfo.Length; j++)
                {
                    //Console.WriteLine(string.Format("i-->{0};j-->{1}",i,j));
                    //Console.WriteLine(arryeachInfo[0]);
                    arrycell[i - 4, j] = arryeachInfo[j];
                }
            }
            fillExcel(arrycell, rows, columns);
        }


        
        private void fillExcel(object[,] arrycell, int rows, int columns)
        {
            checkFolder(programDir);
            xlapp = new Excel.Application();
            xlapp.Visible = false;
            xlapp.DisplayAlerts = false;
            string filePath = textBoxFilepath.Text;
            object missing = Missing.Value;
            object readOnly = false;
            workbook = xlapp.Workbooks.Open(filePath, missing, readOnly,
                                                      missing, missing, missing, missing, missing,
                                                     missing, missing, missing, missing, missing,
                                                     missing, missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)xlapp.Workbooks[1].Worksheets[1];
            Excel.Range range = (Excel.Range)worksheet.Cells[5, 1];
            range = range.get_Resize(rows - 7, columns - 3);
            // Assign the 2-d array to the Excel Range
            range.NumberFormatLocal = "@";
            range.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, arrycell);
            worksheet.get_Range("F1", Missing.Value).ColumnWidth = 14;
            worksheet.get_Range("H1", Missing.Value).ColumnWidth = 20;
            worksheet.get_Range("L1", Missing.Value).ColumnWidth = 10;
            worksheet.get_Range("K1", Missing.Value).ColumnWidth = 10;
            workbook.CheckCompatibility = false;
            Console.WriteLine(this.saveName);
            try
            {
                workbook.SaveAs(this.saveName);
                workbook.Saved = true;
            }
            catch (COMException e)
            {

                MessageBox.Show($"无法保存文件到指定路径,请检查当前保存的文件是否已被打开？\n Exception Message:\n{e.ToString()}", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("格式化后的门诊日志已自动保存至 <D:\\门诊日志> 目录下,现在可手动切换到入门诊首页填写模式。","提示",MessageBoxButtons.OK);
            
        }
        /// <summary>
        /// 格式化地址
        /// </summary>
        /// <param name="address">原始的地址格式</param>
        /// <returns>1.城市：xx市xx区xx街道xx社区xx小区xx-xx-xx 2.农村：xx市xx区xx镇xx村xx[组社队]</returns>
        private string FormatAddress(string address)
        {
            address = "重庆市綦江区" + address.Replace("重庆市", "").Replace("綦江区", "").Replace("綦江县", "").Replace("重庆", "").Replace("綦江", "").Replace(" ", "");

            //Regex cityRegex = new Regex(@"\S+綦江\S+[组社号队]")
            string villagePattern = @"\S+村\S+[组社号队]";// match format : xx村xx组 or xx村xx社 or xx村xx号 with out house number.
            string townPattern = @"\S+[镇]\S+[号]"; // match format: xx镇xx号.

            //1.不含村，含有 number-number的，则属于城镇;要先replace space
            if (Regex.IsMatch(address, @"\d-\d") && !Regex.IsMatch(address, @"[组村社号队镇]+"))
            {
                return address;
            }
            else if (Regex.IsMatch(address, villagePattern))
            {
                return new Regex(villagePattern).Match(address).Value;
            }
            else if (Regex.IsMatch(address, townPattern))
            {
                return new Regex(townPattern).Match(address).Value;
            }

            //2.剩下的2中情况，2.1 不含number-number含有村的【农村】；2.2 不含number-number不含有村的【可能城市】；2.3含有number-number含有村的【农村，错误address】；

            return randomAddress();


        }
        private string randomPhone()
        {
            string[] dog = { "1", "213" };
            string[] arrayHead = { "13", "15", "17", "18" };
            string head = arrayHead[rd.Next(4)];
            string end = rd.Next(19010102, 997939299).ToString();
            //Console.WriteLine(string.Format("函数中{0}", end));
            return head + end;
        }

        /// <summary>
        /// 产生綦江区内的随机地址
        /// </summary>
        /// <returns>xx市xx区xx街道/镇xx社区/村xx组/门牌号</returns>
        private string randomAddress()
        {
            /*string city = "重庆市綦江区";
            string[] arrayContry = { "古南镇","古南街道","文龙街道","三江街道","金桥镇",
                                        "石角镇","东溪镇","赶水镇","打通镇","石壕镇",
                                        "永新镇","三角镇","隆盛镇","郭扶镇","篆塘镇",
                                        "丁山镇","安稳镇","扶欢镇","永城镇","新盛镇",
                                        "中峰镇","横山镇"};
            string contry = arrayContry[rd.Next(arrayContry.Length)];
            return city + contry;*/

            string city = "重庆市綦江区";
            int streetId = 500110002;
            string streetName = "文龙街道";
            string villageName = "版画院社区";
            string endName;
            SQLiteCommand command = new SQLiteCommand("select id,name from street_qj", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int count = 0;
            //Random random = new Random();
            int maxCont = new Random(Guid.NewGuid().GetHashCode()).Next(0, 23);
      
            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString());
                count++;
                if (count>=maxCont)
                {
                    streetId = int.Parse(reader[0].ToString());
                    streetName = reader[1].ToString();
                    command = new SQLiteCommand($"select name from village_qj where id ={streetId}", m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read()) { 
                      villageName = reader[0].ToString();
                    }
                    break;
                }
               
            }
            if (villageName.Contains("村"))
            {
                //endName = $"{new Random(Guid.NewGuid().GetHashCode()).Next(1, 8)}组{new Random(Guid.NewGuid().GetHashCode()).Next(1, 99)}号";
                endName = $"{new Random(Guid.NewGuid().GetHashCode()).Next(1, 8)}组";
            }
            else
            {
                endName = $"{new Random(Guid.NewGuid().GetHashCode()).Next(1, 99)}号{new Random(Guid.NewGuid().GetHashCode()).Next(1, 13)}-{new Random(Guid.NewGuid().GetHashCode()).Next(1, 8)}";
            }
            return city + streetName + villageName + endName;

        }

        /// <summary>
        /// 随机血压生产
        /// </summary>
        /// <returns></returns>
        private string randomBloodPressure()
        {
            int sbp, dbp;
            while (true)
            {
                sbp = rd.Next(94, 140);
                dbp = rd.Next(60, 90);
                if (sbp % 2 == 0 && dbp % 2 == 0 && (sbp - dbp) > 30 && (sbp - dbp) < 60)
                {
                    break;
                }
            }
            return string.Format(@"{0}/{1}mmHg", sbp, dbp);
        }
        private string randomChief(string checkString)
        {
            List<string> data = new DatabaseUnit().GetrandomChief(checkString);
            if (data.Count > 0)
            {
                string result = data[rd.Next(data.Count)].ToString();
                return result;
            }
            else
            {
                return checkString;
            }
        }

        /// <summary>
        /// //如果不存在就创建file文件夹
        /// </summary>
        /// <param name="文件夹名"></param>
        private void checkFolder(string folder)
        {
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }
        }

        /// <summary>
        /// 销毁excel App
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutpatientUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            destoryExcelApp(this.xlapp);
        }

        private void comboBoxSheets_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tableCollection != null)
            {
                DataTable dt = tableCollection[comboBoxSheets.SelectedItem.ToString()];
                uiDataGridViewDesktop.DataSource = dt;
                uiDataGridViewDesktop.Font = new Font("微软雅黑", 10, FontStyle.Regular);
                uiDataGridViewDesktop.ColumnHeadersVisible = false;
                uiDataGridViewDesktop.RowHeadersVisible = false;
            }
            else
            {
                Sunny.UI.UIMessageDialog.ShowWarningDialog(this,"请先选择文件!");
            }

        }

        #region Kill Special Excel Process
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        private void destoryExcelApp(Excel.Application app)
        {
            if (app != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(app.Hwnd), out lpdwProcessId);
                Process.GetProcessById(lpdwProcessId).Kill();
            }
        }
        #endregion

        private void uBtnStart_Click(object sender, EventArgs e)
        {
            
            try
            {
                start();
            }
            catch (System.IndexOutOfRangeException)
            {

                MessageBox.Show("所选文件不正确！可能不是正常导出的门诊日志文件，请查对。", "索引超出了数组界限", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uBtnOpendir_Click(object sender, EventArgs e)
        {
            checkFolder(programDir);
            Process.Start(programDir);
        }


    }
}
