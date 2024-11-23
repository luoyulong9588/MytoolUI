using MytoolMiniWPF.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
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
    /// NotePadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotePadWindow : Window
    {
        private bool noteMode = false;
        private static string file = "config\\note.document";
        public NotePadWindow()
        {
            InitializeComponent();
            CheckStatue();
            //LoadNote();
        }

        private void CheckStatue()
        {
            noteMode = (bool)this.toggleBtnIsNoteMode.IsChecked;
        }

        private void btnClose_Click(object sender=null, RoutedEventArgs e = null)
        {
            if (noteMode)
            {
                SaveNote();

            }

            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        /// <summary>
        /// 字体设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemFontStyle_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                richTextBoxContent.FontFamily = new FontFamily(fd.Font.Name);
                richTextBoxContent.FontSize = fd.Font.Size * 96.0 / 72.0;
                richTextBoxContent.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                richTextBoxContent.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

            }
        }
        /// <summary>
        /// 生成出院记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDischargeRecordBuild_Click(object sender, RoutedEventArgs e)
        {
            if (noteMode) {
                UMessageBox.Show("请先关闭笔记模式！");
                return;
            }
            BuildDischargeRecord app = new BuildDischargeRecord();
            ResultWindow.Show(app.Start(GetRichText()));
            try
            {

                app = new BuildDischargeRecord();
                ResultWindow.Show(app.Start(GetRichText()));
            }
            catch (Exception ex)
            {
                UMessageBox.Show("错误!",ex.ToString());
            }
        }
        /// <summary>
        /// 清除richBox内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            this.richTextBoxContent.Document.Blocks.Clear();
        }
        /// <summary>
        /// 带药格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDragFormat_Click(object sender, RoutedEventArgs e)
        {
            if (noteMode)
            {
                UMessageBox.Show("请先关闭笔记模式！");
                return;
            }
            try
            {
                DrugFormat app = new DrugFormat();
                ResultWindow.Show(app.Start(GetRichText()));

            }
            catch (Exception ex)
            {
                UMessageBox.Show("错误!", ex.ToString());
            }
        }

        private void MenuItemMedicalLaboratorydFormat_Click(object sender, RoutedEventArgs e)
        {
            if (noteMode)
            {
                UMessageBox.Show("请先关闭笔记模式！");
                return;
            }
            try
            {
                var app = new MedicalLaboratoryFormat();
                ResultWindow.Show(app.Start(GetRichText()));
            }
            catch (Exception ex)
            {
                UMessageBox.Show("错误!", ex.ToString());
            }
           

        }
        private void toggleBtnIsNoteMode_Click(object sender, RoutedEventArgs e)
        {
            CheckStatue();

            if (noteMode)
            {
                LoadNote();
            }
            else
            {
                SaveNote();
                richTextBoxContent.Document.Blocks.Clear();
            }

        }

        private string GetRichText()
        { 
            TextRange textRange = new TextRange(richTextBoxContent.Document.ContentStart, richTextBoxContent.Document.ContentEnd);
            
            return textRange.Text;
        }
        private void SaveNote()
        { 
            using (FileStream fileStream = File.Create(file))
            {
                TextRange textRange = new TextRange(richTextBoxContent.Document.ContentStart, richTextBoxContent.Document.ContentEnd);
                textRange.Save(fileStream, System.Windows.DataFormats.Rtf);
            }
        }
        private void LoadNote()
        {
            var  textRange = new TextRange(richTextBoxContent.Document.ContentStart, richTextBoxContent.Document.ContentEnd);
            using (var fs = new FileStream(file,FileMode.Open))
            {
                textRange.Load(fs, System.Windows.DataFormats.Rtf);
            }
        
        }
    }
}
