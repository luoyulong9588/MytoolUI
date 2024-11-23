using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.UIA3;



namespace MytoolUI
{
    public partial class FormMainUI : Form
    {

        /// <summary>
        /// 窗体动画函数（API声明）
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>
        /// <param name="dwTime">指定动画持续的时间</param>
        /// <param name="dwFlags">指定动画类型，可以是一个或多个标志的组合。</param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        //下面是可用的常量，根据不同的动画效果声明自己需要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果

        private UISymbolButton currentSymbolBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private bool istopMost = false;

        UIForm miniCaseForm = null;

        public FormMainUI(bool mini=false)
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            if (Directory.Exists(Application.StartupPath + "\\cache") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\cache");
            }
            this.SetBtnColor();
            
            if (mini) // step into mini mod;
            {
                this.uiSymbolButtonMini_Click();
                Thread th1 = new Thread(hideMainUI);
                th1.Start();
            }
            else
            {
                this.uBtnUserName_Click(uBtnUserName);
            }
        }
        private void hideMainUI()
        {
           
            Thread.Sleep(500);
            this.Visible = false;
        }


        //structs
        private struct RGBColors
        {
            public static Color @case = Color.FromArgb(24, 161, 251);
            public static Color outpatient = Color.FromArgb(156, 204, 101);  //253, 138, 114
            public static Color discharge = Color.FromArgb(172, 126, 241); //  249, 168, 37
            public static Color chronic = Color.FromArgb(249, 88, 155);
            public static Color setting = Color.FromArgb(249, 168, 37); //155, 75, 194  169, 109, 197
            public static Color bloodGas = Color.FromArgb(255, 127, 80);
            public static Color colorCustom = Color.FromArgb(22, 160, 133);
            public static Color colorTurmor = Color.FromArgb(230, 80, 80);
            public static Color colorInCard = Color.FromArgb(38, 198, 218);
        }

        private void SetBtnColor()
        {
             void ChangeBtnColor(UISymbolButton uBtn, Color color)
            {
                uBtn.ForePressColor = color;
                uBtn.ForeHoverColor = color;
                uBtn.ForeSelectedColor = color;
            }
            ChangeBtnColor(uBtnInHCard,RGBColors.colorInCard);
            ChangeBtnColor(uBtnCase,RGBColors.@case);
            ChangeBtnColor(uBtnBloodHistory,RGBColors.colorCustom);
            ChangeBtnColor(uBtnBloodGase,RGBColors.bloodGas);
            ChangeBtnColor(uBtnDischarge,RGBColors.discharge);
            ChangeBtnColor(uBtnTurmor,RGBColors.colorTurmor);  
            ChangeBtnColor(uBtnChronic,RGBColors.chronic);            
            ChangeBtnColor(uBtnOutpatient,RGBColors.outpatient);        
            ChangeBtnColor(uBtnSetting,RGBColors.setting);
        }


        private void FormMainUI_Load(object sender, EventArgs e)
        {
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            //this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 500, AW_SLIDE | AW_ACTIVE | AW_BLEND);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            SetDefaultUserName();

            helpProviderNew.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\config\\help.chm";
            helpProviderNew.SetShowHelp(this, true);
            
        }



