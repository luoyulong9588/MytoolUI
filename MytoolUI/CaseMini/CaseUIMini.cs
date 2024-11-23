using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using MytoolUI.common;
using MytoolUI.CaseMini;
using FlaUI.Core.Input;
using System.IO;
using ExcelDataReader;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace MytoolUI
{
    public partial class CaseUIMini : UIForm
    {
        public Random rd = new Random();
        DatabaseForOutpatient dbInfo = new DatabaseForOutpatient();
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\address.db;Version=3;");
        private Pain pain = new Pain();
        private Form mainUI = null;
        public Bitmap reportCardImage;
        public Bitmap CaseImage;
        private UIStyle currentStyle = UIStyle.Red;
        private List<UIButton> uibuttons = new List<UIButton>();
        private DatabaseUnit db = new DatabaseUnit();
        UIIntegerUpDown uiIntergerUpDownReportsingleX = new UIIntegerUpDown();
        UIIntegerUpDown uiIntergerUpDownReportsingleY = new UIIntegerUpDown();
        UILabel labelReportX = new UILabel();
        UILabel labelReportY = new UILabel();
        UICheckBox isCycleCheckBox;
        UIButton btnStartOutPatient;
        UIButton btnInportFile;

        Font defaultFont = new Font("宋体", 9F, FontStyle.Regular);
        private object[] widgets = new object[] {
                new UITextBox(),
                new UITextBox(),
                new UITextBox(),
                new UITextBox(),
                new UITextBox(),
                new UITextBox(),
                new UITextBox(),
                new UICheckBox(),
                new UILabel(),
                new UITextBox(),
                new UITextBox()};
        private bool createSetting = true;
        private bool createNote = true;
        private bool createStr = true;
        private bool fillPage = true;
        private bool createOutPatient = true;


        private bool withContact = false;
        private bool withPermit = false;
        private bool isUnderLine = false;
        private bool isBold = false;
        private string currentColor = "red";
        private string currentNote = "please input some words.";
        private float reportX = 5;
        private float reportY = 19;
        private float caseX = 5;
        private float caseY = 16;
        private int x = 0;
        private int y = 0;

        public List<string> failedItem = new List<string> { };

        private bool processingOutPatient = false;
        Thread cycleThread;
        UIMessageForm message = new UIMessageForm();

        public CaseUIMini(Form parentUI = null)
        {

            this.mainUI = parentUI;
            this.mainUI.Visible = false;
            this.mainUI.ShowInTaskbar = false;
            this.ShowInTaskbar = false;
            this.currentColor = this.db.GetColorStyle();
            //this.currentNote = this.db.GetNoteContent();
            //this.uTextBox.Text = this.currentNote;
            try
            {
                uTextBox.LoadFile("config\\note.document");
            }
            catch
            {

            }

            uTextBox.Find("Text", RichTextBoxFinds.MatchCase);
            InitializeComponent();
            SetColor(this.currentColor);
            SetDesktopLocation();
            getAdjust("adjust");
            CreateStyleSelectBox();

        }

        // 设置窗口初始位置
        private void SetDesktopLocation()
        {
            int x = (SystemInformation.WorkingArea.Width / 2 - this.Size.Width) / 2;
            int y = (SystemInformation.WorkingArea.Height - this.Size.Height);
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (Point)new Size(x, 0);         //窗体的起始位置为(x,y)

        }

        // 恢复正常模式
        private void uBtnRestMini_Click(object sender, EventArgs e)
        {
            this.SaveChange();
            this.mainUI.Visible = true;
            this.mainUI.ShowInTaskbar = true;
            this.Dispose();
            this.Close();
        }

        
        /// <summary>
        /// 门诊首页填写功能面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uBtnOutPatientMini_Click(object sender, EventArgs e)
        {
            if (!this.createNote) // close note;
            {
                this.uBtnNote_Click(sender, e);
            }
            if (!this.createStr)
            {
                this.uBtnStrMini_Click(sender, e);
            }

            if (!createSetting)
            {
                uBtnSettingMini_Click(sender, e);
            }

            if (createOutPatient)
                    {
                SetWindowHeight(2);

                this.uiTableLayoutPanelDesktop.RowCount = 2;
                for (int i = 0; i < 2; i++)
                {
                    this.uiTableLayoutPanelDesktop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }
                isCycleCheckBox = CreateUiCheckBox("循环");
                btnStartOutPatient = CreateUiButton("开始");
                btnInportFile = CreateUiButton("导入");

                btnStartOutPatient.Click += new System.EventHandler(uiStartStopOutPatientClick);
                btnInportFile.Click += new System.EventHandler(btnInportFileClick);
                PadTableLayout(isCycleCheckBox, 1, 0, 1, 2);
                PadTableLayout(btnStartOutPatient, 1, 5, 1, 2);
                PadTableLayout(btnInportFile, 1, 3, 1, 2);
                this.createOutPatient = false;

            }
            else
            {

                uiTableLayoutPanelDesktop.Controls.Remove(isCycleCheckBox);
                uiTableLayoutPanelDesktop.Controls.Remove(btnStartOutPatient);
                uiTableLayoutPanelDesktop.Controls.Remove(btnInportFile);
                uiTableLayoutPanelDesktop.RowCount--;
                this.ClientSize = new Size(this.Size.Width, 30);
                this.createOutPatient = true;

            }

        }
        /// <summary>
        /// 门诊首页填写项目下的导入按钮功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInportFileClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    try
                    {
                        ReadXlsFile(openFileDialog);
                    }
                    catch (System.IO.IOException)
                    {

                        MessageBox.Show("文件正在被使用，请关闭使用该文件的程序后重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
        }

        private void ReadXlsFile(OpenFileDialog openFileDialog)
        {
            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                    });
                    DataTableCollection tableCollection = result.Tables;

                    DataTable dt = tableCollection[0];
                    StartFormatFile(dt);
                }
            }
            MessageBox.Show("文件读取完成！点击开始即可填写首页内容。mini模式不再保存文件，如需旧的门诊日志导出文件，请在正常模式下处理文件，会自动保存在<D:\\门诊日志>目录下以供查看。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void uiStartStopOutPatientClick(object sender, EventArgs e)
        {
            
            if (!processingOutPatient)
            {
                processingOutPatient = true;

                btnStartOutPatient.Text = "终止";
                CheckCycleForOutPatientProgress();
                
            }

            else
            {
                processingOutPatient = false;
                btnStartOutPatient.Text = "开始";
                cycleThread.Abort();
            }

        }

        private void CheckCycleForOutPatientProgress()
        {

            
            if (isCycleCheckBox.Checked)
            {
                cycleThread = new Thread(StartFill);
                cycleThread.Start();
                void StartFill()
                {
                    string lastName=null;
                    int count = 1;

                    while (isCycleCheckBox.Checked)
                    {
                        if (count%3==0)
                        {
                            lastName = new UIAutoForOutPatient().GetName();
                        }

                        if (new UIAutoForOutPatient().GetName() == lastName)
                        {
                            MessageBox.Show("All finished!");
                            break;
                        }

                        UIAuto uIAuto = new UIAuto();
                        uIAuto.OutPaient();
                        if (uIAuto.failedString !=null)
                        {
                            failedItem.Append(uIAuto.failedString);
                        }

                        Thread.Sleep(3000);
                        Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.DOWN);
                        Thread.Sleep(1000);

                        count++;
                    }
                
                }
            }
            else
            {
                startOutPatientFill();
                btnStartOutPatient.Text = "开始";
            }
            

        }


        private void startOutPatientFill()
        {
                Thread thread = new Thread(StartFill);
                thread.Start();
                void StartFill()
                {
                    new UIAuto().OutPaient();
                }
        }


        /*  这里暂时注释掉病案质量报告，现在无需该功能；  Thread thread = new Thread(StartCase);
        void StartCase()
        {
            try
            {

                pain = new UIAuto(widgets).CaseAuto();
            }
            catch (Exception ex)
            {

                this.message.ShowErrorDialog("查找窗体错误!", ex.ToString());
            }

            if (pain == null)
            {
                return;
            }
            DrawImage image = CaseToolUI.DrawCase(pain, caseX, caseY);
            this.CaseImage = image.image;
            CaseToolUI.PrintImage(printDocumentCase);
        }
        */


        private void uBtnReportMini_Click(object sender, EventArgs e)
        {
            pain = null;
            DrawImage image;
            Thread thread = new Thread(StartReportCard);
            thread.Start();
            void StartReportCard()
            {
                try
                {
                    pain = new UIAuto(widgets).CaseAuto();
                }
                catch (Exception ex)
                {
                    message.ShowErrorDialog("查找窗体错误!", ex.ToString());
                }

                if (pain == null)
                {
                    return;
                }

                if (this.uiCheckcBoxSingleLine.Checked)
                {
                    image = CaseToolUI.DrawReportCard(pain, float.Parse(uiIntergerUpDownReportsingleX.Value.ToString()), float.Parse(uiIntergerUpDownReportsingleY.Value.ToString()));
                }
                else
                {
                    image = CaseToolUI.DrawReportCardNew(pain, reportX, reportY);
                }

                this.reportCardImage = image.image;
                CaseToolUI.PrintImage(printDocumentReport);
            }

        }
        /// <summary>
        /// 流行病学调查表的打印功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uBtnInvestigationMini_Click(object sender, EventArgs e)
        {
            Thread th1 = new Thread(StartInvetigation);
            th1.Start();
            /// <summary>
            /// 开启多线程，开始从zlbh获取信息，填入到流行病学调查表word中
            /// </summary>
            void StartInvetigation()
            {
                Pain pain0 = null;
                Pain painContact = null;
                try
                {
                    pain0 = new UIAuto(widgets).InvestigationAuto(fillPage);
                }
                catch (Exception ex)
                {
                    message.ShowErrorDialog("信息读取错误！", ex.Message);
                    return;
                }

                if (pain0 == null)
                {
                    message.ShowErrorDialog("未打开中联bh,终止线程..");
                    return;
                }


                if (this.withContact)
                {

                    //bool select = message.ShowAskDialog( $"{pain0.Name} 是否伴随家属信息?","是: 请打开门诊工作站，选中家属姓名，再点击继续\r\n否：请点击取消",UIStyle.Red,false);
                    DialogResult select = MessageBox.Show("是: 请打开门诊工作站，选中家属姓名，再点击继续\r\n否：请点击取消", $"【{pain0.Name}】是否伴随家属信息?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                }
                //new Investigation().startProgram(pain0, painContact, withPermit); disabled fill word;
            }
        }


        /// <summary>
        /// 设置按钮扩展页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uBtnSettingMini_Click(object sender, EventArgs e)
        {

            if (!this.createNote) // close note;
            {
                this.uBtnNote_Click(sender, e);
            }
            if (!this.createStr)
            {
                this.uBtnStrMini_Click(sender, e);
            }
           if (!createOutPatient)
            {
             this.uBtnOutPatientMini_Click(sender, e);
            }
            if (this.createSetting)
            {
                if (this.uiCheckcBoxSingleLine.Checked)
                {
                    SetWindowHeight(3);
                }
                else
                {
                    SetWindowHeight(2);
                }

                this.uiTableLayoutPanelDesktop.RowCount = 2;
                for (int i = 0; i < 2; i++)
                {
                    this.uiTableLayoutPanelDesktop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }
                this.uiCheckBoxfill = CreateUiCheckBox("首页");
                this.uiCheckcBoxContact = CreateUiCheckBox("家属");
                this.uiCheckcBoxSingleLine = CreateUiCheckBox("单行");

                this.uiCheckcBoxSingleLine.CheckedChanged += new System.EventHandler(this.uiCheckcBoxSingleLineChange);


                this.uiCheckBoxfill.Checked = this.fillPage;
                this.uiCheckcBoxContact.Checked = this.withContact;
                this.uiCheckcBoxSingleLine.Checked = this.withPermit;

                this.uiCheckBoxfill.CheckedChanged += new System.EventHandler(this.CheckedChange);
                this.uiCheckcBoxContact.CheckedChanged += new System.EventHandler(this.CheckedChange);
                this.uiCheckcBoxSingleLine.CheckedChanged += new System.EventHandler(this.CheckedChange);


                this.uiToolTipMini.SetToolTip(this.uiCheckcBoxContact, "包含联系人信息(需确定后打开门诊工作站)");
                this.uiToolTipMini.SetToolTip(this.uiCheckBoxfill, "自动补充首页缺失项");
                this.uiToolTipMini.SetToolTip(this.uiCheckcBoxSingleLine, "感染病例报告卡-单行");

                PadTableLayout(uiCheckBoxfill, 1, 0, 1, 2);
                PadTableLayout(uiCheckcBoxContact, 1, 1, 1, 2);
                PadTableLayout(uiCheckcBoxSingleLine, 1, 2, 1, 2);
                uCbtn.Visible = true;
                this.uiToolTipMini.SetToolTip(this.uCbtn, "切换主题颜色,通过下拉选取");
                uiTableLayoutPanelDesktop.Controls.Add(uCbtn);
                uiTableLayoutPanelDesktop.SetRow(uCbtn, 1);
                uiTableLayoutPanelDesktop.SetColumn(uCbtn, 5);
                uiTableLayoutPanelDesktop.SetColumnSpan(uCbtn, 2);
                AdjustRow();


                this.createSetting = false;

            }
            else
            {
                uiCheckBoxfill.Visible = false;
                uiCheckcBoxContact.Visible = false;
                uiCheckcBoxSingleLine.Visible = false;
                uCbtn.Visible = false;
                uiTableLayoutPanelDesktop.RowStyles.RemoveAt(1);
                uiTableLayoutPanelDesktop.RowCount--;

                this.ClientSize = new Size(this.Size.Width, 30);

                if (this.uiCheckcBoxSingleLine.Checked)
                {
                    uiTableLayoutPanelDesktop.Controls.Remove(uiIntergerUpDownReportsingleX);
                    uiTableLayoutPanelDesktop.Controls.Remove(uiIntergerUpDownReportsingleY);
                    uiTableLayoutPanelDesktop.Controls.Remove(labelReportX);
                    uiTableLayoutPanelDesktop.Controls.Remove(labelReportY);
                    uiTableLayoutPanelDesktop.RowCount = 1;
                }
                this.createSetting = true;

            }

        }
        
        // 关闭按钮功能，再次保存主题和note内容
        private void uiSymbolButtonCloseMini_Click(object sender, EventArgs e)
        {

            this.SaveChange();
            this.Close();
            this.mainUI.Close();

        }

        // 记事小本功能
        private void uBtnNote_Click(object sender, EventArgs e)
        {
            if (!this.createSetting) // close setting button;
            {
                this.uBtnSettingMini_Click(sender, e);
            }
            if (!this.createStr)
            {
                this.uBtnStrMini_Click(sender, e);
            }
            if (!createOutPatient)
            {
                this.uBtnOutPatientMini_Click(sender, e);
            }
            if (this.createNote)
            {
                this.ClientSize = new Size(this.Size.Width, 30 + 30 + 30 * 5);
                this.uiTableLayoutPanelDesktop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                this.uiTableLayoutPanelDesktop.RowCount = 7;
                for (int i = 0; i < 5; i++)
                {
                    this.uiTableLayoutPanelDesktop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }
                //this.ubtnFontBold = Cr();
                this.ubtnFontSelect = CreateUiButton("\ue656", 14);
                this.ubtnFontUpper = CreateUiButton("\uea33", 14);
                this.ubtnFontDown = CreateUiButton("\uea32", 14);
                this.ubtnFontBold = CreateUiButton("\ue64e", 11);
                this.ubtnUnderLine = CreateUiButton("\ue74b", 11);
                this.ubtnItalic = CreateUiButton("\ue6c3", 11);
                this.ubtnClean = CreateUiButton("\ue900", 13);

                // buttons tips;
                this.uiToolTipMini.SetToolTip(this.ubtnFontSelect, "设置字体");
                this.uiToolTipMini.SetToolTip(this.ubtnFontUpper, "字号+");
                this.uiToolTipMini.SetToolTip(this.ubtnFontDown, "字号-");
                this.uiToolTipMini.SetToolTip(this.ubtnFontBold, "粗体");
                this.uiToolTipMini.SetToolTip(this.ubtnUnderLine, "下划线");
                this.uiToolTipMini.SetToolTip(this.ubtnClean, "清除信息");


                // pad buttons;
                PadTableLayout(ubtnFontSelect, 1, 0);
                PadTableLayout(ubtnFontUpper, 1, 1);
                PadTableLayout(ubtnFontDown, 1, 2);
                PadTableLayout(ubtnFontBold, 1, 3);
                PadTableLayout(ubtnUnderLine, 1, 4);
                PadTableLayout(ubtnItalic, 1, 5);
                PadTableLayout(ubtnClean, 1, 6);

                uTextBox.Dock = DockStyle.Fill;
                uTextBox.Style = this.currentStyle;
                uTextBox.Margin = new Padding(1, 2, 1, 2);
                uTextBox.RadiusSides = UICornerRadiusSides.All;
                uTextBox.Radius = 5;
                uTextBox.BackColor = Color.White;
                uTextBox.RectColor = Color.Gray;


                uiTableLayoutPanelDesktop.Controls.Add(uTextBox);
                uiTableLayoutPanelDesktop.SetRow(uTextBox, 2);
                uiTableLayoutPanelDesktop.SetRowSpan(uTextBox, 5);
                uiTableLayoutPanelDesktop.SetColumn(uTextBox, 0);
                uiTableLayoutPanelDesktop.SetColumnSpan(uTextBox, 8);
                // function;

                this.ubtnFontSelect.Click += new EventHandler(this.ChangeFont);
                this.ubtnFontUpper.Click += new EventHandler(this.FontUpper);
                this.ubtnFontDown.Click += new EventHandler(this.FontDown);
                this.ubtnFontBold.Click += new EventHandler(this.FontBold);
                this.ubtnUnderLine.Click += new EventHandler(this.FontUnderLine);
                this.ubtnClean.Click += new EventHandler(this.ResetNote);
                this.ubtnItalic.Click += new EventHandler(this.FontItalic);
                this.uTextBox.Visible = true;
                this.createNote = false;
            }
            else
            {
                //this.defaultFont = this.uTextBox.Font;
                SaveChange();
                uTextBox.Visible = false;
                this.ubtnFontSelect.Visible = false;
                this.ubtnFontUpper.Visible = false;
                this.ubtnFontDown.Visible = false;
                this.ubtnFontBold.Visible = false;
                this.ubtnUnderLine.Visible = false;
                this.ubtnItalic.Visible = false;
                this.ubtnClean.Visible = false;
                this.ubtnItalic.Visible = false;

                uiTableLayoutPanelDesktop.RowStyles.RemoveAt(1);
                uiTableLayoutPanelDesktop.RowStyles.RemoveAt(2);
                uiTableLayoutPanelDesktop.RowCount = 1;
                this.ClientSize = new Size(this.Size.Width, 30);
                this.createNote = true;

            }
        }

        // 出院、带药格式化
        private void uBtnStrMini_Click(object sender, EventArgs e)
        {
            if (!this.createSetting) // close setting button;
            {
                this.uBtnSettingMini_Click(sender, e);
            }
            if (!this.createNote)
            {
                this.uBtnNote_Click(sender, e);
            }
            if (!createOutPatient)
            {
                this.uBtnOutPatientMini_Click(sender, e);
            }
            if (this.createStr)
            {
                this.ClientSize = new Size(this.Size.Width, 30 + 30 + 30 * 5);
                this.uiTableLayoutPanelDesktop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                for (int i = 0; i < 5; i++)
                {
                    this.uiTableLayoutPanelDesktop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }
                this.uiTableLayoutPanelDesktop.RowCount = 7;

                this.ubtnBuildCase = CreateUiButton("\ue601", 12);
                this.ubtnLaboratory = CreateUiButton("\ue60b", 12);
                this.ubtnClearCase = CreateUiButton("\ue900", 15);
                this.ubtnCopyCase = CreateUiButton("\ue600", 14);
                this.ubtnDrug = CreateUiButton("\ue632", 12);
                // buttons tips;
                this.uiToolTipMini.SetToolTip(this.ubtnBuildCase, "生成出院记录");
                this.uiToolTipMini.SetToolTip(this.ubtnClearCase, "清除所有内容");
                this.uiToolTipMini.SetToolTip(this.ubtnCopyCase, "复制到剪切板");
                this.uiToolTipMini.SetToolTip(this.ubtnLaboratory, "检验结果格式化");
                this.uiToolTipMini.SetToolTip(this.ubtnDrug, "药物格式化");

                // pad buttons;
                PadTableLayout(ubtnBuildCase, 1, 0);
                PadTableLayout(ubtnLaboratory, 1, 1);
                PadTableLayout(ubtnDrug, 1, 2);
                PadTableLayout(ubtnCopyCase, 1, 3);
                PadTableLayout(ubtnClearCase, 1, 4);

                // btn function
                this.ubtnBuildCase.Click += UbtnBuildCase_Click;
                this.ubtnCopyCase.Click += UbtnCopyCase_Click;
                this.ubtnClearCase.Click += UbtnClean_Click;
                this.ubtnDrug.Click += UbtnDrug_Click;
                this.ubtnLaboratory.Click += UbtnLaboratory_Click;



                uTextBoxCase.Dock = DockStyle.Fill;
                uTextBoxCase.Font = this.defaultFont;
                uTextBoxCase.Style = this.currentStyle;
                uTextBoxCase.Margin = new Padding(1, 2, 0, 2);

                uTextBoxCase.RadiusSides = UICornerRadiusSides.LeftBottom;
                uTextBoxCase.RadiusSides = UICornerRadiusSides.LeftTop;

                uTextBoxCase.Radius = 5;
                uTextBoxCase.BackColor = Color.White;
                uTextBoxCase.RectColor = Color.Gray;
                uTextBoxCase.Font = new Font("宋体", 9F);

                uTextBoxResult.Dock = DockStyle.Fill;
                uTextBoxResult.Font = this.defaultFont;
                uTextBoxResult.Style = this.currentStyle;
                uTextBoxResult.Margin = new Padding(0, 2, 1, 2);
                uTextBoxResult.RadiusSides = UICornerRadiusSides.RightBottom;
                uTextBoxResult.RadiusSides = UICornerRadiusSides.RightTop;


                uTextBoxResult.Radius = 5;
                uTextBoxResult.BackColor = Color.White;
                uTextBoxResult.RectColor = Color.Gray;
                uTextBoxResult.RectSides = ToolStripStatusLabelBorderSides.Top;
                uTextBoxResult.RectSides = ToolStripStatusLabelBorderSides.Right;
                uTextBoxResult.RectSides = ToolStripStatusLabelBorderSides.Bottom;


                uTextBoxResult.Font = new Font("宋体", 9F);

                this.uTextBoxCase.Visible = true;
                this.uTextBoxResult.Visible = true;


                uiTableLayoutPanelDesktop.Controls.Add(uTextBoxCase);
                uiTableLayoutPanelDesktop.Controls.Add(uTextBoxResult);

                uiTableLayoutPanelDesktop.SetRow(uTextBoxCase, 2);
                uiTableLayoutPanelDesktop.SetRow(uTextBoxResult, 2);

                uiTableLayoutPanelDesktop.SetRowSpan(uTextBoxCase, 5);
                uiTableLayoutPanelDesktop.SetRowSpan(uTextBoxResult, 5);

                uiTableLayoutPanelDesktop.SetColumn(uTextBoxCase, 0);
                uiTableLayoutPanelDesktop.SetColumn(uTextBoxResult, 4);

                uiTableLayoutPanelDesktop.SetColumnSpan(uTextBoxCase, 4);
                uiTableLayoutPanelDesktop.SetColumnSpan(uTextBoxResult, 4);

                this.createStr = false;
            }
            else
            {
                this.ubtnBuildCase.Visible = false;
                this.ubtnLaboratory.Visible = false;
                this.ubtnClearCase.Visible = false;
                this.ubtnCopyCase.Visible = false;
                this.ubtnDrug.Visible = false;
                this.uTextBoxCase.Visible = false;
                this.uTextBoxResult.Visible = false;
                uiTableLayoutPanelDesktop.RowStyles.RemoveAt(1);
                uiTableLayoutPanelDesktop.RowStyles.RemoveAt(2);
                uiTableLayoutPanelDesktop.RowCount = 1;
                this.ClientSize = new Size(this.Size.Width, 30);
                this.createStr = true;
            }

        }

        private void UbtnLaboratory_Click(object sender, EventArgs e)
        {
            string all = uTextBoxCase.Text;
            all = all.Replace("10*", "10×");
            uTextBoxResult.Text = all.Replace("*", "");
        }

        // 药物格式化功能
        private void UbtnDrug_Click(object sender, EventArgs e)
        {
            DrugFormat app = new DrugFormat();
            this.uTextBoxResult.Text = app.Start(this.uTextBoxCase.Text);
        }

        // 格式化工具下属清除功能
        private void UbtnClean_Click(object sender, EventArgs e)
        {
            this.uTextBoxCase.Text = "Past your case history here.";
            this.uTextBoxResult.Text = "";
            Clipboard.Clear();

        }
        // 复制内容到剪切板
        private void UbtnCopyCase_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.uTextBoxResult.Text);
            message.ShowInfoDialog("已成功复制到剪切板！", false);
        }
        // 生产出院记录
        private void UbtnBuildCase_Click(object sender, EventArgs e)
        {
            try
            {
                BuildDischargeCase app = new BuildDischargeCase();
                this.uTextBoxResult.Text = app.Start(this.uTextBoxCase.Text);
            }
            catch (Exception ex)
            {
                message.ShowErrorDialog("there are something wrong with buliding case record.");
                this.uTextBoxResult.Text = ex.ToString();
            }
        }

        
    }
}
