using ScreenCapture;
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
    /// ScreenShootWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenShootWindow : Window
    {
        public ScreenShootWindow()
        {
            InitializeComponent();
            double x = (SystemParameters.WorkArea.Size.Width / 2 );
            double y = (SystemParameters.WorkArea.Size.Height - this.Height);
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = x;
            this.Top = 0;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnShoot_Click(object sender, RoutedEventArgs e)
        {
            CaptureWindow capture = new CaptureWindow();
            capture.ShowDialog();

        }
    }
}
