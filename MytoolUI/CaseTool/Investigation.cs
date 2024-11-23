using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Aspose.Words;
using Sunny.UI;

namespace MytoolUI
{
    internal class Investigation
    {
        private Pain pain = null;
        private Pain painContact = null;
        private string wordModPath = Application.StartupPath + @"\cache\invetigation.docx";
        private string dbPath = Application.StartupPath + @"\config\data.db";
        private FileStream stream = null;
        private UIMessageForm message = new UIMessageForm();
        public void startProgram(Pain pain = null, Pain painContact = null,bool withPermitPage = false)
        {
            if (pain != null)
            {
                this.pain = pain;
            }


            if (painContact != null)
            {
                this.painContact = painContact;
            }
            if (painContact != null && pain != null)
            {
                painContact.HomeAddr = painContact.HomeAddr.Length < 2 ? pain.HomeAddr : painContact.HomeAddr;
                painContact.Phone = painContact.Phone.Length < 2 ? pain.Phone : painContact.Phone;
            }
            
            /*            pain.Name = "张三";
                        pain.Age = "14";
                        pain.Gender = "male";
                        pain.HomeAddr = "BigRod";
                        pain.IdCardNumber = "123456";
                        pain.Id = "123";
                        pain.DoctorName = "Lucy";
                        pain.InDay = "2020年2月2日";
                        pain.Phone = "4869";*/
            this.checkFolder(@"D:\流行病学调查表");
            this.ReadwordFromDb(withPermitPage);
            this.FillWord(withPermitPage);
        }
        private void ReadwordFromDb(bool withPermitPage = false)
        {   
            int index = withPermitPage ? 0 : 1;
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={this.dbPath};Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand($"select file from investigation where normal = {index}", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] word = reader[0] as byte[];
                if (word != null)
                {
                    string savePath = Application.StartupPath + "\\cache\\invetigation.docx";
                    try
                    {
                        FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(word, 0, word.Length);
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        bool select = message.ShowAskDialog("写入文件被占用,即将尝试结束所有winWord文件进程，请保存好当前打开的Word文档，并按确定以继续。", UIStyle.LightRed);
                        if (select)
                        {
                            killWinWordProcess();
                        }
                        else
                        {
                           message.ShowInfoDialog("点击了取消，请手动结束占用流行病学调查表的进程，再点击重试!");
                            return;
                        }
                    }
                }
            }
            m_dbConnection.Close();
        }

