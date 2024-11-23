using Aspose.Words;
using FlaUI.Core.AutomationElements;
using MytoolMiniWPF.customControls;
using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// InpatientCertificateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InpatientCertificateWindow : System.Windows.Window
    {
        private string CurrentPath, wordModPath, dbPath;
        private string saveDir;
        private Patient patient = null;
        private FileStream stream = null;

        public InpatientCertificateWindow()
        {
            InitializeComponent();
            CurrentPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            wordModPath = CurrentPath + @"\cache\inHospitalCard.docx";
            dbPath = CurrentPath + @"\config\data.db";
            string path = new DatabaseUnit().GetFolderPath().admissionCertificatePath;
            saveDir = string.IsNullOrEmpty(path) ? AppDomain.CurrentDomain.BaseDirectory + "住院证" : path;
        }

        private void btnGetInformation_Click(object sender, RoutedEventArgs e)
        {
            CheckEmpty();

            Thread thread = new Thread(StartHCard);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void btnBulidCard_Click(object sender, RoutedEventArgs e)
        {

            CheckEmpty();

            Thread thread = new Thread(StartBuildDocx);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void StartBuildDocx()
        {
            
            checkFolder(@"D:\住院证");
            ReadwordFromDb();
            FilldDocx();
        }
        private void ReadwordFromDb()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={this.dbPath};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select file from inHospitalCard", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    string savePath = CurrentPath + "\\cache\\inHospitalCard.docx";
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        UMessageBox.Show("错误！", "写入文件被占用!");
                    }
                }
            }
            m_dbConnection.Close();


        }

        private void openFinalFile(string filePath)
        {
            Process.Start("explorer.exe", filePath);
        }

        private void checkFolder(string folder)
        {
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void StartHCard()
        {

            //pain = new Pain();
            //pain.Name = "张三";
            //pain.Age = "14";
            //pain.Gender = "male";
            //pain.HomeAddr = "BigRod";
            //pain.IdCardNumber = "123456";
            //pain.Id = "123";
            //pain.DoctorName = "Lucy";
            //pain.InDay = "2020年2月2日";
            //pain.OutDay = "2021年2月2日";
            //pain.Phone = "4869";
            
            patient = new UIAuto().CaseAuto();

            if (patient == null || patient.Name == null || patient.Name.Length < 2)
            {
                UMessageBox.Show("错误", "终止线程..");
                return;
            }

            //labelTextName.Value = patient.Name;
            //labelTextId.Value = patient.Id;
            //labelTextGender.Value = patient.Gender;
            //labelTextAge.Value = patient.Age;

            UpdateLabtelText(labelTextName, patient.Name);
            UpdateLabtelText(labelTextId, patient.Id);
            UpdateLabtelText(labelTextGender, patient.Gender);
            UpdateLabtelText(labelTextAge, patient.Age);

            UpdateTextBox(textBoxAddr, patient.HomeAddr);
            UpdateTextBox(textBoxMaindiag, patient.MainDiagnose);
            UpdateTextBox(textBoxDoctorName, patient.DoctorName);
 
            this.Dispatcher.Invoke((Action)delegate ()
            {
                dataPicker.Text = patient.InDay;
            });
        }
        private void CheckEmpty()
        {
            if (string.IsNullOrEmpty(textBoxAddr.Text))
            {
                textBoxAddr.Text = "重庆市綦江区";
            }
            if (string.IsNullOrEmpty(labelTextClass.Value))
            {
                labelTextClass.Value = "全科医学科";
            }

            if (string.IsNullOrEmpty(textBoxPay.Text))
            {
                textBoxPay.Text = "500.00";
            }
            if (string.IsNullOrEmpty(labelTextVocation.Value))
            {
                labelTextVocation.Value = "其他";
            }
            if (string.IsNullOrEmpty(labelTextMarriage.Value))
            {
                labelTextMarriage.Value = "已婚";
            }
            if (string.IsNullOrEmpty(labelTextNation.Value))
            {
                labelTextNation.Value = "汉族";
            }
            if (string.IsNullOrEmpty(textBoxDoctorName.Text))
            {
                textBoxDoctorName.Text = new DatabaseUnit().GetuserName();
            }
            if (dataPicker.SelectedDate == null)
            {
                dataPicker.SelectedDate = DateTime.Now;
            }
        }
        private void FilldDocx()
        {

            this.Dispatcher.Invoke((Action)delegate ()
            {
                //string savePath = string.Format(@"D:\住院证\住院证.{0}.{1}.docx", labelTextName.Value, DateTime.Parse(dataPicker.Text).ToString("yyyy-MM-dd"));
                string savePath = $"{saveDir}\\住院证.{labelTextName.Value}." + DateTime.Parse(dataPicker.Text).ToString("yyyy-MM-dd") + ".docx";
                stream = File.Open(wordModPath, FileMode.Open);
                Document document = new Document(stream);
                document.Range.Bookmarks[BookMark.painName].Text = labelTextName.Value;
                document.Range.Bookmarks[BookMark.painName1].Text = labelTextName.Value;
                document.Range.Bookmarks[BookMark.painAge].Text = labelTextAge.Value;
                document.Range.Bookmarks[BookMark.gender].Text = labelTextGender.Value;
                document.Range.Bookmarks[BookMark.gender1].Text = labelTextGender.Value;
                document.Range.Bookmarks[BookMark.vocation].Text = labelTextVocation.Value;
                document.Range.Bookmarks[BookMark.nation].Text = labelTextNation.Value;
                document.Range.Bookmarks[BookMark.department].Text = labelTextClass.Value;
                document.Range.Bookmarks[BookMark.department1].Text = labelTextClass.Value;
                document.Range.Bookmarks[BookMark.painId].Text = labelTextId.Value;
            
                document.Range.Bookmarks[BookMark.addrAlways].Text = textBoxAddr.Text;
                document.Range.Bookmarks[BookMark.reportDoctor].Text = textBoxDoctorName.Text;
                document.Range.Bookmarks[BookMark.inDay].Text = dataPicker.Text;
                document.Range.Bookmarks[BookMark.diagnoseString].Text = textBoxMaindiag.Text;
                document.Range.Bookmarks[BookMark.dollar].Text = textBoxPay.Text;
                document.Range.Bookmarks[BookMark.bed].Text = "未分配";

                document.Save(savePath, SaveFormat.Docx);
                stream.Close();
                openFinalFile(savePath);
            });

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void UpdateLabtelText(LabelText control,string txt)
        {
            this.Dispatcher.Invoke((Action)delegate ()
            {
                control.Value = txt;
            });
        }
        private void UpdateTextBox(System.Windows.Controls.TextBox control, string txt)
        {
            this.Dispatcher.Invoke((Action)delegate ()
            {
                control.Text = txt;
            });
        }

    }
}
