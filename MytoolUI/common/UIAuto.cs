using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using MytoolUI.common;
using Sunny.UI;


namespace MytoolUI
{
    public class UIAuto
    {
        delegate void SetTextCallback(string text);
        private string userName;
        private Pain pain;
        private bool breakProcess = false;
        private ConditionFactory cf;
        private UITextBox uiTextBoxName;
        private UITextBox uiTextBoxGender;
        private UITextBox uiTextBoxAge;
        private UITextBox uiTextBoxId;
        private UITextBox uiTextBoxIdcard;
        private UITextBox uiTextBoxPhone;
        private UITextBox uiTextBoxAddress;
        private UITextBox uiTextBoxMainDiagnose;
        private UITextBox uiTextBoxDoctorName;
        private UICheckBox uiCheckBoxFill;
        private UILabel uiLabelOutPut;
        private AutomationElement ensureButton;
        private AutomationElement cancelButton;
        private DatabaseForOutpatient infoDb = new DatabaseForOutpatient();
        UIMessageForm message = new UIMessageForm();
        AutomationElement[] parentElements;
        private Window window;
        private string processName = "zlbh.exe";
        public string  failedString = null;
        public UIAuto(object[] widgets = null)
        {
            if (widgets != null)
            {
                uiTextBoxName = (UITextBox)widgets[0];
                uiTextBoxGender = (UITextBox)widgets[1];
                uiTextBoxAge = (UITextBox)widgets[2];
                uiTextBoxId = (UITextBox)widgets[3];
                uiTextBoxIdcard = (UITextBox)widgets[4];
                uiTextBoxPhone = (UITextBox)widgets[5];
                uiTextBoxAddress = (UITextBox)widgets[6];
                uiCheckBoxFill = (UICheckBox)widgets[7];
                uiLabelOutPut = (UILabel)widgets[8];
                uiTextBoxMainDiagnose = (UITextBox)widgets[9];
                uiTextBoxDoctorName = (UITextBox)widgets[10];
            }
            try
            {
                userName = new DatabaseUnit().GetuserName();
                Console.WriteLine(userName);
                

            }
            catch (Exception ex)
            {
                this.message.ShowErrorDialog("数据库读取错误!", "未找到用户名！\r" + ex.ToString());
            }
            pain = new Pain
            {
                DoctorName = userName
            };
            cf = new ConditionFactory(new UIA3PropertyLibrary());
            try
            {
                window = FlaUI.Core.Application.Attach(processName).GetMainWindow(new UIA3Automation(), new TimeSpan(0, 0, 3));
            }
            catch (Exception ex)
            {

                this.message.ShowInfoDialog("未打开中联Bh 或 开启多个Bh！请检查后重试！\n" + ex.ToString());
                breakProcess = true;
                return;
            }

        }

