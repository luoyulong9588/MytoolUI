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

namespace MytoolMiniWPF.common
{
    internal partial class UIAuto
    {
        delegate void SetTextCallback(string text);
        private string userName;
        private Patient patient;
        private bool breakProcess = false;
        private ConditionFactory cf;
        private AutomationElement ensureButton;
        private AutomationElement cancelButton;
        
        private DatabaseForPatient infoDb = new DatabaseForPatient();
        AutomationElement[] parentElements;
        private FlaUI.Core.AutomationElements.Window window;
        private string processName = "zlbh.exe";
        public string failedString = null;

        public UIAuto()
        {
            userName = new DatabaseUnit().GetuserName();
            patient = new Patient { DoctorName = userName };
            cf = new ConditionFactory(new UIA3PropertyLibrary());
            CheckMainWindowZLBH();
        }

        public Patient FillMainPage()

        { 
            AutomationElement mainPageInHospital = null;
            AutomationElement mainPageOutPatient = null;
            try
            {
                mainPageInHospital = window.FindChildAt(0, cf.ByName("The Ribbon"))
                                               .FindChildAt(0, cf.ByName("Lower Ribbon"))
                                               .FindFirstChild()
                                               .FindChildAt(0, cf.ByName("报表"))
                                               .FindChildAt(0, cf.ByName("首页"));
            }
            catch (Exception)
            {
                mainPageOutPatient = window.FindChildAt(0, cf.ByAutomationId("ribbonHead"))
                                            .FindChildAt(0, cf.ByName("Lower Ribbon"))
                                            .FindChildAt(0, cf.ByName("开始"))
                                            .FindChildAt(0, cf.ByName("日志"))
                                            .FindChildAt(0, cf.ByName("首页"));
            }
            
            if (mainPageInHospital!=null)
            {
                return MainPageInHospitalAuto(true);
            }
            else if (mainPageOutPatient!=null)
            {
                MainPageOutPatientAuto();
                return null;
            }
            else
            {
                Toast.Show($"未找到门诊/住院首页按钮", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 3000, Location = ToastLocation.ScreenTopCenter });
                return null;
            }
             

        }

