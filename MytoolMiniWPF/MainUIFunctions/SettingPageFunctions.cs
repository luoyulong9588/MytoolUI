using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MytoolMiniWPF.views;

namespace MytoolMiniWPF
{
    public partial class MainWindow
    {

        Window settingWindow = null;

        public void SettingWindowCreate()
        {

            if (settingWindow == null)
            {

                settingWindow = new Settings();
                settingWindow.Show();
            }
            else if (PresentationSource.FromVisual(settingWindow) == null)
            {
                settingWindow = new Settings();
                settingWindow.Show();
            }

            else
            {
                settingWindow.Focus();
                MessageBox.Show("setting Window is alerady running! Do not running the same Page!", "warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
