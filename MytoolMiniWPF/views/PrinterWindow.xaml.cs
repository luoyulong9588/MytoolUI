using Aspose.Words;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Shapes;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// PrinterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrinterWindow : Window
    {
        private delegate void MyDelegate(int value);
        private List<string> printList = new List<string>();
        private List<string> painNameList = new List<string>();
        private List<string> pathList = new List<string>();
        private string defaultPrinter;
        private string selectedPrinter;
        private bool isTumorFiles = false;

        public PrinterWindow(List<string> pathList)
        {
            this.pathList= pathList;
            InitializeComponent();
            SetSelectItems();
        }

        private void SetSelectItems()
        {
            this.printList = Cprinter.GetLocalPrinter();
            this.defaultPrinter = Cprinter.DefaultPrinter;
            foreach (string item in this.printList)
            {
                comboBoxSelectPrinter.Items.Add(item);
            }
            comboBoxSelectPrinter.SelectedItem = this.printList[0];
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void MergeFile()
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                //要做的事
                this.selectedPrinter = comboBoxSelectPrinter.SelectedItem.ToString();
            }));


            Cprinter.SetDefaultPrinter(this.selectedPrinter);
            OutMessage(string.Format("\n设置默认打印机 -- {0}\r", this.selectedPrinter));
            OutMessage("合并文件可能需要花一些时间...\r");
            //MergeDocxFiles mergeApp = new MergeDocxFiles();
            //mergeApp.InsertMerge(finalDoc, this.pathList, finalDoc, textBoxOutMessage);

            MergeDocxToPDF();
            OutMessage("ok ok  ok \r");
            
            
            this.Dispatcher.Invoke(new Action(delegate
            {
                ButtonProgressAssist.SetIsIndeterminate(btnEnsure, false);
            btnEnsure.Content = "开始";
            }));

            Cprinter.SetDefaultPrinter(this.defaultPrinter);
        }

        private void MergeDocxToPDF()
        {
            FileStream fs = File.Open(this.pathList[0], FileMode.Open);
            OutMessage($"合并文件:{this.pathList[0]}..\r");
            Document doc = new Document(fs);
            fs.Close();
            for (int i = 1; i < this.pathList.Count; i++)
            {
                FileStream fs1 = File.Open(this.pathList[i], FileMode.Open);
                doc.AppendDocument(new Document(fs1), ImportFormatMode.UseDestinationStyles);
                fs1.Close();
                OutMessage($"合并文件:{this.pathList[i]}..\r");
            }
            OutMessage($"保存文件:cache\\mergerd.doc..\r");

            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.PageSetup.PaperSize = Aspose.Words.PaperSize.A4;//A4纸
            builder.PageSetup.Orientation = Aspose.Words.Orientation.Portrait;//方向
            builder.PageSetup.VerticalAlignment = Aspose.Words.PageVerticalAlignment.Top;//垂直对准
            if (this.isTumorFiles)
            {
                builder.PageSetup.LeftMargin = 42;//页面左边距
                builder.PageSetup.RightMargin = 42;//页面右边距
                builder.PageSetup.TopMargin = 14;//页面上边距
                builder.PageSetup.BottomMargin = 14;//页面下边距
            }
            else
            {
                builder.PageSetup.LeftMargin = 84;//页面左边距
                builder.PageSetup.RightMargin = 84;//页面右边距
                builder.PageSetup.TopMargin = 30;//页面上边距
                builder.PageSetup.BottomMargin = 30;//页面下边距

            }

            doc.Save("cache\\mergerd.docx", SaveFormat.Docx);
            OutMessage($"输出到打印机..\r");
            doc.Print();
        }
        private void OutMessage(string txt)
        {
           
                
            this.Dispatcher.Invoke(new Action(delegate
            {
                //要做的事
                this.richBoxMessageOut.AppendText(txt);
            }));
        }

        /// <summary>
        /// 获取所有打印机
        /// </summary>
        public class Cprinter
        {
            private static PrintDocument fPrintDocument = new PrintDocument();

            [DllImport("winspool.drv")]
            public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
            ///<summary>
            ///获取本地默认打印机名称
            ///</summary>
            public static string DefaultPrinter
            {
                get { return fPrintDocument.PrinterSettings.PrinterName; }
            }
           

            /// <summary>
            ///  获取本地打印机的列表，第一项就是默认打印机
            /// </summary>
            public static List<string> GetLocalPrinter()
            {
                List<string> fPrinters = new List<string>();
                fPrinters.Add(DefaultPrinter);  //默认打印机出现在列表的第一项
                foreach (string fPrinterName in PrinterSettings.InstalledPrinters)
                {
                    if (!fPrinters.Contains(fPrinterName))
                        fPrinters.Add(fPrinterName);
                }
                return fPrinters;
            }
        }

        private void btnEnsure_Click(object sender, RoutedEventArgs e)
        {
            UpdateBtnStyle(true);

            Thread th1 = new Thread(MergeFile);
            th1.Start();
        }

        private void UpdateBtnStyle(bool isStart=true)
        {
            if (isStart)
            {
                ButtonProgressAssist.SetIsIndeterminate(btnEnsure, true);
                btnEnsure.Content = "等待";
            }
            else
            {
                ButtonProgressAssist.SetIsIndeterminate(btnEnsure, false);
                btnEnsure.Content = "开始";
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void richBoxMessageOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            richBoxMessageOut.ScrollToEnd();
        }
    }
}