        private void FillWord(bool withPermitPage = false)
        {

            stream = File.Open(wordModPath, FileMode.Open);
            string savePath = "";
            Document document = new Document(stream);
            if (this.pain != null)
            {
                document.Range.Bookmarks[BookMark.painName].Text = pain.Name;
                document.Range.Bookmarks[BookMark.painName1].Text = pain.Name;
                document.Range.Bookmarks[BookMark.painAge].Text = pain.Age;
                document.Range.Bookmarks[BookMark.gender].Text = pain.Gender;
                document.Range.Bookmarks[BookMark.gender1].Text = pain.Gender;
                document.Range.Bookmarks[BookMark.addrAlways].Text = pain.HomeAddr;
                document.Range.Bookmarks[BookMark.id1].Text = pain.Id;
                document.Range.Bookmarks[BookMark.idCard].Text = pain.IdCardNumber;
                document.Range.Bookmarks[BookMark.phone].Text = pain.Phone;
                document.Range.Bookmarks[BookMark.reportDoctor].Text = pain.DoctorName;
                document.Range.Bookmarks[BookMark.reportDoctor1].Text = pain.DoctorName;
                document.Range.Bookmarks[BookMark.reportTime].Text = pain.InDay;
                document.Range.Bookmarks[BookMark.reportTime1].Text = pain.InDay;
                savePath = string.Format(@"D:\流行病学调查表\流行病学调查表.{0}.{1}.docx", pain.Name, pain.InDay);
            }

            if (this.painContact != null)
            {
                document.Range.Bookmarks[BookMark.painNameContact].Text = painContact.Name;
                document.Range.Bookmarks[BookMark.painAgeContact].Text = painContact.Age;
                document.Range.Bookmarks[BookMark.genderContact].Text = painContact.Gender;
                document.Range.Bookmarks[BookMark.phoneContact].Text = painContact.Phone;
                document.Range.Bookmarks[BookMark.addrAlwaysContact].Text = painContact.HomeAddr;
                document.Range.Bookmarks[BookMark.idCardContact].Text = painContact.IdCardNumber;
                if (this.pain == null)
                {
                    document.Range.Bookmarks[BookMark.reportTime].Text =  System.DateTime.Now.ToString("yyyy-MM-dd");
                    document.Range.Bookmarks[BookMark.reportTime1].Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    savePath = string.Format(@"D:\流行病学调查表\流行病学调查表.{0}.{1}.{2}.docx", "仅家属", painContact.Name, System.DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    savePath = string.Format(@"D:\流行病学调查表\流行病学调查表.{0}.{1}.{2}.docx", pain.Name, painContact.Name, pain.InDay);
                }


            }
            if (withPermitPage)
            {
                bool withContact = false;
                string phoneNumber =  "              ";
                if (this.painContact != null)
                {
                    withContact = true;
                    if (painContact.Phone.Length > 3)
                    {
                        phoneNumber = painContact.Phone;
                    }
                }
                document.Range.Bookmarks[BookMark.painNamePerimssion].Text = pain.Name;
                document.Range.Bookmarks[BookMark.painNamePerimssion1].Text = pain.Name;
                document.Range.Bookmarks[BookMark.painGenderPerimssion].Text = pain.Gender;
                document.Range.Bookmarks[BookMark.painGenderPerimssion1].Text = pain.Gender;
                document.Range.Bookmarks[BookMark.painAgePermission].Text = pain.Age;
                document.Range.Bookmarks[BookMark.painAgePermission1].Text = pain.Age;
                document.Range.Bookmarks[BookMark.painIdCardPermission].Text = pain.IdCardNumber;
                document.Range.Bookmarks[BookMark.painAddressPermission].Text = pain.HomeAddr;
                // 联系人
                document.Range.Bookmarks[BookMark.painNameContact].Text = withContact ? painContact.Name : "             ";
                document.Range.Bookmarks[BookMark.painPhoneContactPermission].Text = phoneNumber;
                document.Range.Bookmarks[BookMark.painGenderContactPermission].Text = withContact ? painContact.Gender : "   ";
                document.Range.Bookmarks[BookMark.painIdCardContactPermission].Text = withContact ? painContact.IdCardNumber : "                    ";
                document.Range.Bookmarks[BookMark.painAddressContactPermission].Text = withContact ? painContact.HomeAddr : "  ";
                document.Range.Bookmarks[BookMark.painAgeContactPermission].Text = withContact ? painContact.Age : "   ";

            }


            document.Save(savePath, SaveFormat.Docx);
            stream.Close();
            #region
            /*Word.Application wordApp = new Word.Application();                   //Word应用程序变量 
            Word.Document wordDoc;                  //Word文档变量
            object File = this.wordModPath;
            object savePath = string.Format(@"D:\流行病学调查表\流行病学调查表.{0}.{1}.docx", pain.Name, pain.InDay);
            object missing = System.Reflection.Missing.Value;
            object readOnly = false;//不是只读
            object isVisible = false;
            object saveChanges = wordApp.Options.BackgroundSave;//文档另存为 
            BookMark bookMark = new BookMark();
            wordApp.Visible = false;
            wordDoc = wordApp.Documents.Open(ref File, ref missing, ref readOnly,
                                             ref missing, ref missing, ref missing, ref missing, ref missing,
                                             ref missing, ref missing, ref missing, ref isVisible, ref missing,
                                             ref missing, ref missing, ref missing);
            wordDoc.Activate();
            
            this.changeBookMarkValue(bookMark.painName, pain.Name, wordDoc);
            this.changeBookMarkValue(bookMark.painName1, pain.Name, wordDoc);
            this.changeBookMarkValue(bookMark.painAge, pain.Age, wordDoc);
            this.changeBookMarkValue(bookMark.gender, pain.Gender, wordDoc);
            this.changeBookMarkValue(bookMark.gender1, pain.Gender, wordDoc);
            this.changeBookMarkValue(bookMark.addrAlways, pain.HomeAddr, wordDoc);
            this.changeBookMarkValue(bookMark.id1, pain.Id, wordDoc);
            this.changeBookMarkValue(bookMark.idCard, pain.IdCardNumber, wordDoc);
            this.changeBookMarkValue(bookMark.phone, pain.Phone, wordDoc);
            this.changeBookMarkValue(bookMark.reportDoctor, pain.DoctorName, wordDoc);
            this.changeBookMarkValue(bookMark.reportDoctor1, pain.DoctorName, wordDoc);
            this.changeBookMarkValue(bookMark.reportTime, pain.InDay, wordDoc);
            this.changeBookMarkValue(bookMark.reportTime1, pain.InDay, wordDoc);
            wordDoc.SaveAs(ref savePath, ref missing, ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing);
            wordDoc.Close(ref saveChanges, ref missing, ref missing);//关闭文档*/
            #endregion
            openFinalFile(savePath);
        }

        /* private void changeBookMarkValue(object bookMarkName, string editText, Word.Document wordDoc)
         {
             Word.Bookmark bm = wordDoc.Bookmarks.get_Item(ref bookMarkName);//返回书签 
             bm.Range.Text = editText;//设置书签域的内容
         }*/
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

        #region 结束winword进程
        /// <summary>
        /// 杀掉winword.exe进程   
        /// </summary>   
        public void killWinWordProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }
        #endregion
    }
}
