using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace MytoolUI
{
    public partial class UpdateMessage : UIForm
    {
        public UpdateMessage()
        {
            InitializeComponent();
            SetMessage();
            SetStyle();
        }

        private void SetStyle()
        {
            this.uiSymbolButtonensure.Style = UIStyle.LightRed;
            this.uiSymbolButtoncancel.Style = UIStyle.LightRed;   
            this.uiRichTextBoxUpdateMessage.Style = UIStyle.LightRed;

        }

        private void SetMessage()
        {
            string message = "初次运行，请在设置中更改用户名后继续使用，遇到问题可使用F1查看帮助。\r\n\r\n" +
                "------------------2022.03.29------------------\r\n" +
                "增加授权委托书\r\n" +
                "------------------2022.03.20------------------\r\n" +
                "fixed bugs\r\n" +
                "------------------2022.02.03------------------\r\n" +
                "1.修复肿瘤报卡不能填写非本源信息的bug\r\n" +
                "2.添加肿瘤死亡信息的更正\r\n" +
                "3.从慢病报卡中移除支气管扩张\r\n" +
                "------------------2022.1.12------------------\r\n" +
                "1.更新流行病学调查表(最新)\r\n" +
                "2.更新病房管理告知书(最新)\r\n" +
                "3.更新慢性肺病报告卡样式(2022)\r\n" +
                "4.更新卒中报卡样式(2022)\r\n" +
                "5.更新肿瘤报卡样式(2022)\r\n\r\n" 
                ;
            this.uiRichTextBoxUpdateMessage.Text = message;


        }

        private void uiSymbolButtonensure_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uiSymbolButtoncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
