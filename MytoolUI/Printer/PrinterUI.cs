using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;

namespace MytoolUI
{
    public partial class PrinterUI : Form
    {
        private List<string> printList = new List<string>();
        private List<string> painNameList = new List<string>();
        private List<string> pathList = new List<string>();
     /*   private CheckedListBox checkedListBoxCOPD;
        private CheckedListBox checkedListBoxAMI;
        private CheckedListBox checkedListBoxAPoplexy;*/
        private string defaultPrinter;
        private string selectedPrinter;
        private bool isTumorFiles = false;



        public PrinterUI(List<string> pathList,bool tumorFiles=false)
        {
            /*this.checkedListBoxCOPD = checkedListBoxes[0];
            this.checkedListBoxAMI = checkedListBoxes[1];
            this.checkedListBoxAPoplexy = checkedListBoxes[2];*/
            this.pathList = pathList;
            this.isTumorFiles = tumorFiles;
            InitializeComponent();
            setPrinterSelectBox();
        }

        private void setPrinterSelectBox()
        {
            this.printList = Cprinter.GetLocalPrinter();
            this.defaultPrinter = Cprinter.DefaultPrinter;
            foreach (string item in this.printList)
            {
                comboxSelectPrinter.Items.Add(item);
            }
            comboxSelectPrinter.SelectedItem = this.printList[0];

        }

        private void mergeDocs()
        {
/*            // 获取哪些选择了;
            foreach (var item in this.checkedListBoxCOPD.CheckedItems)
            {
                string result = item.ToString();
                painNameList.Add(result);
            }
            foreach (var item in this.checkedListBoxAMI.CheckedItems)
            {
                painNameList.Add(item.ToString());
            }
            foreach (var item in this.checkedListBoxAPoplexy.CheckedItems)
            {
                painNameList.Add(item.ToString());
            }*/
            this.selectedPrinter = comboxSelectPrinter.SelectedItem.ToString();
            Cprinter.SetDefaultPrinter(this.selectedPrinter);
            textBoxOutMessage.AppendText(string.Format("\n设置默认打印机 -- {0}\r", this.selectedPrinter));
            textBoxOutMessage.AppendText("合并文件可能需要花一些时间...\r");
            //MergeDocxFiles mergeApp = new MergeDocxFiles();
            //mergeApp.InsertMerge(finalDoc, this.pathList, finalDoc, textBoxOutMessage);
            MergeDocxToPDF();
            textBoxOutMessage.AppendText("ok ok  ok \r");
            Cprinter.SetDefaultPrinter(this.defaultPrinter);

        }

        private void MergeDocxToPDF()
        {
            FileStream fs = File.Open(this.pathList[0], FileMode.Open);
            textBoxOutMessage.AppendText($"合并文件:{this.pathList[0]}..\r");
            Document doc = new Document(fs);
            fs.Close();
            for (int i = 1; i < this.pathList.Count; i++)
            {
                FileStream fs1 = File.Open(this.pathList[i], FileMode.Open);
                doc.AppendDocument(new Document(fs1), ImportFormatMode.UseDestinationStyles);
                fs1.Close();
                textBoxOutMessage.AppendText($"合并文件:{this.pathList[i]}..\r");
            }
            textBoxOutMessage.AppendText($"保存文件:cache\\mergerd.doc..\r");

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
            textBoxOutMessage.AppendText($"输出到打印机..\r");
            doc.Print();
            //textBoxOutMessage.AppendText($"完成..\r");
        }

        private void uiTitlePanelPrinter_Click(object sender, EventArgs e)
        {

        }

        private void ubtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrinterUI_Load(object sender, EventArgs e)
        {

        }

        private void ubtnStart_Click(object sender, EventArgs e)
        {

            Thread th1 = new Thread(mergeDocs);
            th1.Start();

        }

        private void comboxSelectPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxOutMessage_TextChanged(object sender, EventArgs e)
        {
            textBoxOutMessage.SelectionStart = textBoxOutMessage.Text.Length; //Set the current caret position at the end
            textBoxOutMessage.ScrollToCaret(); //Now scroll it automatically
        }
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
}
