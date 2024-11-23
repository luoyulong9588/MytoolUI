using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Sunny.UI;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MytoolUI
{
    public delegate void DummyDelegate();
    public partial class ChronicDiseasesUI : Form
    {
        DataTableCollection tableCollection;
        private Form currentPrintForm;
        private List<string> pathList;
        private UIMessageForm message = new UIMessageForm();
        DataSet data;
        Color color;

        public ChronicDiseasesUI(Color color, string currentUserName)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.color = color;
            SetBtnColor(color);
            //SetDefaultUser();
            this.comboBoxSelectUser.Text = currentUserName;
        }
        private void SetBtnColor(Color color)
        {
            List<UIButton> buttons = new List<UIButton> { uBtnBrowse, uBtnStart };
            foreach (UIButton btn in buttons)
            {
                btn.ForeHoverColor = color;
                btn.ForePressColor = color;
                btn.ForeSelectedColor = color;
                btn.RectColor = Color.White;
                btn.FillColor = Color.White;
                btn.FillDisableColor = Color.FromArgb(244, 244, 244);
                btn.FillHoverColor = Color.FromArgb(250, 223, 236);
                btn.FillSelectedColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.ForeHoverColor = color;
                btn.ForePressColor = Color.Black;
                btn.ForeSelectedColor = Color.Black;
                btn.RectColor = Color.White;
                btn.RectDisableColor = Color.White;
                btn.RectHoverColor = Color.White;
                btn.RectPressColor = Color.White;
                btn.RectSelectedColor = Color.White;
            }

            this.textBoxFilepath.RectColor = Color.DimGray;
            List<UIButton> functionButtons = new List<UIButton> { uBtnClear, uBtnOpendir,uBtnPrint };
            foreach (UIButton btn in functionButtons)
            {
                btn.ForeHoverColor = color;
                btn.ForePressColor = color;
                btn.ForeSelectedColor = color;
            }

            this.ucheckBoxAlignstr.CheckBoxColor = color;
            this.uiCheckBoxCheckAMI.CheckBoxColor = color;

            this.comboBoxSelectUser.ItemSelectBackColor = color;
            this.comboBoxSelectUser.ItemSelectForeColor = Color.White;

            this.progressBar1.RectColor = color;
            this.progressBar1.Visible = false;

            this.uiListBoxCopd.ItemSelectBackColor = UIlabelCopd.SymbolColor;
            this.uiListBoxCopd.ItemSelectForeColor = Color.White;
            this.uiListBoxCopd.HoverColor = UIlabelCopd.SymbolColor;

            this.uiListBoxAmi.ItemSelectBackColor = UIlabelAMI.SymbolColor;
            this.uiListBoxAmi.ItemSelectForeColor = Color.White;
            this.uiListBoxAmi.HoverColor = UIlabelAMI.SymbolColor;

            this.uiListBoxApoplexy.ItemSelectBackColor = UIlabelApoplexy.SymbolColor;
            this.uiListBoxApoplexy.ItemSelectForeColor = Color.White;
            this.uiListBoxApoplexy.HoverColor = UIlabelApoplexy.SymbolColor;

            this.progressBar1.FillColor = color;
            //this.progressBar1.RectColor = Color.White;


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
                        }
                    }
                }

            }
        }
        private List<Pain> getBasicInformation()
        {
            int rows, columns;
            List<Pain> painInfoList = new List<Pain>();
            List<string> diag_select = new List<string>
            //{ "慢性阻塞性", "慢性支气管炎", "哮喘", "肺气肿", "支气管扩张", "急性支气管炎" };
            { "慢性阻塞性", "慢性支气管炎", "哮喘", "肺气肿",  "急性支气管炎" }; // 2022年1月8日 16:08  移除支气管扩张
            if (uiCheckBoxCheckAMI.Checked)
            {
                diag_select.Add("心肌梗死");
                diag_select.Add("急性冠脉综合征");
            }
            if (ucheckBoxAlignstr.Checked)
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
                        Pain person = new Pain();
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
                        painInfoList.Add(person);
                    }
                }
            }
            return painInfoList;

        }


        private void StartProgram()
        {

            if (this.textBoxFilepath.Text == "")
            {
                message.ShowWarningDialog("未选择文件!");
                return;
            }
            if (this.comboBoxSelectUser.Text == "选择姓名")
            {
                message.ShowWarningDialog("未选择用户!");
                return;
            }



            List<Pain> painList = new List<Pain>();
            FillCardToWord app;
            string userName = "";
            this.ChangeButtonStyle(false);
            try
            {
                painList = getBasicInformation();
            }
            catch (Exception ex)
            {
                message.ShowErrorDialog("尝试读取文件时发生错误,可能的原因为未选择导出文件或选择文件错误！\r\n" + ex);
            }
            try
            {
                comboBoxSelectUser.Items.ToString().Trim();
                userName = comboBoxSelectUser.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                message.ShowErrorDialog(string.Format("尝试读取用户名时发生错误!可能的原因为没有选择用户信息。\r\n{0}", ex));
            }



            app = new FillCardToWord(painList);

            progressBar1.Value = 1;
            //CheckedListBox[] checkList = { checkedListBoxCOPD, checkedListBoxAMI, checkedListBoxAPoplexy };
            UIListBox[] listBoxs = { uiListBoxCopd, uiListBoxAmi, uiListBoxApoplexy };

            this.pathList = app.startProgram(userName, progressBar1, listBoxs);
            this.ChangeButtonStyle(true);
            ShowCounts(true);

        }
        private void ShowCounts(bool isFinished)
        {
            if (isFinished)
            {
                this.UIlabelAMI.Text ="心血管疾病" + "（" + this.uiListBoxAmi.Items.Count.ToString() + "）";
                this.UIlabelCopd.Text ="慢性肺病" +"（" + this.uiListBoxCopd.Items.Count.ToString() + "）";
                this.UIlabelApoplexy.Text ="脑卒中"+ "（" + this.uiListBoxApoplexy.Items.Count.ToString() + "）";
            }
            else
            {
                this.UIlabelAMI.Text = "心血管疾病";
                this.UIlabelCopd.Text = "慢性肺病";
                this.UIlabelApoplexy.Text = "脑卒中";
            }

        }

        private void ChangeButtonStyle(bool finished)
        {
            if (finished)
            {
                this.uBtnStart.Cursor = Cursors.Arrow;
                this.uBtnStart.Symbol = 61452;
                this.uBtnStart.Text = "完成";
                this.uBtnStart.ForeColor = this.color;
                this.uBtnPrint.Enabled = true;
                this.uBtnPrint.Symbol = 61487;
            }
            else
            {

                this.uBtnStart.Cursor = Cursors.WaitCursor;
                this.uBtnStart.Symbol = 57389;
                this.uBtnStart.Text = "运行中..";
                this.uBtnPrint.Enabled = false;
                this.uBtnPrint.Symbol = 61534;
            }
        }

        private void OpenPrinterForm(Form childForm)
        {
            if (currentPrintForm != null)
            {
                currentPrintForm.Close();
            }
            currentPrintForm = childForm;
            childForm.TopLevel = true;
            //childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ChronicDiseasesUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\cache";
                var files = Directory.GetFiles(path, "*.*");
                foreach (var file in files)
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                       message.ShowErrorDialog(string.Format("缓存文件清除失败!,\r\n{0}", ex));
                    }
            }
            catch (Exception ex)
            {

                Console.WriteLine("缓存文件清除失败" + ex);
            }
        }

        private void uBtnOpendir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"D:\重庆市居民慢病报卡\");
        }
        private void uBtnPrint_Click(object sender, EventArgs e)
        {
            OpenPrinterForm(new PrinterUI(this.pathList));
        }

        private void uBtnStart_Click(object sender, EventArgs e)
        {
            
            this.progressBar1.Visible = true;
            Thread th1 = new Thread(StartProgram);
            th1.Start();

        }

        private void uBtnClear_Click(object sender, EventArgs e)
        {
            this.progressBar1.Visible = false;
            this.progressBar1.Value = 0;
            this.uiListBoxCopd.Items.Clear();
            this.uiListBoxAmi.Items.Clear();
            uiListBoxApoplexy.Items.Clear();
            this.ucheckBoxAlignstr.Checked = false;
            this.uiCheckBoxCheckAMI.Checked = false;
            this.textBoxFilepath.Text = "";
            this.comboBoxSelectUser.Text = "选择姓名";
            this.uBtnStart.Symbol = 61515;
            this.uBtnStart.ForeColor = Color.FromArgb(39, 39, 39);
            this.uBtnStart.Text = "开始";
            ShowCounts(false);

        }

        private void LabelMouseEnter(object sender, EventArgs e)
        {
            try
            {
                Sunny.UI.UISymbolLabel label = (Sunny.UI.UISymbolLabel)sender;
                label.ForeColor = label.SymbolColor;
            }
            catch (Exception)
            {

                Console.WriteLine("转换失败");
            }
        }

        private void LabelMouseLeave(object sender, EventArgs e)
        {
            try
            {
                Sunny.UI.UISymbolLabel label = (Sunny.UI.UISymbolLabel)sender;
                label.ForeColor = Color.FromArgb(64, 64, 64);
            }
            catch (Exception)
            {
                Console.WriteLine("转换失败");
            }
        }
    }


}