        #region 调用委托线程对主线程的窗体赋值
        private void SetName(string text)
        {
            if (this.uiTextBoxName.InvokeRequired)
            {
                UIAuto.SetTextCallback d = new UIAuto.SetTextCallback(SetName);
                this.uiTextBoxName.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxName.Text = text;
            }

        }
        private void SetGender(string text)
        {
            if (this.uiTextBoxGender.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetGender);
                this.uiTextBoxGender.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxGender.Text = text;
            }
        }
        private void SetAge(string text)
        {
            if (this.uiTextBoxAge.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetAge);
                this.uiTextBoxAge.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxAge.Text = text;
            }
        }
        private void SetId(string text)
        {
            if (this.uiTextBoxId.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetId);
                this.uiTextBoxId.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxId.Text = text;
            }
        }
        private void SetIdcard(string text)
        {
            if (this.uiTextBoxIdcard.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetIdcard);
                this.uiTextBoxIdcard.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxIdcard.Text = text;
            }
        }
        private void SetPhone(string text)
        {
            if (this.uiTextBoxPhone.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetPhone);
                this.uiTextBoxPhone.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxPhone.Text = text;
            }
        }
        private void SetAddress(string text)
        {
            if (this.uiTextBoxAddress.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetAddress);
                this.uiTextBoxAddress.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxAddress.Text = text;
            }
        }
        private void OutPut(string text)
        {
            if (this.uiLabelOutPut.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(OutPut);
                this.uiLabelOutPut.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiLabelOutPut.Text = text;
            }
        }
        private void SetMainDiagnose(string text)
        {
            if (this.uiTextBoxMainDiagnose.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetMainDiagnose);
                this.uiTextBoxMainDiagnose.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxMainDiagnose.Text = text;
            }
        }
        private void SetDoctorName(string text)
        {
            if (this.uiTextBoxDoctorName.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDoctorName);
                this.uiTextBoxDoctorName.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiTextBoxDoctorName.Text = text;
            }
        }
        #endregion

        /// <summary>
        /// 流行病学调查表的自动化，点开首页、获取信息;
        /// </summary>
        /// <param name="widgets">需要动态赋值的控件数组</param>
        /// <returns>患者信息类</returns>
        public Pain InvestigationAuto(bool fillPage = false)
        {
            if (breakProcess)
            {
                return null;
            }
            window.SetForeground();

            #region 常量赋值
            const int CONST_AGE = 0;
            const int CONST_IDCARD = 2;
            const int CONST_PAINID = 3;
            const int CONST_MEDICAL_INSURANCE = 6;

            const int CONST_NAME = 7;
            const int CONST_GENDER = 8;
            //const int CONST_BIRTHDAY = 9;
            //const int CONST_MARRAGE = 10;
            const int CONST_VOCATION = 11;
            //const int CONST_MINGZU = 13;
            const int CONST_ADDR_JIGUAN = 14;
            const int CONST_ADDR_BORN = 15;
            const int CONST_ADDR_HUKOU = 16;
            const int CONST_ADDR_WORK = 19;
            const int CONST_ADDR_NOW = 39;
            const int CONST_PHONE_HUKOU = 17;
            const int CONST_PHONE_WORK = 20;
            const int CONST_PHONE_NOW = 40;
            const int CONST_RELATION_NAME = 22;
            const int CONST_RELATION_ADDR = 24;
            const int CONST_RELATION_PHONE = 37;
            const int CONST_RELATIONSHIP = 23;// 本人或户主
            const int CONST_MAIL_HUKOU = 18;
            const int CONST_MAIL_WORK = 21;
            const int CONST_MAIL_NOW = 41;
            const int CONST_TIME_RY = 25;
            #endregion

            #region 变量赋值
            string addr_jiguan;
            string addr_hukou;
            string addr_born;
            string addr_work;
            string addr_now;
            string addr_relation;

            string phone_hukou;
            string phone_work;
            string phone_now;
            string phone_relation;

            string mail_work;
            string mail_now;
            string mail_hukou;
            #endregion

            /*string userName = this.GetUserName();
            if (userName.Contains("莉"))
            {
                return null;
            }*/
            try
            {
                parentElements = FindParentElements();
            }
            catch (Exception ex)
            {
                this.message.ShowInfoDialog("未找到首页按钮，可能的原因是没有打开住院医生工作站。\r\n" + ex);
                return null;
            }
            this.OutPut("获取姓名..");
            pain.Name = GetText(CONST_NAME);
            this.SetName(pain.Name);

            this.OutPut("获取年龄..");
            pain.Age = GetText(CONST_AGE);
            this.SetAge(pain.Age);

            this.OutPut("获取性别..");
            pain.Gender = GetText(CONST_GENDER);
            this.SetGender(pain.Gender);

            this.OutPut("获取住院Id..");
            pain.Id = GetText(CONST_PAINID);
            this.SetId(pain.Id);

            this.OutPut("获取身份证号码..");
            pain.IdCardNumber = GetText(CONST_IDCARD);
            this.SetIdcard(pain.IdCardNumber);

            this.OutPut("获取入院时间..");
            pain.InDay = GetText(CONST_TIME_RY).Split(' ')[0];

            this.OutPut("获取电话号码..");
            pain.Phone = GetPhone();
            this.SetPhone(pain.Phone);

            this.OutPut("获取住址..");
            pain.HomeAddr = GetAddress();
            this.SetAddress(pain.HomeAddr);

            this.OutPut("获取职业..");
            string vocation = GetText(CONST_VOCATION);

            string medicalInsurance = GetText(CONST_MEDICAL_INSURANCE);

            if (uiCheckBoxFill.Checked || fillPage)
            {
                this.OutPut("准备补充空白..");
                //Thread thread = new Thread(FillinBlanks);   // 取消多线程填空测试,避免后面被阻塞;
                //thread.Start();
                FillinBlanks();
            }
            else
            {
                //cancelButton.Click(); disabled cancel button click.,2023.02.05
            }
            return pain;

            void FillinBlanks()
            {
                // 无论籍贯里面填写的什么，都清除重新填写；
                string districName = "重庆市綦江区";
                string mail = GetMail();
                AddressData db = new AddressData();
                var result = db.GetDistrictName(pain.IdCardNumber);
                if (result != null)
                {
                    districName = result;
                }
                else
                {
                    this.message.ShowInfoDialog("行政区域数据库内未检索到身份证信息所对应的地区码,默认以'重庆市綦江区'填入，请核对后修改！");
                }
                GetObject(CONST_ADDR_JIGUAN).AsTextBox().Enter(districName);


                if (addr_hukou.Length < 3) { GetObject(CONST_ADDR_HUKOU).AsTextBox().Enter(pain.HomeAddr); }
                if (addr_born.Length < 3) { GetObject(CONST_ADDR_BORN).AsTextBox().Enter(pain.HomeAddr); }


                if (vocation.Contains("农民") ||
                    vocation.Contains("其他") ||
                    vocation.Contains("无业") ||
                    vocation.Contains("不便分类") ||
                    vocation.Contains("自由职业") ||
                    vocation.Contains("无职业") ||
                    vocation.Contains("待业") ||
                    vocation.Contains("学生") ||
                    medicalInsurance.Contains("居民") ||
                    medicalInsurance.Contains("农村") ||
                    medicalInsurance.Contains("贫困")
                    )
                {
                    GetObject(CONST_ADDR_WORK).AsTextBox().Enter("无");
                    GetObject(CONST_PHONE_WORK).AsTextBox().Enter("");
                    GetObject(CONST_MAIL_WORK).AsTextBox().Enter("");
                }
                else // 判断工人
                {

                    GetObject(CONST_PHONE_WORK).AsTextBox().Enter(pain.Phone);
                    GetObject(CONST_MAIL_WORK).AsTextBox().Enter(mail);
                    if (addr_work.Length < 3 || addr_work == addr_born)
                    {
                        this.message.ShowErrorDialog($"患者可能为工人,首页职业信息为{vocation},但工作地址不正确，请注意手动更新！");
                    }
                }


                /*if (addr_work.Length < 3 || addr_work == addr_born)
                {
                    GetObject(CONST_ADDR_WORK).AsTextBox().Enter("无");
                    GetObject(CONST_PHONE_WORK).AsTextBox().Enter("");
                    GetObject(CONST_MAIL_WORK).AsTextBox().Enter("");
                }
                else // 工作相关的信息必须要在判断存在工作地址时填写，否则全部赋值为空;
                {
                    
                    GetObject(CONST_PHONE_WORK).AsTextBox().Enter(pain.Phone);
                    GetObject(CONST_MAIL_WORK).AsTextBox().Enter(mail);

                }*/
                //GetObject(CONST_PHONE_WORK).AsTextBox().Enter("");
                if (addr_now.Length < 3) { GetObject(CONST_ADDR_NOW).AsTextBox().Enter(pain.HomeAddr); }
                if (addr_relation.Length < 3) { GetObject(CONST_RELATION_ADDR).AsTextBox().Enter(pain.HomeAddr); }

                if (phone_hukou.Length < 5) { GetObject(CONST_PHONE_HUKOU).AsTextBox().Enter(pain.Phone); }
                //if (phone_work.Length < 5 && addr_work.Length>=3) 
                if (phone_now.Length < 5) { GetObject(CONST_PHONE_NOW).AsTextBox().Enter(pain.Phone); }
                if (phone_relation.Length < 5) { GetObject(CONST_RELATION_PHONE).AsTextBox().Enter(pain.Phone); }


                //if (mail_work.Length < 5 && addr_work.Length >= 3) { GetObject(CONST_MAIL_WORK).AsTextBox().Enter(mail); }
                if (mail_now.Length < 5) { GetObject(CONST_MAIL_NOW).AsTextBox().Enter(mail); }
                if (mail_hukou.Length < 5) { GetObject(CONST_MAIL_HUKOU).AsTextBox().Enter(mail); }

                if (GetText(CONST_RELATIONSHIP).Length < 1)
                {
                    GetObject(CONST_RELATIONSHIP).AsTextBox().Enter("本人或户主");
                    Thread.Sleep(50);
                    SendKeys.SendWait("{ENTER}");
                }
                if (GetText(CONST_RELATION_NAME).Length < 2)
                {
                    GetObject(CONST_RELATION_NAME).AsTextBox().Enter(pain.Name);
                }
                //ensureButton.Click();  disable click,2023.02.05
            }

            string GetPhone()
            {
                phone_hukou = GetText(CONST_PHONE_HUKOU);
                phone_work = GetText(CONST_PHONE_WORK);
                phone_now = GetText(CONST_PHONE_NOW);
                phone_relation = GetText(CONST_RELATION_PHONE);
                phone_hukou = phone_hukou ?? "";
                phone_work = phone_work ?? "";
                phone_now = phone_now ?? "";
                phone_relation = phone_relation ?? "";

                string[] phoneAll = new string[4] { phone_hukou, phone_work, phone_now, phone_relation };
                phoneAll = phoneAll.OrderByDescending(s => s.Length).ToArray();
                return phoneAll[0];
            }

            string GetAddress()
            {
                addr_jiguan = GetText(CONST_ADDR_JIGUAN);
                addr_hukou = GetText(CONST_ADDR_HUKOU);
                addr_born = GetText(CONST_ADDR_BORN);
                addr_work = GetText(CONST_ADDR_WORK);
                addr_now = GetText(CONST_ADDR_NOW);
                addr_relation = GetText(CONST_RELATION_ADDR);

                addr_jiguan = addr_jiguan ?? "";
                addr_hukou = addr_hukou ?? "";
                addr_born = addr_born ?? "";
                addr_work = addr_work ?? "";
                addr_now = addr_now ?? "";
                addr_relation = addr_relation ?? "";

                string[] addressAll = new string[6] { addr_jiguan, addr_hukou, addr_born, addr_work, addr_now, addr_relation };
                addressAll = addressAll.OrderByDescending(s => s.Length).ToArray();
                if (addressAll[0].Length < 3)
                {
                    return "重庆市綦江区文龙街道";
                }
                return addressAll[0];
            }

            string GetMail()
            {
                mail_work = GetText(CONST_MAIL_WORK);
                mail_now = GetText(CONST_MAIL_NOW);
                mail_hukou = GetText(CONST_MAIL_HUKOU);
                mail_work = mail_work ?? "";
                mail_now = mail_now ?? "";
                mail_hukou = mail_hukou ?? "";

                string[] mailAll = new string[3] { mail_hukou, mail_work, mail_now };
                mailAll = mailAll.OrderByDescending(s => s.Length).ToArray();
                if (mailAll[0].Length < 3)
                {
                    return "401420";
                }
                return mailAll[0];
            }

            AutomationElement[] FindParentElements()
            {
                AutomationElement mainPage = null;
                #region 查找到主页;




                var window_0 = window.FindChildAt(0, cf.ByName("The Ribbon"));
                var window_1 = window_0.FindChildAt(0, cf.ByName("Lower Ribbon"));
                var window_2 = window_1.FindFirstChild();
                var window_3 = window_2.FindChildAt(0, cf.ByName("报表"));
                var window_4 = window_3.FindChildAt(0, cf.ByName("首页"));
                this.OutPut("打开首页..");
                window_4.Click();
                #endregion

                while (true)  // 等待主页加载
                {
                    try
                    {
                        mainPage = window.FindChildAt(0, cf.ByName("首页整理"));
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }
                    if (mainPage != null)
                    {
                        break;
                    }
                    this.OutPut("等待首页加载..");
                }
                var smartPane = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm"));
                var designHost = smartPane.FindChildAt(0, cf.ByAutomationId("designHost"));
                var panel_0 = designHost.FindChildAt(0, cf.ByAutomationId("94a22e6d-8578-409d-bf9e-6d05081fb714"));
                var buttonsParent = panel_0.FindFirstChild().FindFirstChild();
                this.ensureButton = buttonsParent.FindFirstChild();
                this.cancelButton = buttonsParent.FindChildAt(0, cf.ByName("取消(&C)"));
                var panel_1 = designHost.FindChildAt(0, cf.ByAutomationId("2eadca8b-9c28-46c2-903e-26542e93be21"));
                var lcRecordViewHolder = panel_1.FindFirstChild().FindFirstChild();
                var childs = lcRecordViewHolder.FindAllChildren();
                this.OutPut("获取父元素集..");
                return childs;
            }

        }
        // 获取控件文本;
        string GetText(int index)
        {
            return parentElements[index].FindFirstChild().Name;
        }
        // 获取控件元素;
        AutomationElement GetObject(int index)
        {
            return parentElements[index].FindFirstChild().FindFirstChild();
        }

        /// <summary>
        /// 感染病例报卡和病案质量评分的自动化，从主界面获取信息,需要完成首页填写;
        /// </summary>
        /// <returns>患者信息类</returns>
        public Pain CaseAuto()
        {
            if (this.breakProcess)
            {
                return null;
            }

            #region 常量赋值
            const int CONST_ALLERY = 0;
            const int CONST_NAME = 1;
            const int CONST_GENDER = 2;
            const int CONST_AGE = 3;
            const int CONST_INDAY = 4;
            const int CONST_BED = 7;
            const int CONST_DURINGDAY = 8;
            const int CONST_ID = 10;
            const int CONST_DIAG = 11;
            #endregion
            AutomationElement[] parentElements = FindParentElements();
            if (parentElements != null)
            {
                string allery = parentElements[CONST_ALLERY].FindFirstChild().Name;

                if (allery != null && allery.Length > 1 && !allery.Contains("无"))
                {
                    this.message.ShowWarningDialog($"该患者存在可疑过敏史【{allery}】，请核对首页是否填写过敏史！");
                }

                pain.Name = parentElements[CONST_NAME].FindFirstChild().Name;
                SetName(pain.Name);
                pain.Gender = parentElements[CONST_GENDER].FindFirstChild().Name;
                SetGender(pain.Gender);
                pain.Age = parentElements[CONST_AGE].FindFirstChild().Name;
                SetAge(pain.Age);
                pain.InDay = parentElements[CONST_INDAY].FindFirstChild().Name;
                pain.DuringDay = parentElements[CONST_DURINGDAY].FindFirstChild().Name.Replace("天", "");
                pain.Id = parentElements[CONST_ID].FindFirstChild().Name.Replace("住院号：", "");
                SetId(pain.Id);
                pain.Bed = parentElements[CONST_BED].FindFirstChild().Name;
                pain.OutDiagnose = parentElements[CONST_DIAG].FindFirstChild().Name;
                pain.MainDiagnose = Regex.Replace(pain.OutDiagnose.Split(',')[0], @"[A-Z]+\d+\.*[a-z]*\d*[a-z]*\d*", "").Replace("诊断：", "");
                SetMainDiagnose(pain.MainDiagnose);
            }
            else
            {
                return null;
            }
            try
            {
                DateTime inDay = DateTime.Parse(pain.InDay);
                pain.OutDay = inDay.AddDays(int.Parse(pain.DuringDay)).ToString("yyyy年MM月dd日");
            }
            catch (Exception ex)
            {

                this.message.ShowInfoDialog("获取出院信息失败，患者可能未出院或未完成首页填写！\n" + ex);
            }
            return pain;
            AutomationElement[] FindParentElements()
            {
                try
                {
                    var emptyPane = window.FindChildAt(0, cf.ByName("clientPanel"));
                    var panelControl1 = emptyPane.FindChildAt(0, cf.ByName("panelControl1"));
                    var xTabMain = panelControl1.FindChildAt(0, cf.ByAutomationId("xTabMain"));
                    var 住院医生站 = xTabMain.FindChildAt(0, cf.ByName("住院医生站"));
                    var SmartForm = 住院医生站.FindChildAt(0, cf.ByAutomationId("SmartForm"));
                    var designHost = SmartForm.FindChildAt(0, cf.ByAutomationId("designHost"));
                    var panel_0 = designHost.FindChildAt(0, cf.ByAutomationId("8ef25f4e-709d-470c-a47c-afd4b06cb0ff"));
                    //var 病人信息 = panel_0.FindChildAt(0, cf.ByAutomationId("131826"));
                    var 病人信息 = panel_0.FindFirstChild();
                    // 131586
                    var lcRecordViewHolder = 病人信息.FindChildAt(0, cf.ByAutomationId("lcRecordViewHolder"));
                    return lcRecordViewHolder.FindAllChildren();
                }
                catch (Exception)
                {
                    this.message.ShowErrorDialog("获取信息错误！", "住院医师工作站未打开！");
                    return null;
                }
            }
        }

        /// <summary>
        /// 门诊首页的填写自动
        /// 
        /// </summary>
        /// <returns></returns>

        public void OutPaient()
        {
            #region 常量赋值
            int CONST_AGE = 0; /*2b7e68df-e8e4-419c-89d8-54bca01c8ed7*/
            int CONST_GENDER = 6; /*8bbec089-e0d3-4f5e-85a9-c370e9a2ef0b*/
            int CONST_BORN_DATE = 7;

            int CONST_IDCARD = 3; /*68f3b230-aae6-411f-9b40-2d4d195e845c*/
            int CONST_NEIGHBORHOOD = 4;// 居委会
            int CONST_NAME = 5; /*8343e07e-fa20-407b-b723-ca651e7734d1*/
            int CONST_CONTRY = 8;// 国家
            int CONST_CITY = 9;//重庆市
            int CONST_NATION = 10;// 汉族
            int CONST_MARRIAGE = 11;
            int CONST_WORK_ADDR = 12;
            int CONST_ADDR = 13; /* e3528b88-e6b8-451b-8ce3-06d23d4776c8*/
            int CONST_RELACTION_PEOPLE = 14;
            int CONST_RELACTION = 15;
            int CONST_DEGREE_OF_EDUCATION = 16;
            int CONST_BLOODTYPE_ABO = 17;
            int CONST_BLOODTYPE_RH = 18;
            int CONST_MAIL = 19;
            int CONST_VOCATION = 20;
            int CONST_PHONE = 21; /* 7d3e1c46-3297-468c-a128-48ead5d2d794*/
            int CONST_RELATION_PHONE = 22; /* 9b46b95d-72ac-4304-aaa8-ded8bd172167*/
            #endregion  

            AutomationElement mainPageButton = null;
            if (this.breakProcess)
            {
                return;
            }
            try
            {
                mainPageButton = window.FindChildAt(0, cf.ByAutomationId("ribbonHead")).FindChildAt(0, cf.ByName("Lower Ribbon")).FindFirstChild().FindChildAt(0, cf.ByName("日志")).FindChildAt(0, cf.ByName("首页"));
            }
            catch (Exception)
            {

                this.message.ShowErrorDialog("未打开门诊医师工作站!");
                return;
            }

            window.SetForeground();
            mainPageButton.Click();

            AutomationElement mainPage = null;
            while (true)  // 等待主页加载
            {
                try
                {
                    mainPage = window.FindChildAt(0, cf.ByName("门诊首页"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                if (mainPage != null)
                {
                    break;
                }
            }

            try
            {
                ensureButton = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).FindChildAt(0, cf.ByAutomationId("designHost")).FindChildAt(0, cf.ByAutomationId("045a3b77-3e05-4446-a5fc-75d7d2ee23e0")).FindFirstChild().FindFirstChild().FindChildAt(0, cf.ByName("确定(&O)"));
            }
            catch (Exception)
            {
                //MessageBox.Show("Cannnot find ensure button,the MainPage may ReadOnly.", "error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                UIMessageBox.ShowError("Cannnot find the Ensure Button,the MainPage may ReadOnly.",true,true);
                return;
            }

            parentElements = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).FindChildAt(0, cf.ByAutomationId("designHost")).FindFirstChild().FindFirstChild().FindFirstChild().FindAllChildren();
            DateTime date = Convert.ToDateTime(GetOnSetDate());

            string patientName = GetText(CONST_NAME);
            string IdCardNumber = GetText(CONST_IDCARD);

            try
            {
                pain = infoDb.QueryInfo(patientName, IdCardNumber,date);
            }
            catch (System.Data.SQLite.SQLiteException)
            {

                MessageBox.Show("数据库无此人记录！");
                return;
            }
            
            GetObject(CONST_ADDR).AsTextBox().Enter(pain.HomeAddr);

            
            if (GetText(CONST_WORK_ADDR).Length < 3)
            {
                GetObject(CONST_WORK_ADDR).AsTextBox().Enter("无");
                GetObject(CONST_VOCATION).AsTextBox().Enter("农民");
            }
            else
            {
                GetObject(CONST_WORK_ADDR).AsTextBox().Enter(pain.WorkAddr);

            }

            if (GetText(CONST_CONTRY).Length < 1)
            {
                GetObject(CONST_CONTRY).AsTextBox().Enter("中国");
            }
            if (GetText(CONST_MAIL).Length < 5)
            {
                GetObject(CONST_MAIL).AsTextBox().Enter("401420");
            }
            if (GetText(CONST_PHONE).Length < 10)
            {
                GetObject(CONST_PHONE).AsTextBox().Enter(pain.Phone);
            }

            if (GetText(CONST_MARRIAGE).Length<1) // 婚姻项为空时;
            {
                GetObject(CONST_MARRIAGE).AsTextBox().Enter(pain.Marriage ?? "已婚");
                KeyPressEnter();
            }

            if (GetText(CONST_CITY).Length<2)
            {
                GetObject(CONST_CITY).AsTextBox().Enter("重庆市");
                KeyPressEnter();
            }

            if (GetText(CONST_DEGREE_OF_EDUCATION).Length < 1)
            {
                GetObject(CONST_DEGREE_OF_EDUCATION).AsTextBox().Enter("不详");
                KeyPressEnter();
            }

            if (GetText(CONST_NATION).Length < 1)
            {
                GetObject(CONST_NATION).AsTextBox().Enter("汉族");
                KeyPressEnter();
            }
            if (GetText(CONST_IDCARD).Length<10)
            {
                GetObject(CONST_IDCARD).AsTextBox().Enter("未带");
                KeyPressEnter();
            }

            GetObject(CONST_VOCATION).AsTextBox().Enter(pain.Vocation);

            KeyPressEnter();

            string currentVocation = GetText(CONST_VOCATION).ToString();
            string reportVocations = "幼托儿童、散居儿童、 学生（大中小学）、教师、保育员及保姆、餐饮食品业、商业服务、医务人员、工人、民工、农民、牧民、渔(船)民、干部职员、离退人员、 家务及待业、其他（）、不详";

            if (currentVocation.Contains("职员") && GetText(CONST_WORK_ADDR).Length < 2)
            {
                UIMessageBox.Show("监测到：患者职业为职员，但工作地址不全，请手动更新！程序结束后会保存失败的列表");
                failedString = (pain.Name + " -- " + pain.IdCardNumber + " -- " + pain.InDay);
            }

            
            if (!reportVocations.Contains(currentVocation)&& GetText(CONST_WORK_ADDR).Length < 2)
            {
                    GetObject(CONST_VOCATION).AsTextBox().Enter("农民");
                KeyPressEnter();
            }










            if (GetText(CONST_NEIGHBORHOOD).Length < 2) // 居委会，如没有默认填写文龙
            {
                GetObject(CONST_NEIGHBORHOOD).AsTextBox().Enter("004013");
                KeyPressEnter();
            }

            if (GetText(CONST_RELACTION).Length < 1)
            {
                GetObject(CONST_RELACTION).AsTextBox().Enter("本人或户主");
                KeyPressEnter();
            }

            if (GetText(CONST_RELACTION_PEOPLE).Length<1)
            {
                GetObject(CONST_RELACTION_PEOPLE).AsTextBox().Enter(pain.Name);
            }
            if (GetText(CONST_RELATION_PHONE).Length < 8)
            {
                GetObject(CONST_RELATION_PHONE).AsTextBox().Enter(pain.Phone);
            }

            // ABO血型
            if (GetText(CONST_BLOODTYPE_ABO).Length<1)
            {
                GetObject(CONST_BLOODTYPE_ABO).Click();
                Thread.Sleep(100);
                Mouse.MoveTo(983, 432);
                Mouse.Scroll(-20);
                Thread.Sleep(100);
                Mouse.LeftDoubleClick();
                Thread.Sleep(100);
            }

            if (GetText(CONST_BLOODTYPE_RH).Length<1)
            {
                // rh血型
                GetObject(CONST_BLOODTYPE_RH).Click();
                Thread.Sleep(100);
                Mouse.MoveTo(983, 443);
                Mouse.Scroll(-20);
                Thread.Sleep(100);
                Mouse.LeftDoubleClick();
                Thread.Sleep(100);
            }

            SwitchPage(1);
            Thread.Sleep(100);

            AutomationElement[] bloodPresure = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                          FindChildAt(0, cf.ByAutomationId("designHost")).
                                                          FindChildAt(0, cf.ByAutomationId("90b3ff9c-8af8-478a-a415-ff7587d41905")).
                                                          FindFirstChild().
                                                          FindFirstChild().
                                                          FindAllChildren(); 
            AutomationElement SBP = bloodPresure[0].FindFirstChild().FindFirstChild();
            AutomationElement DBP = bloodPresure[1].FindFirstChild().FindFirstChild();
            AutomationElement mainDiag = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                          FindChildAt(0, cf.ByAutomationId("designHost")).
                                                          FindChildAt(0, cf.ByAutomationId("b4ebf09a-6abf-4941-ad46-f65e8e77063b")).
                                                          FindFirstChild().
                                                          FindChildAt(0, cf.ByAutomationId("lcRecordViewHolder")).
                                                          FindFirstChild().
                                                          FindChildAt(0, cf.ByAutomationId("MemoEdit"));

            SBP.AsTextBox().Enter("0");
            DBP.AsTextBox().Enter("0");
            SBP.AsTextBox().Enter(pain.BloodPresure.Replace("mmHg", "").Split("/")[0]);
            DBP.AsTextBox().Enter(pain.BloodPresure.Replace("mmHg", "").Split("/")[1]);

            mainDiag.AsTextBox().Enter(pain.MainChief);
            ensureButton.Click();

            //return;

            AutomationElement GetObject(int index)
            {
                return parentElements[index].FindFirstChild().FindFirstChild();
            }

            void KeyPressEnter()
            {
                Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }

            string GetOnSetDate()
            {
                SwitchPage(1);
                string visitDate = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                              FindChildAt(0, cf.ByAutomationId("designHost")).
                                                              FindChildAt(0, cf.ByAutomationId("b4ebf09a-6abf-4941-ad46-f65e8e77063b")).
                                                              FindFirstChild().
                                                              FindFirstChild().
                                                              FindAllChildren()[2].
                                                              FindFirstChild().Name; // 获取就诊时间;
                Thread.Sleep(500);
                SwitchPage(0);
                return visitDate;
            }

            void SwitchPage(int number)
            {
                if (number==1)
                {
                    AutomationElement visitInformation = mainPage.
                                                FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                FindChildAt(0, cf.ByAutomationId("designHost")).
                                                FindAllChildren()[2].
                                                FindFirstChild().
                                                FindAllChildren()[1];
                    visitInformation.Click();
                }
                else
                {
                    AutomationElement basicInformation = mainPage.
                                                FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                FindChildAt(0, cf.ByAutomationId("designHost")).
                                                FindAllChildren()[7].
                                                FindFirstChild().
                                                FindAllChildren()[0];
                    basicInformation.Click();
                }
            }

        }
        public string GetUserName()
        {
            var window_0 = window.FindChildAt(0, cf.ByAutomationId("ribbonStatusBar"));
            string name = window_0.FindFirstChild().Name;
            return name.Split('：')[1];
        }

        public void FillMedicalRecord()
        {
            AutomationElement[] navigationBar = window.FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                      FindFirstChild().FindFirstChild().FindFirstChild().
                                                      FindFirstChild().FindFirstChild().
                                                      FindChildAt(0, cf.ByAutomationId("dockPanelDocNav")).
                                                      FindChildAt(0, cf.ByAutomationId("dockPanel1_Container")).
                                                      FindChildAt(0, cf.ByAutomationId("DocNavPanel")).
                                                      FindChildAt(0, cf.ByAutomationId("xtraTabControl1")).
                                                      FindChildAt(0, cf.ByAutomationId("TabPage_DocStruct")).
                                                      FindChildAt(0, cf.ByAutomationId("zlTreeList1")).
                                                      FindAllChildren();

            AutomationElement mainChief = navigationBar[0]; //主诉
            AutomationElement historyOfPresentIllness = navigationBar[1]; //现病史
            AutomationElement passHistory = navigationBar[2]; //既往史
            AutomationElement AllergyHistory = navigationBar[3]; //过敏史
            AutomationElement pyhsicalExamination = navigationBar[4]; //查体
            AutomationElement supplementaryExamination = navigationBar[5];  //辅助检查
            AutomationElement diagnosis = navigationBar[6];//诊断
            AutomationElement handle = navigationBar[7];//处理

            handle.Click();




        }

    }

}