        #region panel拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        //panel拖动窗体用
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 0.025)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer1.Stop();
                Application.Exit();
            }
        }
        private void SetDefaultUserName()
        {
            uBtnUserName.Text = new DatabaseUnit().GetuserName();
        }
        private void CleanCaceFiles()
        {
            notifyIconCustom.Visible = false;
            notifyIconCustom.Dispose();
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
                        Console.WriteLine(string.Format("缓存文件清除失败!,\r\n{0}", ex), "注意！");
                    }
            }
            catch (Exception ex)
            {

                Console.WriteLine("缓存文件清除失败" + ex);
            }

        }
        private void readDocxToCache()
        {
            string path = Application.StartupPath + "\\cache";
            if (System.IO.Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(path);
            }
            DatabaseUnit data = new DatabaseUnit();
            data.GetwordfromDb();
        }
        private void Rest()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            uBtnCurrentChildForm.Symbol = 361461;
            uBtnCurrentChildForm.SymbolColor = Color.PaleVioletRed;
            lbTitleChildForm.Text = "主页";
            //close、max、min button Color
            uiSymbolButtonC.ForeColor = Color.PaleVioletRed;
            uiSymbolButtonMax.ForeColor = Color.PaleVioletRed;
            uiSymbolButtonMin.ForeColor = Color.PaleVioletRed;
            uBtnTopMost.ForeColor = Color.PaleVioletRed;
            uiSymbolButtonMini.ForeColor = Color.PaleVioletRed;

            uBtnUserName.ForeHoverColor = Color.PaleVioletRed;
            uBtnUserName.ForePressColor = Color.PaleVioletRed;

            panelChildBloodGas.Visible = false;
            panelChildCase.Visible = false;
            panelChildTurmor.Visible = false;
            uBtnBloodHistory.ForeColor = Color.White;
        }
        private void OpenCHildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbTitleChildForm.Text = childForm.Text;
        }
        /// <summary>
        /// set clicked button custom color;hide panels;
        /// </summary>
        /// <param name="senderBtn"></param>
        /// <param name="color"></param>
        private void ActiveButton(object senderBtn, Color color)
        {
     
            if (senderBtn != null)
            {
                DisableButton();
                panelChildCase.Visible = false;
                panelChildBloodGas.Visible = false;
                panelChildTurmor.Visible = false;

                currentSymbolBtn = (UISymbolButton)senderBtn;

                currentSymbolBtn.FillColor = Color.FromArgb(48, 48, 70);
                currentSymbolBtn.ForeColor = color;
                currentSymbolBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentSymbolBtn.ImageAlign = ContentAlignment.MiddleRight;
                currentSymbolBtn.ForeHoverColor = color;
                currentSymbolBtn.ForePressColor = color;
                currentSymbolBtn.ForeSelectedColor = color;


                // left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentSymbolBtn.Location.Y);
                leftBorderBtn.Height = currentSymbolBtn.Size.Height;
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                uBtnCurrentChildForm.Symbol = currentSymbolBtn.Symbol;
                uBtnCurrentChildForm.SymbolColor = color;
                // panel color
                //panelTitleBar.BackColor = color;
                //close、max、min button Color
                uiSymbolButtonC.ForeColor = color;
                uiSymbolButtonMax.ForeColor = color;
                uiSymbolButtonMin.ForeColor = color;
                uBtnTopMost.ForeColor = color;
                uiSymbolButtonMini.ForeColor = color;

                uBtnUserName.ForeHoverColor = color;
                uBtnUserName.ForePressColor = color;

                uBtnBloodHistory.ForeColor = Color.White;
                uBtnInHCard.ForeColor = Color.White;
                uBtnTurmor.ForeColor = Color.White;


                // panel; blood gas
                

            }

        }
        private void DisableButton()
        {
            if (currentSymbolBtn != null)
            {
                currentSymbolBtn.FillColor = Color.FromArgb(51, 51, 76);
                currentSymbolBtn.ForeColor = Color.Gainsboro;
                currentSymbolBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        #region 右上角按键功能
        private void uiSymbolButtonC_Click(object sender, EventArgs e)
        {
            CleanCaceFiles();
            this.timer1.Start();
        }
        private void uiSymbolButtonMax_Click(object sender, EventArgs e)
        {
            SetWindowMax();
        }

        private void SetWindowMax()
        {
            if (WindowState == FormWindowState.Normal)
            {
                Console.WriteLine("Normal");
                WindowState = FormWindowState.Maximized;
                uiSymbolButtonMax.Symbol = 62160;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                uiSymbolButtonMax.Symbol = 62162;
            }

        }


        private void uiSymbolButtonMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void uBtnTopMost_Click(object sender, EventArgs e)
        {
            if (istopMost)
            {
                this.TopMost = false;
                this.istopMost = false;
                this.uBtnTopMost.Symbol = 57373;
            }
            else
            {
                this.TopMost = true;
                this.istopMost = true;
                this.uBtnTopMost.Symbol = 57374;
            }
        }
        #endregion

        #region 左侧按键功能
        private void uBtnCase_Click(object sender, EventArgs e=null)
        {
            OpenCHildForm(new CaseToolUI(this, RGBColors.@case));
            ActiveButton(sender, RGBColors.@case);
            panelChildCase.Location = new Point(0,176);
            panelChildCase.Visible = true;


        }
        private void uBtnBloodGase_Click(object sender, EventArgs e=null)
        {
            OpenCHildForm(new BloodGasUI(RGBColors.bloodGas));
            
            ActiveButton(sender, RGBColors.bloodGas);
            panelChildBloodGas.Visible = true;
            panelChildBloodGas.Location = new Point(0, 236);

            //uBtnBloodHistory.ForeColor = RGBColors.colorCustom;

        }

        private void uBtnOutpatient_Click(object sender, EventArgs e)
        {
            OpenCHildForm(new OutpatientUI(RGBColors.outpatient));
            ActiveButton(sender, RGBColors.outpatient);
        }

        private void uBtnChronic_Click(object sender, EventArgs e = null)
        {
            OpenCHildForm(new ChronicDiseasesUI(RGBColors.chronic, this.uBtnUserName.Text));
            ActiveButton(sender, RGBColors.chronic);
            panelChildTurmor.Location = new Point(0,356);
            panelChildTurmor.Visible = true;
            Thread th1 = new Thread(readDocxToCache);
            th1.Start();
        }

        private void uBtnDischarge_Click(object sender, EventArgs e)
        {
            OpenCHildForm(new DischargeFollowUpUI(RGBColors.discharge, this.uBtnUserName.Text));
            ActiveButton(sender, RGBColors.discharge);
        }

        private void uBtnSetting_Click(object sender, EventArgs e)
        {

            OpenCHildForm(new SettingUI(uBtnUserName, RGBColors.setting));
            ActiveButton(sender, RGBColors.setting);
            //this.panelChildSetting.Visible = true;
        }

        private void uBtnUserName_Click(object sender, EventArgs e=null)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            Rest();
        }

        private void uBtnBloodHistory_Click(object sender, EventArgs e)
        {
            OpenCHildForm(new BloodGasHistoryUI());
            DisableButton();


            panelChildBloodGas.Visible = true;
            leftBorderBtn.Location = new Point(0, panelChildBloodGas.Location.Y);
            leftBorderBtn.Height = panelChildBloodGas.Size.Height;
            leftBorderBtn.BackColor = RGBColors.colorCustom;

            uBtnBloodHistory.FillColor = Color.FromArgb(48, 48, 70);
            uBtnBloodHistory.ForeColor = RGBColors.colorCustom;
            uBtnBloodHistory.ImageAlign = ContentAlignment.MiddleRight;

            uiSymbolButtonC.ForeColor = RGBColors.colorCustom;
            uiSymbolButtonMax.ForeColor = RGBColors.colorCustom;
            uiSymbolButtonMin.ForeColor = RGBColors.colorCustom;
            uBtnTopMost.ForeColor = RGBColors.colorCustom;

            uBtnCurrentChildForm.Symbol = uBtnBloodHistory.Symbol;
            uBtnCurrentChildForm.SymbolColor = RGBColors.colorCustom;

            currentSymbolBtn = uBtnBloodHistory;



        }
        #endregion

        private void uBtnInHCard_Click(object sender, EventArgs e)
        {
            OpenCHildForm(new InHospitalCardUI(RGBColors.colorInCard));
            DisableButton();

            leftBorderBtn.Location = new Point(0, panelChildCase.Location.Y);
            leftBorderBtn.Height = panelChildCase.Size.Height;
            leftBorderBtn.BackColor = RGBColors.colorInCard;

            uBtnInHCard.FillColor = Color.FromArgb(48, 48, 70);
            uBtnInHCard.ForeColor = RGBColors.colorInCard;
            uBtnInHCard.ImageAlign = ContentAlignment.MiddleRight;
            uiSymbolButtonC.ForeColor = RGBColors.setting;
            uiSymbolButtonMax.ForeColor = RGBColors.colorInCard;
            uiSymbolButtonMin.ForeColor = RGBColors.colorInCard;
            uiSymbolButtonC.ForeColor = RGBColors.colorInCard;
            uBtnTopMost.ForeColor = RGBColors.colorInCard;
            uBtnCurrentChildForm.Symbol = uBtnInHCard.Symbol;
            uBtnCurrentChildForm.SymbolColor = RGBColors.colorInCard;
            currentSymbolBtn = uBtnInHCard;

        }

        private void uBtnTurmor_Click(object sender, EventArgs e)
        {
            OpenCHildForm(new TumorReportUI(RGBColors.colorTurmor, this.uBtnUserName.Text));
            DisableButton();

            leftBorderBtn.Location = new Point(0, panelChildTurmor.Location.Y);
            leftBorderBtn.Height = panelChildTurmor.Size.Height;
            leftBorderBtn.BackColor = RGBColors.colorTurmor;

            uBtnTurmor.FillColor = Color.FromArgb(48, 48, 70);
            uBtnTurmor.ForeColor = RGBColors.colorTurmor;
            uBtnTurmor.ImageAlign = ContentAlignment.MiddleRight;


            uiSymbolButtonC.ForeColor = RGBColors.colorTurmor;
            uiSymbolButtonMax.ForeColor = RGBColors.colorTurmor;
            uiSymbolButtonMin.ForeColor = RGBColors.colorTurmor;
            uBtnTopMost.ForeColor = RGBColors.colorTurmor;
            uBtnCurrentChildForm.Symbol = uBtnTurmor.Symbol;
            uBtnCurrentChildForm.SymbolColor = RGBColors.colorTurmor;

            currentSymbolBtn = uBtnTurmor;
        }


        private void menuMax_Click(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                changeMiniWindow(false);
            }
            SetWindowMax();
        }

        private void menuNormal_Click(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                changeMiniWindow(false);
            }

            WindowState = FormWindowState.Normal;
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            notifyIconCustom.Visible = false;
            this.Close();
        }

        private void notifyIconCustom_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// 进入mini模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiSymbolButtonMini_Click(object sender=null, EventArgs e=null)
        {
            
            changeMiniWindow(true);

        }

        /// <summary>
        /// 切换mini模式窗口
        /// </summary>
        /// <param name="create"></param>
        private void changeMiniWindow(bool create)
        {
            if (create) 
            { 
                this.Visible = false;
                miniCaseForm = new CaseUIMini(this);
                miniCaseForm.Visible = true;
                miniCaseForm.TopMost = true;
                miniCaseForm.Show();
            }
            else
            {
                this.Visible = true;
                if (miniCaseForm != null)
                {
                    miniCaseForm.Close();
                }
            }
        }

        private void changeMiniWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                changeMiniWindow(false);

            }
            else
            {
                changeMiniWindow(true);
            }

        }


    }
}
