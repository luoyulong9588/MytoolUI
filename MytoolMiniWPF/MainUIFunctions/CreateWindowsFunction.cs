using MytoolMiniWPF.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfToast.Controls;

namespace MytoolMiniWPF
{
    public partial class MainWindow
    {
        Window certificate = null;
        Window tumorWindow = null;
        Window bloodGasWindow = null;
        Window chronicDiseaseWindow = null;
        Window followUpWindow = null;
        Window noteWindow = null;
        Window settingWindow = null;
        Window reportWindow = null;
        Window recoginitionWindow = null;

        private void CertificateWindowCreate()
        {
            if (certificate == null)
            {

                certificate = new InpatientCertificateWindow();
                certificate.Show();
            }
            else if (PresentationSource.FromVisual(certificate) == null)
            {
                certificate = new InpatientCertificateWindow();
                certificate.Show();
            }

            else
            {
                certificate.Focus();
                certificate.WindowState = WindowState.Normal;
                Toast.Show(this, $"住院证窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }

        private void TumorWindowCreate()
        {
            if (tumorWindow == null)
            {

                tumorWindow = new TumorReportWindow();
                tumorWindow.Show();
            }
            else if (PresentationSource.FromVisual(tumorWindow) == null)
            {
                tumorWindow = new TumorReportWindow();
                tumorWindow.Show();
            }

            else
            {
                tumorWindow.Focus();
                tumorWindow.WindowState = WindowState.Normal;
                Toast.Show(this, $"tumorWindow is already running！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }
        private void BloodGasWindowCreate()
        {
            // PresentationSource.FromVisual(bloodGasWindow)==null



            if (bloodGasWindow == null)
            {

                bloodGasWindow = new BloodGasPage();
                bloodGasWindow.Show();
            }
            else if (PresentationSource.FromVisual(bloodGasWindow) == null)
            {
                bloodGasWindow = new BloodGasPage();
                bloodGasWindow.Show();
            }

            else
            {
                bloodGasWindow.Focus();
                bloodGasWindow.WindowState = WindowState.Normal;
                //UMessageBox.Show("血气分析窗体已经在运行！");
                Toast.Show(this, $"血气分析窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }

        private void ChronicDiseaseWindowCreate()
        {

            if (chronicDiseaseWindow == null)
            {

                chronicDiseaseWindow = new ChronicDiseaseWindow();
                chronicDiseaseWindow.Show();
            }
            else if (PresentationSource.FromVisual(chronicDiseaseWindow) == null)
            {
                chronicDiseaseWindow = new ChronicDiseaseWindow();
                chronicDiseaseWindow.Show();
            }

            else
            {
                chronicDiseaseWindow.Focus();
                chronicDiseaseWindow.WindowState = WindowState.Normal;
                Toast.Show(this, $"慢病窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }
        private void FollowUpWindowCreate()
        {


            if (followUpWindow == null)
            {

                followUpWindow = new DischargeFollowUpWindow();
                followUpWindow.Show();
            }
            else if (PresentationSource.FromVisual(followUpWindow) == null)
            {
                followUpWindow = new DischargeFollowUpWindow();
                followUpWindow.Show();
            }

            else
            {
                followUpWindow.Focus();
                followUpWindow.WindowState = WindowState.Normal;
                Toast.Show(this, $"出院随访窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }
        private void NoteWindowCreate()
        {


            if (noteWindow == null)
            {

                noteWindow = new NotePadWindow();
                noteWindow.Show();
                noteWindow.Topmost = true;
            }
            else if (PresentationSource.FromVisual(noteWindow) == null)
            {
                noteWindow = new NotePadWindow();
                noteWindow.Show();
            }

            else
            {
                noteWindow.Focus();
                noteWindow.WindowState = WindowState.Normal;
                Toast.Show(this, $"记事小本窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }
        public void SettingWindowCreate()
        {

            if (settingWindow == null)
            {

                settingWindow = new Settings();
                settingWindow.Show();
            }
            else if (PresentationSource.FromVisual(settingWindow) == null)
            {
                settingWindow = new Settings();
                settingWindow.Show();
            }

            else
            {
                settingWindow.Focus();
                settingWindow.WindowState = WindowState.Normal;
                Toast.Show(this, $"设置窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }
        private void ReportCardPrintWindowCreate()
        {

            Point ptLeftUp = new Point(0, 0);
            Point ptRightDown;
            if (isHide)
            {
                ptRightDown = new Point(this.ActualWidth + 310, this.ActualHeight + 40);
            }
            else
            {
                ptRightDown = new Point(this.ActualWidth, this.ActualHeight);
            }

            

            ptLeftUp = this.PointToScreen(ptLeftUp);
            ptRightDown = this.PointToScreen(ptRightDown);


            if (reportWindow == null)
            {
                reportWindow = new ReportCardSettingWindow();
                reportWindow.Left = ptLeftUp.X + this.ActualWidth * 2;
                reportWindow.Top = 0.0;
                reportWindow.Show();
                reportWindow.Topmost= true;
            }
            else if (PresentationSource.FromVisual(reportWindow) == null)
            {
                reportWindow = new ReportCardSettingWindow();
                reportWindow.Left = ptLeftUp.X + this.ActualWidth * 2;
                reportWindow.Top = 0.0;
                reportWindow.Show();
                reportWindow.Topmost = true;
            }

            else
            {
                reportWindow.Focus();
                reportWindow.WindowState = WindowState.Normal;
                UMessageBox.Show("感染报告卡窗体已经在运行！");
            }
        }

        private void RecoginitionWindowCreate()
        {
            if (recoginitionWindow == null)
            {

                recoginitionWindow = new RecoginitionWindow();
                recoginitionWindow.Show();
            }
            else if (PresentationSource.FromVisual(recoginitionWindow) == null)
            {
                recoginitionWindow = new RecoginitionWindow();
                recoginitionWindow.Show();
            }

            else
            {
                recoginitionWindow.Focus();
                recoginitionWindow.WindowState = WindowState.Normal;
                Toast.Show(this, $"医检互认窗体已经在运行！", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5, 5, 20, 0), Time = 2000, Location = ToastLocation.ScreenTopCenter });

            }

        }
    }
}

