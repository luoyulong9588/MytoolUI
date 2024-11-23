using MytoolMiniWPF.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MytoolMiniWPF
{
    public partial class MainWindow
    {

        Window chronicDiseaseWindow = null;


        private void ChronicDiseaseWindowCreate()
        {

            if (chronicDiseaseWindow == null)
            {

                chronicDiseaseWindow = new ChronicDiseaseWindow();
                chronicDiseaseWindow.Show();
            }
            else if (PresentationSource.FromVisual(chronicDiseaseWindow) == null)
            {
                chronicDiseaseWindow = new ChronicDiseaseWindow();
                chronicDiseaseWindow.Show();
            }

            else
            {
                chronicDiseaseWindow.Focus();
                MessageBox.Show("ChronicDisease Window is alerady running! Do not running the same Page!", "warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
