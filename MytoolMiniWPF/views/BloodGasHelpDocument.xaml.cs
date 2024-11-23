using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// BloodGasHelpDocument.xaml 的交互逻辑
    /// </summary>
    public partial class BloodGasHelpDocument : Window
    {
        public BloodGasHelpDocument()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void helpShowBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@".\config\血气分析.pptx");
        }
    }
}
