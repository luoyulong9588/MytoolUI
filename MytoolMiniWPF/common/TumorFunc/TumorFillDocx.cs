using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Aspose.Words;
using MaterialDesignThemes.Wpf.Converters.CircularProgressBar;
using MytoolMiniWPF;
using MytoolMiniWPF.views;

namespace MytoolMiniWPF.common.TumorFunc
{
    internal class TumorFillDocx
    {
       private  PatientForTumor patient;
        private static string LocalHospital = "綦江区文龙街道社区卫生服务中心";
        private List<string> idcardBookmarkList;
        string savePath;
        private string symbolTrue = "☑";
        private string symbolFalse = "□";
        private string docxFilePath = "./cache/turmorReportCard.docx";
        private FileStream fileStream = null;
        public TumorFillDocx( PatientForTumor data) {
            patient = data;
            savePath = string.Format(@"D:\MytoolDataFiles\{0}\肿瘤报卡.{1}.{2}.{3}.docx", "肿瘤报卡", patient.Name, patient.DiagnoseName, Convert.ToDateTime(patient.ReportDate).ToString("yyyy年MM月dd日"));

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
            fileStream = File.Open(docxFilePath, FileMode.Open);
            FillDocx();
            fileStream.Close();
            Process.Start("explorer.exe", savePath);
        }

