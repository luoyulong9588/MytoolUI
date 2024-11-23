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

namespace MytoolUI.common
{
    class UIAutoForOutPatient
    {
        private Window window;
        private ConditionFactory cf;
        private string processName = "zlbh.exe";
        UIMessageForm message = new UIMessageForm();

        public UIAutoForOutPatient()
        {
            cf = new ConditionFactory(new UIA3PropertyLibrary());
            try
            {
                window = FlaUI.Core.Application.Attach(processName).GetMainWindow(new UIA3Automation(), new TimeSpan(0, 0, 3));
            }
            catch (Exception ex)
            {

                this.message.ShowInfoDialog("未打开中联Bh 或 开启多个Bh！请检查后重试！\n" + ex.ToString());
                return;
            }
        }

        public string GetName()
        {

            string currentName = window.FindChildAt(0, cf.ByAutomationId("clientPanel"))
                                       .FindChildAt(0, cf.ByAutomationId("panelControl1"))
                                       .FindChildAt(0, cf.ByAutomationId("xTabMain"))
                                       .FindChildAt(0, cf.ByName("全科医生站"))
                                       .FindChildAt(0, cf.ByAutomationId("SmartForm"))
                                       .FindChildAt(0, cf.ByAutomationId("designHost"))
                                       .FindChildAt(0, cf.ByAutomationId("6283ca74-e4b9-4128-a1a3-5855c127cbbd"))
                                       .FindFirstChild()
                                       .FindChildAt(0, cf.ByAutomationId("lcRecordViewHolder"))
                                       .FindAllChildren()[3]
                                       .FindFirstChild().Name;
            return currentName;


        }

    }
}
