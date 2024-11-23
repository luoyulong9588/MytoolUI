using MytoolMiniWPF.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using static System.Net.Mime.MediaTypeNames;
using MytoolMiniWPF.common;

using FlaUI.Core.AutomationElements;
using Grid = System.Windows.Controls.Grid;
using Label = System.Windows.Controls.Label;
using WpfToast.Controls;
using System.Windows.Media.Animation;
using ScreenCapture;

namespace MytoolMiniWPF
{
    public partial class MainWindow
    {
        private SynchronizationContext mainThreadSynContext;
        public bool reportCreated = false;
        public bool inHospitalCreated = false;
        public bool outPatientCreated = false;
        public bool bloodGasCreated = false;
        public bool chronicDiaseaseCreated = false;
        public bool followUpCreated = false;
        public bool certificateCreated = false;
        public bool noteCreated = false;
        public bool settingCreated = false;


        // 把reportCard的功能换成病历质量评价表打印，2024年04月09日
        //private void BtnReport_Click(object sender, RoutedEventArgs e)
        //{
        //    ReportCardPrintWindowCreate();

        //}


        //取消ReportButton，更改为医检互认  2024.11.22
        private void BtnRecoginition_Click(object sender, RoutedEventArgs e)
        {
            //ReportCardPrintWindowCreate();
            RecoginitionWindowCreate();
        }

        #region  // 设置主窗体大小，现在弃用
        /*
        private void SetMainWindowHeight(int number)
        {
            this.Width = 300;
            this.Height = 30 * number;
            rowFirst = new RowDefinition();
            rowSecond = new RowDefinition();
            rowFirst.Height = new GridLength(30);
            rowSecond.Height = new GridLength(30); //如果不指定高度，默认是最大。

            if (number == 2)
            {
                mainGrid.RowDefinitions.Add(rowFirst);
                mainGrid.RowDefinitions.Add(rowSecond);
            }
            else
            {
                mainGrid.RowDefinitions.Remove(rowSecond);
            }
        }
        */
        #endregion

        /// <summary>
        /// 首页自动填写。自动区分住院和门诊；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMainPageFillBlank_Click(object sender, RoutedEventArgs e)
        {
           
            //Thread th1 = new Thread(StartInvetigation);
            //th1.Start();
            new Thread(() =>
            {
                mainThreadSynContext.Post(new SendOrPostCallback(s => StartInvetigation()), null);//通知主线程
            }).Start();

            /// <summary>
            /// 开启多线程，开始从zlbh获取信息，填写到首页中;
            /// </summary>
            void StartInvetigation()
            {
                Patient patient = null;
                
                try
                {
                    patient = new UIAuto().FillMainPage();
                }
                catch (Exception ex)
                {
                    return;
                }

                if (patient == null)
                {

                    return;
                }
            }

        }


        private void BtnMainBloodGas_Click(object sender, RoutedEventArgs e)
        {
            //UMessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.ToString());
            BloodGasWindowCreate();
        }
        private void BtnChronicDisease_Click(object sender, RoutedEventArgs e)
        {
            ChronicDiseaseWindowCreate();
        }
        private void BtnFollowUp_Click(object sender, RoutedEventArgs e)
        {

            FollowUpWindowCreate();
        }
        private void BtnTumorReport_Click(object sender, RoutedEventArgs e)
        {
            TumorWindowCreate();
        }
        private void BtnCertificate_Click(object sender, RoutedEventArgs e)
        {
            RegexToolWindow regexToolWindow = new RegexToolWindow();
            regexToolWindow.Show();
            CertificateWindowCreate();
        }
        private void BtnNote_Click(object sender, RoutedEventArgs e)
        {
            NoteWindowCreate();
        }
        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindowCreate();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private System.Windows.Controls.Button CreateBtn(string txt=null)
        {

            System.Windows.Controls.Button button = new System.Windows.Controls.Button();
            button.Height = 30;
            button.Width = 30;
            button.Style = (Style)this.FindResource("MainButton");
            if (txt!=null)
            {
                TextBlock textBlock = CreateTextBlock(txt);
                button.Content= textBlock;
            }
            return button;
        }
        

        private TextBlock CreateTextBlock(string txt = "\ue829")
        {
            TextBlock txtblock = new TextBlock();
            txtblock.Style = (Style)this.FindResource("IconFont");
            txtblock.Text = txt;
            txtblock.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./resources/font/#iconfont");
            return txtblock;
        }
        private void SetControlGrid(int row, int column, UIElement control)
        {
            Grid.SetRow(control, row);
            Grid.SetColumn(control, column);
        }


    
        
    }
}
