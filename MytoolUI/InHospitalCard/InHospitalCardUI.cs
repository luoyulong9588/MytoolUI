using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;
using Sunny.UI;

namespace MytoolUI
{
    
    public partial class InHospitalCardUI : Form
    {
        private Color color;
        private Pain pain=null;
        private FileStream stream = null;
        private string wordModPath = Application.StartupPath + @"\cache\inHospitalCard.docx";
        private string dbPath = Application.StartupPath + @"\config\data.db";

        public InHospitalCardUI(Color color)
        {
            this.color = color;
            InitializeComponent();
            SetBtnColor();
        }
        private void SetBtnColor()
        {
            uBtnBulidDocx.ForeHoverColor = color;
            uBtnBulidDocx.ForePressColor = color;
            uBtnBulidDocx.ForeSelectedColor = color;
            uBtnStart.ForeHoverColor = color;
            uBtnStart.ForePressColor = color;
            uBtnStart.ForeSelectedColor = color;
        }

        private void uBtnStart_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(StartHCard);
            thread.Start();
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
            object[] widgets = {
            uiTextBoxName,
            uiTextBoxGender,
            uiTextBoxAge,
            uiTextBoxPainId,
            null,
            null,
            uiTextBoxHomeAddress,
            null,
            null,
            uiTextBoxDiagnose,
            uiTextBoxDoctorName };
            pain= new UIAuto(widgets).CaseAuto();

            if (pain == null || pain.Name == null || pain.Name.Length < 2)
            {
                UIMessageDialog.ShowErrorDialog(this,"未打开中联bh,终止线程..");
                return;
            }
            uiTextBoxName.Text = pain.Name;
            uiTextBoxPainId.Text = pain.Id;
            uiTextBoxGender.Text = pain.Gender;
            uiTextBoxDoctorName.Text = pain.DoctorName;
            uiTextBoxAge.Text = pain.Age;
            uiTextBoxBed.Text = pain.Bed;
            uiTextBoxHomeAddress.Text = pain.HomeAddr;
            uiTextBoxWorkAddresss.Text = pain.WorkAddr;
            uiTextBoxDiagnose.Text = pain.MainDiagnose;
            uiDatetimePicker.Text = pain.InDay;
            if (uiTextBoxHomeAddress.Text=="")
            {
                uiTextBoxHomeAddress.Text = "重庆市綦江区";
            }
            if (uiTextBoxDepartment.Text=="")
            {
                uiTextBoxDepartment.Text = "全科医学科";
            }
            if (uiTextBoxDollar.Text =="")
            {
                uiTextBoxDollar.Text = "500.00";
            }
            if (uiTextBoxVocation.Text == "")
            {
                uiTextBoxVocation.Text = "其他";
            }
            if (uiComboboxExMarriage.Text=="")
            {
                uiComboboxExMarriage.Text = "已婚";
            }
            if (uiTextBoxNation.Text =="")
            {
                uiTextBoxNation.Text = "汉族";
            }
            if (uiTextBoxDoctorName.Text=="")
            {
                uiTextBoxDoctorName.Text =  new DatabaseUnit().GetuserName();
            }
        }
        private void FilldDocx()
        {

            stream = File.Open(wordModPath, FileMode.Open);
            string savePath = string.Format(@"D:\住院证\住院证.{0}.{1}.docx", uiTextBoxName.Text, DateTime.Parse(uiDatetimePicker.Text).ToString("yyyy-MM-dd"));
            Document document = new Document(stream);
            document.Range.Bookmarks[BookMark.painName].Text = uiTextBoxName.Text;
            document.Range.Bookmarks[BookMark.painName1].Text = uiTextBoxName.Text;
            document.Range.Bookmarks[BookMark.painAge].Text = uiTextBoxAge.Text;
            document.Range.Bookmarks[BookMark.gender].Text = uiTextBoxGender.Text;
            document.Range.Bookmarks[BookMark.gender1].Text = uiTextBoxGender.Text;
            document.Range.Bookmarks[BookMark.addrAlways].Text = uiTextBoxHomeAddress.Text;
            document.Range.Bookmarks[BookMark.painId].Text = uiTextBoxPainId.Text;
            document.Range.Bookmarks[BookMark.reportDoctor].Text = uiTextBoxDoctorName.Text;
            document.Range.Bookmarks[BookMark.inDay].Text = uiDatetimePicker.Text;
            document.Range.Bookmarks[BookMark.diagnoseString].Text = uiTextBoxDiagnose.Text;
            document.Range.Bookmarks[BookMark.vocation].Text = uiTextBoxVocation.Text;
            document.Range.Bookmarks[BookMark.nation].Text = uiTextBoxNation.Text;
            document.Range.Bookmarks[BookMark.department].Text = uiTextBoxDepartment.Text;
            document.Range.Bookmarks[BookMark.department1].Text = uiTextBoxDepartment.Text;
            document.Range.Bookmarks[BookMark.dollar].Text = uiTextBoxDollar.Text;
            document.Range.Bookmarks[BookMark.bed].Text = uiTextBoxBed.Text;
            document.Save(savePath, SaveFormat.Docx);
            stream.Close();

            openFinalFile(savePath);

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
                    string savePath = Application.StartupPath + "\\cache\\inHospitalCard.docx";
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        UIMessageDialog.ShowErrorDialog(this,"写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。");
                    }
                }
            }
            m_dbConnection.Close();


        }

        private void openFinalFile(string savePath)
        {
            Process.Start("explorer.exe", savePath);
        }
        private void checkFolder(string folder)
        {
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }
        }

        private void uBtnBulidDocx_Click(object sender, EventArgs e)
        {
           
                Thread thread = new Thread(StartBuildDocx);
                thread.Start();
          
          
              
           
            
        }
        private void StartBuildDocx()
        {
            checkFolder(@"D:\住院证");
            ReadwordFromDb();
            FilldDocx();
        }
    }
}
