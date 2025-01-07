using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Drawing;
using FontStyle = System.Drawing.FontStyle;
using Color = System.Drawing.Color;

namespace MytoolMiniWPF
{
    

    public struct BookMark
    {
        public static string addrAlways = "addrAlways";
        public static string addrBorn = "addrBorn";
        public static string addrWork = "addrWork";
        public static string birthDay = "birthDay";
        public static string contactPerson = "contactPerson";
        public static string diagoseNumer = "diagoseNumer";
        public static string diagTime = "diagTime";
        public static string ensureTime = "ensureTime";
        public static string family = "family";
        public static string gender = "gender";
        public static string hightest = "hightest";
        public static string icdNumber = "icdNumber";
        public static string marriage = "marriage";
        public static string nation = "nation";
        public static string painId = "painId";
        public static string painName = "painName";
        public static string phone = "phone";
        public static string relation = "relation";
        public static string reportDoctor = "reportDoctor";
        public static string reportTime = "reportTime";
        public static string vocation = "vocation";
        public static string xueli = "xueli";
        public static string diagnoseString = "diagnoseString";
        public static string idCard0 = "idCard0";
        public static string idCard1 = "idCard1";
        public static string idCard2 = "idCard2";
        public static string idCard3 = "idCard3";
        public static string idCard4 = "idCard4";
        public static string idCard5 = "idCard5";
        public static string idCard6 = "idCard6";
        public static string idCard7 = "idCard7";
        public static string idCard8 = "idCard8";
        public static string idCard9 = "idCard9";
        public static string idCard10 = "idCard10";
        public static string idCard11 = "idCard11";
        public static string idCard12 = "idCard12";
        public static string idCard13 = "idCard13";
        public static string idCard14 = "idCard14";
        public static string idCard15 = "idCard15";
        public static string idCard16 = "idCard16";
        public static string idCard17 = "idCard17";
        // for 流掉表
        public static string idCard = "idCard";
        public static string painAge = "painAge";
        public static string reportDoctor1 = "reportDoctor1";
        public static string reportTime1 = "reportTime1";
        public static string painName1 = "painName1";
        public static string gender1 = "gender1";
        public static string id1 = "id1";

        //流调表  家属信息
        public static string painNameContact = "painNameContact";
        public static string genderContact = "genderContact";
        public static string painAgeContact = "painAgeContact";
        public static string phoneContact = "phoneContact";
        public static string addrAlwaysContact = "addrAlwaysContact";
        public static string idCardContact = "idCardContact";

        //授权委托书
        public static string painNamePerimssion = "painNamePerimssion";
        public static string painGenderPerimssion = "painGenderPerimssion";
        public static string painAgePermission = "painAgePermission";
        public static string painNamePerimssion1 = "painNamePerimssion1";
        public static string painGenderPerimssion1 = "painGenderPerimssion1";
        public static string painAgePermission1 = "painAgePermission1";
        public static string painIdCardPermission = "painIdCardPermission";
        public static string painAddressPermission = "painAddressPermission";
        public static string painNameContactPermission = "painNameContactPermission";
        public static string painGenderContactPermission = "painGenderContactPermission";
        public static string painAgeContactPermission = "painAgeContactPermission";
        public static string painPhoneContactPermission = "painPhoneContactPermission";
        public static string painIdCardContactPermission = "painIdCardContactPermission";
        public static string painAddressContactPermission = "painAddressContactPermission";

        // for 住院证
        public static string bed = "bed";
        public static string department = "department";
        public static string department1 = "department1";
        public static string inDay = "inDay";
        public static string dollar = "dollar";



        // for 肿瘤报卡
        public static string painIdOut = "painIdOut";
        public static string accidentDay = "accidentDay";
        public static string accidentAge = "accidentAge";
        public static string turmorLocation = "turmorLocation";  // 肿瘤位置
        public static string turmorBodyLocation = "turmorBodyLocation";  // 解剖学位置
        public static string pathological = "pathological";  // 病理学
        public static string morphology = "morphology";  // 形态学
        public static string level = "level";  // 分级
        public static string cure = "Cure";
        public static string unCure = "unCure";
        public static string dontknowCure = "dontknowCure";
        public static string cureItem0 = "cureitem0";
        public static string cureItem1 = "cureitem1";
        public static string cureItem2 = "cureitem2";
        public static string cureItem3 = "cureitem3";
        public static string cureItem4 = "cureitem4";
        public static string cureItem5 = "cureitem5";

