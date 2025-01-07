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
    {            /// <summary>
                 /// 住院首页自动填写；点开首页、获取信息、填写信息;
                 /// </summary>
                 /// <param name="widgets">需要动态赋值的控件数组</param>
                 /// <returns>患者信息类</returns>
        public Patient MainPageInHospitalAuto(bool fillPage = true)
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
            const int CONST_MARRAGE = 10;
            const int CONST_VOCATION = 11;
            const int CONST_MINGZU = 13;
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
            const int CONST_TIME_CY = 36;
            const int TABLE_BASIC_INFO = 0;
            const int TABLE_INHOSPITAL_INFO = 4;
            const int TABLE_OLD_INFO = 6;
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
            AutomationElement[] tableElements = null;
            #endregion

            try
            {
                parentElements = FindParentElements();
            }
            catch (Exception ex)
            {
                UMessageBox.Show("错误！", "未找到首页按钮，可能的原因是没有打开住院医生工作站。\r\n" + ex);
                return null;
            }

            SwitchTable(TABLE_BASIC_INFO);

            patient.Name = GetText(CONST_NAME);

            patient.Age = GetText(CONST_AGE);

            patient.Gender = GetText(CONST_GENDER);

            patient.Id = GetText(CONST_PAINID);

            patient.IdCardNumber = GetText(CONST_IDCARD);

            patient.InDay = GetText(CONST_TIME_RY).Split(' ')[0];


            patient.OutDay = GetText(CONST_TIME_CY).Split(' ')[0];
            patient.Phone = GetPhone();

            patient.HomeAddr = GetAddress();

            string vocation = GetText(CONST_VOCATION);

            string medicalInsurance = GetText(CONST_MEDICAL_INSURANCE);

            if (fillPage)
            {
                //Thread thread = new Thread(FillinBlanks);   // 取消多线程填空测试,避免后面被阻塞;
                //thread.Start();
                FillinBlanks();
            }
            FillTableOldInformations();
            FillTableInHospitalInfomations();

            return patient;
            ///
            /// 方法填写在院信息
            ///
            void FillTableInHospitalInfomations()
            {
                #region item 说明
                /*
                   0：清空
                   1：临床路径管理
                   2：死亡原因
                   3：新发肿瘤
                   4：血型
                   5：Rh型
                   6：门诊医生
                   7：科主任
                   8：副主任医师
                   9：主治医师
                   10：住院医师
                   11：实习医师

                   12：进修医师
                   13：研究生医师
                   14：质控医师
                   15：质控护士
                   16：死亡时间
                   17：死亡患者尸检
                   18：责任护士
                   19：出院31天内在住院计划
                   27：输液反应
                   28：质控日期
                   29：病案质量
                    */
                #endregion
                SwitchTable(TABLE_INHOSPITAL_INFO);
                var dict = new DatabaseForZLBHPageAuto().QueryInfo(new DatabaseUnit().GetuserName());

                var residentPhysician = string.IsNullOrEmpty(dict["residentPhysician"]) ? "罗玉龙" : dict["residentPhysician"];
                var attendingPhysician = string.IsNullOrEmpty(dict["attendingPhysician"]) ? "刘益宏" : dict["attendingPhysician"];
                var associateChiefPhysician = string.IsNullOrEmpty(dict["associateChiefPhysician"]) ? "邓先桂" : dict["associateChiefPhysician"];
                var qualityControlDoctor = string.IsNullOrEmpty(dict["qualityControlDoctor"]) ? "刘益宏" : dict["qualityControlDoctor"];
                var qualityControlNurse = string.IsNullOrEmpty(dict["qualityControlNurse"]) ? "叶华顺" : dict["qualityControlNurse"];
                var headOfDepartment = string.IsNullOrEmpty(dict["headOfDepartment"]) ? "邓先桂" : dict["headOfDepartment"];
                try
                {
                    var items = getElementItems();
                    FillBlank(items, 4, "未查");
                    FillBlank(items, 5, "未查");

                    #region 输液反应
                    items[27].FindFirstChild().FindFirstChild().Click();
                    Thread.Sleep(100);
                    KeyPressUp();
                    KeyPressUp();
                    KeyPressUp();
                    //Thread.Sleep(100);
                    KeyPressDown();
                    Thread.Sleep(50);
                    KeyPressEnter();
                    #endregion

                    FillBlank(items, 7, headOfDepartment);
                    FillBlank(items, 8, associateChiefPhysician);
                    FillBlank(items, 9, attendingPhysician);
                    FillBlank(items, 10, residentPhysician);

                    #region 病案质量
                    Thread.Sleep(100);
                    items[29].FindFirstChild().FindFirstChild().Click();
                    KeyPressEnter();
                    #endregion

                    FillBlank(items, 15, qualityControlNurse);

                    FillBlank(items, 14, qualityControlDoctor);

                    items[0].Click();
                    Thread.Sleep(200);


                    FillBlank(items, 11, "");
                    FillBlank(items, 12, "");
                    //FillBlank(items, 13, "");

                    if (patient.OutDay.Length >= 2)
                    {
                        items[28].FindFirstChild().FindFirstChild().Click();

                        string oldText = Clipboard.GetText();
                        Clipboard.SetText(patient.OutDay);
                        Thread.Sleep(50);

                        FlaUI.Core.Input.Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);
                        FlaUI.Core.Input.Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_V);
                        FlaUI.Core.Input.Keyboard.Release(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);
                        Thread.Sleep(50);
                        Clipboard.SetText(oldText);

                        Thread.Sleep(100);

                    }
                    else
                    {

                        FillBlank(items, 28, "");
                        KeyPressEnter();
                    }
                    // KeyPressEnter();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }

                void FillBlank(AutomationElement[] elements, int index, string txt)
                {
                    if (string.IsNullOrEmpty(elements[index].FindFirstChild().FindFirstChild().Name))
                    {
                        elements[index].FindFirstChild().FindFirstChild().Click();
                        elements[index].FindFirstChild().FindFirstChild().AsTextBox().Enter(txt);
                        Thread.Sleep(100);
                        KeyPressEnter();
                        Thread.Sleep(150);
                    }

                }
            }
            /// 旧的登记项填写程序
            void FillTableOldInformations()
            {
                SwitchTable(TABLE_OLD_INFO);
                /*
                    4：hbsAg
                    5：hcvAb
                    6：HIVab
                    8：输血反应
                 */
                try
                {
                    var items = getElementItems();

                    if (string.IsNullOrEmpty(items[4].FindFirstChild().FindFirstChild().Name))
                    {
                        items[4].FindFirstChild().FindFirstChild().Click();
                        Thread.Sleep(100);
                        KeyPressEnter();
                        Thread.Sleep(100);
                    }
                    if (string.IsNullOrEmpty(items[5].FindFirstChild().FindFirstChild().Name))
                    {
                        items[5].FindFirstChild().FindFirstChild().Click();
                        KeyPressEnter();
                        Thread.Sleep(100);
                    }
                    if (string.IsNullOrEmpty(items[6].FindFirstChild().FindFirstChild().Name))
                    {
                        items[6].FindFirstChild().FindFirstChild().Click();
                        Thread.Sleep(100);
                        KeyPressEnter();
                        Thread.Sleep(100);
                    }

                    if (string.IsNullOrEmpty(items[8].FindFirstChild().FindFirstChild().Name))
                    {
                        items[8].FindFirstChild().FindFirstChild().Click();
                        Thread.Sleep(100);
                        KeyPressDown();
                        KeyPressDown();
                        KeyPressEnter();
                        Thread.Sleep(200);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

            void SwitchTable(int tableId)
            {
                if (tableElements != null)
                {
                    tableElements[tableId].Click();
                }
                else
                {
                    MessageBox.Show("find table elements error.");
                }
            }

            void FillinBlanks()
            {
                // 无论籍贯里面填写的什么，都清除重新填写；
                string mail = GetMail();
                string districName = new AddressData().GetDistrictName(patient.IdCardNumber) ?? "重庆市綦江区";
                List<string> vocations = new List<string> { "农民", "其他", "无业", "不便分类", "自由职业", "无职业", "待业", "学生" };
                List<string> insurances = new List<string> { "居民", "农村", "贫困" };
                //2024.10.18更新，李琳：对于自行参保的职工医保，工作单位填家庭住址。所以判断工人非工人则使用医保类别判断，不再使用职业判断。
                //但限于职业类别的乱填现象，对填入非职工医保的首页副本，作职业判断。


                GetObject(CONST_ADDR_JIGUAN).AsTextBox().Enter(districName);

                if (medicalInsurance == "城镇职工基本医疗保险")
                {  //肯定为工人
                    if (vocations.Any(v => vocation.Contains(v)) || insurances.Any(i => medicalInsurance.Contains(i)))
                    {
                        UMessageBox.Show("注意！", $"患者为“{medicalInsurance}”，w{vocation}，请注意手动更新！");
                    }
                        if (addr_work.Length<3)
                    {
                        EnterText(CONST_ADDR_WORK, patient.HomeAddr);
                    }
                    if (phone_work.Length<8)
                    {
                        EnterText(CONST_PHONE_WORK, patient.Phone);
                    }
                    if (mail_work.Length<4)
                    {
                        EnterText(CONST_MAIL_WORK, mail);
                    }
                }
                else
                {//非职工医保
                    if (!(vocations.Any(v => vocation.Contains(v)) || insurances.Any(i => medicalInsurance.Contains(i))))
                    {
                        UMessageBox.Show("注意！", $"患者为“{medicalInsurance}”，但首页职业信息为{vocation}，请注意手动更新！");
                    }
                    EnterText(CONST_ADDR_WORK, "无");
                    EnterText(CONST_PHONE_WORK, "");
                    EnterText(CONST_MAIL_WORK, "");

                }


                /*
                if (vocations.Any(v => vocation.Contains(v)) || insurances.Any(i => medicalInsurance.Contains(i)))
                {
                    EnterText(CONST_ADDR_WORK, "-");
                    EnterText(CONST_PHONE_WORK, "");
                    EnterText(CONST_MAIL_WORK, "");
                }
                else // 判断工人
                {
                    EnterText(CONST_PHONE_WORK, patient.Phone);
                    EnterText(CONST_MAIL_WORK, mail);

                    if (addr_work.Length < 3 || addr_work == addr_born)
                    {
                        UMessageBox.Show("注意！", $"患者可能为工人,首页职业信息为{vocation},但工作地址不正确，请注意手动更新！");
                    }
                }
                */

                EnterText(CONST_ADDR_NOW, patient.HomeAddr);


                EnterText(CONST_RELATION_ADDR, patient.HomeAddr);
                EnterText(CONST_ADDR_BORN, addr_born);
                EnterText(CONST_ADDR_HUKOU, addr_born); // 出生地址 == 户口地址（）


                if (phone_hukou.Length < 5) { EnterText(CONST_PHONE_HUKOU, patient.Phone); }
                if (phone_now.Length < 5) { EnterText(CONST_PHONE_NOW, patient.Phone); }
                if (phone_relation.Length < 5) { EnterText(CONST_RELATION_PHONE, patient.Phone); }


                if (mail_now.Length < 5) { EnterText(CONST_MAIL_NOW, mail); }
                if (mail_hukou.Length < 5) { EnterText(CONST_MAIL_HUKOU, mail); }

                if (GetText(CONST_RELATIONSHIP).Length < 1)
                {
                    EnterText(CONST_RELATIONSHIP, "本人或户主");
                    Thread.Sleep(50);
                    //SendKeys.SendWait("{ENTER}");
                    KeyboardToolkit.Keyboard.Type(Key.Enter);
                }
                if (GetText(CONST_RELATION_NAME).Length < 2)
                {
                    EnterText(CONST_RELATION_NAME, patient.Name);
                }

                if (GetText(CONST_RELATION_NAME) == patient.Name)
                {   //  联系人的地址应该一致
                    EnterText(CONST_RELATION_ADDR, patient.HomeAddr);
                }

                if (string.IsNullOrEmpty(patient.Gender))
                {
                    GetObject(CONST_MARRAGE).Click();
                    Thread.Sleep(100);
                    KeyPressDown();
                    Thread.Sleep(50);
                    KeyPressEnter();
                }
                if (string.IsNullOrEmpty(GetText(CONST_MEDICAL_INSURANCE)))
                {
                    GetObject(CONST_MEDICAL_INSURANCE).Click();
                    Thread.Sleep(100);
                    KeyPressDown();
                    KeyPressSpace();
                    KeyPressSpace();
                    Thread.Sleep(50);
                    KeyPressEnter();
                    Thread.Sleep(100);
                }
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
                //addr_jiguan = CheckAddress(GetText(CONST_ADDR_JIGUAN));
                //addr_hukou = CheckAddress(GetText(CONST_ADDR_HUKOU));
                //addr_born = CheckAddress(GetText(CONST_ADDR_BORN));
                //addr_work = CheckAddress(GetText(CONST_ADDR_WORK));
                //addr_now = CheckAddress(GetText(CONST_ADDR_NOW));
                //addr_relation = CheckAddress(GetText(CONST_RELATION_ADDR));

                addr_jiguan =GetText(CONST_ADDR_JIGUAN);
                addr_hukou =GetText(CONST_ADDR_HUKOU);
                addr_born = GetText(CONST_ADDR_BORN);
                addr_work = GetText(CONST_ADDR_WORK);
                addr_now = GetText(CONST_ADDR_NOW);
                addr_relation =GetText(CONST_RELATION_ADDR);

                string[] addressAll = new string[6] { addr_jiguan, addr_hukou, addr_born, addr_work, addr_now, addr_relation };
                addressAll = addressAll.OrderByDescending(s => s.Length).ToArray();
                string addr = CheckAddress(addressAll[0]); // 

                addr_jiguan = string.IsNullOrEmpty(addr_jiguan) ? addr: CheckAddress(addr_jiguan);
                addr_hukou = string.IsNullOrEmpty(addr_hukou) ? addr : CheckAddress(addr_hukou);
                addr_born = string.IsNullOrEmpty(addr_born) ? addr : CheckAddress(addr_born);
              //  addr_work = string.IsNullOrEmpty(addr_work) ? addr : CheckAddress(addr_work);
                addr_now = string.IsNullOrEmpty(addr_now) ? addr : CheckAddress(addr_now);
                addr_relation = string.IsNullOrEmpty(addr_relation) ? addr: CheckAddress(addr_relation);


                return addr;
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

            ///为首页中“住院情况、旧的登记项”查找操作元素集合
            AutomationElement[] getElementItems()
            {
                var items = window.FindChildAt(0, cf.ByName("首页整理"))
                                         .FindChildAt(0, cf.ByAutomationId("SmartForm"))
                                         .FindChildAt(0, cf.ByAutomationId("designHost"))
                                         .FindAllChildren()[1]
                                         .FindFirstChild()
                                         .FindChildAt(0, cf.ByAutomationId("lcRecordViewHolder"))
                                         .FindAllChildren();
                return items;
            }

            /// 找到主页的元素;
            AutomationElement[] FindParentElements()
            {
                AutomationElement mainPage = null;
                #region 查找到主页;

                var window_0 = window.FindChildAt(0, cf.ByName("The Ribbon"));
                var window_1 = window_0.FindChildAt(0, cf.ByName("Lower Ribbon"));
                var window_2 = window_1.FindFirstChild();
                var window_3 = window_2.FindChildAt(0, cf.ByName("报表"));
                var window_4 = window_3.FindChildAt(0, cf.ByName("首页"));
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
                }

                var smartPane = mainPage.FindChildAt(0, cf.ByAutomationId("SmartForm"));
                var designHost = smartPane.FindChildAt(0, cf.ByAutomationId("designHost"));
                tableElements = designHost.FindChildAt(0, cf.ByName("首页整理")).FindFirstChild().FindAllChildren();

                var panel_0 = designHost.FindChildAt(0, cf.ByAutomationId("94a22e6d-8578-409d-bf9e-6d05081fb714"));
                var buttonsParent = panel_0.FindFirstChild().FindFirstChild();
                this.ensureButton = buttonsParent.FindFirstChild();
                this.cancelButton = buttonsParent.FindChildAt(0, cf.ByName("取消(&C)"));
                var panel_1 = designHost.FindChildAt(0, cf.ByAutomationId("2eadca8b-9c28-46c2-903e-26542e93be21"));
                var lcRecordViewHolder = panel_1.FindFirstChild().FindFirstChild();
                var childs = lcRecordViewHolder.FindAllChildren();
                return childs;
            }
        }
    }
}