        private void FillDocx()
        {
            
            if (Directory.Exists("D:\\MytoolDataFiles\\肿瘤报卡\\") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory("D:\\MytoolDataFiles\\肿瘤报卡\\");
            }

            Document document = new Document(fileStream);
            document.Range.Bookmarks[BookMark.isCurrentHospital].Text = symbolTrue;
            document.Range.Bookmarks[BookMark.diagnoseHospital].Text = string.IsNullOrEmpty(patient.DiagnoseHospital) ? LocalHospital : patient.DiagnoseHospital;

            if (patient.IsInHospital)
            {
                document.Range.Bookmarks[BookMark.painId].Text = patient.Id;
            }
            else
            {
                document.Range.Bookmarks[BookMark.painIdOut].Text = patient.Id;
            }

            switch (patient.Cure)
            {
                case "未治疗":
                    document.Range.Bookmarks[BookMark.unCure].Text = symbolTrue;
                    break;
                case "治疗":
                    document.Range.Bookmarks[BookMark.cure].Text = symbolTrue;
                    break;
                case "不明":
                    document.Range.Bookmarks[BookMark.dontknowCure].Text = symbolTrue;
                    break;
                default:
                    document.Range.Bookmarks[BookMark.dontknowCure].Text = symbolTrue;
                    break;
            }

            switch (patient.CureItem)
            {
                case "手术治疗":
                    document.Range.Bookmarks[BookMark.cureItem0].Text = symbolTrue;
                    break;
                case "放射治疗":
                    document.Range.Bookmarks[BookMark.cureItem1].Text = symbolTrue;
                    break;
                case "化学治疗":
                    document.Range.Bookmarks[BookMark.cureItem2].Text = symbolTrue;
                    break;
                case "内分泌治疗":
                    document.Range.Bookmarks[BookMark.cureItem3].Text = symbolTrue;
                    break;
                case "靶向治疗":
                    document.Range.Bookmarks[BookMark.cureItem4].Text = symbolTrue;
                    break;
                case "免疫治疗":
                    document.Range.Bookmarks[BookMark.cureItem5].Text = symbolTrue;
                    break;
                default:
                    break;
            }



            document.Range.Bookmarks[BookMark.painName].Text = patient.Name;
            document.Range.Bookmarks[BookMark.painAge].Text = patient.Age;
            document.Range.Bookmarks[BookMark.gender].Text = patient.Gender;
            document.Range.Bookmarks[BookMark.addrAlways].Text = patient.CurrentAddress;
            document.Range.Bookmarks[BookMark.addrBorn].Text = patient.RegistrationAddress; // 户籍地址
            document.Range.Bookmarks[BookMark.addrWork].Text = patient.WorkAddr; // 户籍地址
            
            document.Range.Bookmarks[BookMark.reportDoctor].Text = patient.ReportDoctorName;
            document.Range.Bookmarks[BookMark.nation].Text = patient.Nation;
            //document.Range.Bookmarks[BookMark.phone].Text = person.Phone;
            document.Range.Bookmarks[BookMark.marriage].Text = patient.Marriage;
            document.Range.Bookmarks[BookMark.reportTime].Text = DateTime.Parse(patient.ReportDate).ToString("yyyy-MM-dd");
            
            #region 2名联系人
            document.Range.Bookmarks[BookMark.contactName1].Text = string.IsNullOrEmpty(patient.FirstContact)? patient.Name:patient.FirstContact;
            document.Range.Bookmarks[BookMark.contactPhone1].Text = string.IsNullOrEmpty(patient.FirstContactPhone) ? patient.Phone : patient.FirstContactPhone;
            switch (patient.FirstContactRelation)
            {
                case "本人": 
                    document.Range.Bookmarks[BookMark.contactRelation_br].Text = symbolTrue;
                    break;
                case "家属":
                    document.Range.Bookmarks[BookMark.contactRelation_js].Text = symbolTrue;
                    break;
                case "朋友":
                    document.Range.Bookmarks[BookMark.contactRelation_py].Text = symbolTrue;
                    break;
                case "工作单位":
                    document.Range.Bookmarks[BookMark.contactRelation_gzdw].Text = symbolTrue;
                    break;
                case "不详":
                    document.Range.Bookmarks[BookMark.contactRelation_bx].Text = symbolTrue;
                    break;
                default:
                    document.Range.Bookmarks[BookMark.contactRelation_bx].Text = symbolTrue;
                    break;
            }

            document.Range.Bookmarks[BookMark.contactName2].Text = string.IsNullOrEmpty(patient.SecondContact) ? patient.Name : patient.SecondContact;
            document.Range.Bookmarks[BookMark.contactPhone2].Text = string.IsNullOrEmpty(patient.SecondtContactPhone) ? patient.Phone : patient.SecondtContactPhone;
            switch (patient.SecondContactRelation)
            {
                case "本人":
                    document.Range.Bookmarks[BookMark.contactRelation2_br].Text = symbolTrue;
                    break;
                case "家属":
                    document.Range.Bookmarks[BookMark.contactRelation2_js].Text = symbolTrue;
                    break;
                case "朋友":
                    document.Range.Bookmarks[BookMark.contactRelation2_py].Text = symbolTrue;
                    break;
                case "工作单位":
                    document.Range.Bookmarks[BookMark.contactRelation2_gzdw].Text = symbolTrue;
                    break;
                case "不详":
                    document.Range.Bookmarks[BookMark.contactRelation2_bx].Text = symbolTrue;
                    break;
                default:
                    document.Range.Bookmarks[BookMark.contactRelation2_bx].Text = symbolTrue;
                    break;
            }
            #endregion



            document.Range.Bookmarks[BookMark.vocation].Text = patient.Vocation;
            document.Range.Bookmarks[BookMark.workType].Text = patient.WorkType;
            document.Range.Bookmarks[BookMark.education].Text = patient.Education;

            if (patient.IsKnown)
            {
                document.Range.Bookmarks[BookMark.painKnow].Text = symbolTrue;

            }
            else
            {
                document.Range.Bookmarks[BookMark.painDontKnow].Text = symbolTrue;

            }
            switch (patient.SurvivalStatus)
            {
                case "存活":
                    document.Range.Bookmarks[BookMark.alive].Text = symbolTrue;
                    break;
                case "死亡":
                    document.Range.Bookmarks[BookMark.dead].Text = symbolTrue;
                    document.Range.Bookmarks[BookMark.deathTime].Text = patient.DeathTime;
                    document.Range.Bookmarks[BookMark.deathReason].Text = patient.DeathReason;
                    document.Range.Bookmarks[BookMark.deathIcd10Num].Text = patient.DeathIcd10Number;
                    document.Range.Bookmarks[BookMark.deathIcd10Name].Text = patient.DeathIcd10Name;
                    break;
                case "失访":
                    document.Range.Bookmarks[BookMark.lost].Text = symbolTrue;
                    break;
                case "拒访":
                    document.Range.Bookmarks[BookMark.refuse].Text = symbolTrue;
                    break;
                default:
                    document.Range.Bookmarks[BookMark.alive].Text = symbolTrue;
                    break;
            }

            if (patient.IsMultTumors)
            {
                document.Range.Bookmarks[BookMark.isMultYes].Text = symbolTrue;
            }
            else
            {
                document.Range.Bookmarks[BookMark.isMultNo].Text = symbolTrue;
            }

            document.Range.Bookmarks[BookMark.accidentDay].Text = patient.OnsetDate;
            document.Range.Bookmarks[BookMark.accidentAge].Text = patient.OnsetAge;

            document.Range.Bookmarks[BookMark.IARC].Text = string.Join(",", patient.DiagnosticIARC);
            document.Range.Bookmarks[BookMark.IndustryStandard].Text = string.Join(",", patient.DiagnosticIndustryStandard);
            document.Range.Bookmarks[BookMark.mainDiagnose].Text = patient.DiagnoseName;
            document.Range.Bookmarks[BookMark.pathological].Text = patient.PathologyDiagnoseName;
            document.Range.Bookmarks[BookMark.turmorLocation].Text = patient.PathologicalSite;
            document.Range.Bookmarks[BookMark.turmorBodyLocation].Text = patient.AnatomicalSite;
            document.Range.Bookmarks[BookMark.morphology].Text = patient.Morphology;
            document.Range.Bookmarks[BookMark.hightest].Text = patient.ToplevelDiagnosticUnit;

            document.Range.Bookmarks[BookMark.icdNumber].Text = patient.Icd10;
            document.Range.Bookmarks[BookMark.Tumorlevel].Text = patient.TumorLevel;


            switch (patient.TumorSide)
            {
                case "左侧":
                    document.Range.Bookmarks[BookMark.lateralLeft].Text = symbolTrue;
                    break;
                case "右侧":
                    document.Range.Bookmarks[BookMark.lateralRight].Text = symbolTrue;
                    break;
                case "双侧":
                    document.Range.Bookmarks[BookMark.lateralDouble].Text = symbolTrue;
                    break;
                default:
                    document.Range.Bookmarks[BookMark.lateralNone].Text = symbolTrue;
                    break;
            }

            // 处理身份证和生日
            if (patient.IdCardNumber.Length > 15)
            {
                try
                {

                    char[] idCardChar = patient.IdCardNumber.ToCharArray(); // 身份证，可能为空!
                    for (int j = 0; j < idCardChar.Length; j++)
                    {
                        document.Range.Bookmarks[idcardBookmarkList[j]].Text = idCardChar[j].ToString();
                    }
                    string birthDay = string.Format("{0}{1}{2}{3}年{4}{5}月{6}{7}日", idCardChar[6], idCardChar[7], idCardChar[8], idCardChar[9], idCardChar[10], idCardChar[11], idCardChar[12], idCardChar[13]);
                   document.Range.Bookmarks[BookMark.birthDay].Text = birthDay;
                    // 发病年龄；因需要读取出生日期，故写到这里;
                    //try
                    //{
                    //    int day = (DateTime.Parse(person.AccidentDay) - DateTime.Parse(birthDay)).Days / 365;
                    //    document.Range.Bookmarks[BookMark.accidentAge].Text = day.ToString() + " 岁";
                    //}
                    //catch (Exception)
                    //{

                    //    Console.WriteLine("发病年龄写入错误");
                    //}

                }
                catch (Exception)
                {

                    UMessageBox.Show("身份证格式错误!可能会导致身份证、出生年月日及发病年龄无法自动填写");
                }



            }
            else
            {
                string showString = string.Format("读取患者'{0}'的信息时发生异常,可能的原因为该患者没有身份证号码!\r\n", patient.Name);
                UMessageBox.Show(showString);
            }
            document.Save(savePath, SaveFormat.Docx);
        }


    }
}
