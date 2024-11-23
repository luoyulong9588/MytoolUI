using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Sunny.UI;
using Excel = Microsoft.Office.Interop.Excel;
using Aspose.Words;

namespace MytoolUI
{
    class FillCardToWord
    {
        public List<Pain> painList
        {
            get; set;
        }
        private string projectDir = @"D:\重庆市居民慢病报卡\";
        private string diagnoticsPath;
        private string userName = "罗玉龙";
        private List<string> pathList = new List<string>();
        public List<Pain> painReportedList = new List<Pain>();
        private UIListBox checkedListBoxCOPD;
        private UIListBox checkedListBoxAMI;
        private UIListBox checkedListBoxAPoplexy;
        private UIProcessBar progressBar;
        private FileStream[] streams = new FileStream[7];
        private UIMessageForm message = new UIMessageForm();

        public FillCardToWord(List<Pain> painList)
        {
            this.painList = painList;

        }

        public List<string> startProgram(string userName, UIProcessBar progressBar, UIListBox[] listBoxes)
        {
            this.userName = userName;
            checkFolder(this.projectDir);
            int maxLenth = this.painList.Count;
            this.progressBar = progressBar;
            this.progressBar.Maximum = maxLenth;
            this.checkedListBoxCOPD = listBoxes[0];
            this.checkedListBoxAMI = listBoxes[1];
            this.checkedListBoxAPoplexy = listBoxes[2];
            for (int i = 0; i < streams.Length; i++)
            {
                string modPath = string.Format(@"{0}\cache\mod{1}.docx", Application.StartupPath, i);
                streams[i] = File.Open(modPath, FileMode.Open);
            }
            foreach (Pain person in this.painList)
            {
                this.checkDisease(person);
                if (this.progressBar.Value < maxLenth)
                {
                    this.progressBar.Value += 1;
                }
            }

            foreach (FileStream item in streams)
            {
                item.Close();
            }

            try
            {
                SaveDiagnoticsToXlsx();
                message.ShowInfoDialog("报卡完毕", $"报卡日志已自动导出\r\n" +
                    $"默认保存路径:\r\n D -> 重庆市居民慢病报卡 -> '报卡月份'\r\n" +
                    $"本次保存路径:{this.diagnoticsPath}",UIStyle.Red);
            }
            catch (Exception ex)
            {

                message.ShowErrorDialog($"尝试生成报卡日志出错!可能的原因为未安装Microsoft Office或安装文件丢失！\r\n{ex}");
            }

            return pathList;

        }

