using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// InputBox.xaml 的交互逻辑
    /// </summary>
    public partial class InputBox : Window
    {
        private bool _update = false;

        public bool Update 

        { get { return this._update; }
        }

        public string Title
        {
            get { return textBlockTitle.Text; }
            set { this.textBlockTitle.Text = value; }
        }

        public string Message
        {
            get { return this.inputTextBox.Text; }
            set { this.inputTextBox.Text = value; }
        }


        public InputBox(string currentContent = null)
        {
            InitializeComponent();

            Info student = new Info()
            {
                Message = string.IsNullOrEmpty(currentContent) ? "键入新增的主诉" : currentContent,
                Title = "编辑"
            };
            this.DataContext= student;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void btnEnsure_Click(object sender, RoutedEventArgs e)
        {
            this._update = true;
            this.Close();
        }
    }

    public class Info
    {
        public string Message { get; set; }
        public string Title { get; set; }
    }
}