        // 检查中联主窗口是否打开
        private void CheckMainWindowZLBH()
        {
            try
            {
                window = FlaUI.Core.Application.Attach(processName).GetMainWindow(new UIA3Automation(), new TimeSpan(0, 0, 3));
            }
            catch (Exception ex)
            {

                //UMessageBox.Show("警告！","未打开中联Bh 或 开启多个Bh！请检查后重试！\n" + ex.ToString());
                Toast.Show($"未打开中联Bh 或 开启多个Bh", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 3000, Location = ToastLocation.ScreenTopCenter });

                breakProcess = true;
            }

        }
        public static void Send(Key key)
        {
            if (System.Windows.Input.Keyboard.PrimaryDevice != null)
            {
                if (System.Windows.Input.Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(System.Windows.Input.Keyboard.PrimaryDevice, System.Windows.Input.Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = System.Windows.Input.Keyboard.KeyDownEvent
                    };
                    InputManager.Current.ProcessInput(e);

                }
            }
        }

        void EnterText(int objectIndex, string text)
        {
            GetObject(objectIndex).AsTextBox().Enter(text);
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
        public void KeyPressEnter()
        {
            
            FlaUI.Core.Input.Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
        }
        public void KeyPressDown()
        {
            FlaUI.Core.Input.Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.DOWN);
            Thread.Sleep(100);
        }
        public void KeyPressUp()
        {
            FlaUI.Core.Input.Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.UP);
            Thread.Sleep(100);
        }
        public void KeyPressSpace()
        {
            FlaUI.Core.Input.Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.SPACE);
            Thread.Sleep(100);
        }
        private string CheckAddress(string addr)
        {
            addr = addr.Replace("綦江县", "綦江区").Replace("重庆綦江", "重庆市綦江区").Replace("重庆綦江区", "重庆市綦江区");

            // "重庆市綦江区" + addr.Replace("中国", "").Replace("无", "").Replace("重庆市", "").Replace("重庆", "").Replace("綦江区", "").Replace("綦江县", "").Replace("綦江", "");

            var randAddr = new FormatXlsDataForOutPatients().randomAddress();

            var res = Analysis(addr);
            // todo  这里需要判断，如果没有重庆市，如果有綦江区，只需要添加重庆市返回即可？！ 

            var province = string.IsNullOrEmpty(res.province)? "重庆市": res.province;//省  重庆市
            var city = res.city;//市
            var county = string.IsNullOrEmpty(res.county) ? "綦江区" : res.county;//县  綦江区
            var town = string.IsNullOrEmpty(res.town) ? randAddr.streetName : res.town;//镇/街道
            var village = string.IsNullOrEmpty(res.village) ? randAddr.villageName : res.village; //村/社区

            if (village.Contains("组")||village.Contains("号") || village.Contains("-"))
            {
                return province + county + town + village;
            }

            else
            {
                return province + county + town + village + randAddr.endName;
            }
    

       //     return "重庆市綦江区" + addr.Replace("中国", "").Replace("无", "").Replace("重庆市", "").Replace("重庆", "").Replace("綦江区", "").Replace("綦江县", "").Replace("綦江", "");

        }

        /// <summary>
        /// 解析省市区
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
		public (string province, string city, string county, string town, string village) Analysis(string address)
        {
            string regex = "(?<province>[^省]+自治区|.*?省|.*?行政区|.*?市)?(?<city>[^市]+自治州|.*?地区|.*?行政单位|.+盟|市辖区|.*?市|.*?县)?(?<county>[^县]+县|.*?区|.+市|.+旗|.+海域|.+岛)?(?<town>[^区].+镇|.*?街道)?(?<village>.*)";

            var m = Regex.Match(address, regex, RegexOptions.IgnoreCase);
            var province = m.Groups["province"].Value;
            var city = m.Groups["city"].Value;
            var county = m.Groups["county"].Value;
            var town = m.Groups["town"].Value;
            var village = m.Groups["village"].Value;
            return (province, city, county, town, village);
        }


        /// <summary>
        /// 感染病例报卡和病案质量评分的自动化、住院证的信息获取，从主界面获取信息,需要完成首页填写;
        /// </summary>
        /// <returns>患者信息类</returns>
        public Patient CaseAuto()
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
                    UMessageBox.Show("注意！", $"该患者存在可疑过敏史【{allery}】，请核对首页是否填写过敏史！");
                }

                patient.Name = parentElements[CONST_NAME].FindFirstChild().Name;
                patient.Gender = parentElements[CONST_GENDER].FindFirstChild().Name;
                patient.Age = parentElements[CONST_AGE].FindFirstChild().Name;
                patient.InDay = parentElements[CONST_INDAY].FindFirstChild().Name;
                patient.DuringDay = parentElements[CONST_DURINGDAY].FindFirstChild().Name.Replace("天", "");
                patient.Id = parentElements[CONST_ID].FindFirstChild().Name.Replace("住院号：", "");
                patient.Bed = parentElements[CONST_BED].FindFirstChild().Name;
                patient.OutDiagnose = parentElements[CONST_DIAG].FindFirstChild().Name;
                patient.MainDiagnose = Regex.Replace(patient.OutDiagnose.Split(',')[0], @"[A-Z]+\d+\.*[a-z]*\d*[a-z]*\d*", "").Replace("诊断：", "");
            }
            else
            {
                return null;
            }
            try
            {
                DateTime inDay = DateTime.Parse(patient.InDay);
                patient.OutDay = inDay.AddDays(int.Parse(patient.DuringDay)).ToString("yyyy年MM月dd日");
            }
            catch (Exception ex)
            {

                UMessageBox.Show("警告！", "获取出院信息失败，患者可能未出院或未完成首页填写！\n" + ex);
            }
            return patient;
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
                    UMessageBox.Show("错误！", "住院医师工作站未打开！");
                    return null;
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