        public static string contactName1 = "contactName1";
        public static string contactPhone1 = "contactPhone1";
        public static string contactRelation_br = "contactrelation_br";
        public static string contactRelation_js = "contactrelation_js";
        public static string contactRelation_py = "contactrelation_py";
        public static string contactRelation_gzdw = "contactrelation_gzdw";
        public static string contactRelation_bx = "contactrelation_bx";

        public static string contactName2 = "contactName2";
        public static string contactPhone2 = "contactPhone2";
        public static string contactRelation2_br = "contactrelation2_br";
        public static string contactRelation2_js = "contactrelation2_js";
        public static string contactRelation2_py = "contactrelation2_py";
        public static string contactRelation2_gzdw = "contactrelation2_gzdw";
        public static string contactRelation2_bx = "contactrelation2_bx";


        public static string painKnowUncertain = "painKnowUncertain";
        public static string lost = "lost";
        public static string refuse = "refuse";
        public static string IndustryStandard = "IndustryStandard";
        public static string IARC = "IARC";


        public static string education = "education";
        public static string workType = "workType";

        public static string painKnow = "painKnow";
        public static string painDontKnow = "painDontKnow";
        public static string isMultYes = "MultYes";
        public static string isMultNo = "MultNo";

        public static string mainDiagnose = "mainDiagnose";
        public static string Tumorlevel = "level";



        public static string level1 = "level1";
        public static string level2 = "level2";
        public static string level3 = "level3";
        public static string level4 = "level4";
        public static string level5 = "level5";
        public static string level6 = "level6";
        public static string level7 = "level7";
        public static string level8 = "level8";
        public static string level9 = "level9";

        public static string isCurrentHospital = "isCurrentHospital";
        public static string isnotCurrentHospital = "isnotCurrentHospital";
        public static string diagnoseHospital = "diagnoseHospital";

        public static string iarc1 = "iarc1";
        public static string iarc2 = "iarc2";
        public static string iarc3 = "iarc3";
        public static string iarc4 = "iarc4";
        public static string iarc5 = "iarc5";
        public static string iarc6 = "iarc6";

        public static string industry1 = "industry1";
        public static string industry2 = "industry2";
        public static string industry3 = "industry3";
        public static string industry4 = "industry4";
        public static string industry5 = "industry5";
        public static string industry6 = "industry6";
        public static string industry7 = "industry7";
        public static string industry8 = "industry8";
        public static string industry9 = "industry9";
        public static string industry10 = "industry10";
        public static string industry11 = "industry11";



        public static string deathTime = "deathTime";
        public static string deathReason = "deathReason";
        public static string deathIcd10Num = "deathIcd10Num";
        public static string deathIcd10Name = "deathIcd10Name";
        public static string alive = "alive";
        public static string dead = "dead";

        public static string lateralLeft = "lateralLeft";
        public static string lateralRight = "lateralRight";

        public static string lateralDouble = "lateralDouble";
        public static string lateralNone = "lateralNone";

    }

  
    public class Patient
    {
        public string Name
        {
            get; set;
        }
        public string Gender
        {
            get; set;
        }
        public string Age
        {
            get; set;
        }

        public string Marriage
        {
            get; set;
        }
       
        public string Id
        {
            get; set;
        }
       
        public string IdCardNumber
        {
            get; set;
        }
        public string Vocation
        {
            get; set;
        }
        public string Phone
        {
            get; set;
        }
        public string HomeAddr
        {
            get; set;
        }
        public string WorkAddr
        {
            get; set;
        }
        public string DoctorName
        {
            get; set;
        }
        public string InDay
        {
            get; set;
        }
        public string OutDay
        {
            get; set;
        }
        public string InDiagnose
        {
            get; set;
        }
        public string OutDiagnose
        {
            get; set;
        }
        public string DuringDay
        {
            get; set;
        }
        public string MainDiagnose
        {
            get; set;
        }
        public string Bed
        {
            get; set;
        }
        public string BloodPresure
        {
            get; set;
        }
        public string MainChief
        {
            get; set;
        }


    }

    public class PatientForTumor : Patient
    {
        public bool IsCurrentHospitalReported
        {
            get; set; }
        public string DiagnoseHospital
        {
            get; set;
        }
        public string Cure
        {
            get; set;
        }

        public string CureItem
        { get; set;
        }

        public bool IsInHospital { get; set; } // 是否住院，否：门诊；是：住院

