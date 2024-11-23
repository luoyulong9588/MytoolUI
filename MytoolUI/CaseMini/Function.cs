using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Markup;
using MytoolUI.common;
using Sunny.UI;

namespace MytoolUI
{
    public partial class CaseUIMini
    {

        /// <summary>
        /// 将控件放置到tableLayout上；
        /// </summary>
        /// <param name="control">控件，支持button、CheckBox等</param>
        /// <param name="row">放置的行</param>
        /// <param name="column">放置的列</param>
        /// <param name="rowSPan">占用行数</param>
        /// <param name="columnSpan">占用列数</param>
        private void PadTableLayout(Control control, int row, int column, int rowSPan = 1, int columnSpan = 1)
        {
            uiTableLayoutPanelDesktop.Controls.Add(control);
            uiTableLayoutPanelDesktop.SetRow(control, row);
            uiTableLayoutPanelDesktop.SetRowSpan(control, rowSPan);
            uiTableLayoutPanelDesktop.SetColumn(control, column);
            uiTableLayoutPanelDesktop.SetColumnSpan(control, columnSpan);
        }
        /// <summary>
        /// checkBox更改时保存值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChange(object sender, EventArgs e)
        {
            this.fillPage = this.uiCheckBoxfill.Checked;
            this.withContact = this.uiCheckcBoxContact.Checked;
            this.withPermit = this.uiCheckcBoxSingleLine.Checked;
        }
        // 感染病例报告卡打印功能
        private void printDocumentReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(this.reportCardImage, 0, 0);
        }
        // 病历质量评分表打印功能
        private void printDocumentCase_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(this.CaseImage, 0, 0);
        }
        /// <summary>
        /// 为note、str按钮创建UIButton
        /// </summary>
        /// <param name="fontText">显示内容，可以用iconfont代替</param>
        /// <param name="fontSize">字号</param>
        /// <returns></returns>
        
        private void SaveChange()
        {
            // 保存主题
            Console.WriteLine("save style");
            this.db.UpdateColorStyle(this.currentColor);

            uTextBox.SaveFile("config\\note.document", RichTextBoxStreamType.RichText);
        }
        /// <summary>
        /// 设置caseMiniUI的高度
        /// </summary>
        /// <param name="rowCount"></param>
        private void SetWindowHeight(int rowCount)
        {
            this.ClientSize = new Size(this.Size.Width, 30 * rowCount);
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
        private void DragWindow_MouseDown(object sender, MouseEventArgs e)
        {
            x = this.Location.X;
            y = this.Location.Y;

            try
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (x == this.Location.X && y == this.Location.Y)
            {
                try
                {    // 这是uibutton;
                    switch (((UIButton)sender).Name)
                    {
                        case "uBtnReportMini":
                            this.uBtnReportMini_Click(sender, e);
                            break;
                        case "uBtnOutPatientMini":
                            this.uBtnOutPatientMini_Click(sender, e);
                            break;
                        case "uBtnInvestigationMini":
                            this.uBtnInvestigationMini_Click(sender, e);
                            break;
                        case "uBtnSettingMini":
                            this.uBtnSettingMini_Click(sender, e);
                            break;
                        case "uBtnRestMini":
                            this.uBtnRestMini_Click(sender, e);
                            break;
                        case "uiSymbolButtonCloseMini":
                            this.uiSymbolButtonCloseMini_Click(sender, e);
                            break;
                        case "uBtnNoteMini":
                            this.uBtnNote_Click(sender, e);
                            break;
                        case "uBtnStrMini":
                            this.uBtnStrMini_Click(sender, e);
                            break;
                    }

                }
                catch (Exception ex)  // 这是checkbox
                {
                    Console.WriteLine(ex);
                }
            }

        }
        #endregion

        private void SaveNewNum(object sender, int value)
        {
            string[] adjustList = new string[2];
            adjustList[0] = uiIntergerUpDownReportsingleX.Value.ToString();
            adjustList[1] = uiIntergerUpDownReportsingleY.Value.ToString();
            new DatabaseUnit().SetAdjustNum(adjustList,"adjust_single");
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        /// <param name="color"></param>
        private void SetColor(string color)
        {
            switch (color)
            {
                case "red":
                    this.currentStyle = UIStyle.LightRed;
                    break;
                case "gray":
                    this.currentStyle = UIStyle.Gray;
                    break;
                case "green":
                    this.currentStyle = UIStyle.Green;
                    break;
                case "orange":
                    this.currentStyle = UIStyle.Orange;
                    break;
                case "blue":
                    this.currentStyle = UIStyle.Blue;
                    break;
                case "black":
                    this.currentStyle = UIStyle.Black;
                    break;
                case "lightBlue":
                    this.currentStyle = UIStyle.LightBlue;
                    break;
                case "purple":
                    this.currentStyle = UIStyle.LightPurple;
                    break;
            }
            this.currentColor = color;

            uibuttons = new List<UIButton> {
                this.uBtnOutPatientMini,
                this.uBtnReportMini,
                this.uBtnRestMini,
                this.uBtnSettingMini,
                this.uBtnInvestigationMini,
                this.uiSymbolButtonCloseMini,
                this.uBtnNoteMini,
                this.uBtnStrMini,

                };
            foreach (var item in uibuttons)
            {
                item.Style = this.currentStyle;
                //item.RectColor = this.BackColor;
                item.RectColor = item.BackColor;
            }


            List<UICheckBox> checkboxs = new List<UICheckBox> { this.uiCheckBoxfill, this.uiCheckcBoxContact, this.uiCheckcBoxSingleLine };

            try
            {
                foreach (var item in checkboxs)
                {
                    item.Style = this.currentStyle;
                }
                uCbtn.Style = this.currentStyle;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            this.uiIntergerUpDownReportsingleX.Style = this.currentStyle;
            this.uiIntergerUpDownReportsingleY.Style = this.currentStyle;
        }

        private void getAdjust(string tabelName)
        {
            List<string> adjustList = this.db.GetAdjustNum(tabelName);
            reportX = float.Parse(adjustList[0]);
            reportY = float.Parse(adjustList[1]);


            if (tabelName == "adjust")
            {
                caseX = float.Parse(adjustList[2]);
                caseY = float.Parse(adjustList[3]);
            }
            else
            {
                uiIntergerUpDownReportsingleX.Value = int.Parse(reportX.ToString());
                uiIntergerUpDownReportsingleY.Value = int.Parse(reportY.ToString());
            }
        }

        private void ChangeStyle(object sender, EventArgs e)
        {
            string color = "red";

            if (uCbtn.SelectedItem != null)
            {
                color = uCbtn.SelectedItem.ToString();
            }
            this.SetColor(color);

        }
        private void uiCheckcBoxSingleLineChange(object sender, EventArgs e)
        {
            AdjustRow();
        }

        /// <summary>
        /// carete report card adjust button for single line.
        /// </summary>
        private void AdjustRow()
        {
            if (uiCheckcBoxSingleLine.Checked)
            {
                getAdjust("adjust_single");
                this.uiTableLayoutPanelDesktop.RowCount = 3;
                SetWindowHeight(3);
                labelReportX.Text = ":X";
                labelReportY.Text = "Y:";
                labelReportX.TextAlign = ContentAlignment.MiddleRight;
                labelReportY.TextAlign = ContentAlignment.MiddleRight;
                labelReportX.Dock = DockStyle.Fill;
                labelReportY.Dock = DockStyle.Fill;
                uiIntergerUpDownReportsingleX.Margin = new Padding(0, 2, 10, 2);
                uiIntergerUpDownReportsingleY.Margin = new Padding(0, 2, 10, 2);
                uiIntergerUpDownReportsingleY.Padding = new Padding(2, 0, 2, 0);
                uiIntergerUpDownReportsingleX.Padding = new Padding(2, 0, 2, 0);
                PadTableLayout(labelReportX, 2, 3, 1, 1);
                PadTableLayout(labelReportY, 2, 4, 1, 1);
                PadTableLayout(uiIntergerUpDownReportsingleX, 2, 0, 1, 1);
                PadTableLayout(uiIntergerUpDownReportsingleY, 2, 5, 1, 1);

                uiIntergerUpDownReportsingleX.ValueChanged += new Sunny.UI.UIIntegerUpDown.OnValueChanged(this.SaveNewNum);
                uiIntergerUpDownReportsingleY.ValueChanged += new Sunny.UI.UIIntegerUpDown.OnValueChanged(this.SaveNewNum);
            }
            else
            {
                getAdjust("adjust");
                uiTableLayoutPanelDesktop.Controls.Remove(uiIntergerUpDownReportsingleX);
                uiTableLayoutPanelDesktop.Controls.Remove(uiIntergerUpDownReportsingleY);
                uiTableLayoutPanelDesktop.Controls.Remove(labelReportX);
                uiTableLayoutPanelDesktop.Controls.Remove(labelReportY);
                uiTableLayoutPanelDesktop.RowCount = 2;
                SetWindowHeight(2);
            }
        }

        #region  Font Format Settings;
        private void ChangeFont(object sender, EventArgs e)
        {
            Font oldFont;
            oldFont = this.uTextBox.SelectionFont;
            this.fontDialog.Font = this.uTextBox.Font;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                //newFont = new Font(fontDialog.Font.FontFamily,fontDialog.Font.Size,oldFont.Style);
                uTextBox.SelectionFont = fontDialog.Font;
                uTextBox.Focus();
            }
        }
        private void FontUpper(object sender, EventArgs e)
        {
            string selectText = this.uTextBox.SelectedText;
            Font oldFont = this.uTextBox.Font;
            try
            {
                oldFont = this.uTextBox.SelectionFont;
            }
            catch
            {

            }
            if (oldFont.Size > 20)
            {
                message.ShowInfoDialog("搞这么大干嘛呢？");
            }
            else
            {
                this.uTextBox.SelectionFont = new Font(oldFont.Name, oldFont.Size + 1, oldFont.Style, GraphicsUnit.Point);
            }
            this.uTextBox.Focus();
            this.uTextBox.Select(uTextBox.Text.IndexOf(selectText), selectText.Length);
        }
        private void FontDown(object sender, EventArgs e)
        {
            string selectText = this.uTextBox.SelectedText;
            Font oldFont = this.uTextBox.Font;
            try
            {
                oldFont = this.uTextBox.SelectionFont;
            }
            catch
            {

            }

            if (oldFont.Size < 5)
            {
                message.ShowInfoDialog("搞这么小干嘛呢？");
            }
            else
            {
                this.uTextBox.SelectionFont = new Font(oldFont.Name, oldFont.Size - 1, oldFont.Style, GraphicsUnit.Point);
            }
            this.uTextBox.Focus();
            this.uTextBox.Select(uTextBox.Text.IndexOf(selectText), selectText.Length);
        }
        private void FontBold(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Bold);
        }
        private void FontUnderLine(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Underline);
        }
        private void FontItalic(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Italic);
        }
        private void ResetNote(object sender, EventArgs e)
        {
            this.uTextBox.Text = "";
            this.uTextBox.Font = this.defaultFont;
            this.uTextBox.SelectionFont = this.defaultFont;
        }

        #endregion
        ///<summary>  
        ///设置字体格式：粗体、斜体、下划线  
        ///</summary>  
        /// <param name="style">事件触发后传参：字体格式类型</param>  
        private void ChangeFontStyle(FontStyle style)
        {
            string selectText = this.uTextBox.SelectedText;
            if (style != FontStyle.Bold && style != FontStyle.Italic &&
                style != FontStyle.Underline)
                throw new System.InvalidProgramException("字体格式错误");
            RichTextBox tempRichTextBox = new RichTextBox();   //将要存放被选中文本的副本  
            int curRtbStart = uTextBox.SelectionStart;
            int len = uTextBox.SelectionLength;
            int tempRtbStart = 0;
            Font font = uTextBox.SelectionFont;
            if (len <= 1 && font != null)  //与上边的那段代码类似，功能相同  
            {
                if (style == FontStyle.Bold && font.Bold ||
                    style == FontStyle.Italic && font.Italic ||
                    style == FontStyle.Underline && font.Underline)
                {
                    uTextBox.SelectionFont = new Font(font, font.Style ^ style);
                }
                else if (style == FontStyle.Bold && !font.Bold ||
                        style == FontStyle.Italic && !font.Italic ||
                        style == FontStyle.Underline && !font.Underline)
                {
                    uTextBox.SelectionFont = new Font(font, font.Style | style);
                }
                return;
            }
            tempRichTextBox.Rtf = uTextBox.SelectedRtf;
            tempRichTextBox.Select(len - 1, 1);  //选中副本中的最后一个文字  
                                                 //克隆被选中的文字Font，这个tempFont主要是用来判断  
                                                 //最终被选中的文字是否要加粗、去粗、斜体、去斜、下划线、去下划线  
            Font tempFont = (Font)tempRichTextBox.SelectionFont.Clone();

            //清空2和3  
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);   //每次选中一个，逐个进行加粗或去粗  
                if (style == FontStyle.Bold && tempFont.Bold ||
                    style == FontStyle.Italic && tempFont.Italic ||
                    style == FontStyle.Underline && tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style ^ style);
                }
                else if (style == FontStyle.Bold && !tempFont.Bold ||
                        style == FontStyle.Italic && !tempFont.Italic ||
                        style == FontStyle.Underline && !tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style | style);
                }
            }
            tempRichTextBox.Select(tempRtbStart, len);
            uTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;  //将设置格式后的副本拷贝给原型  
            uTextBox.Select(curRtbStart, len);

            this.uTextBox.Focus();
            this.uTextBox.Select(uTextBox.Text.IndexOf(selectText), selectText.Length);
        }
    }
}
