using System;
using System.Drawing;
using System.Windows.Forms;
using Sunny.UI;

namespace MytoolUI
{
    public partial class BloodGasUI : Form
    {
        private double ph, pco2, po2, na, cl, ab, ag, fio2, ri;
        private bool acidosis;
        private Color color;
        private Form currentChild;
        private string test;
        UIMessageForm message = new UIMessageForm();
        public BloodGasUI(Color color)
        {
            InitializeComponent();
            for (int i = 21; i < 101; i++)
            {
                ucomBoxO2percent.Items.Add(i.ToString());
            }
            ucomBoxO2percent.SelectedItem = "21";
            this.color = color;
            setDefaultMessage();
            SetBtnColor();

        }

        private void SetBtnColor()
        {
            ubtnClear.ForeHoverColor = color;
            ubtnClear.ForePressColor = color;
            ubtnClear.ForeSelectedColor = color;
            ubtnCopy.ForeHoverColor = color;
            ubtnCopy.ForePressColor = color;
            ubtnCopy.ForeSelectedColor = color;
            ubtnHelp.ForeHoverColor = color;
            ubtnHelp.ForePressColor = color;
            ubtnHelp.ForeSelectedColor = color;
            ubtnStart.ForeHoverColor = color;
            ubtnStart.ForePressColor = color;
            ubtnStart.ForeSelectedColor = color;

            uiRadioButtonQian.RadioButtonColor = color;
            uiRadioButtonDiag.RadioButtonColor = color;
            uiRadioButtonZhong.RadioButtonColor = color;

        }

        private void setDefaultMessage()
        {

            richTextBoxOut.ResetText();
            string info = "1.该工具提供简便血气分析,红色为必填项目。\r\n" +
                        "2.该工具默认仅对慢性酸碱失衡样本进行计算,如填入为急性酸碱失衡样本,其结果不可信!\r\n" +
                        "3.计算结果仅供参考,请以临床实际情况为准。\r\n" +
                        "4.不同公式计算结果可能有较小差异!\r\n" +
                        "提示:可使用tab快速切换输入焦点!\r\n";
            richTextBoxOut.AppendText(info);
        }


        private void FormBloodGas_Load(object sender, EventArgs e)
        {

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();

            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
.SetValue(tableLayoutPanel1, true, null);
        }

        private void ubtnStart_Click(object sender, EventArgs e)
        {

            StartProgram();

        }
        private void StartProgram()
        {
            if (!CheckNecessaryValue())
            {
                return;
            }
            richTextBoxOut.ResetText();
            Henderson_hasselbach();
            richTextBoxOut.AppendText(Respiratory_failure() + "\r"); // 呼吸衰竭判断

            string result = diagNostics();
            richTextBoxOut.AppendText(result + "\r");
            CheckSupplementingCaustic();
            richTextBoxOut.AppendText("查看更多信息请点击帮助按钮或按F1" + "\r");
            SaveToDataBase(result);
        }

        private bool CheckNecessaryValue()
        {
            try
            {
                ph = double.Parse(uiTextBoxPh.Text);
                po2 = double.Parse(uiTextBoxPO2.Text);
                pco2 = double.Parse(uiTextBoxPCO2.Text);
                na = double.Parse(uiTextBoxNa.Text);
                cl = double.Parse(uiTextBoxCl.Text);
                ab = double.Parse(uiTextBoxAb.Text);
                ag = double.Parse(uiTextBoxAg.Text);
                return true;
            }
            catch (Exception ex)
            {
                this.message.ShowWarningDialog("ph、Po2、Pco2、Na、Cl、Ab、Ag必须赋值！\n\n" + ex, UIStyle.LightRed);
                return false;
            }
        }

        private void SaveToDataBase(string result)
        {
            double sb = 0, k = 0, glu = 0, be = 0;
            if (!double.TryParse(uiTextBoxSb.Text, out sb)) { Console.WriteLine("Sb无值"); }
            if (!double.TryParse(uiTextBoxK.Text, out k)) { Console.WriteLine("K无值"); } 
            if (!double.TryParse(uiTextBoxGlu.Text, out glu)) { Console.WriteLine("Glu无值"); } 
            if (!double.TryParse(uiTextBoxBe.Text, out be)) { Console.WriteLine("Be无值"); } 
            string sqlString = $"insert into bloodgas (ph,co2,o2,na,ab,sb,cl,ag,k,glu,be,result) VALUES({ph},{pco2},{po2},{na},{ab},{sb},{cl},{ag},{k},{glu},{be},'{result}') ";
            DatabaseUnit db = new DatabaseUnit();
            db.SaveBloodGasResult(sqlString);

        }



