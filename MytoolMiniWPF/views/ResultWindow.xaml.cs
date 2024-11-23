
using System.Windows;
using System.Windows.Documents;
using WpfToast.Controls;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// ResultWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ResultWindow : Window
    {
        private string _result;
        public string Result
            {
            get { return _result; }
            set { 
                
                _result = value;
                Run run = new Run(_result);
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(run);
                richBoxResult.Document.Blocks.Add(paragraph);
            }
            }
        public ResultWindow()
        {
            InitializeComponent();

        }



        public static bool? Show(string msg)
        {
            var msgBox = new ResultWindow();
            msgBox.Title = "提示";
            msgBox.Result = msg;
            
            return msgBox.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Toast.Show(this,"复制成功！", new ToastOptions { Icon = ToastIcons.Information, ToastMargin = new Thickness(2), Time = 2000, Location = ToastLocation.OwnerCenter });

            TextRange textRange = new TextRange(richBoxResult.Document.ContentStart, richBoxResult.Document.ContentEnd);
            common.ClipBoardHelper.CopyText(textRange.Text);


        }
    }
}
