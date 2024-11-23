using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Sunny.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Aspose.Words;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Threading;

namespace MytoolUI
{

    public partial class TumorReportUI : Form
    {
        private string symbolTrue = "☑";
        private string symbolFalse = "□";
        private string wordModPath = Application.StartupPath + @"\cache\turmorReportCard.docx";
        private string userName;
        private UIMessageForm message = new UIMessageForm();
        private bool isMain = true;
        private bool ispathological = false;
        private List<Pain> painLists;
        private List<UICheckBox> checkBoxes;
        private List<UIComboboxEx> listComboboxs;
        private List<UIRadioButton> radiobuttons;
        private List<string> idcardBookmarkList;
        private List<string> pathList = new List<string>();
        private Dictionary<string, string> painCardDir = new Dictionary<string, string>();
        private Dictionary<string, string> diagnosesDict;
        private FileStream streamWord = null;
        private Form currentPrintForm;
        private Color color;
        private DataSet data;
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\data.db;Version=3;");

        public TumorReportUI(Color color, string currentUserName)
        {
            this.color = color;
            userName = new DatabaseUnit().GetuserName();
            InitializeComponent();
            SetBtnColor();
            this.uiProcessBar.Visible = false;
            m_dbConnection.Open();
            m_dbConnection.EnableExtensions(true);
            this.uiDatePickerAccidentDay.Text = "";
            m_dbConnection.LoadExtension("SQLite.Interop.dll", "sqlite3_fts5_init");
            m_dbConnection.LoadExtension("simple\\simple.dll");
            this.comboBoxSelectUser.Text = currentUserName;
        }
        private void SetBtnColor()
        {
            checkBoxes = new List<UICheckBox> {
            uiCheckBoxclinicIarc,uiCheckBoxNoneIarc,uiCheckBoxOnlyDeathIarc,uiCheckBoxpathologyIarc,
            uiCheckBoxXRayIarc,uiCheckBoxbiochemistryIarc,uiCheckBoxclinicIIndustry,uiCheckBoxXrayIndustry,
            uiCheckBoxCTindustry,uiCheckBoxultrasonicindustry,uiCheckBoxendoscopeIndustry,uiCheckBoxMriIndustry,
            uiCheckBoxOperationIndustry,uiCheckBoxCellIndustry,uiCheckBoxpathologyIndustry0,uiCheckBoxpathologyIndustry1,
            uiCheckBoxpostmortemIndustry
            };
            foreach (UICheckBox item in checkBoxes)
            {
                //item.CheckBoxColor = this.color;
                item.Style = UIStyle.Office2010Silver;
            }
            List<UISymbolButton> buttons = new List<UISymbolButton>
            {
                ubtnSearch,uBtnOpendir,uBtnPrint,uBtnBuild,uBtnBrowse,uBtnStart,uBtnUpdateAddress
            };
            foreach (var button in buttons)
            {
                button.ForeHoverColor = color;
                button.ForePressColor = color;
                button.ForeSelectedColor = color;
            }

            uiListBoxPainList.HoverColor = this.color;
            uiListBoxPainList.ItemSelectBackColor = this.color;
            uiListBoxPainList.ItemSelectForeColor = Color.White;


            comboBoxSelectUser.ItemSelectBackColor = this.color;
            comboBoxSelectUser.ItemSelectForeColor = Color.White;
            listComboboxs = new List<UIComboboxEx>() {
                //comboBoxSelectUser,  去掉的目的:清除时避免清除用户名
                uiComboboxExVocation,
                uiComboboxExLevel,
                uiComboboxMarriage,
                uiComboboxMainDiagnose,
                uiComboboxpathological,
                uiComboboxIcd10,
                uiComboboxTumorLoaction,
                uiComboboxWorkType,
                uiComboboxCure,
                uiComboboxCureItem,
                uiComboboxlateral,
                uiComboboxisMult,
                uiComboboxDiagnoseHospital
            };
            foreach (var bombobox in listComboboxs)
            {
                bombobox.ItemSelectBackColor = this.color;
                bombobox.ItemSelectForeColor = Color.White;
            }
            radiobuttons = new List<UIRadioButton> { uiRadioButtonNo, uiRadioButtonYes, uiRadioButtonKnow, uiRadioButtonDontKnow, uiRadioButtonunDead, uiRadioButtonDead };
            foreach (var radiobutton in radiobuttons)
            {
                //radiobutton.RadioButtonColor = this.color;
                radiobutton.Style = UIStyle.Office2010Silver;

            }

            List<UIButton> btns = new List<UIButton>() { uBtnBrowse, uBtnStart };
            foreach (UIButton btn in btns)
            {
                btn.ForeHoverColor = color;
                btn.ForePressColor = color;
                btn.ForeSelectedColor = color;
                btn.RectColor = Color.White;
                btn.FillColor = Color.White;
                btn.FillDisableColor = Color.FromArgb(244, 244, 244);
                btn.FillHoverColor = Color.FromArgb(255, 228, 228);
                btn.FillPressColor = Color.FromArgb(248, 160, 160);
                btn.FillSelectedColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.ForeHoverColor = color;
                btn.ForePressColor = Color.Black;
                btn.ForeSelectedColor = Color.Black;
                btn.RectColor = Color.White;
                btn.RectDisableColor = Color.White;
                btn.RectHoverColor = Color.White;
                btn.RectPressColor = Color.White;
                btn.RectSelectedColor = Color.White;
            }
            this.textBoxFilepath.RectColor = Color.DimGray;



        }
        private void uBtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilepath.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            data = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                            });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 从导入的excel表中获取含有肿瘤/癌的信息
        /// </summary>
        /// <returns></returns>
        private void getBasicInformation()
        {
            int rows;
            painLists = new List<Pain>();
            //List<Pain> painInfoList = new List<Pain>();
            List<string> diag_select = new List<string> { "肿瘤", "癌" };
            rows = data.Tables[0].Rows.Count;//获取行数
            for (int i = 0; i < rows; i++)
            {
                string outDiagnose = data.Tables[0].Rows[i][16].ToString();
                foreach (string item in diag_select)
                {
                    if (outDiagnose.Contains(item))
                    {
                        Pain person = new Pain();
                        person.Name = data.Tables[0].Rows[i][2].ToString();
                        person.Id = data.Tables[0].Rows[i][1].ToString();
                        person.Gender = data.Tables[0].Rows[i][3].ToString();
                        person.Age = data.Tables[0].Rows[i][4].ToString();
                        person.IdCardNumber = data.Tables[0].Rows[i][5].ToString();
                        person.Vocation = data.Tables[0].Rows[i][6].ToString();
                        person.Phone = data.Tables[0].Rows[i][7].ToString();
                        person.HomeAddr = data.Tables[0].Rows[i][8].ToString();
                        person.WorkAddr = data.Tables[0].Rows[i][9].ToString();
                        person.DoctorName = data.Tables[0].Rows[i][11].ToString();
                        person.InDay = data.Tables[0].Rows[i][12].ToString();
                        person.InDiagnose = data.Tables[0].Rows[i][13].ToString();
                        person.OutDay = data.Tables[0].Rows[i][14].ToString();
                        person.DuringDay = data.Tables[0].Rows[i][15].ToString();
                        person.OutDiagnose = data.Tables[0].Rows[i][16].ToString();
                        person.IsDeath = false; // 默认未死亡，赋初值
                        string[] eachDiagnose = outDiagnose.Split(",");
                        foreach (var diag_ in eachDiagnose)
                        {
                            if (diag_.Contains(item))
                            {
                                person.MainDiagnose = diag_;
                                break;
                            }
                        }
                        if (person.DoctorName == userName)
                        {
                            uiListBoxPainList.Items.Add(person.Name);
                            painLists.Add(person);
                            //painInfoList.Add(person);
                        }
                        Console.WriteLine(person.Name);
                    }
                }
            }
            //return painInfoList;
        }
        private void uBtnStart_Click(object sender, EventArgs e)
        {
            if (data == null)
            {
                message.ShowErrorDialog( "未实例化对象","未选择文件!");
                return;
            }

            uiListBoxPainList.Items.Clear();

            try
            {
                userName = comboBoxSelectUser.SelectedItem.ToString();
            }
            catch (Exception)
            {
                message.ShowErrorDialog($"尝试读取用户名时发生错误!可能的原因为没有选择用户信息。\r\n调用默认用户名:{userName}");
                return;
            }
            try
            {
                getBasicInformation();
            }
            catch (Exception ex)
            {
                message.ShowErrorDialog($"从Excel表格中获取信息错误!可能的原因为是否文件损坏或选择错误？\r\n{ex}");
            }

            message.ShowInfoDialog("提示","信息读取完毕,填写信息并成功生成报卡文件后,可通过双击姓名快速查看所生成的报卡[Word]文件,可进一步完善详细信息,确认无误后,可返回程序一键打印。",UIStyle.LightRed,showMask:false);
        }
        /// <summary>
        /// 保存当前控件值到PainList列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChange(object sender = null, EventArgs e = null)
        {
            if (painLists == null || painLists.Count < 1)
            {
                return;
            }
            if (uiRadioButtonYes.Checked)
            {
                uiComboboxDiagnoseHospital.Enabled = false;
                uiComboboxDiagnoseHospital.Text = "本院";
            }
            else if (uiRadioButtonNo.Checked)
            {
                uiComboboxDiagnoseHospital.Enabled = true;
                //uiComboboxDiagnoseHospital.Text = "";
            }
            ChangeStyle();

            int index = uiListBoxPainList.SelectedIndex;
            painLists[index].Marriage = uiComboboxMarriage.Text ?? "2.已婚";
            painLists[index].Vocation = uiComboboxExVocation.Text ?? painLists[index].Vocation;
            painLists[index].AccidentDay = uiDatePickerAccidentDay.Text ?? DateTime.Parse(painLists[index].InDay).ToString("yyyy-MM-dd");
            painLists[index].Icd10 = uiComboboxIcd10.Text ?? "";
            painLists[index].TurmorLocation = uiComboboxTumorLoaction.Text ?? "";
            painLists[index].TurmorBodyLocation = uiTextBoxTumorAnatomicalLocation.Text ?? "";
            painLists[index].Pathological = uiComboboxpathological.Text ?? "";
            painLists[index].Morphology = uitextboxmorphology.Text ?? "";
            painLists[index].Level = uiComboboxExLevel.Text ?? "";
            painLists[index].MainDiagnose = uiComboboxMainDiagnose.Text;

            painLists[index].IsCurrentHospital = uiRadioButtonYes.Checked;
            painLists[index].DiagnoseHospitalName = uiComboboxDiagnoseHospital.Text ?? "本院";

            painLists[index].Cure = uiComboboxCure.Text ?? "";
            painLists[index].CureItem = uiComboboxCureItem.Text ?? "";
            painLists[index].IsMult = uiComboboxisMult.Text ?? "";
            painLists[index].IsKnow = uiRadioButtonKnow.Checked;
            painLists[index].DontKnow = uiRadioButtonDontKnow.Checked;
            painLists[index].WorkType = uiComboboxWorkType.Text ?? "";



            painLists[index].Clinic = uiCheckBoxclinicIarc.Checked;
            painLists[index].XRay = uiCheckBoxXRayIarc.Checked;
            painLists[index].Pathology0 = uiCheckBoxpathologyIarc.Checked;
            painLists[index].None = uiCheckBoxNoneIarc.Checked;
            painLists[index].Biochemistry = uiCheckBoxbiochemistryIarc.Checked;
            painLists[index].OnlyDeath = uiCheckBoxOnlyDeathIarc.Checked;

            painLists[index].ClinicIndustry = uiCheckBoxclinicIIndustry.Checked;
            painLists[index].XrayIndustry = uiCheckBoxXrayIndustry.Checked;
            painLists[index].CTIndustry = uiCheckBoxCTindustry.Checked;
            painLists[index].MRIIndustry = uiCheckBoxMriIndustry.Checked;
            painLists[index].UltrasonicIndustry = uiCheckBoxultrasonicindustry.Checked;
            painLists[index].EndoscopeIndustry = uiCheckBoxendoscopeIndustry.Checked;
            painLists[index].OperationIndustry = uiCheckBoxOperationIndustry.Checked;
            painLists[index].CellIndustry = uiCheckBoxCellIndustry.Checked;
            painLists[index].PathologyIndustry0 = uiCheckBoxpathologyIndustry0.Checked;
            painLists[index].PathologyIndustry1 = uiCheckBoxpathologyIndustry1.Checked;
            painLists[index].PostmortemIndustry = uiCheckBoxpostmortemIndustry.Checked;

            painLists[index].IsDeath = uiRadioButtonDead.Checked;
            painLists[index].UnDead = uiRadioButtonunDead.Checked;
            painLists[index].Lateral = uiComboboxlateral.Text ?? "无需填写侧位";

        }
        /// <summary>
        /// 当选中painList的一项时，将内存中的赋给控件
        /// </summary>
        private void SetDefaultMessage()
        {
            int index = uiListBoxPainList.SelectedIndex;
            uiComboboxMarriage.Text = painLists[index].Marriage ?? "2.已婚";

            if (!string.IsNullOrEmpty(painLists[index].Vocation))
            {
                if (painLists[index].Vocation == "农民")
                {
                    uiComboboxExVocation.Text = "9.无业";
                }
                else if (painLists[index].Vocation == "工人" || painLists[index].Vocation == "退(离)休人员")
                {
                    uiComboboxExVocation.Text = "11.不便分类的其它从业人员";
                }
                foreach (var item in uiComboboxExVocation.Items)
                {
                    if (item.ToString().Contains(painLists[index].Vocation))
                    {
                        uiComboboxExVocation.Text = item.ToString();
                        break;
                    }
                }
            }
            else
            {
                uiComboboxExVocation.Text = "9.无业";
            }

            uiDatePickerAccidentDay.Text = painLists[index].AccidentDay ?? DateTime.Parse(painLists[index].InDay).ToString("yyyy-MM-dd");
            uiComboboxIcd10.Text = painLists[index].Icd10 ?? "";
            uiComboboxTumorLoaction.Text = painLists[index].TurmorLocation ?? "";
            uiTextBoxTumorAnatomicalLocation.Text = painLists[index].TurmorBodyLocation ?? "";
            uiComboboxpathological.Text = painLists[index].Pathological ?? "";
            uitextboxmorphology.Text = painLists[index].Morphology ?? "";
            uiComboboxExLevel.Text = painLists[index].Level ?? "";
            uiComboboxMainDiagnose.Text = painLists[index].MainDiagnose ?? "";

            uiCheckBoxclinicIarc.Checked = painLists[index].Clinic;
            uiCheckBoxXRayIarc.Checked = painLists[index].XRay;
            uiCheckBoxpathologyIarc.Checked = painLists[index].Pathology0;
            uiCheckBoxNoneIarc.Checked = painLists[index].None;
            uiCheckBoxbiochemistryIarc.Checked = painLists[index].Biochemistry;
            uiCheckBoxOnlyDeathIarc.Checked = painLists[index].OnlyDeath;

            uiCheckBoxclinicIIndustry.Checked = painLists[index].ClinicIndustry;
            uiCheckBoxXrayIndustry.Checked = painLists[index].XrayIndustry;
            uiCheckBoxCTindustry.Checked = painLists[index].CTIndustry;
            uiCheckBoxMriIndustry.Checked = painLists[index].MRIIndustry;
            uiCheckBoxultrasonicindustry.Checked = painLists[index].UltrasonicIndustry;
            uiCheckBoxendoscopeIndustry.Checked = painLists[index].EndoscopeIndustry;
            uiCheckBoxOperationIndustry.Checked = painLists[index].OperationIndustry;
            uiCheckBoxCellIndustry.Checked = painLists[index].CellIndustry;
            uiCheckBoxpathologyIndustry0.Checked = painLists[index].PathologyIndustry0;
            uiCheckBoxpathologyIndustry1.Checked = painLists[index].PathologyIndustry1;
            uiCheckBoxpostmortemIndustry.Checked = painLists[index].PostmortemIndustry;
            uiComboboxDiagnoseHospital.Text = painLists[index].DiagnoseHospitalName ?? "未填写";
            uiComboboxCure.Text = painLists[index].Cure ?? "";
            uiComboboxCureItem.Text = painLists[index].CureItem ?? "";
            uiComboboxisMult.Text = painLists[index].IsMult ?? "";
            uiComboboxWorkType.Text = painLists[index].WorkType ?? "";
            uiRadioButtonYes.Checked = painLists[index].IsCurrentHospital;
            uiRadioButtonDontKnow.Checked = painLists[index].DontKnow;
            uiRadioButtonDead.Checked = painLists[index].IsDeath;
            uiRadioButtonunDead.Checked = painLists[index].UnDead;
            uiRadioButtonKnow.Checked = painLists[index].IsKnow;
            uiComboboxlateral.Text = painLists[index].Lateral;
            ChangeStyle();
        }

        /// <summary>
        /// 检查空的值，暂时不用
        /// </summary>
        /// <returns></returns>
        private bool ChekEmptyTextInut()
        {
            bool showMessage = false;
            List<UITextBox> listTextBox = new List<UITextBox>() { uiTextBoxTumorAnatomicalLocation, uitextboxmorphology };
            foreach (var item in listTextBox)
            {
                if (string.IsNullOrEmpty(item.Text))
                {
                    item.RectColor = Color.Red;
                    showMessage = true;
                }
            }
            List<UIComboboxEx> listCombobox = new List<UIComboboxEx>() { uiComboboxExVocation, uiComboboxExLevel, uiComboboxMarriage, uiComboboxMainDiagnose, uiComboboxpathological, uiComboboxIcd10, uiComboboxTumorLoaction };
            foreach (var item in listCombobox)
            {
                if (string.IsNullOrEmpty(item.Text))
                {
                    item.RectColor = Color.Red;
                    showMessage = true;
                }
            }
            if (showMessage)
            {

                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 在对每一个新的患者赋值前，清除所有的控件值
        /// </summary>
        private void ClearText()
        {
            //uiComboboxMarriage.ResetText();
            //uiComboboxExVocation.ResetText();
            //uiDatePickerAccidentDay.ResetText();
            //uiComboboxIcd10.ResetText();
            //uiComboboxTumorLoaction.ResetText();
            //uiTextBoxTumorAnatomicalLocation.ResetText();
            //uiComboboxpathological.ResetText();
            //uitextboxphology.ResetText();

            //uiComboboxExLevel.ResetText();
            //uiComboboxMainDiagnose.ResetText();

            foreach (UICheckBox checkbox in this.checkBoxes)
            {
                checkbox.Checked = false;
            }
            foreach (UIComboboxEx uICombobox in this.listComboboxs)
            {
                uICombobox.ResetText();
            }
            List<UIRadioButton> uiradiobuttons = new List<UIRadioButton>() {
            uiRadioButtonDead,uiRadioButtonunDead,uiRadioButtonKnow,uiRadioButtonDontKnow,uiRadioButtonYes,uiRadioButtonNo
            };
            foreach (var item in uiradiobuttons)
            {
                item.Checked = false;
            }

            uiDatePickerAccidentDay.Text = "";

        }
        /// <summary>
        ///根据主要诊断检索其他的诊断编码，并尝试自动填充数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ubtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiComboboxMainDiagnose.Text))
            {
                message.ShowWarningDialog("请输入规范的诊断信息以便更精确的匹配!",UIStyle.LightRed);
                return;
            }

            var result = new DatabaseUnit().GetIcdO3Numbers(uiComboboxMainDiagnose.Text);
            if (result != null)
            {
                diagnosesDict = result;
                SetCheckResultToTextBox();
            }
            else
            {
                message.ShowWarningDialog($"未检索到诊断'{uiComboboxMainDiagnose.Text}'的信息!\r\n这意味着您所填写的诊断不包含在ICD-O-3编码范围内,这也是不符合肿瘤报卡规范的。\r\n请填入标准诊断或手动查阅ICD-O-3编码后补全信息！",UIStyle.LightRed);
            }
        }
        private void SetCheckResultToTextBox()
        {
            uiComboboxpathological.Text = diagnosesDict["pathological"];
            uiTextBoxTumorAnatomicalLocation.Text = diagnosesDict["anatomicalLocation"];
            uitextboxmorphology.Text = diagnosesDict["phologyDiagnose"];
            uiComboboxExLevel.Text = diagnosesDict["level"];

            uiComboboxIcd10.Text = diagnosesDict["icd10"];
            uiComboboxMainDiagnose.Text = diagnosesDict["mainDiagnose"];
            uiComboboxTumorLoaction.Text = diagnosesDict["anatomicalLocationName"];
            SaveChange();
        }

        private void uBtnBuild_Click(object sender, EventArgs e)
        {
            this.uiProcessBar.Visible = true;
            ReadDocxFromDb();
            idcardBookmarkList = new List<string>()
                    {
                    BookMark.idCard0,BookMark.idCard1,
                    BookMark.idCard2,BookMark.idCard3,
                    BookMark.idCard4,BookMark.idCard5,
                    BookMark.idCard6,BookMark.idCard7,
                    BookMark.idCard8,BookMark.idCard9,
                    BookMark.idCard10,BookMark.idCard11,
                    BookMark.idCard12,BookMark.idCard13,
                    BookMark.idCard14,BookMark.idCard15,
                    BookMark.idCard16,BookMark.idCard17
                    };
            streamWord = File.Open(wordModPath, FileMode.Open);
            CheckPainEmpty();
            streamWord.Close();
        }

        private void CheckPainEmpty()
        {
            if (painLists == null)
            {
                message.ShowErrorDialog("需要先读取文件！");
                return;
            }
            uiProcessBar.Maximum = painLists.Count;
            List<string> unFilledPersons = new List<string>();
            List<string> filledPersons = new List<string>();
            int count = 0;
            foreach (Pain item in painLists)
            {
                uiProcessBar.Value = ++count;
                if (string.IsNullOrEmpty(item.MainDiagnose) ||
                    string.IsNullOrEmpty(item.Marriage) ||
                    string.IsNullOrEmpty(item.Vocation) ||
                    string.IsNullOrEmpty(item.AccidentDay) ||
                    string.IsNullOrEmpty(item.Icd10) ||
                    string.IsNullOrEmpty(item.TurmorLocation) ||
                    string.IsNullOrEmpty(item.Pathological) ||
                    string.IsNullOrEmpty(item.TurmorBodyLocation) ||
                    string.IsNullOrEmpty(item.Pathological) ||
                    string.IsNullOrEmpty(item.Morphology) ||
                    string.IsNullOrEmpty(item.Level) ||
                    string.IsNullOrEmpty(item.IsMult) ||
                    string.IsNullOrEmpty(item.WorkType) ||
                    string.IsNullOrEmpty(item.Cure) ||
                    (!item.UnDead&&!item.IsDeath) || 
                    (!item.IsKnow && !item.DontKnow)
                    )
                {
                    unFilledPersons.Add(item.Name);
                }
                else
                {
                    FilldDocx(item);
                    filledPersons.Add(item.Name);
                }
            }
            if (unFilledPersons.Count > 0)
            {
                message.ShowWarningDialog($"'{string.Join("、", unFilledPersons.ToArray())}'的信息未填写完整！补全后再试");
            }
            if (filledPersons.Count > 0)
            {
                message.ShowInfoDialog($"'{string.Join("、", filledPersons.ToArray())}'的报卡文件已生产完毕，可双击姓名列表以查看，确认无误后，点击打印可批量打印本次生成的所有文件。");
            }

        }
        /// <summary>
        /// 填表操作
        /// </summary>
        /// <param name="person"></param>
        private void FilldDocx(Pain person)
        {
            List<string> educationList = new List<string> { "不详", "不详", "小学" };
            string childFolder = Convert.ToDateTime(person.OutDay).ToString("yyyy年MM月");
            string savePath = string.Format(@"D:\重庆市居民慢病报卡\{0}\肿瘤报卡.{1}.{2}.{3}.docx", childFolder, person.Name, person.MainDiagnose, Convert.ToDateTime(person.OutDay).ToString("yyyy年MM月dd日"));
            checkFolder(@"D:\重庆市居民慢病报卡\" + childFolder);
            Document document = new Document(streamWord);
            document.Range.Bookmarks[BookMark.painName].Text = person.Name;
            document.Range.Bookmarks[BookMark.painAge].Text = person.Age;
            document.Range.Bookmarks[BookMark.gender].Text = person.Gender;
            document.Range.Bookmarks[BookMark.addrAlways].Text = person.HomeAddr;
            document.Range.Bookmarks[BookMark.addrBorn].Text = person.HomeAddr;
            document.Range.Bookmarks[BookMark.painId].Text = person.Id;
            document.Range.Bookmarks[BookMark.reportDoctor].Text = person.DoctorName;
            document.Range.Bookmarks[BookMark.nation].Text = "汉族";
            //document.Range.Bookmarks[BookMark.phone].Text = person.Phone;
            document.Range.Bookmarks[BookMark.painNameContact].Text = person.Name;
            document.Range.Bookmarks[BookMark.phoneContact].Text = person.Phone;
            document.Range.Bookmarks[BookMark.vocation].Text = person.Vocation;
            document.Range.Bookmarks[BookMark.accidentDay].Text = person.AccidentDay;
            document.Range.Bookmarks[BookMark.turmorLocation].Text = person.TurmorLocation;
            document.Range.Bookmarks[BookMark.turmorBodyLocation].Text = person.TurmorBodyLocation;
            document.Range.Bookmarks[BookMark.pathological].Text = person.Pathological;
            document.Range.Bookmarks[BookMark.morphology].Text = person.Morphology;
            document.Range.Bookmarks[BookMark.reportTime].Text = DateTime.Parse(person.OutDay).ToString("yyyy-MM-dd");
            document.Range.Bookmarks[BookMark.marriage].Text = person.Marriage;
            document.Range.Bookmarks[BookMark.education].Text = educationList[new Random().Next(educationList.Count)];
            document.Range.Bookmarks[BookMark.workType].Text = person.WorkType;
            document.Range.Bookmarks[BookMark.icdNumber].Text = person.Icd10;
            try
            {
                string level = person.Level.Split("-")[0];
                switch (level)
                {
                    case "1": document.Range.Bookmarks[BookMark.level1].Text = symbolTrue; break;
                    case "2": document.Range.Bookmarks[BookMark.level2].Text = symbolTrue; break;
                    case "3": document.Range.Bookmarks[BookMark.level3].Text = symbolTrue; break;
                    case "4": document.Range.Bookmarks[BookMark.level4].Text = symbolTrue; break;
                    case "5": document.Range.Bookmarks[BookMark.level5].Text = symbolTrue; break;
                    case "6": document.Range.Bookmarks[BookMark.level6].Text = symbolTrue; break;
                    case "7": document.Range.Bookmarks[BookMark.level7].Text = symbolTrue; break;
                    case "8": document.Range.Bookmarks[BookMark.level8].Text = symbolTrue; break;
                    case "9": document.Range.Bookmarks[BookMark.level9].Text = symbolTrue; break;
                }
            }
            catch (Exception)
            {

                Console.WriteLine("肿瘤Level未设置！");
            }

            if (person.IsCurrentHospital)
            {
                document.Range.Bookmarks[BookMark.isCurrentHospital].Text = symbolTrue;
                document.Range.Bookmarks[BookMark.diagnoseHospital].Text = person.DiagnoseHospitalName;
            }
            else
            {
                document.Range.Bookmarks[BookMark.isnotCurrentHospital].Text = symbolTrue;
            }
            switch (person.Cure)
            {
                case "治疗": document.Range.Bookmarks[BookMark.cure].Text = symbolTrue; break;
                case "未治疗": document.Range.Bookmarks[BookMark.unCure].Text = symbolTrue; break;
                default: document.Range.Bookmarks[BookMark.dontknowCure].Text = symbolTrue; break;
            }

            if (person.Cure == "治疗")
            {
                try
                {
                    string cureItem = person.CureItem.Split(".")[0];
                    switch (cureItem)
                    {
                        case "1": document.Range.Bookmarks[BookMark.cureItem0].Text = symbolTrue; break;
                        case "2": document.Range.Bookmarks[BookMark.cureItem1].Text = symbolTrue; break;
                        case "3": document.Range.Bookmarks[BookMark.cureItem2].Text = symbolTrue; break;
                        case "4": document.Range.Bookmarks[BookMark.cureItem3].Text = symbolTrue; break;
                        case "5": document.Range.Bookmarks[BookMark.cureItem4].Text = symbolTrue; break;
                        case "6": document.Range.Bookmarks[BookMark.cureItem5].Text = symbolTrue; break;
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("cureItem填写失败");
                }
            }
            switch (person.IsKnow)
            {
                case true: document.Range.Bookmarks[BookMark.painKnow].Text = symbolTrue; break;
                case false: document.Range.Bookmarks[BookMark.painDontKnow].Text = symbolTrue; break;
                default: document.Range.Bookmarks[BookMark.painKnow].Text = symbolTrue; break;
            }
            switch (person.IsMult)
            {
                case "多原发肿瘤患者": document.Range.Bookmarks[BookMark.isMultYes].Text = symbolTrue; break;
                default: document.Range.Bookmarks[BookMark.isMultNo].Text = symbolTrue; break;
            }

            if (person.IsCurrentHospital)
            {
                document.Range.Bookmarks[BookMark.hightest].Text = "綦江区文龙街道社区卫生服务中心";
            }
            else
            {
                document.Range.Bookmarks[BookMark.hightest].Text = person.DiagnoseHospitalName;
            }
            if (person.OnlyDeath)
            {
                document.Range.Bookmarks[BookMark.iarc1].Text = symbolTrue;
            }
            if (person.Clinic)
            {
                document.Range.Bookmarks[BookMark.iarc2].Text = symbolTrue;
            }
            if (person.XRay)
            {
                document.Range.Bookmarks[BookMark.iarc3].Text = symbolTrue;
            }
            if (person.Biochemistry)
            {
                document.Range.Bookmarks[BookMark.iarc4].Text = symbolTrue;
            }
            if (person.Pathology0)
            {
                document.Range.Bookmarks[BookMark.iarc5].Text = symbolTrue;
            }
            if (person.None)
            {
                document.Range.Bookmarks[BookMark.iarc6].Text = symbolTrue;
            }
            if (person.ClinicIndustry)
            {
                document.Range.Bookmarks[BookMark.industry1].Text = symbolTrue;
            }
            if (person.XrayIndustry)
            {
                document.Range.Bookmarks[BookMark.industry2].Text = symbolTrue;
            }
            if (person.CTIndustry)
            {
                document.Range.Bookmarks[BookMark.industry3].Text = symbolTrue;
            }
            if (person.UltrasonicIndustry)
            {
                document.Range.Bookmarks[BookMark.industry4].Text = symbolTrue;
            }
            if (person.EndoscopeIndustry)
            {
                document.Range.Bookmarks[BookMark.industry5].Text = symbolTrue;
            }
            if (person.MRIIndustry)
            {
                document.Range.Bookmarks[BookMark.industry6].Text = symbolTrue;
            }
            if (person.OperationIndustry)
            {
                document.Range.Bookmarks[BookMark.industry7].Text = symbolTrue;
            }
            if (person.CellIndustry)
            {
                document.Range.Bookmarks[BookMark.industry8].Text = symbolTrue;
            }
            if (person.PathologyIndustry0)
            {
                document.Range.Bookmarks[BookMark.industry9].Text = symbolTrue;
            }
            if (person.PathologyIndustry1)
            {
                document.Range.Bookmarks[BookMark.industry10].Text = symbolTrue;
            }
            if (person.PostmortemIndustry)
            {
                document.Range.Bookmarks[BookMark.industry11].Text = symbolTrue;
            }
            document.Range.Bookmarks[BookMark.mainDiagnose].Text = person.MainDiagnose;

            //身份证和出生日期
            if (person.IdCardNumber.Length > 15)
            {
                try
                {

                    char[] idCardChar = person.IdCardNumber.ToCharArray(); // 身份证，可能为空!
                    for (int j = 0; j < idCardChar.Length; j++)
                    {
                        document.Range.Bookmarks[idcardBookmarkList[j]].Text = idCardChar[j].ToString();
                    }
                    string birthDay = string.Format("{0}{1}{2}{3}年{4}{5}月{6}{7}日", idCardChar[6], idCardChar[7], idCardChar[8], idCardChar[9], idCardChar[10], idCardChar[11], idCardChar[12], idCardChar[13]);
                    document.Range.Bookmarks[BookMark.birthDay].Text = birthDay;
                    // 发病年龄；因需要读取出生日期，故写到这里;
                    try
                    {
                        int day = (DateTime.Parse(person.AccidentDay) - DateTime.Parse(birthDay)).Days / 365;
                        document.Range.Bookmarks[BookMark.accidentAge].Text = day.ToString() + " 岁";
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("发病年龄写入错误");
                    }

                }
                catch (Exception)
                {

                    message.ShowErrorDialog("身份证格式错误!可能会导致身份证、出生年月日及发病年龄无法自动填写");
                }



            }
            else
            {
                string showString = string.Format("读取患者'{0}'的信息时发生异常,可能的原因为该患者没有身份证号码!\r\n", person.Name);
                message.ShowErrorDialog(showString);
            }

            //死亡信息
            if (person.IsDeath)
            {
                document.Range.Bookmarks[BookMark.dead].Text = symbolTrue;
                document.Range.Bookmarks[BookMark.deathTime].Text = person.DeathTime;
                document.Range.Bookmarks[BookMark.deathReason].Text = person.DeathReason.Split(".")[1];
                if (person.DeathReason == "1.肿瘤")
                {

                    document.Range.Bookmarks[BookMark.deathIcd10Name].Text = person.MainDiagnose;
                    document.Range.Bookmarks[BookMark.deathIcd10Num].Text = person.Icd10;
                }
                else
                {
                    document.Range.Bookmarks[BookMark.deathIcd10Name].Text = person.DeathIcd10Name;
                    document.Range.Bookmarks[BookMark.deathIcd10Num].Text = person.DeathIcd10Number;
                }
            }
            else
            {
                document.Range.Bookmarks[BookMark.alive].Text = symbolTrue;
            }

            document.Save(savePath, SaveFormat.Docx);



            try
            {
                painCardDir.Add(person.Name, savePath);
            }
            catch (Exception ex)
            {

                Console.WriteLine("已经添加了重复项" + ex.ToString());
            }

            this.pathList.Add(savePath);
            Console.WriteLine("保存成功!");
        }
        private void checkFolder(string Dir)
        {
            if (Directory.Exists(Dir) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Dir);
            }
        }

        private void ReadDocxFromDb()
        {
            string Dir = Application.StartupPath + "\\cache";
            if (Directory.Exists(Dir) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Dir);
            }
            SQLiteCommand command = new SQLiteCommand("select file from turmor", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    string savePath = Dir + $"\\turmorReportCard.docx";
                    Console.WriteLine(savePath);
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        message.ShowErrorDialog("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。");
                    }
                }
            }
        }

        private void uBtnOpendir_Click(object sender, EventArgs e)
        {
            string savePath = @"D:\重庆市居民慢病报卡\";
            System.Diagnostics.Process.Start("explorer.exe", savePath);
        }

        private void uBtnPrint_Click(object sender, EventArgs e)
        {
            OpenPrinterForm(new PrinterUI(this.pathList,tumorFiles:true));
        }

        private void OpenPrinterForm(Form childForm)
        {
            if (currentPrintForm != null)
            {
                currentPrintForm.Close();
            }
            currentPrintForm = childForm;
            childForm.TopLevel = true;
            //childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.BringToFront();
            childForm.Show();
        }

        private void uiListBoxPainList_SelectedIndexChanged(object sender, EventArgs e)
        {

            ClearText();
            SetDefaultMessage();
        }

        /// <summary>
        /// 通过委托线程重新绑定数据;
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="ds"></param>
        public static void BindDataSource(ComboBox ctrl, DataSet ds)
        {
            try
            {
                ctrl.BeginUpdate();

                // make sure change it to false, or there will be exception if the droppedDownList is empty  

                ctrl.DroppedDown = false;

                string oldText = ctrl.Text;

                ctrl.DataSource = ds.Tables[0];
                ctrl.DisplayMember = ds.Tables[0].Columns[0].ColumnName;
                //ctrl.ValueMember = ds.Tables[0].Columns[0].ColumnName;

                // set the text, so user can input continuely  
                ctrl.Text = oldText;

                // set the cursor at the end of the text  
                ctrl.Focus();

                ctrl.Select(oldText.Length, oldText.Length);
                ctrl.MaxDropDownItems = 12;

                ctrl.AutoCompleteMode = AutoCompleteMode.None;

                //do not drop down if it is empty, or there will be exception
                if (ctrl.Items.Count > 0)
                {
                    ctrl.DroppedDown = true;
                }

                ctrl.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //statusLabel.Text = ex.Message;  
            }
            finally
            {
                ctrl.EndUpdate();
            }

        }

        private void uBtnUpdateAddress_Click(object sender, EventArgs e)
        {

            if (uiListBoxPainList.SelectedIndex == -1)
            {
                message.ShowWarningDialog("请先完善其他信息");
                return;
            }
            else
            {
                Pain currentSelectedPain = painLists[uiListBoxPainList.SelectedIndex];
                OpenPrinterForm(new AddressUI(ref currentSelectedPain));
            }
        }
        private void uiComboboxMainDiagnose_TextUpdate(object sender, EventArgs e)
        {
            //string mainDiagnoseInput = uiComboboxMainDiagnose.Text;
            Thread th = new Thread(UpdateMainDiagnoseAutoComplete);
            th.Start();
            /// <summary>
            /// 使用子线程开启，目的为了等待200ms，看用户是否有继续输入，如果有，则不更新选择列表，没有则更新;
            /// </summary>
            void UpdateMainDiagnoseAutoComplete()
            {
                DataSet ds = new DataSet();
                string abbr = uiComboboxMainDiagnose.Text.Trim();
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                Thread.Sleep(300);
                if (abbr != uiComboboxMainDiagnose.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    ds = SearchDb($"select hospitalDiagnose from icdo3 where  pinyinHospital like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select hospitalDiagnose from icdo3 where  hospitalDiagnose like '%{abbr}%'");

                }
                ds = GetDistinctTable(ds.Tables[0], "hospitalDiagnose");
                // 重新邦定  
                uiComboboxMainDiagnose.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxMainDiagnose, ds);
                Cursor = Cursors.Default;
            }
        }


        private DataSet SearchDb(string sql)
        {

            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, m_dbConnection);
            adapter.Fill(ds, "hospitalDiagnose");
            return ds;
        }

        private void uiComboboxpathological_TextUpdate(object sender, EventArgs e)
        {
            Thread th = new Thread(UpdatePathologicalAutocomplete);
            th.Start();

            void UpdatePathologicalAutocomplete()
            {
                DataSet ds = new DataSet();
                string abbr = uiComboboxpathological.Text.Trim();
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                Thread.Sleep(300);
                if (abbr != uiComboboxpathological.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    ds = SearchDb($"select pathologicalDiagnose from icdo3 where  pinyinPathological like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select pathologicalDiagnose from icdo3 where  pathologicalDiagnose like '%{abbr}%'");
                }
                ds = GetDistinctTable(ds.Tables[0], "pathologicalDiagnose");
                // 重新邦定  
                uiComboboxpathological.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxpathological, ds);
                Cursor = Cursors.Default;
            }
        }

        private void uiComboboxMainDiagnose_Enter(object sender, EventArgs e)
        {
            this.isMain = true;
            this.ispathological = false;
        }

        private void uiComboboxMainDiagnose_Leave(object sender, EventArgs e)
        {
            this.isMain = false;
            this.ispathological = true;
        }

        private void uiComboboxpathological_SelectedValueChanged(object sender, EventArgs e)
        {
            /*if (ispathological)
            {
                var result = new DatabaseUnit().GetIcdO3Numbers(uiComboboxpathological.Text);
                if (result != null)
                {
                    diagnosesDict = result;
                    SetCheckResultToTextBox();
                }
            }*/
            SaveChange();
        }

        private void uiComboboxMainDiagnose_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isMain)  // 加这个判断，防止在其他控件改变该控件值的时候，循环赋值;
            {
                var result = new DatabaseUnit().GetIcdO3Numbers(uiComboboxMainDiagnose.Text);
                if (result != null)
                {
                    diagnosesDict = result;
                    SetCheckResultToTextBox();
                }
            }
            SaveChange();
        }

        private void uiComboboxIcd10_TextUpdate(object sender, EventArgs e)
        {
            Thread th = new Thread(UpdateIcd10Autocomplete);
            th.Start();
            void UpdateIcd10Autocomplete()
            {
                DataSet ds = new DataSet();
                string abbr = uiComboboxIcd10.Text.Trim();
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                Thread.Sleep(300);
                if (abbr != uiComboboxIcd10.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }


                if (isAbc)
                {
                    ds = SearchDb($"select name,num from icd10 where name_pinyin like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select name,num from icd10 where name match '{abbr}'");
                }
                ds = GetDistinctTable(ds.Tables[0], "name");
                // 重新邦定  
                uiComboboxIcd10.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxIcd10, ds);
                Cursor = Cursors.Default;
            }

        }

        private void TurmorReportUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_dbConnection.Close();
        }

        private void uiComboboxTumorLoaction_TextUpdate(object sender, EventArgs e)
        {

            Thread th = new Thread(UpdateanatomicalLocationAutocomplete);
            th.Start();

            void UpdateanatomicalLocationAutocomplete()
            {
                DataSet ds = new DataSet();
                string abbr = uiComboboxTumorLoaction.Text.Trim();
                bool isAbc = Regex.IsMatch(abbr, @"^[A-Za-z]+$");
                Thread.Sleep(300);
                if (abbr != uiComboboxTumorLoaction.Text.Trim() || abbr.Length < 1)
                {
                    return;
                }
                if (isAbc)
                {
                    ds = SearchDb($"select anatomicalLocationName,anatomicalLocationPinyin from icdo3 where anatomicalLocationPinyin like '%{abbr}%'");
                }
                else
                {
                    ds = SearchDb($"select anatomicalLocationName,anatomicalLocationPinyin from icdo3 where anatomicalLocationName like '%{abbr}%'");
                }
                ds = GetDistinctTable(ds.Tables[0], "anatomicalLocationName");
                // 重新邦定  
                uiComboboxTumorLoaction.BeginInvoke(new ReBindDataSource(BindDataSource), uiComboboxTumorLoaction, ds);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 获取对固定列不重复的新DataTable
        /// </summary>
        /// <param name="dt">含有重复数据的DataTable</param>
        /// <param name="colName">需要验证重复的列名</param>
        /// <returns>新的DataSet，colName列不重复，表格式保持不变</returns>
        private DataSet GetDistinctTable(DataTable dt, string colName)
        {
            DataView dv = dt.DefaultView;
            DataTable dtCardNo = dv.ToTable(true, colName);
            DataTable Pointdt = dv.ToTable();
            Pointdt.Clear();
            for (int i = 0; i < dtCardNo.Rows.Count; i++)
            {
                DataRow dr = dt.Select(colName + "='" + dtCardNo.Rows[i][0].ToString() + "'")[0];
                Pointdt.Rows.Add(dr.ItemArray);
            }
            DataSet newds = new DataSet();
            newds.Tables.Add(Pointdt);
            return newds;
        }

        private void uiListBoxPainList_ItemDoubleClick(object sender, EventArgs e)
        {
            string selectedPain = uiListBoxPainList.SelectedItem.ToString();
            string selectedPainCardPath = "";
            bool isFilled = painCardDir.TryGetValue(selectedPain, out selectedPainCardPath);
            if (isFilled)
            {
                System.Diagnostics.Process.Start(selectedPainCardPath);
            }
            else
            {
                message.ShowErrorDialog($"{selectedPain}的报卡文件未找到,可能的原因是没有先生成报卡文件,请重试。");
            }

        }

        private void uiComboboxCure_SelectedValueChanged(object sender, EventArgs e)
        {
            if (uiComboboxCure.Text == "治疗")
            {
                uiComboboxCureItem.Enabled = true;
                uiComboboxCureItem.Text = "";
            }
            else
            {
                uiComboboxCureItem.Enabled = false;
                uiComboboxCureItem.Text = "无需填写";
            }
        }

        private void uiRadioButtonDead_Click(object sender, EventArgs e)
        {
            if (uiListBoxPainList.SelectedIndex == -1)
            {
                message.ShowErrorDialog("请先完善其他信息");
                return;
            }
            else
            {
                if (uiRadioButtonDead.Checked)
                {
                    Pain currentSelectedPain = painLists[uiListBoxPainList.SelectedIndex];
                    OpenPrinterForm(new DeathInformationUI(ref currentSelectedPain, ref uiRadioButtonDead, ref uiRadioButtonunDead));
                }
                SaveChange();
            }

        }
        private void ChangeStyle()
        {

            foreach (var item in radiobuttons)
            {
                if (item.Checked)
                {
                    item.Style = UIStyle.LightRed;
                }
                else
                {
                    item.Style = UIStyle.Office2010Silver;
                }
            }
            foreach (var item in checkBoxes)
            {
                if (item.Checked)
                {
                    item.Style = UIStyle.LightRed;
                }
                else
                {
                    item.Style = UIStyle.Office2010Silver;
                }
            }
        }

        private void uiComboboxIcd10_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CheckLateral()
        {

            bool isLateral = false;
            SQLiteCommand command = new SQLiteCommand($"select lateral from icdo3 where icd10 = '{uiComboboxIcd10.Text}'", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    isLateral = (bool)reader[0];
                }
                catch (Exception)
                {

                    Console.WriteLine("isLateral is None,set default false");
                }
                reader.Close();
                break;
            }
            if (isLateral)
            {
                uiComboboxlateral.Enabled = true;
                uiComboboxlateral.Text = "";
            }
            else
            {
                uiComboboxlateral.Enabled = false;
                uiComboboxlateral.Text = "无需填写侧位";
            }
        }

        private void uiComboboxIcd10_TextChanged(object sender, EventArgs e)
        {
            CheckLateral();

        }
    }
}