        private void CheckSupplementingCaustic()
        {
            if (ph < 7.10 || ab < 10)
            {
                richTextBoxOut.AppendText("请注意是否需要补碱" + "\r");
            }

        }
        private void Henderson_hasselbach()
        {
            double h = 24 * pco2 / ab;
            string result = "酸碱失衡超出预期，无法根据Henderson-Hasselbach公式校验，请检查结果";
            if (22 <= h && h < 25)
            {
                if (7.6 < ph && ph <= 7.65)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.60, 7.65);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.60, 7.65);
                }
            }
            else if (25 <= h && h < 28)
            {
                if (7.55 < ph && ph <= 7.6)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.55, 7.60);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.65, 7.60);
                }
            }
            else if (28 <= h && h < 32)
            {
                if (7.50 < ph && ph <= 7.55)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.50, 7.55);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.50, 7.55);
                }
            }
            else if (32 <= h && h < 35)
            {
                if (7.45 < ph && ph <= 7.50)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.45, 7.50);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.45, 7.50);
                }
            }
            else if (35 <= h && h < 40)
            {
                if (7.40 < ph && ph <= 7.45)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.40, 7.45);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.40, 7.45);
                }
            }
            else if (40 <= h && h < 45)
            {
                if (7.35 < ph && ph <= 7.40)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.35, 7.40);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.35, 7.40);
                }
            }
            else if (45 <= h && h < 50)
            {
                if (7.30 < ph && ph <= 7.35)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.30, 7.35);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.30, 7.35);
                }
            }
            else if (50 <= h && h < 56)
            {
                if (7.25 < ph && ph <= 7.30)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.25, 7.30);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.25, 7.30);
                }
            }
            else if (56 <= h && h < 63)
            {
                if (7.20 < ph && ph <= 7.25)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.20, 7.25);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.20, 7.25);
                }
            }
            else if (63 <= h && h < 71)
            {
                if (7.15 < ph && ph <= 7.20)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.15, 7.20);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.15, 7.20);
                }
            }
            else if (71 <= h && h < 79)
            {
                if (7.10 < ph && ph <= 7.15)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.10, 7.15);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.10, 7.15);
                }
            }
            else if (79 <= h && h < 89)
            {
                if (7.05 < ph && ph <= 7.10)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.05, 7.10);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.05, 7.10);
                }
            }
            else if (89 <= h && h < 100)
            {
                if (7.00 < ph && ph <= 7.05)
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验通过\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.00, 7.05);
                }
                else
                {
                    result = string.Format("根据Henderson-Hasselbach公式，内在一致性校验失败，结果可能不准确！\n[H+]：{0:0.00}(mmol/L);PH参考区间：[{1}, {2})", h, 7.00, 7.05);
                }
            }
            richTextBoxOut.AppendText(result + "\n");
        }

        private string Respiratory_failure()
        {
            string result = "", oxygen = null;

            fio2 = double.Parse(ucomBoxO2percent.SelectedItem.ToString());
            ri = 100 * po2 / fio2;
            // if (ucomBoxO2percent.SelectedItem.ToString() == "21") {
            //     oxygen = "未吸氧状态";
            // }
            // else
            // {
            //     oxygen = "吸氧状态";
            // }
            oxygen = ucomBoxO2percent.SelectedItem.ToString() == "21" ? "未吸氧状态" : "吸氧状态";

            if (po2 >= 60 && pco2 <= 50)
            {
                result = ri >= 300 ? "无呼吸衰竭" : "呼吸指数<300,病例可能存在ARDS或低氧血症!";
            }
            else if (po2 > 60 && pco2 > 50)
            {
                result = ucomBoxO2percent.SelectedItem.ToString() == "21" ? "低通气综合征（高碳酸血症）" : "II型呼吸衰竭(吸氧状态)";
            }
            else if (po2 < 60 && pco2 <= 50)
            {
                result = "I型呼吸衰竭";
            }
            else if (po2 < 60 && pco2 > 50)
            {
                result = "II型呼吸衰竭";
            }
            this.print($"PO2({po2}) {(po2 < 60 ? "<" : ">")} 60 && PCO2({pco2}) {(pco2 > 50 ? ">" : "<=")} 50,{oxygen}");
            return result;



            //     if (po2 >= 60 && pco2 <= 50)
            // {
            //     if (ri >= 300)
            //     {
            //         this.print("呼吸指数>300,PCO2、PO2未达到呼吸衰竭指标");
            //         result = "无呼吸衰竭";
            //     }
            //     else
            //     {
            //         result = "呼吸指数<300,病例可能存在ARDS或低氧血症!";
            //     }
            // }
            // else if (po2 > 60 && pco2 > 50)
            // {
            //     if (ucomBoxO2percent.SelectedItem.ToString() == "21")
            //     {
            //         this.print($"PO2({po2}) > 60 && PCO2({pco2}) > 50,{oxygen}");
            //         result = "低通气综合征（高碳酸血症）";
            //     }
            //     else
            //     {
            //         this.print($"PO2({po2}) > 60 && PCO2({pco2}) > 50,{oxygen}");
            //         result = "II型呼吸衰竭(吸氧状态)";
            //     }
            // }
            // else if (po2 < 60 && pco2 <= 50)
            // {
            //     this.print($"PO2({po2}) < 60 && PCO2({pco2}) ≤ 50,{oxygen}");
            //     result = "I型呼吸衰竭";
            // }
            // else if (po2 < 60 && pco2 > 50)
            // {
            //     this.print($"PO2({po2}) < 60 && PCO2({pco2}) > 50,{oxygen}");
            //     result = "II型呼吸衰竭";
            // }
            // return result;
        }

        private void print(string message)
        {
            richTextBoxOut.AppendText(message + "\r");
        }

        private string diagNostics()
        {
            Interval<double> phNormal = new Interval<double>(7.35, 7.45);
            Interval<double> co2Normal = new Interval<double>(35, 45);
            Interval<double> hco3Normal = new Interval<double>(22, 27);
            Interval<double> agNormal = new Interval<double>(12, 16);
            Interval<double> possibleHco3Normal = new Interval<double>(22, 27);
            Interval<double> hco3Interval, co2Interval;
            //double deltaAg = ag = 12;
            double deltaAg = (double)(ag - agNormal.End);
            double possibleHco3 = deltaAg + ab;
            this.print($"计算潜在HCO3:[HCO3 = deltaAg({deltaAg}) + ab({ab}) = {possibleHco3}]");
            string result = "";
            if (ph < phNormal.Start) // 酸中毒
            {
                this.print($"ph < {phNormal.Start},判定原发酸中毒");   
                if (pco2 > 40)
                {
                    double deltaCo2 = pco2 - 40;
                    hco3Interval = getHco3(true, deltaCo2);
                    if (hco3Interval.InRange(ab) && ag < agNormal.End)
                    {
                        this.print("实际碳酸氢盐在代偿区间内,判断阴离子间隙无升高");
                        result = "(单纯)呼吸性酸中毒";
                    }
                    else if (ab > hco3Interval.Start && ag < agNormal.End) //# ???反向计算是否单纯失衡;
                    {
                        this.print("实际碳酸氢盐未在代偿区间内,判断阴离子间隙无升高");
                        result = "呼吸性酸中毒合并代谢性碱中毒";
                    }
                    else
                    {
                        result = "呼吸性酸中毒合并代谢性酸中毒(或)[代谢性酸中毒合并呼吸性酸中毒]";
                        this.acidosis = true;
                    }

                }
                else //# 排除呼吸性酸中毒;但PH<7.35，肯定是代谢性酸中毒->进一步判断是否合并呼吸性碱中毒;
                {
                    
                    this.acidosis = true;
                    co2Interval = getCo2(acidosis:true, hco3: ab);
                    if (co2Interval.InRange(pco2))
                    {
                        this.print("比较: PCO2在代偿区间内");
                        result = "(单纯)代谢性酸中毒";
                    }
                    else if (pco2 < co2Interval.Start)
                    {
                        this.print("比较: PCO2 < 代偿区间最小值");
                        if (ab < hco3Normal.Start)
                        {
                            result = "代谢性酸中毒合并呼吸性碱中毒";
                        }
                        else
                        {
                            result = "样本有误?";
                        }
                    }
                    else
                    {
                        this.print("比较: PCO2 > 代偿区间最大值");
                        result = "代谢性酸中毒合并呼吸性碱中毒(或) [呼吸性碱中毒合并代谢性酸中毒]";
                    }
                }
            }
            else if (ph > phNormal.End)
            {
                this.print($"ph > {phNormal.End},判定原发碱中毒");
                if (pco2 > 40)
                {
                    co2Interval = getCo2(acidosis: false,hco3: ab);
                    if (co2Interval.InRange(pco2))
                    {
                        this.print("比较: PCO2 在代偿区间内,无多重酸碱失衡");
                        result = "(单纯)代谢性碱中毒";
                    }
                    else if (pco2 > co2Interval.End)
                    {
                        this.print($"比较: PCO2 > 代偿区间最大值,存在多重酸碱失衡,PCO2存在原发升高");
                        if (ab > hco3Normal.End)
                        {
                            result = "代谢性碱中毒合并呼吸性酸中毒(失代偿)";
                        }
                        else
                        {
                            result = "样本有误?";
                        }
                    }
                    else
                    {
                        this.print($"比较: PCO2 < 代偿区间最小值,存在多重酸碱失衡，PCO2存在原发降低");
                        result = "代谢性碱中毒合并呼吸性碱中毒(失代偿)(或) [呼吸性碱中毒合并代谢性碱中毒]";
                    }
                }
                else //不确定是否有代谢性碱中毒;默认以呼吸性碱中毒来计算;
                {
                    this.print("不确定是否有代谢性碱中毒;默认以呼吸性碱中毒来计算");
                    hco3Interval = getHco3(false, (40 - pco2));
                    if (hco3Interval.InRange(ab))
                    {
                        result = "(单纯)呼吸性碱中毒";
                    }
                    else if (ab < hco3Interval.Start)
                    {
                        result = "呼吸性碱中毒合并代谢性酸中毒(失代偿)";
                        this.acidosis = true;
                    }
                    else
                    {
                        result = "呼吸性碱中毒合并代谢性碱中毒(失代偿)(或)[代谢性碱中毒合并呼吸性碱中毒(失代偿)]";
                    }
                }
            }
            else //[7.35,7.45]
            {
                this.print("Ph 在正常范围内");
                if (co2Normal.InRange(pco2))
                {
                    if (hco3Normal.InRange(ab))
                    {
                        result = "无酸碱失衡";

                    }
                    else if (ab < hco3Normal.Start)
                    {
                        result = "代谢性酸中毒";
                        this.acidosis = true;
                    }
                    else if (ab > hco3Normal.End)
                    {
                        result = "代谢性碱中毒";
                    }
                }
                else if (pco2 < co2Normal.Start)
                {
                    if (hco3Normal.InRange(ab))
                    {
                        result = "呼吸性碱中毒";
                    }
                    else if (ab < hco3Normal.Start)
                    {
                        result = "呼吸性碱中毒合并代谢性酸中毒";
                    }
                    else if (ab > hco3Normal.End)
                    {
                        if (ph >= 7.40)
                        {
                            result = "代谢性碱中毒合并呼吸性碱中毒(不便于判断的原发失衡)";
                        }
                        else
                        {
                            result = "标本有误?";
                        }
                        this.acidosis = true;
                    }
                }
                else if (pco2 > co2Normal.End)
                {
                    if (hco3Normal.InRange(ab))
                    {
                        result = "呼吸性酸中毒";
                    }
                    else if (ab < hco3Normal.Start)
                    {
                        result = "呼吸性酸中毒合并代谢性酸中毒";
                        this.acidosis = true;
                    }
                    else if (ab > hco3Normal.End)
                    {
                        if (ph < 7.4)
                        {
                            result = "呼吸性酸中毒合并代谢性碱中毒(代偿)";
                        }
                        else
                        {
                            result = "代谢性碱中毒合并呼吸性酸中毒(代偿)";
                        }
                    }
                }
            }
            if (result.Contains("样本有误"))
            {
                return result;
            }
            //this.print("最后判断是否合并阴离子间隙升高的酸碱失衡");
            if (this.acidosis && (ag > agNormal.End))
            {
                if (possibleHco3 > hco3Normal.End)
                {
                    if (result.Contains("代谢性碱中毒"))
                    {
                        if (result.Contains("代谢性酸中毒"))
                        {
                            result = result.Replace("代谢性酸中毒", "(AG增高)代谢性酸中毒");
                        }
                        else
                        {
                            result = result + "(AG增高)代谢性酸中毒";
                        }
                    }
                }
                else
                {
                    if (result.Contains("代谢性酸中毒"))
                    {
                        result = result.Replace("代谢性酸中毒", "(AG增高)代谢性酸中毒");
                    }
                    else
                    {
                        result = result + "(AG增高)代谢性酸中毒";
                    }
                }
            }
            else if (this.acidosis && ag < agNormal.End)
            {
                if (possibleHco3 < hco3Normal.Start)
                {
                    if (result.Contains("代谢性酸中毒"))
                    {

                        result = result.Replace("代谢性酸中毒", "(AG正常)代谢性酸中毒");
                    }
                    else
                    {
                        result = result + "(AG正常)代谢性酸中毒";
                    }
                }
                else if (possibleHco3 > hco3Normal.End)
                {
                    if (result.Contains("代谢性碱中毒"))
                    {
                    }
                    else
                    {
                        result = result + "(并存)代谢性碱中毒";
                    }
                }
            }
            //else if (ph<7.35 && ag>agNormal.End)
            //{
            //    result += "(可能)并存高Ag代谢性酸中毒";
            //}
            return result;
        }

        private Interval<double> getHco3(bool acidosis, double deltaCo2)
        {
            /*只有呼吸性酸碱失衡才会调用潜在HCO3的计算
            : param acidosis: 是否为酸中毒;
            :param delta_co2: 变化的CO2;
            :return: 碳酸氢根变化区间;*/
            double min_hco3 = 0, max_hco3 = 0;
            if (acidosis)
            {
                //内科学 和 钱桂生\诊断学均一样;
                min_hco3 = deltaCo2 * 0.35 - 5.58;
                max_hco3 = deltaCo2 * 0.35 + 5.58;
                
            }
            else
            {
                if (uiRadioButtonZhong.Checked)
                {
                    min_hco3 = deltaCo2 * 0.49 - 1.72;
                    max_hco3 = deltaCo2 * 0.49 + 1.72;
                    this.print("本次计算代偿公式:[ΔHCO3 = ΔPCO2 * 0.49 ± 1.72]");
                }
                else if (uiRadioButtonDiag.Checked || uiRadioButtonDiag.Checked)
                {
                    min_hco3 = deltaCo2 * 0.5 - 2.5;
                    max_hco3 = deltaCo2 * 0.5 + 2.5;
                    this.print("本次计算代偿公式:[ΔHCO3 = ΔPCO2 * 0.5 ± 2.5]");
                }
            }
            Interval<double> result = new Interval<double>(24 - max_hco3, 24 + max_hco3);
            this.print($"根据PCO2计算ΔHCO3代偿区间:[{result.Start}:{result.End}]");
            return result;

        }


        private Interval<double> getCo2(bool acidosis, double hco3 = 0)
        {
            double min_co2 = 0, max_co2 = 0;
            double deltaHco3 = Math.Abs(24 - hco3);
            Interval<double> result;
            if (acidosis)
            {
                min_co2 = hco3 * 1.5 + 8 - 2;
                max_co2 = hco3 * 1.5 + 8 + 2;
                this.print("本次计算代偿公式:[PCO2 = hco3 * 1.5 + 8 ± 2]");
                result = new Interval<double>(min_co2, max_co2);
                this.print($"代谢性酸中毒时，根据AbHCO3计算PCO2区间:[{result.Start}:{result.End}]");
                return result;
            }
            else // 非酸中毒
            {
                if (uiRadioButtonDiag.Checked)
                {
                    min_co2 = deltaHco3 * 0.9 - 1.5;
                    max_co2 = deltaHco3 * 0.9 + 1.5;
                    this.print("本次计算代偿公式:[ΔPCO2 = Δhco3 * 0.9 ± 1.5]");
                }
                else
                {
                    min_co2 = hco3 * 0.9 - 5;
                    max_co2 = hco3 * 0.9 + 5;
                    this.print("本次计算代偿公式:[ΔPCO2 = hco3 * 0.9 ± 5]");
                }
            }
            result = new Interval<double>(40 - max_co2, 40 + max_co2);
            this.print($"非酸中毒时，根据AbHCO3计算ΔPCO2区间:[{result.Start}:{result.End}]");
            return result;
        }

        private void copyString(object sender = null, EventArgs e = null)
        {
            string result = string.Format("Ph:{0}、PCO2:{1}mmHg、PO2:{2}mmHg、Na+:{3}mmHg、Cl:{4}mmHg、", ph, pco2, po2, na, cl);

            //result = uiTextBoxK.Text == "" ? result : (result + "K:" + uiTextBoxK.Text + "mmol/L、");


            if (uiTextBoxK.Text != "")
            {
                result = result + "K:" + uiTextBoxK.Text + "mmol/L、";
            }
            if (uiTextBoxGlu.Text != "")
            {
                result = result + "Glu:" + uiTextBoxGlu.Text + "mmol/L、";
            }
            if (uiTextBoxSb.Text != "")
            {
                result = result + "Sb:" + uiTextBoxSb.Text + "mmol/L、";
            }
            if (uiTextBoxBe.Text != "")
            {
                result = result + "Be:" + uiTextBoxBe.Text + "mmol/L、";
            }
            if (uiTextBoxPOP.Text != "")
            {
                result = result + "POP:" + uiTextBoxPOP.Text + "mOsm/L、";
            }
            if (ph == 0)
            {
                this.message.ShowInfoDialog("还没开始计算,没有内容可复制！");
                return;
            }
            else
            {
                richTextBoxOut.ResetText();
                richTextBoxOut.AppendText(result);
                Clipboard.SetText(result);
                this.message.ShowInfoDialog("成功,输入信息已经复制到剪切板！");
            }

        }

        private void limitTxtInput(object sender, KeyPressEventArgs e)
        {
            //ACSII中48-57表示数字:0-9
            //ACSII中8表示退格键（删除键）
            //ACSII中46表示小数点
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;

            //小数点的处理。
            if ((int)e.KeyChar == 46 || (int)e.KeyChar == 45) //小数点 或负号
            {
                if (this.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位

                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(this.Text, out oldf);
                    b2 = float.TryParse(this.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }
        #region 限制输入浮点数

        private void uiTextBoxPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }
        private void uiTextBoxPCO2_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxPO2_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxNa_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxCl_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxK_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }
        private void uiTextBoxGlu_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxAb_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxSb_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxBe_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxAg_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        private void uiTextBoxPOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            limitTxtInput(sender, e);
        }

        #endregion


        private void ubtnClear_Click(object sender, EventArgs e)
        {
            uiTextBoxPh.Text = "";
            uiTextBoxPCO2.Text = "";
            uiTextBoxPO2.Text = "";
            uiTextBoxNa.Text = "";
            uiTextBoxK.Text = "";
            uiTextBoxCl.Text = "";
            uiTextBoxAb.Text = "";
            uiTextBoxSb.Text = "";
            uiTextBoxBe.Text = "";
            uiTextBoxPOP.Text = "";
            uiTextBoxGlu.Text = "";
            uiTextBoxAg.Text = "";
            setDefaultMessage();
        }


        private void TextboxEnter(object sender, EventArgs e)
        {
            UITextBox texBox = (UITextBox)sender;
            texBox.RectColor = Color.Red;
        }
        private void TextboxLeave(object sender, EventArgs e)
        {
            UITextBox texBox = (UITextBox)sender;
            texBox.RectColor = Color.FromArgb(64, 64, 64);
        }

        private void ToolStripMenuItemHistory_Click(object sender, EventArgs e)
        {
            OpenHistoryrForm(new BloodGasHistoryUI());  // 打开历史记录 窗口
        }

        private void OpenHistoryrForm(Form childForm)
        {
            if (currentChild != null)
            {
                currentChild.Close();
            }
            currentChild = childForm;
            childForm.TopLevel = true;
            childForm.BringToFront();
            childForm.Show();
        }

        private void toolStripMenuItemStart_Click(object sender, EventArgs e)
        {
            richTextBoxOut.ResetText();
            StartProgram();
        }

        private void SHowHelp(object sender = null, EventArgs e = null)
        {
            try
            {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\config\\help.chm");
            }
            catch (Exception)
            {
                string title = "帮助信息";
                string help = "1.该工具提供简便血气分析,红色为必填项目。\r\n" +
                         "2.该工具默认仅对慢性酸碱失衡样本进行计算," +
                         "如填入为急性酸碱失衡样本,其结果不可信!\r\n" +
                         "3.计算结果仅供参考,请以临床实际情况为准。\r\n" +
                         "4.不同公式计算结果可能有较小差异!\r\n" +
                         "提示:可使用tab快速切换输入焦点!\r\n\r\n";
                string message = "公式说明:\r\n①钟南山《呼吸病学》第二版公式\r\n" +
                           "呼吸性酸中毒：\r\n" +
                           "急性：代偿引起HCO3-升高3-4mmHg\r\n" +
                           "慢性：ΔHCO3-=ΔPaCO2x0.35±5.58\r\n" +
                           "呼吸性碱中毒：\r\n" +
                           "急性：ΔHCO3-=ΔPaCO2x0.2±2.5\r\n" +
                           "慢性：ΔHCO3-=ΔPaCO2x0.49±1.72\r\n" +
                           "代谢性酸中毒：\r\n" +
                           "PaCO2=HCO3-x1.5+8±2\r\n" +
                           "代谢性碱中毒：\r\n" +
                           "ΔPaCO2=HCO3-x0.9±5\r\n" +
                           "--------------------------------------------\r\n" +
                           "②钱桂生《中华肺部疾病杂志》2010.04版公式\r\n" +
                           "呼吸性酸中毒：\r\n" +
                           "急性：代偿引起HCO3-升高3-4mmHg\r\n" +
                           "慢性：ΔHCO3-=ΔPaCO2x0.35±5.58\r\n" +
                           "呼吸性碱中毒：\r\n" +
                           "急性：ΔHCO3-=ΔPaCO2x0.2±2.5\r\n" +
                           "慢性：ΔHCO3-=ΔPaCO2x0.5±2.5\r\n" +
                           "代谢性酸中毒：\r\n" +
                           "PaCO2=HCO3-x1.5+8±2\r\n" +
                           "代谢性碱中毒：\r\n" +
                           "ΔPaCO2=HCO3-x0.9±5\r\n" +
                           "--------------------------------------------\r\n" +
                           "③《诊断学》第八版公式\r\n" +
                           "呼吸性酸中毒：\r\n" +
                           "急性：ΔHCO3-=ΔPaCO2x0.07±1.5 \r\n" +
                           "慢性：ΔHCO3-=ΔPaCO2x0.35±5.58\r\n" +
                           "呼吸性碱中毒：\r\n" +
                           "急性：ΔHCO3-=ΔPaCO2x0.2±2.5\r\n" +
                           "慢性：ΔHCO3-=ΔPaCO2x0.5±2.5\r\n" +
                           "代谢性酸中毒：\r\n" +
                           "PaCO2=HCO3-x1.5+8±2\r\n" +
                           "代谢性碱中毒：\r\n" +
                           "ΔPaCO2=ΔHCO3-x0.9±1.5\r\n";
                try
                {
                    Label label = (Label)sender;
                    help = "";
                    title = "公式说明";

                }
                catch (Exception ex)
                {

                    Console.WriteLine("点击的是label" + ex.ToString());
                }
                finally
                {
                    this.message.ShowInfoDialog(title, help + message, UIStyle.LightRed);

                }
            }
        }

    }
}
