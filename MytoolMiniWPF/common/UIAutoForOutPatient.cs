using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
//using System.Windows;

using MytoolMiniWPF.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WpfToast.Controls;
using Mouse = FlaUI.Core.Input.Mouse;
/*
 * 这是为门诊日志获取患者姓名的自动化模块
 * 
 * 
 */
namespace MytoolMiniWPF.common
{
    internal partial class UIAuto
    {
        string mainDiagOutPatient = null;

        /// <summary>
        /// 门诊首页的填写自动
        /// 
        /// </summary>
        /// <returns></returns>

        public void MainPageOutPatientAuto()
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

                UMessageBox.Show("警告！", "未打开门诊医师工作站!");
                return;
            }

            window.SetForeground();




            mainDiagOutPatient = window.FindChildAt(0, cf.ByAutomationId("clientPanel"))
                                .FindChildAt(0, cf.ByAutomationId("panelControl1"))
                                .FindChildAt(0, cf.ByAutomationId("xTabMain"))
                                .FindChildAt(0, cf.ByName("全科医生站"))
                                .FindChildAt(0, cf.ByAutomationId("SmartForm"))
                                .FindChildAt(0, cf.ByAutomationId("designHost"))

                                .FindChildAt(0, cf.ByAutomationId("6283ca74-e4b9-4128-a1a3-5855c127cbbd"))

