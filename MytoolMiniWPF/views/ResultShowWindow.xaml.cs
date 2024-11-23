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
    /// ResultShowWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ResultShowWindow : Window
    {
        public ResultShowWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(richBoxResult.Document.ContentStart, richBoxResult.Document.ContentEnd);
            common.ClipBoardHelper.CopyText(textRange.Text);
        }
    }
}
