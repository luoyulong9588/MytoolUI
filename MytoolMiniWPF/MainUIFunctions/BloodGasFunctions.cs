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
        Window bloodGasWindow = null;


        private void BloodGasWindowCreate()
        {
            // PresentationSource.FromVisual(bloodGasWindow)==null



            if (bloodGasWindow ==null)
            {

                bloodGasWindow = new BloodGasPage();
                bloodGasWindow.Show();
            }
            else if (PresentationSource.FromVisual(bloodGasWindow) == null)
            {
                bloodGasWindow = new BloodGasPage();
                bloodGasWindow.Show();
            }

            else
                {
                    bloodGasWindow.Focus();
                    MessageBox.Show("BloodGas Page is alerady running! Do not running the same Page!", "warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            
        }
    }
}