        public string ReportHospital { get; set; } //报卡单位

        public string ReportDoctorName { get; set; }

        public string ReportDate { get; set; }

        public string DateOfBirth { get; set; }

        public string Education { get; set; }

        public string Nation { get; set; }  //民族

        public string WorkType { get; set; }  //具体工种

        public string CurrentAddress { get; set; }

        public string FirstContact { get; set; }

        public string FirstContactPhone { get; set; }

        public string FirstContactRelation { get; set; }

        public string SecondContact { get; set; }

        public string SecondContactRelation { get; set; }

        public string SecondtContactPhone { get; set; }

        public string RegistrationAddress { get; set; } // 户籍地址

        public bool IsKnown { get; set; }

        public string SurvivalStatus { get; set; }

        public string DeathTime { get; set; }

        public string DeathReason { get; set; }


        public string DeathIcd10Number { get; set; }

        public string DeathIcd10Name { get; set; }

        public bool IsMultTumors { get; set; }  //是否多发

        public string OnsetDate { get; set; } //发病日期

        public string OnsetAge { get; set; }

        public string DiagnoseName { get; set; }

        public string PathologyDiagnoseName { get; set; }

        public List<string> DiagnosticIARC { get; set; }

        public List<string> DiagnosticIndustryStandard { get; set; }

        public string PathologicalSite { get; set; } // 病理学部位

        public string AnatomicalSite { get; set; } // 解剖学部位

        public string Morphology { get; set; } // 形态学

        public string ToplevelDiagnosticUnit { get; set; }

        public TNM PathologicalstageTNM { get; set; }

        public TNM ClinicalstageTNM { get; set; }

        public string Icd10 { get; set; }

        public string TumorLevel { get; set; }

        public string TumorSide { get; set; }// 侧位
        public string OriginalDiagnosis { get; set; }
        public string OriginalPathologicalDiagnosis { get; set; }
        public string OriginalDiagnosisDate { get; set; }

        public PatientForTumor()
            {
            DiagnosticIARC = new List<string>();
            DiagnosticIndustryStandard = new List<string>();
            PathologicalstageTNM = new TNM();
            ClinicalstageTNM = new TNM();
              }
    }

    public class TNM
    { 
        public string T { get; set; }
        public string N { get; set; }
        public string M { get; set; }
    }


    public class DrawImage
    {
        private int wid = 2480;
        private int high = 3508;
        public Bitmap image;
        private SolidBrush brush;  // 颜色
        private StringFormat format;
        Font font;
        public Graphics g;

        public DrawImage()
        {
            font = new Font("Arial", 10, FontStyle.Regular);
            brush = new SolidBrush(System.Drawing.Color.Black);
            format = new StringFormat(StringFormatFlags.NoClip);
            image = new Bitmap(wid, high);
            image.SetResolution(300, 300); //dpi
            g = Graphics.FromImage(image);
            g.Clear(Color.White);//透明
        }

        public void Draw(string text, float x, float y)
        {
            g.DrawString(text, this.font, this.brush, x, y);
        }
    }

    /// <summary>
    /// 自定义一个区间类
    /// </summary>
    /// <typeparam name="区间值类型"></typeparam>
    public class Interval<T> where T : struct, IComparable
    {
        public T? Start
        {
            get; set;
        }
        public T? End
        {
            get; set;
        }

        public Interval(T? start, T? end)
        {
            this.Start = start;
            this.End = end;
        }

        public bool InRange(T value)
        {
            return ((!this.Start.HasValue || value.CompareTo(this.Start.Value) > 0) &&
                    (!this.End.HasValue || this.End.Value.CompareTo(value) > 0));
        }


    }

    public static class Global {

        public static string mySqlConnectionString { get; set; }
        static  Global()
        {
            #if DEBUG
                        // 在调试模式下使用本地数据库
                        mySqlConnectionString = "Server=localhost;Database=mytooldata;Uid=mytool;Pwd=123321;Connection Timeout=10;charset=utf8;Allow User Variables=True;allowPublicKeyRetrieval=true;SslMode=None;";
#else
                    // 在发布模式下使用远程数据库
                    mySqlConnectionString = "Server=10.61.28.66;Database=mytooldata;Uid=mytool;Pwd=123321;Connection Timeout=10;charset=utf8;Allow User Variables=True;allowPublicKeyRetrieval=true;SslMode=None;";
#endif
        }
    }
}
