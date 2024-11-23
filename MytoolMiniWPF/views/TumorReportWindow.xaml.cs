using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MytoolMiniWPF.common.TumorFunc;
using static System.Net.Mime.MediaTypeNames;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// TumorReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TumorReportWindow : Window
    {
        private const string DefaultHospital = "綦江区文龙街道社区卫生服务中心";
        private ObservableCollection<ComboBoxAgeItemViewModel> Ageitems;
        public TumorReportWindow()
        {
            InitializeComponent();
            new DatabaseUnit().GetDocxFilefromDbForTumor();
            DiagnoseNameitems = new ObservableCollection<ComboBoxDiagnoseNameItemViewModel>();

            ICD10items = new ObservableCollection<ComboBoxICD10ItemViewModel>();

            PathologyDiagnoseNameitems = new ObservableCollection<ComboBoxPathologyDiagnoseNameItemViewModel>();
            Ageitems = new ObservableCollection<ComboBoxAgeItemViewModel>();
            comboboxDiagnoseName.ItemsSource = DiagnoseNameitems;
            comboboxPathologyDiagnoseName.ItemsSource = PathologyDiagnoseNameitems;

            comboboxICD10.ItemsSource = ICD10items;

            comboBoxOnsetAge.ItemsSource = Ageitems;
            SetAge();
        }

        private void min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetAge()
        {
            for (int i = 15; i < 120; i++)
            {
                Ageitems.Add(new ComboBoxAgeItemViewModel { Age = i.ToString()});
            }
        }

        private bool CheckComplete()
         {
            List<TextBox> listTextBox = new List<TextBox>() {
                textBoxReportDoctorName,
                textBoxPatientName,
                textBoxPatientAge,
                textBoxPatientNation,
                textBoxCurrentAddress,
                textBoxFirstContact,
                textboxIdcardNumber,
                textBoxFirstContactPhone,
                textBoxSecondContact,
                textBoxSecondContactPhone,
                textBoxHouseholdRegistrationAddress,
                textBoxWorkAddress,
                textboxPathologicalSite,
                textboxAnatomicalSite,
                textboxMorphology,
                textboxToplevelDiagnosticUnit,
              
            };

            List<ComboBox> listComboBox = new List<ComboBox>()
            {comboboxCure,
            comboboxICD10,
            comboboxCureItem,
            comboboxInorout,
            comboboxPatientGender,
            comboboxEducation,
            comboboxMarriage,
            comboboxVocation,
            comboboxWorkType,
            comboboxFirstContactRelation, comboboxSecondContactRelation,
            comboboxSurvivalStatus,
            comboBoxOnsetAge,
            comboboxDiagnoseName,
            comboboxPathologyDiagnoseName,
           
            comboboxTumorLevel,comboboxTumorSide
            };

            List<DatePicker> listDatePicker = new List<DatePicker>() {

            datapickerReportDate,
            datepickerBornDate,
                datepickerOnsetDate
                };

            foreach (TextBox textbox in listTextBox) {
                if (string.IsNullOrEmpty(textbox.Text)||textbox.Text.Length<1)
                {
                    UMessageBox.Show( $"please input {textbox.Name} and try again.");
                    return false;
                }
            }
            foreach (ComboBox combox in listComboBox)
            {
                if (combox.SelectedItem == null)
                {
                    UMessageBox.Show($"please input {combox.Name} and try again.");
                    return false;
                }
            }
            foreach (DatePicker datepicker in listDatePicker)
            {
                if (string.IsNullOrEmpty(datepicker.Text))
                {
                    UMessageBox.Show($"please input{datepicker.Name} and try again.");
                    return false;
                }
            }

            if (comboboxSurvivalStatus.SelectedItem == "死亡")
            {
                if (string.IsNullOrEmpty(datePickerDeathTime.Text))
                {
                    UMessageBox.Show("未选择死亡时间！");
                    return false;
                }
                if (string.IsNullOrEmpty(textBoxDeathReason.Text))
                {
                    UMessageBox.Show("未选择死亡原因！");
                    return false;
                }
                if (string.IsNullOrEmpty(textBoxDeathIcd.Text))
                {
                    UMessageBox.Show("未选择死亡ICD编码！");
                    return false;
                }
                if (string.IsNullOrEmpty(textBoxDeathIcdName.Text))
                {
                    UMessageBox.Show("未填写死亡ICD名称！");
                    return false;
                }

                if (radiobtnKnown.IsChecked == false && radiobtnUnknown.IsChecked == false)
                {
                    UMessageBox.Show("患者是否知情？");
                    return false;
                }
                if (radioBtnMultTumors.IsChecked == false && radioBtnSingleTumors.IsChecked == false)
                {
                    UMessageBox.Show("肿瘤单发多发？");
                    return false;
                }
            }
            return true;

        }




        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            bool isAllCompleted = CheckComplete();
            if (!isAllCompleted) { return;}
            PatientForTumor patient = Assignment();
            try
            {

                new TumorFillDocx(patient);
            }
            catch (Exception ex)
            {

                UMessageBox.Show(ex.ToString());
            }

        }
        private PatientForTumor Assignment()
        {
            List<string> getDiagnosticIARC()
            {
                List<string> response = new List<string>();
                string pattern = @"\[\d+\]";
                MatchCollection matches = Regex.Matches(multiSelectComboboxDiagnosticIARC.Text, pattern);
                foreach (Match match in matches)
                {

                    response.Add(match.Value);

                }
                return response;
            }
            List<string> getDiagnosticIndustryStandard()
            {
                List<string> response = new List<string>();
                string pattern = @"\[\d+\]";
                MatchCollection matches = Regex.Matches(multiSelectComboboxDiagnosticIndustryStandard.Text, pattern);

                foreach (Match match in matches)
                {

                    response.Add(match.Value);

                }
                return response;
            }

            TNM getTNM(ComboBox T, ComboBox N, ComboBox M)
            { 
                TNM response = new TNM();
                response.T = T.Text;
                response.N = N.Text;
                response.M = M.Text;
                return response;
            }

            PatientForTumor patient = new PatientForTumor();
            patient.IsCurrentHospitalReported = true;
            patient.DiagnoseHospital = string.IsNullOrEmpty(textBoxDiagnosisHospitalName.Text) ? DefaultHospital : textBoxDiagnosisHospitalName.Text; 
            patient.ReportHospital = string.IsNullOrEmpty(textBoxReportHospitalName.Text) ? DefaultHospital : textBoxReportHospitalName.Text;
            patient.Cure = comboboxCure.Text;
            patient.CureItem = comboboxCureItem.Text;
            patient.IsInHospital = comboboxInorout.Text== "住院号";
            patient.Id = textBoxHospitalId.Text;
            patient.ReportDoctorName = textBoxReportDoctorName.Text;
            patient.ReportDate = datapickerReportDate.Text;

            patient.Name = textBoxPatientName.Text;
            patient.Gender = comboboxPatientGender.Text;
            patient.Age = textBoxPatientAge.Text;
            patient.DateOfBirth = datepickerBornDate.Text;
            patient.Education = comboboxEducation.Text;
            patient.Nation = textBoxPatientNation.Text;
            patient.Marriage = comboboxMarriage.Text;
            patient.Vocation = comboboxVocation.Text;
            patient.WorkType = comboboxWorkType.Text;
            patient.CurrentAddress = textBoxCurrentAddress.Text;
            patient.RegistrationAddress = textBoxHouseholdRegistrationAddress.Text;
            patient.WorkAddr = textBoxWorkAddress.Text;
            patient.IdCardNumber = textboxIdcardNumber.Text;
            patient.FirstContact = textBoxFirstContact.Text;
            patient.SecondContact = textBoxSecondContact.Text;
            patient.FirstContactPhone = textBoxFirstContactPhone.Text;
            patient.SecondtContactPhone = textBoxSecondContactPhone.Text;
            patient.FirstContactRelation = comboboxFirstContactRelation.Text;
            patient.SecondContactRelation = comboboxSecondContactRelation.Text;
            patient.IsKnown = radiobtnKnown.IsChecked == true;
            patient.SurvivalStatus = comboboxSurvivalStatus.Text;

            patient.DeathTime = datePickerDeathTime.Text;
            patient.DeathReason = textBoxDeathReason.Text;
            patient.DeathIcd10Number = textBoxDeathIcd.Text;  
            patient.DeathIcd10Name = textBoxDeathIcdName.Text;

            patient.IsMultTumors = radioBtnMultTumors.IsChecked == true;
            patient.OnsetDate = datepickerOnsetDate.Text;
            patient.OnsetAge = comboBoxOnsetAge.Text;
            patient.DiagnoseName = comboboxDiagnoseName.Text;
            patient.PathologyDiagnoseName = comboboxPathologyDiagnoseName.Text;

           

            patient.DiagnosticIARC = getDiagnosticIARC();
            patient.DiagnosticIndustryStandard = getDiagnosticIndustryStandard();

            patient.PathologicalSite = textboxPathologicalSite.Text;
            patient.AnatomicalSite = textboxAnatomicalSite.Text;
            patient.Morphology = textboxMorphology.Text;
            patient.ToplevelDiagnosticUnit = textboxToplevelDiagnosticUnit.Text;
            patient.PathologicalstageTNM = getTNM(comboboxPathologicalstageT, comboboxPathologicalstageN, comboboxPathologicalstageM);
            patient.ClinicalstageTNM = getTNM(comboboxClinicalstageT, comboboxClinicalstageN, comboboxClinicalstageM);////

            patient.Icd10 = comboboxICD10.Text;
            patient.TumorLevel = comboboxTumorLevel.Text;
            patient.TumorSide = comboboxTumorSide.Text;
            patient.OriginalDiagnosis = textboxOriginalDiagnosis.Text;
            patient.OriginalPathologicalDiagnosis = textboxOriginalPathologicalDiagnosis.Text;
            patient.OriginalDiagnosisDate = datepickerOriginalDiagnosisDate.Text;
            return patient;

        }
        public class ComboBoxAgeItemViewModel
        {
            public string Age { get; set; }
        }

        private void comboboxSurvivalStatus_MouseLeave(object sender, MouseEventArgs e)
        {
            if (comboboxSurvivalStatus.Text == "死亡")
            {
                datePickerDeathTime.IsEnabled = true;
                textBoxDeathReason.IsEnabled = true;
                textBoxDeathIcd.IsEnabled = true;
                textBoxDeathIcdName.IsEnabled = true;
            }
            else
            {
                datePickerDeathTime.IsEnabled = false;
                textBoxDeathReason.IsEnabled = false;
                textBoxDeathIcd.IsEnabled = false;
                textBoxDeathIcdName.IsEnabled = false;
            }

        }

    

        private void textBoxHospitalId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(comboboxInorout.Text))
            {
                UMessageBox.Show("请先选择门诊或住院！");
            }
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
