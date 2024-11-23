using System;
using System.Drawing;
using System.Windows.Forms;
using MytoolUI.common;
using Sunny.UI;

/// 为caseMini创建控件；
///

namespace MytoolUI
{
    public partial class CaseUIMini
    {
        private UICheckBox CreateUiCheckBox(string showText)
        {
            UICheckBox uiCheckBox = new UICheckBox();
            uiCheckBox.Text = showText;
            uiCheckBox.Dock = DockStyle.Fill;
            //uiCheckBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            uiCheckBox.Font = new Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            uiCheckBox.Style = this.currentStyle;
            return uiCheckBox;
        }
        private UIButton CreateUiButton(string fontText = "\ue69f;", int fontSize = 15)
        {
            UIButton ubtn = new UIButton();
            ubtn.Dock = DockStyle.Fill;
            //ubtn.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            ubtn.Style = this.currentStyle;
            ubtn.Text = fontText;
            ubtn.Font = new Font(FontText.PFCC.Families[0], 11);
            ubtn.Margin = new Padding(2, 1, 1, 2);
            ubtn.RectSides = ToolStripStatusLabelBorderSides.All;
            ubtn.RectColor = Color.Gray;
            ubtn.FillColor = Color.Transparent;
            ubtn.RectHoverColor = Color.Red;
            ubtn.RectPressColor = Color.DarkRed;

            Console.WriteLine(this.uBtnSettingMini.FillColor);
            if (this.uBtnSettingMini.FillColor == Color.FromArgb(255, 251, 238, 238))
            {
                ubtn.ForeColor = Color.OrangeRed;
            }
            else if (this.uBtnSettingMini.FillColor == Color.FromArgb(255, 244, 242, 251) || this.uBtnSettingMini.FillColor == Color.FromArgb(255, 235, 243, 255))
            {
                ubtn.ForeColor = this.uBtnSettingMini.ForeColor;
            }
            else
            {
                ubtn.ForeColor = this.uBtnSettingMini.FillColor;
            }


            ubtn.ForeHoverColor = Color.Black;
            ubtn.ForePressColor = Color.DarkRed;
            return ubtn;
        }

        /// <summary>
        /// 创建颜色选择控件，combobox
        /// </summary>
        private void CreateStyleSelectBox()
        {
            uCbtn.Items.Add("red");
            uCbtn.Items.Add("blue");
            uCbtn.Items.Add("green");
            uCbtn.Items.Add("black");
            uCbtn.Items.Add("purple");
            uCbtn.Items.Add("orange");
            uCbtn.Items.Add("gray");
            uCbtn.Items.Add("lightBlue");
            uCbtn.Dock = DockStyle.Fill;
            uCbtn.Font = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            uCbtn.ItemHeight = 15;
            uCbtn.IntegralHeight = false;
            uCbtn.Margin = new Padding(2, 4, 4, 4);
            uCbtn.Text = "change color style";
            uCbtn.SelectedValueChanged += new EventHandler(this.ChangeStyle);

        }
    }
}
