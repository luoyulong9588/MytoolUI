using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace MytoolUI
{
    public partial class CaseToolUI : Form
    {
        /// <summary>
        /// 委托线程，为了访问主线程创建的控件赋值
        /// </summary>
        /// <param name="text">需要显示到控件的文字</param>
        delegate void SetTextCallback(string text);
        public Bitmap reportCardImage;
        public Bitmap CaseImage;
        public Pain pain = new Pain();
        public Form parent;
        UIMessageForm message = new UIMessageForm();
        object[] widgets;



        public CaseToolUI(Form form, Color color)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            SetDefaultNum();
            parent = form;
            SetBtnColor(color);
            widgets = new object[] {
            uiTextBoxName,uiTextBoxGender,uiTextBoxAge,uiTextBoxId,uiTextBoxIdcard,uiTextBoxPhone,uiTextBoxAddress,uiCheckBoxFill,uiLabelOutPut,uiTextBoxMainDiagnose,uiTextBoxDoctorName};
        }

        /// <summary>
        /// 设置控件颜色主题
        /// </summary>
        /// <param name="color"></param>
        private void SetBtnColor(Color color)
        {
            this.ubtnInvetigation.ForeHoverColor = color;
            this.ubtnInvetigation.ForePressColor = color;
            this.ubtnInvetigation.ForeSelectedColor = color;
            this.ubtnCase.ForeHoverColor = color;
            this.ubtnCase.ForePressColor = color;
            this.ubtnCase.ForeSelectedColor = color;
            this.ubtnReport.ForeHoverColor = color;
            this.ubtnReport.ForePressColor = color;
            this.ubtnReport.ForeSelectedColor = color;
            this.uiCheckBoxContactOnly.CheckBoxColor = color;
            
            this.uiCheckBoxFill.CheckBoxColor = color;
            this.uiCheckBoxContact.CheckBoxColor = color;
        }

        private void ubtnInvetigation_Click(object sender, EventArgs e)
        {
            parent.TopMost = true;
            Thread th1 = new Thread(StartInvetigation);
            th1.Start();
            /// <summary>
            /// 开启多线程，开始从zlbh获取信息，填入到流行病学调查表word中
            /// </summary>
            void StartInvetigation()
            {
                this.OutPutMessage("开启线程..");
                Pain pain0 = null;
                Pain painContact = null;
                /*pain0 = new Pain();
                pain0.Name = "123";
                pain0.Gender = "123";
                pain0.IdCardNumber = "123"; 
                pain0.Phone = "123";
                pain0.Age = "123";
                pain0.HomeAddr = "123";
                pain0.DoctorName = "lyl";
                pain0.Id ="123123";
                pain0.InDay = "123123";
                new Investigation().startProgram(pain0, painContact);
                return;*/
                if (this.uiCheckBoxContactOnly.Checked)
                {
                    parent.TopMost = false;
                    //bool select = this.message.ShowAskDialog($"是否仅填写家属信息?", "是: 请打开门诊工作站，选中家属姓名，再点击继续\r\n否：请点击取消",UIStyle.LightRed,false);
                    DialogResult select = MessageBox.Show("是: 请打开门诊工作站，选中家属（陪护）姓名，再点击继续\r\n否：请点击取消", $"是否仅打印家属（陪护）信息?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (select == DialogResult.OK)
                    {
                        try
                        {
                            new UIAuto(widgets).OutPaient();
                        }
                        catch (Exception ex)
                        {
                            this.message.ShowErrorDialog("家属信息获取失败!",ex.ToString());
                        }
                        new Investigation().startProgram(pain0, painContact);
                        this.OutPutMessage("Success!");
                        return;
                    }
                }
                try
                {
                    pain0 = new UIAuto(widgets).InvestigationAuto();
                }
                catch (Exception ex)
                {
                    parent.TopMost = false;
                    this.OutPutMessage("信息读取错误..");
                    this.message.ShowErrorDialog( "信息读取错误！", ex.Message);
                    return;
                }

                if (pain0 == null)
                {
                    parent.TopMost = false;
                    this.OutPutMessage("未打开中联bh,终止线程..");
                    return;
                }
                this.OutPutMessage("信息获取完毕,正在填卡..");

                if (this.uiCheckBoxContact.Checked)
                {
                    parent.TopMost = false;
                    //bool select = this.message.ShowAskDialog($"{pain0.Name} 是否伴随家属信息?", "是: 请打开门诊工作站，选中家属姓名，再点击继续\r\n否：请点击取消", UIStyle.LightRed,false);
                    DialogResult select = MessageBox.Show("是: 请打开门诊工作站，选中家属姓名，再点击继续\r\n否：请点击取消", $"【{pain0.Name}】是否伴随家属信息?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (select == DialogResult.OK)
                    {
                        try
                        {
                             new UIAuto(widgets).OutPaient();


                        }
                        catch (Exception ex)
                        {
                            this.message.ShowErrorDialog("家属信息获取失败!",ex.ToString());
                        }
                    }
                }
                

                new Investigation().startProgram(pain0, painContact);
                this.OutPutMessage("Success!");
                parent.TopMost = false;
            }
        }
        private void ubtnReport_Click(object sender, EventArgs e)
        {
            pain = null;
            Thread thread = new Thread(StartReportCard);
            thread.Start();
            void StartReportCard()
            {
                DrawImage image;
                /*Pain pain = new Pain();
                pain.Name = "张三";
                pain.Age = "14";
                pain.Gender = "male";
                pain.HomeAddr = "BigRod";
                pain.IdCardNumber = "123456";
                pain.Id = "123";
                pain.DoctorName = "Lucy";
                pain.InDay = "2020年2月2日";
                pain.OutDay = "2021年2月2日";
                pain.Phone = "4869";*/

                this.OutPutMessage("开启线程..");

                this.OutPutMessage("尝试从zlbh或许信息..");
                try
                {
                    pain = new UIAuto(widgets).CaseAuto();
                }
                catch (Exception ex)
                {
                    this.OutPutMessage("查找窗体错误!");
                    this.message.ShowErrorDialog("查找窗体错误!",ex.ToString());
                }

                if (pain == null)
                {
                    this.OutPutMessage("未打开中联bh,终止线程..");
                    return;
                }
                this.OutPutMessage("信息获取完毕,正在绘图..");
             
               image = DrawReportCard(pain, float.Parse(uiIntegerUpDownReportX.Value.ToString()), float.Parse(uiIntegerUpDownReportY.Value.ToString()));
         
                this.reportCardImage = image.image;
                this.OutPutMessage("绘图完毕,发送到打印机..");
                PrintImage(printDocumentReport);
                this.OutPutMessage("ok ok ok");
            }
        }
        private void ubtnCase_Click(object sender, EventArgs e)
        {
            pain = null;
            Thread thread = new Thread(StartCase);
            thread.Start();
            void StartCase()
            {
                #region the patient message for quick debug;
                /*Pain pain = new Pain();
                pain.Name = "张三";
                pain.Age = "14";
                pain.Gender = "male";
                pain.HomeAddr = "BigRod";
                pain.IdCardNumber = "123456";
                pain.Id = "123";
                pain.DoctorName = "Lucy";
                pain.InDay = "2020年2月2日";
                pain.OutDay = "2021年2月2日";
                pain.Phone = "4869";*/
                #endregion
                this.OutPutMessage("开启线程..");
                this.OutPutMessage("尝试从zlbh或许信息..");
                try
                {
                    
                    pain = new UIAuto(widgets).CaseAuto();
                }
                catch (Exception ex)
                {

                    this.OutPutMessage("未打开中联bh,终止线程..");
                    this.message.ShowErrorDialog("查找窗体错误!",ex.ToString());

                }
                
                if (pain == null)
                {
                    this.OutPutMessage("未打开中联bh,终止线程..");
                    return;
                }
                OutPutMessage("信息获取完毕,正在绘图..");

               DrawImage image = DrawCase(pain, float.Parse(uiIntegerUpDownCaseX.Value.ToString()), float.Parse(uiIntegerUpDownCaseY.Value.ToString()));


                
                this.CaseImage = image.image;
                this.OutPutMessage("绘图完毕,发送到打印机..");
                PrintImage(printDocumentCase);
                this.OutPutMessage("ok ok ok");
            }
        }

        /// <summary>
        /// 感染病例报卡绘制(单行)
        /// </summary>
        /// <param name="pain">患者信息类</param>
        /// <param name="x">x轴打印偏移</param>
        /// <param name="y">y轴打印偏移</param>
        /// <returns></returns>
        public static DrawImage DrawReportCard(Pain pain, float x, float y)
        {
            int line7Y = 3050;
            DateTime inDay = DateTime.Parse(pain.InDay);
            DateTime outDay = DateTime.Parse(pain.OutDay);
            DateTime now = DateTime.Now;
            DrawImage image = new DrawImage();
            image.Draw(pain.Name, 448 + x, 485 + y);
            image.Draw(pain.Id, 855 + x, 485 + y);
            image.Draw(inDay.ToString("yyyy"), 1380 + x, 485 + y);
            image.Draw(inDay.ToString("MM"), 1670 + x, 485 + y);
            image.Draw(inDay.ToString("dd"), 1890 + x, 485 + y);
            image.Draw(pain.Gender, 448 + x, 620 + y);
            image.Draw(pain.Age, 855 + x, 620 + y);
            image.Draw("——", 1390 + x, 620 + y);
            image.Draw("—", 1690 + x, 620 + y);
            image.Draw("—", 1890 + x, 620 + y);
            image.Draw(pain.MainDiagnose, 420 + x, 760 + y);

            image.Draw("——", 1450 + x, 750 + y);

            image.Draw("——", 520 + x, 890 + y);
            image.Draw("——", 820 + x, 890 + y);

            image.Draw("——", 460 + x, 1300 + y);
            image.Draw("——", 840 + x, 1300 + y);

            image.Draw("普内科", 420 + x, 1425 + y);
            image.Draw(pain.DoctorName, 845 + x, 1425 + y);

            image.Draw(outDay.ToString("yyyy"), 1550 + x, line7Y + y);
            image.Draw(outDay.ToString("MM"), 1750 + x, line7Y + y);
            image.Draw(outDay.ToString("dd"), 1915 + x, line7Y + y);
            image.g.Dispose();
            return image;
        }

        /// <summary>
        /// 新版【感染病例报告卡(双层标题)】的打印函数
        /// </summary>
        /// <param name="pain"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static DrawImage DrawReportCardNew(Pain pain, float x, float y)
        {
            int line1Y = 580;
            int line2Y = 730;
            int line3Y = 880;
            int line4Y = 1050;
            int line5Y = 1490;
            int line6Y = 1650;
            int line7Y = 3250;

            DateTime inDay = DateTime.Parse(pain.InDay);
            DateTime outDay = DateTime.Parse(pain.OutDay);
            DateTime now = DateTime.Now;
            DrawImage image = new DrawImage();
            image.Draw(pain.Name, 450 + x, line1Y + y);
            image.Draw(pain.Id, 900 + x, line1Y + y);

            image.Draw(inDay.ToString("yyyy"), 1500 + x, line1Y + y);
            image.Draw(inDay.ToString("MM"), 1750 + x, line1Y + y);
            image.Draw(inDay.ToString("dd"), 1950 + x, line1Y + y);

            image.Draw(pain.Gender, 450 + x, line2Y + y);
            image.Draw(pain.Age, 880 + x, line2Y + y);

            image.Draw("——", 1500 + x, line2Y + y);
            image.Draw("—", 1750 + x, line2Y + y);
            image.Draw("—", 1950 + x, line2Y + y);

            image.Draw(pain.MainDiagnose, 420 + x, line3Y + y);

            image.Draw("——", 1500 + x, line3Y + y);

            image.Draw("——", 600 + x, line4Y + y);
            image.Draw("——", 840 + x, line4Y + y);

            image.Draw("——", 460 + x, line5Y + y);
            image.Draw("——", 840 + x, line5Y + y);

            image.Draw("普内科", 430 + x, line6Y + y);
            image.Draw(pain.DoctorName, 950 + x, line6Y + y);

            image.Draw(outDay.ToString("yyyy"), 1550 + x, line7Y + y);
            image.Draw(outDay.ToString("MM"), 1850 + x, line7Y + y);
            image.Draw(outDay.ToString("dd"), 2100 + x, line7Y + y);
            image.g.Dispose();
            return image;
        }

        /// <summary>
        /// 病历质量报卡绘制
        /// </summary>
        /// <param name="pain">患者信息类</param>
        /// <param name="x">x轴偏移</param>
        /// <param name="y">y轴偏移</param>
        /// <returns></returns>
        public static DrawImage DrawCase(Pain pain, float x, float y)
        {
            DrawImage image = new DrawImage();
            image.Draw("全科医学科", 260 + x, 150 + y);
            image.Draw(pain.Name, 700 + x, 150 + y);
            image.Draw(pain.DoctorName, 1200 + x, 150 + y);
            image.Draw(pain.Id, 1550 + x, 150 + y);
            image.g.Dispose();
            return image;
        }

        #region 打印机的设置
        public static void PrintImage(System.Drawing.Printing.PrintDocument printDocument)
        {
            PrintDialog MyPrintDg = new PrintDialog();
            MyPrintDg.Document = printDocument;
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custum", 2480, 3508);
            printDocument.DefaultPageSettings.PrinterResolution.X = 300;
            printDocument.DefaultPageSettings.PrinterResolution.Y = 300;
            if (MyPrintDg.ShowDialog() == DialogResult.OK)  // 后面改为姓名提示;
            {
                try
                {
                    printDocument.Print();
                }
                catch
                {   //停止打印
                    printDocument.PrintController.OnEndPrint(printDocument, new System.Drawing.Printing.PrintEventArgs());
                }
            }

        }
        private void printDocumentReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(this.reportCardImage, 0, 0);
        }
        private void printDocumentCase_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(this.CaseImage, 0, 0);
        }
        #endregion 

        private void SetDefaultNum()
        {
            List<string> adjustList = new DatabaseUnit().GetAdjustNum("adjust");
            try
            {
                uiIntegerUpDownReportX.Value = int.Parse(adjustList[0]);
                uiIntegerUpDownReportY.Value = int.Parse(adjustList[1]);
                uiIntegerUpDownCaseX.Value = int.Parse(adjustList[2]);
                uiIntegerUpDownCaseY.Value = int.Parse(adjustList[3]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void SaveNewNum(object sender, int value)
        {
            string[] adjustList = new string[4];
            adjustList[0] = uiIntegerUpDownReportX.Value.ToString();
            adjustList[1] = uiIntegerUpDownReportY.Value.ToString();
            adjustList[2] = uiIntegerUpDownReportX.Value.ToString();
            adjustList[3] = uiIntegerUpDownCaseY.Value.ToString();
            new DatabaseUnit().SetAdjustNum(adjustList, "adjust");
        }
        private void OutPutMessage(string text)
        {
            if (this.uiLabelOutPut.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(OutPutMessage);
                this.uiLabelOutPut.Invoke(d, new object[] { text });
            }
            else
            {
                this.uiLabelOutPut.Text = text;
            }
        }

        private void CaseToolUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.TopMost = false;
        }
        #region  首页填充、合并联系人、单独联系人的逻辑判断

        private void uiCheckBoxContact_CheckedChanged(object sender, EventArgs e)
        {
            if (uiCheckBoxContact.Checked)
            {
                uiCheckBoxContactOnly.Checked = false;
            }
        }

        private void uiCheckBoxContactOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (uiCheckBoxContactOnly.Checked)
            {
                uiCheckBoxContact.Checked = false;
                uiCheckBoxFill.Checked = false;
            }
        }

        private void uiCheckBoxFill_CheckedChanged(object sender, EventArgs e)
        {
            if (uiCheckBoxContactOnly.Checked&&uiCheckBoxFill.Checked)
            {
                uiCheckBoxContactOnly.Checked = false;
            }
        }
        #endregion
    }

}