                                .FindFirstChild()
                                .FindChildAt(0, cf.ByAutomationId("lcRecordViewHolder"))
                                .FindFirstChild()
                                .FindFirstChild().Name;
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
                UMessageBox.Show("warning!", "Cannnot find the Ensure Button,the MainPage may ReadOnly.");
                return;
            }

            parentElements = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).FindChildAt(0, cf.ByAutomationId("designHost")).FindFirstChild().FindFirstChild().FindFirstChild().FindAllChildren();
            DateTime date = Convert.ToDateTime(GetOnSetDate());

            string patientName = GetText(CONST_NAME);
            string IdCardNumber = GetText(CONST_IDCARD);
            string mainPageGender = GetText(CONST_GENDER);

            try
            {
                patient = infoDb.QueryInfo(patientName, IdCardNumber, date);
            }
            catch (System.Data.SQLite.SQLiteException)
            {



                Toast.Show($"{patientName}：数据库无此人记录！", new ToastOptions { Icon = ToastIcons.Warning, ToastMargin = new Thickness(5, 5, 20, 0), Time = 3000, Location = ToastLocation.OwnerCenter });

            }


            EnterText(CONST_ADDR, patient.HomeAddr ?? CheckAddress(GetText(CONST_ADDR)));



            if (GetText(CONST_WORK_ADDR).Length < 3)
            {
                EnterText(CONST_WORK_ADDR, "-");
                EnterText(CONST_VOCATION, "农民");
            }
            else
            {
                EnterText(CONST_WORK_ADDR, patient.WorkAddr ?? "-");

            }

            if (GetText(CONST_CONTRY).Length < 1)
            {
                EnterText(CONST_CONTRY, "中国");
            }
            if (GetText(CONST_MAIL).Length < 5)
            {
                EnterText(CONST_MAIL, "401420");
            }
            if (GetText(CONST_PHONE).Length < 10)
            {
                EnterText(CONST_PHONE, patient.Phone ?? new FormatXlsDataForOutPatients().randomPhone());
            }

            if (GetText(CONST_MARRIAGE).Length < 1) // 婚姻项为空时;
            {
                EnterText(CONST_MARRIAGE, patient.Marriage ?? "已婚");
                KeyPressEnter();
            }

            if (GetText(CONST_CITY).Length < 2)
            {
                EnterText(CONST_CITY, "重庆市");
                KeyPressEnter();
            }

            if (GetText(CONST_DEGREE_OF_EDUCATION).Length < 1)
            {
                EnterText(CONST_DEGREE_OF_EDUCATION, "不详");
                KeyPressEnter();
            }

            if (GetText(CONST_NATION).Length < 1)
            {
                EnterText(CONST_NATION, "汉族");
                KeyPressEnter();
            }
            if (GetText(CONST_IDCARD).Length < 10)
            {
                EnterText(CONST_IDCARD, "未带");
                KeyPressEnter();
            }

            EnterText(CONST_VOCATION, patient.Vocation ?? "农民");

            KeyPressEnter();

            string currentVocation = GetText(CONST_VOCATION).ToString();
            string reportVocations = "幼托儿童、散居儿童、 学生（大中小学）、教师、保育员及保姆、餐饮食品业、商业服务、医务人员、工人、民工、农民、牧民、渔(船)民、干部职员、离退人员、 家务及待业、其他（）、不详";

            if (currentVocation.Contains("职员") && GetText(CONST_WORK_ADDR).Length < 2)
            {
                UMessageBox.Show("注意！", "监测到：患者职业为职员，但工作地址不全，请手动更新！程序结束后会保存失败的列表");
                failedString = (patient.Name + " -- " + patient.IdCardNumber + " -- " + patient.InDay);
            }

            if (!reportVocations.Contains(currentVocation) && GetText(CONST_WORK_ADDR).Length < 2)
            {
                EnterText(CONST_VOCATION, "农民");
                KeyPressEnter();
            }


            if (GetText(CONST_NEIGHBORHOOD).Length < 2) // 居委会，如没有默认填写文龙
            {
                EnterText(CONST_NEIGHBORHOOD, "004013");
                KeyPressEnter();
            }

            if (GetText(CONST_RELACTION).Length < 1)
            {
                EnterText(CONST_RELACTION, "本人或户主");
                KeyPressEnter();
            }

            if (GetText(CONST_RELACTION_PEOPLE).Length < 1)
            {
                EnterText(CONST_RELACTION_PEOPLE, patient.Name ?? patientName);
            }
            if (GetText(CONST_RELATION_PHONE).Length < 8)
            {
                EnterText(CONST_RELATION_PHONE, patient.Phone ?? new FormatXlsDataForOutPatients().randomPhone());
            }

            // ABO血型
            if (GetText(CONST_BLOODTYPE_ABO).Length < 1)
            {
                GetObject(CONST_BLOODTYPE_ABO).Click();
                Thread.Sleep(100);
                Mouse.MoveTo(983, 432);
                Mouse.Scroll(-20);
                Thread.Sleep(100);
                Mouse.LeftDoubleClick();
                Thread.Sleep(100);
            }

            if (GetText(CONST_BLOODTYPE_RH).Length < 1)
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

            AutomationElement[] XtraLayoutControl = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                          FindChildAt(0, cf.ByAutomationId("designHost")).
                                                          FindChildAt(0, cf.ByAutomationId("90b3ff9c-8af8-478a-a415-ff7587d41905")).
                                                          FindFirstChild().
                                                          FindFirstChild().
                                                          FindAllChildren();
            AutomationElement SBP = XtraLayoutControl[0].FindFirstChild().FindFirstChild();
            AutomationElement DBP = XtraLayoutControl[1].FindFirstChild().FindFirstChild();
            AutomationElement mainDiag = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm")).
                                                          FindChildAt(0, cf.ByAutomationId("designHost")).
                                                          FindChildAt(0, cf.ByAutomationId("b4ebf09a-6abf-4941-ad46-f65e8e77063b")).
                                                          FindFirstChild().
                                                          FindChildAt(0, cf.ByAutomationId("lcRecordViewHolder")).
                                                          FindFirstChild().
                                                          FindChildAt(0, cf.ByAutomationId("MemoEdit"));

            AutomationElement Height = XtraLayoutControl[2].FindFirstChild().FindFirstChild();
            AutomationElement Weight = XtraLayoutControl[3].FindFirstChild().FindFirstChild();
            AutomationElement Temperature = XtraLayoutControl[4].FindFirstChild().FindFirstChild();
            AutomationElement Pulse = XtraLayoutControl[5].FindFirstChild().FindFirstChild();
            AutomationElement Respiration = XtraLayoutControl[6].FindFirstChild().FindFirstChild();

            FormatXlsDataForOutPatients rdData = new FormatXlsDataForOutPatients();

            SBP.AsTextBox().Enter("0");
            DBP.AsTextBox().Enter("0");

            Height.AsTextBox().Enter(rdData.randomHeight(mainPageGender));
            Weight.AsTextBox().Enter(rdData.randomWeight(mainPageGender));
            Temperature.AsTextBox().Enter(rdData.randomTempture());
            Pulse.AsTextBox().Enter(rdData.randomPulse());
            Respiration.AsTextBox().Enter(rdData.randomRespiration());


            if (string.IsNullOrEmpty(patient.BloodPresure))
            {
                string randomBloodPresure = rdData.randomBloodPressure();

                SBP.AsTextBox().Enter(randomBloodPresure.Replace("mmHg", "").Split('/')[0]);
                DBP.AsTextBox().Enter(randomBloodPresure.Replace("mmHg", "").Split('/')[1]);
            }
            else
            {
                SBP.AsTextBox().Enter(patient.BloodPresure.Replace("mmHg", "").Split('/')[0]);
                DBP.AsTextBox().Enter(patient.BloodPresure.Replace("mmHg", "").Split('/')[1]);
            }


            string mainChief = null;
            //mainDiag.AsTextBox().Enter(patient.MainChief??"test string");
            if (string.IsNullOrEmpty(patient.MainChief))
            {
                Toast.Show($"主诉为空，随机产生主诉，种子：“{mainDiagOutPatient}”。", new ToastOptions { Icon = ToastIcons.Warning, ToastMargin = new Thickness(5, 5, 20, 0), Time = 3000, Location = ToastLocation.OwnerCenter });
                mainChief = randomChief(mainDiagOutPatient);
            }

            mainDiag.AsTextBox().Enter(patient.MainChief ?? mainChief);



            //ensureButton.Click();

            //return;

            AutomationElement GetObject(int index)
            {
                return parentElements[index].FindFirstChild().FindFirstChild();
            }
            // 根据诊断,产生主诉,如果数据库无记录,则返回"诊断"
             string randomChief(string checkString)
            {
                List<string> data = new DatabaseUnit().GetrandomChief(checkString);
                if (data.Count > 0)
                {
                    string result = data[new Random().Next(data.Count)].ToString();
                    return result;
                }
                else
                {
                    return checkString;
                }
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
                if (number == 1)
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
                return;
            }
        }

    }
}
