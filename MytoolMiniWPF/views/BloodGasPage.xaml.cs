using FlaUI.Core.AutomationElements;
using MytoolMiniWPF.customControls;
using System;
using System.Collections.Generic;

using System.Windows;

using System.Windows.Input;

using WpfToast.Controls;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// BloodGasPage.xaml 的交互逻辑
    /// </summary>
    public partial class BloodGasPage : System.Windows.Window
    {
       
        private double ph, pco2, po2, na, cl, ab, ag, k, fio2, ri;
        private bool acidosis;
        Interval<double> phNormal = new Interval<double>(7.35, 7.45);
        Interval<double> co2Normal = new Interval<double>(35, 45);
        Interval<double> hco3Normal = new Interval<double>(22, 27);
        Interval<double> agNormal = new Interval<double>(12, 16);
        Interval<double> possibleHco3Normal = new Interval<double>(22, 27);
        Interval<double> hco3Interval, co2Interval;
        private bool _isClosed = true;
        public bool IsClosed {
            get { return _isClosed; }
            set { _isClosed = value; }
        }

        public BloodGasPage()
        {
            InitializeComponent();
            setComboBoxItems();
            setDefaultMessage();
            radioBtnQian.IsChecked = true;
            BloodGasGrid.Tag = false;
        }

        private bool CheckNecessaryValue()
        {
            try
            {
                ph = double.Parse(ph_box.Value);
                po2 = double.Parse(po2_box.Value);
                pco2 = double.Parse(pco2_box.Value);
                na = double.Parse(na_box.Value);
                cl = double.Parse(cl_box.Value);
                ab = double.Parse(ab_box.Value);
                ag = double.Parse(ag_box.Value);
                return true;
            }
            catch (Exception ex)
            {
                UMessageBox.Show("警告!","ph、Po2、Pco2、Na、Cl、Ab、Ag必须赋值！\n\n" + ex);
                return false;
            }
            
        }


        private void setComboBoxItems()
        {
            for (int i = 21; i < 101; i++)
            {
                fio2_combobox.Items.Add(i.ToString());
            }
            fio2_combobox.SelectedItem = "21";
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
            textBoxOut.AppendText(result + "\n");
        }

        private string Respiratory_failure()
        {
            string result = "", oxygen = null;

            fio2 = double.Parse(fio2_combobox.SelectedItem.ToString());
            ri = 100 * po2 / fio2;

            oxygen = fio2_combobox.SelectedItem.ToString() == "21" ? "未吸氧状态" : "吸氧状态";

            if (po2 >= 60 && pco2 <= 50)
            {
                result = ri >= 300 ? "无呼吸衰竭" : "呼吸指数<300,病例可能存在ARDS或低氧血症!";
            }
            else if (po2 > 60 && pco2 > 50)
            {
                result = fio2_combobox.SelectedItem.ToString() == "21" ? "低通气综合征（高碳酸血症（当前为未吸氧状态，如为吸氧状态下，也应诊断为呼吸衰竭））" : "II型呼吸衰竭(吸氧状态)";
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

        }


        private void setDefaultMessage()
        {

            textBoxOut.Clear();
            string info = "1.该工具提供简便血气分析,红色为必填项目。\r\n" +
                        "2.该工具默认仅对慢性酸碱失衡样本进行计算,如填入为急性酸碱失衡样本,其结果不可信!\r\n" +
                        "3.计算结果仅供参考,请以临床实际情况为准。\r\n" +
                        "4.不同公式计算结果可能有较小差异!\r\n" +
                        "提示:可使用tab快速切换输入焦点!\r\n";
            textBoxOut.AppendText(info);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNecessaryValue()) { return; }
            textBoxOut.Clear();
            Henderson_hasselbach();
            textBoxOut.AppendText(Respiratory_failure() + "\r"); // 呼吸衰竭判断

            string result = diagNostics();
            textBoxOut.AppendText(result + "\r");
            CheckSupplementingCaustic();
            textBoxOut.AppendText("查看更多信息请点击帮助按钮或按F1" + "\r");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ph_box.Value = string.Empty;

            na_box.Value = string.Empty;
            cl_box.Value = string.Empty;
            k_box.Value = string.Empty;
            po2_box.Value = string.Empty;
            pco2_box.Value = string.Empty;
            glu_box.Value = string.Empty;
            ab_box.Value = string.Empty;
            sb_box.Value = string.Empty;
            be_box.Value = string.Empty;
            ag_box.Value = string.Empty;
            pop_box.Value = string.Empty;
            lac_box.Value = string.Empty;
            fio2_combobox.SelectedIndex= 0;





            setDefaultMessage();
        }

        private void mini_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void copyString(object sender, RoutedEventArgs e)
        {

            List<string> valuesToCopy = new List<string>();

            if (!string.IsNullOrEmpty(k_box.Value))
            {
                valuesToCopy.Add($"K:{k_box.Value}mmol/L");
            }
            if (!string.IsNullOrEmpty(glu_box.Value))
            {
                valuesToCopy.Add($"Glu:{glu_box.Value}mmol/L");
            }
            if (!string.IsNullOrEmpty(ab_box.Value))
            {
                valuesToCopy.Add($"Ab:{ab_box.Value}mmol/L");
            }
            if (!string.IsNullOrEmpty(sb_box.Value))
            {
                valuesToCopy.Add($"Sb:{sb_box.Value}mmol/L");
            }
            if (!string.IsNullOrEmpty(be_box.Value))
            {
                valuesToCopy.Add($"Be:{be_box.Value}mmol/L");
            }
            if (!string.IsNullOrEmpty(pop_box.Value))
            {
                valuesToCopy.Add($"POP:{pop_box.Value}mOsm/L");
            }
            if (!string.IsNullOrEmpty(lac_box.Value))
            {
                valuesToCopy.Add($"Lac:{lac_box.Value}mmol/L");
            }
            if (!string.IsNullOrEmpty(ag_box.Value))
            {
                valuesToCopy.Add($"Ag:{ag_box.Value}mmol/L");
            }

            string result = string.Format(
                "Ph:{0}、PCO2:{1}mmHg、PO2:{2}mmHg、Na+:{3}mmHg、Cl:{4}mmHg、{5}FiO2:{6}%;",
                ph, pco2, po2, na, cl,
                string.Join("、", valuesToCopy),
                fio2_combobox.SelectedValue.ToString()
            );

            if (ph == 0)
            {
                UMessageBox.Show("提示", "还没开始计算，没有内容可复制！");
                return;
            }
            else
            {
                textBoxOut.Clear();
                textBoxOut.AppendText(result);
                Clipboard.SetText(result);
                //UMessageBox.Show("提示", "成功，输入信息已经复制到剪切板！");
                //notifier.ShowSuccess("输入信息已经复制到剪切板！");
                Toast.Show(this,"输入信息已经复制到剪切板！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(2), Time = 2000, Location = ToastLocation.OwnerTopLeft });
            }
        }

        private string diagNostics()
        {
            
            //double deltaAg = ag = 12;
            double deltaAg = (double)(ag - agNormal.End);
            double possibleHco3 = deltaAg + ab;
            this.print($"计算潜在HCO3:[HCO3 = deltaAg({deltaAg}) + ab({ab}) = {possibleHco3}]");
            string result = "";
            if (ph < phNormal.Start) // 酸中毒
            {
                result = DiagnoseAcidosis();
            }
            else if (ph > phNormal.End)
            {
                result = DiagnoseAlkalosis();
            }
            else //[7.35,7.45]
            {
                result = NormalPhDiagnoseAcidosis();

            }
            if (result.Contains("样本有误"))
            {
                return result;
            }
            //this.print("最后判断是否合并阴离子间隙升高的酸碱失衡");

            if (!acidosis)
            {
                return result;
            }

            if (ag > agNormal.End && possibleHco3 > hco3Normal.End)
            {
                result = UpdateResult(result, "代谢性碱中毒", "代谢性酸中毒", "(AG增高)代谢性酸中毒");
            }
            else if (ag < agNormal.End && possibleHco3 < hco3Normal.Start)
            {
                result = UpdateResult(result, "代谢性酸中毒", "代谢性酸中毒", "(AG正常)代谢性酸中毒");
            }
            else if (ag > agNormal.End && possibleHco3 > hco3Normal.End)
            {
                result = UpdateResult(result, "代谢性碱中毒", null, "(并存)代谢性碱中毒");
            }
            return DiagnoseLacticAcidosis(result);
        }
        string UpdateResult(string currentResult, string searchSubstring, string replaceSubstring, string appendSubstring)
        {
            if (!currentResult.Contains(searchSubstring))
            {
                return currentResult + appendSubstring;
            }
            if (replaceSubstring != null)
            {
                currentResult = currentResult.Replace(searchSubstring, appendSubstring);
            }
            return currentResult;
        }

        private string DiagnoseLacticAcidosis(string result) // 判断乳酸酸中毒;
        {
            if (string.IsNullOrEmpty(lac_box.Value))
            {
                return result;
            }
            if (int.Parse(lac_box.Value) > 2)
            {
                if (result.Contains("AG增高"))
                {
                    return result.Replace("AG增高", "AG增高、高乳酸");
                }
                if (result.Contains("AG正常"))
                {
                    return result.Replace("AG正常", "AG正常、高乳酸");
                }
            }
            return result;
        }
        private string DiagnoseAcidosis() //判断酸所有的中毒；
        {
            this.print($"ph < {ph},判定原发酸中毒");
            if (pco2 > 40)  // RespiratoryAcidosis
            {
                return RespiratoryAcidosis();

            }
            else //# 排除呼吸性酸中毒;但PH<7.35，肯定是代谢性酸中毒->进一步判断是否合并呼吸性碱中毒;
            {
                return MetabolicAcidosis();
            }

        }

        private string NormalPhDiagnoseAcidosis() {
            this.print("Ph 在正常范围内");
            if (co2Normal.InRange(pco2) && hco3Normal.InRange(ab))
            {
                return "无酸碱失衡";
            }
            // 判断酸中毒，其中可能为：
            // 1.呼吸性酸中毒 -- 代偿：PC02 👆
            // 2.代谢性酸中毒 -- 代偿：AB 👇


            // 判断碱中毒，其中可能为：
            // 1.呼吸性碱中毒 -- 代偿：PCO2 👇
            // 2.代谢性碱中毒 -- 代偿：AB 👆

            // 所有怀疑酸中毒均应该考虑乳酸。

            if (co2Normal.InRange(pco2))
            {
                if (ab < hco3Normal.Start)
                {
                    this.acidosis = true;
                    return "(可能)代谢性酸中毒"; 
                }
                else if (ab > hco3Normal.End)
                {
                    return "（可能）代谢性碱中毒"; 
                }
            }

            if (pco2 < co2Normal.Start)
            {
                if (hco3Normal.InRange(ab))
                {
                    return "呼吸性碱中毒";
                }
                else if (ab < hco3Normal.Start)
                {
                    return ph>=7.40?  "呼吸性碱中毒合并代谢性酸中毒":"代谢性酸中毒合并呼吸性碱中毒";
                }
                else if (ab > hco3Normal.End)
                {
                    this.acidosis = true;
                    return ph >= 7.40 ? "代谢性碱中毒合并呼吸性碱中毒(不便于判断的原发失衡)" : "标本有误?";
                }
            }
            if (pco2 > co2Normal.End)
            {
                if (hco3Normal.InRange(ab))
                {
                    return "呼吸性酸中毒";
                }
                else if (ab < hco3Normal.Start)
                {
                    this.acidosis = true;
                    return "呼吸性酸中毒合并代谢性酸中毒?";
                    
                }
                else if (ab > hco3Normal.End)
                {
                    return ph < 7.4 ? "呼吸性酸中毒合并代谢性碱中毒(代偿)" : "代谢性碱中毒合并呼吸性酸中毒(代偿)";
                }
            }

            return "无酸碱失衡";

        }
        private string RespiratoryAcidosis()  //呼吸性酸中毒，PCO2>40;
        {
            string result;
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
            return result;
        }

        private string MetabolicAcidosis() //代谢性酸中毒,
        {
            this.acidosis = true;
            co2Interval = getCo2(acidosis: true, hco3: ab);

            if (co2Interval.InRange(pco2))
            {
                return "(单纯)代谢性酸中毒";
            }

            if (pco2 < co2Interval.Start)
            {
                this.print("比较: PCO2 < 代偿区间最小值");
                return ab < hco3Normal.Start ? "代谢性酸中毒合并呼吸性碱中毒" : "样本有误?";
            }

            this.print("比较: PCO2 > 代偿区间最大值");
            return "代谢性酸中毒合并呼吸性酸中毒(或) [呼吸性酸中毒合并代谢性酸中毒]";
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Help();
        }

        private string DiagnoseAlkalosis()
        {
            this.print($"ph > {phNormal.End},判定原发碱中毒");

            if (pco2 > 40)
            {
                return DiagnoseMetabolicAlkalosis();
            }
            else
            {
                return DiagnoseRespiratoryAlkalosis();
            }
        }
        private string DiagnoseMetabolicAlkalosis()
        {
            co2Interval = getCo2(acidosis: false, hco3: ab);

            if (co2Interval.InRange(pco2))
            {
                this.print("比较: PCO2 在代偿区间内,无多重酸碱失衡");
                return "(单纯)代谢性碱中毒";
            }
            else if (pco2 > co2Interval.End)
            {
                this.print($"比较: PCO2 > 代偿区间最大值,存在多重酸碱失衡,PCO2存在原发升高");
                return ab > hco3Normal.End ? "代谢性碱中毒合并呼吸性酸中毒(失代偿)" : "样本有误?";
            }
            else
            {
                this.print($"比较: PCO2 < 代偿区间最小值,存在多重酸碱失衡，PCO2存在原发降低");
                return "代谢性碱中毒合并呼吸性碱中毒(失代偿)(或) [呼吸性碱中毒合并代谢性碱中毒]";
            }
        }

        private string DiagnoseRespiratoryAlkalosis()
        {
            this.print("不确定是否有代谢性碱中毒;默认以呼吸性碱中毒来计算");
            hco3Interval = getHco3(false, (40 - pco2));

            if (hco3Interval.InRange(ab))
            {
                return "(单纯)呼吸性碱中毒";
            }
            else if (ab < hco3Interval.Start)
            {
                this.acidosis = true;
                return "呼吸性碱中毒合并代谢性酸中毒(失代偿)";
            }
            else
            {
                return "呼吸性碱中毒合并代谢性碱中毒(失代偿)(或)[代谢性碱中毒合并呼吸性碱中毒(失代偿)]";
            }
        }

        private void print(string message)
        {
            textBoxOut.AppendText(message + "\r");
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
                if (radioBtnZhong.IsChecked == true)
                {
                    min_hco3 = deltaCo2 * 0.49 - 1.72;
                    max_hco3 = deltaCo2 * 0.49 + 1.72;
                    this.print("本次计算代偿公式:[ΔHCO3 = ΔPCO2 * 0.49 ± 1.72]");
                }
                else if (radioBtnQian.IsChecked == true || radioBtnZhen.IsChecked == true)
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
                if (radioBtnZhen.IsChecked == true)
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


        private void limitTxtInput(object sender, KeyEventArgs e)
        {
            LabelText labelText = (LabelText)sender;

            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Decimal ||
                e.Key == Key.OemMinus ||
                e.Key == Key.Subtract ||
                e.Key == Key.Back ||
                e.Key == Key.Delete ||
                e.Key == Key.Left ||
                e.Key == Key.Right ||
                e.Key == Key.Tab))
            {
                e.Handled = true;
            }
            

        }
        private void CheckSupplementingCaustic()
        {
            if (ph < 7.10 || ab < 10)
            {
                textBoxOut.AppendText("请注意是否需要补碱" + "\r");
            }

        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            _isClosed= true;
            this.Close();
        }

        private void Help()
        {
            var window = new BloodGasHelpDocument();
            window.Show();

        }

    }
}
