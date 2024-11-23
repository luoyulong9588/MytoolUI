using Aspose.Words.Drawing;
using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using WpfToast.Controls;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// ReportCardSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportCardSettingWindow : Window
    {
        private Size _pageSize;
        private MyFontSyle _fontStyle;

        public ReportCardSettingWindow()
        {
            InitializeComponent();
            GetValueFromDb();
            GetFontStyle();

        }
        private void GetValueFromDb()
        {
            List<string> adjustList = new DatabaseUnit().GetAdjustNumber(tableName: "adjust");
            try
            {
                singleX.Value = adjustList[0];
                singleY.Value = adjustList[1];
                doubleX.Value = adjustList[2];
                doubleY.Value = adjustList[3];
            }
            catch (Exception ex)
            {
                UMessageBox.Show("数据库读取错误！", ex.ToString());
            }
        }
        

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }
        private void SetPageSize()
        {
            //我们使用A4纸张大小
            var pageMediaSize = LocalPrintServer.GetDefaultPrintQueue()
                              .GetPrintCapabilities()
                              .PageMediaSizeCapability
                              .FirstOrDefault(x => x.PageMediaSizeName == PageMediaSizeName.ISOA4);

            if (pageMediaSize != null)
            {
                _pageSize = new Size((double)pageMediaSize.Width, (double)pageMediaSize.Height);
            }
        }
        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            
           // UMessageBox.Show("提示", "单行未开发!");
            Toast.Show(this, $"单行未开发!", new ToastOptions { Icon = ToastIcons.None, ToastMargin = new Thickness(5,5,20,0), Time = 4000, Location = ToastLocation.OwnerCenter });

        }

        private void btnDouble_Click(object sender, RoutedEventArgs e)
        {
            SetPageSize();

            // Patient patient = null;
            Thread thread = new Thread(StartReportCard);
            thread.Start();
            void StartReportCard()
            {
                //_patient.Name = "张三";
                //_patient.Age = "年龄测试";
                //_patient.Gender = "性别测试";
                //_patient.HomeAddr = "住址测试";
                //_patient.IdCardNumber = "123456";
                //_patient.Id = "123";
                //_patient.MainDiagnose = "asdlfjk asfkj";
                //_patient.DoctorName = "医师测试";
                //_patient.InDay = "2020年2月2日";
                //_patient.OutDay = "2021年2月2日";
                //_patient.Phone = "4869";

                Dispatcher.Invoke( // 解决多线程调用的冲突
                new Action(
                        delegate
                        {
                            Patient _patient = new UIAuto().CaseAuto();
                            if (_patient == null)
                            {
                                return;
                            }

                            System.Windows.Controls.PrintDialog p = new System.Windows.Controls.PrintDialog();

                            DocumentPaginatorForDouble docPaginator = new DocumentPaginatorForDouble(_patient, 10, 10,_fontStyle);
                            bool print = (bool)p.ShowDialog();
                            if (print)
                            {
                                p.PrintDocument(docPaginator, "感染病例报告卡");
                            }
                            
                        }
                    ));
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            UpdateAdjustString();
            this.Close();
        }
        private void UpdateAdjustString()
        {
            string[] adjustList = new string[4];
            adjustList[0] = singleX.Value.ToString();
            adjustList[1] = singleY.Value.ToString();
            adjustList[2] = doubleX.Value.ToString();
            adjustList[3] = doubleY.Value.ToString();
            new DatabaseUnit().SetAdjustNum(adjustList, "adjust");

        }

        private void btnSelectFont_Click(object sender, RoutedEventArgs e)
        {

            FontDialog fd = new FontDialog();
            fd.Font = new System.Drawing.Font(_fontStyle.fontFamily,_fontStyle.fontSize);
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var fontStyle = new MyFontSyle();
                fontStyle.fontFamily = fd.Font.Name;
                fontStyle.fontSize = (int)fd.Font.Size;
                new DatabaseUnit().UpdateFontStyleForReportCard(fontStyle);
            }
        }

        private void GetFontStyle()
        {
            
           _fontStyle = new DatabaseUnit().GetFontStyleForReportCard();
        
        }
    }
}