        private void checkDisease(Pain person)
        {

            //= (Pain)obj;
            //Console.WriteLine(person.Name);
            FileStream fileStream;
            List<string> diagSselect = new List<string>
            //{ "慢性阻塞性肺疾病", "慢性支气管炎", "哮喘", "肺气肿", "支气管扩张", "急性支气管炎","心肌梗死","急性冠脉综合征","脑梗死","脑出血"};
            { "慢性阻塞性肺疾病", "慢性支气管炎", "哮喘", "肺气肿", "急性支气管炎","心肌梗死","急性冠脉综合征","脑梗死","脑出血"}; //2022年1月8日 16:08 移除支气管扩张

            List<string> educationList = new List<string> { "不详", "不详", "小学" };

            List<string> marriageList = new List<string> { "已婚", "已婚", "已婚", "已婚", "已婚", "已婚", "已婚", "已婚", "未婚" };

            List<string> familyList = new List<string> { "否认", "否认", "否认", "无", "不详" };

            List<string> hightestList = new List<string> { "③", "③", "③", "③", "③", "③", "③", "③", "③", "⑥", "④", "④", "④", "④", "①", "②" };

            List<string> diagnoseList = new List<string> { "①③⑧", "①③", "①③⑧", "③⑧", "①", "①", "①③", "①⑧", "①③" };

            List<string> hospitalName = new List<string> { "文龙医院", "文龙医院", "文龙医院", "文龙医院", "文龙医院", "古南医院","新桥医院","大坪医院","重医附一院","西南医院"};

            Dictionary<string, string> vocationDict = new Dictionary<string, string>()
            {
                { "公务员","①"},
                { "专业技术人员","②"},
                { "职员","③"},
                { "企业管理者","④"},
                { "工人","⑤"},
                { "农民","⑥"},
                { "学生","⑦"},
                { "现役军人","⑧"},
                { "自由职业者","⑨"},
                { "个体经营者","⑩"},
                { "无业人员","⑪"},
                { "无职业","⑪"},
                { "离退休人员","⑫"},
                { "退(离)休人员","⑫"},
                { "其他","⑬"},
            };

            Dictionary<string, string> icd10Dict = new Dictionary<string, string>()
            {
                {"慢性阻塞性肺疾病","J44.100" },{"慢性支气管炎","J42.x00" },{"哮喘","J45.900" },{"支气管扩张","J47.x00" },
                {"急性支气管炎","J40.x00" },{"急性心肌梗死","I21.900" },{"心肌梗死","I21.900x011" },{"急性冠脉综合征","I24.901"},
                {"脑梗死","I63.900" },{"脑出血","I64.x00" },{"肺气肿","J43.900" }
            };
            string childFolder = Convert.ToDateTime(person.OutDay).ToString("yyyy年MM月");
            checkFolder(this.projectDir + childFolder);
            string savePath, classify;
            for (int i = 0; i < diagSselect.Count; i++)
            {
                if (person.MainDiagnose == diagSselect[i] && this.userName == person.DoctorName)
                {
                    this.painReportedList.Add(person); //为报卡日志存储数据
                    if (i <= 4)
                    {
                        fileStream = streams[i];
                        classify = "肺病报卡";
                    }
                    else
                    {
                        fileStream = streams[6];
                        classify = "卒中报卡";

                    }
                    savePath = string.Format(@"{0}\{1}\{2}.{3}.{4}.{5}.docx", projectDir, childFolder, person.Name, classify, person.MainDiagnose, Convert.ToDateTime(person.OutDay).ToString("yyyy年MM月dd日"));

                    List<string> idcardBookmarkList = new List<string>()
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
                    Document document = new Document(fileStream);

                    DocumentBuilder builder = new DocumentBuilder(document);
                    Font font = builder.Font;
                    font.Color = System.Drawing.Color.DarkBlue;
                    font.Italic = false;
                    font.Name = "庞中华行书";
                    font.Size = 9;



                    document.Range.Bookmarks[BookMark.painName].Text = person.Name;
                    document.Range.Bookmarks[BookMark.painId].Text = person.Id;
                    document.Range.Bookmarks[BookMark.gender].Text = person.Gender;
                    document.Range.Bookmarks[BookMark.phone].Text = person.Phone;
                    document.Range.Bookmarks[BookMark.contactPerson].Text = person.Name;
                    document.Range.Bookmarks[BookMark.nation].Text = "汉族";
                    document.Range.Bookmarks[BookMark.xueli].Text = educationList[new Random().Next(educationList.Count)];
                    document.Range.Bookmarks[BookMark.reportDoctor].Text = person.DoctorName;
                    document.Range.Bookmarks[BookMark.marriage].Text = marriageList[new Random().Next(marriageList.Count)];
                    document.Range.Bookmarks[BookMark.family].Text = familyList[new Random().Next(familyList.Count)];
                    
                    string hightest = hightestList[new Random().Next(hightestList.Count)];
                    document.Range.Bookmarks[BookMark.hightest].Text = hightest;
                    if (hightest == "③")
                    {
                        document.Range.Bookmarks[BookMark.diagnoseHospital].Text = "綦江区人民医院";
                    }
                    else if (hightest == "⑥")
                    {
                        document.Range.Bookmarks[BookMark.diagnoseHospital].Text = "不详";
                    }
                    else if (hightest == "④")
                    {
                        document.Range.Bookmarks[BookMark.diagnoseHospital].Text = hospitalName[new Random().Next(6)];
                    }
                    else if (hightest == "①")
                    {
                        document.Range.Bookmarks[BookMark.diagnoseHospital].Text = hospitalName[new Random().Next(4) + 6];
                    }
                    else if (hightest == "②")
                    {
                        document.Range.Bookmarks[BookMark.diagnoseHospital].Text = hospitalName[new Random().Next(4) + 6];
                    }

                    document.Range.Bookmarks[BookMark.addrAlways].Text = person.HomeAddr;
                    document.Range.Bookmarks[BookMark.addrBorn].Text = person.HomeAddr;
                    document.Range.Bookmarks[BookMark.addrWork].Text = person.WorkAddr;
                    DateTime inday = Convert.ToDateTime(person.InDay);
                    DateTime outday = Convert.ToDateTime(person.OutDay);
                    // 把诊断时间限制在25天之内;
                    inday = outday.AddDays(-25) > inday ? outday.AddDays(-25) : inday;

                    document.Range.Bookmarks[BookMark.reportTime].Text = outday.ToString("yyyy年MM月dd日");
                    document.Range.Bookmarks[BookMark.diagTime].Text = inday.ToString("yyyy年MM月dd日");
                    document.Range.Bookmarks[BookMark.ensureTime].Text = inday.ToString("yyyy年MM月dd日");
                    document.Range.Bookmarks[BookMark.icdNumber].Text = icd10Dict[diagSselect[i]];
                    //document.Range.Bookmarks[BookMark.relation].Text = "本人";
                    document.Range.Bookmarks[BookMark.phoneContact].Text = person.Phone;
                    //document.Range.Bookmarks[BookMark.contactPerson].Text

                    //职业;
                    if (vocationDict.ContainsKey(person.Vocation))
                    {
                        document.Range.Bookmarks[BookMark.vocation].Text = vocationDict[person.Vocation];
                    }
                    else
                    {
                        document.Range.Bookmarks[BookMark.vocation].Text = vocationDict["其他"];
                    }

                    if (person.MainDiagnose == "心肌梗死" || person.MainDiagnose == "急性冠脉综合征")
                    {
                        List<string> amiList = new List<string> { "①⑨", "①⑨", "①⑨", "①⑨", "①⑨", "⑨", "①" };

                        document.Range.Bookmarks[BookMark.diagoseNumer].Text = amiList[new Random().Next(amiList.Count)];
                        document.Range.Bookmarks[BookMark.diagnoseString].Text = person.MainDiagnose;
                        this.checkedListBoxAMI.Items.Add(person.Name);
                    }
                    else if (person.MainDiagnose == "脑梗死" || person.MainDiagnose == "脑出血")
                    {
                        List<string> apoplexyList = new List<string> { "①②⑨", "①②⑨", "①②⑨", "①②⑨", "①②⑨", "①②⑨", "①②⑤⑨", "①⑤" };
                        document.Range.Bookmarks[BookMark.diagoseNumer].Text = apoplexyList[new Random().Next(apoplexyList.Count)];
                        document.Range.Bookmarks[BookMark.diagnoseString].Text = person.MainDiagnose;
                        this.checkedListBoxAPoplexy.Items.Add(person.Name);
                    }
                    else
                    {
                        document.Range.Bookmarks[BookMark.diagoseNumer].Text = diagnoseList[new Random().Next(diagnoseList.Count)];
                        this.checkedListBoxCOPD.Items.Add(person.Name);
                    }
                    try
                    {
                        char[] idCardChar = person.IdCardNumber.ToCharArray(); // 身份证，可能为空!
                        for (int j = 0; j < idCardChar.Length; j++)
                        {
                            document.Range.Bookmarks[idcardBookmarkList[j]].Text = idCardChar[j].ToString();


                        }
                        string birthDay = string.Format("{0}{1}{2}{3}年{4}{5}月{6}{7}日", idCardChar[6], idCardChar[7], idCardChar[8], idCardChar[9], idCardChar[10], idCardChar[11], idCardChar[12], idCardChar[13]);
                        document.Range.Bookmarks[BookMark.birthDay].Text = birthDay;
                    }
                    catch (Exception ex)
                    {
                        string showString = string.Format("读取患者{0}的信息时发生异常,可能的原因为该患者没有身份证号码!\r\n{1}", person.Name, ex);
                        message.ShowErrorDialog(showString);
                    }

                    document.Save(savePath);

                    this.diagnoticsPath = string.Format(@"{0}{1}慢病报卡日志.导出日期{2}.xlsx", projectDir, Convert.ToDateTime(person.OutDay).ToString("yyyy年MM月"), DateTime.Now.ToString("yyyy-MM-dd"));
                    pathList.Add(savePath.ToString());  // 作为返回
                }
            }

        }
        private void SaveDiagnoticsToXlsx()
        {
            Excel.Application xlapp = new Excel.Application();
            xlapp.Visible = false;
            xlapp.DisplayAlerts = false;
            Excel.Workbook wb = xlapp.Workbooks.Add();
            Excel.Worksheet ws = (Excel.Worksheet)xlapp.Workbooks[1].Worksheets[1];
            string[] title = { "报卡类别", "出院时间","姓名", "性别","年龄", "出院诊断",
                                "联系电话","身份证号","报卡者"};
            int rows = this.painReportedList.Count;
            int columns = title.Length;
            object[,] objData = new object[rows + 4, columns + 1];
            objData[0, 0] = "报卡日志" + DateTime.Now.ToString("F");
            for (int i = 0; i < title.Length; i++)
            {
                objData[1, i] = title[i];
            }

            for (int j = 0; j < rows; j++)
            {
                objData[j + 2, 0] = painReportedList[j].MainDiagnose;
                objData[j + 2, 1] = painReportedList[j].OutDay;
                objData[j + 2, 2] = painReportedList[j].Name;
                objData[j + 2, 3] = painReportedList[j].Gender;
                objData[j + 2, 4] = painReportedList[j].Age;
                objData[j + 2, 5] = painReportedList[j].OutDiagnose;
                objData[j + 2, 6] = painReportedList[j].Phone;
                objData[j + 2, 7] = painReportedList[j].IdCardNumber;
                objData[j + 2, 8] = painReportedList[j].DoctorName;
            }
            Excel.Range range = (Excel.Range)ws.Cells[1, 1];
            range = range.get_Resize(rows + 3, columns);
            range.NumberFormatLocal = "@";
            range.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, objData);
            wb.SaveAs(this.diagnoticsPath);
            wb.Close();
            xlapp.Quit();
        }
        private void checkFolder(string Dir)
        {
            if (Directory.Exists(Dir) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Dir);
            }
        }
    }

}
