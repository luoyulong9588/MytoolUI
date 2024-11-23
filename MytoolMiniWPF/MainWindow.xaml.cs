using MytoolMiniWPF.views;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ScreenCapture;
using MytoolMiniWPF.common;
using System;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using WpfToast.Controls;
using System.Drawing;
using System.Windows.Media;

namespace MytoolMiniWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string MutexName = "YourCompanyName.YourAppName"; // 定义互斥锁名称  
        private Mutex mutex;
        private bool isHide = false;



        public MainWindow()
        {
            InitializeComponent();
            SetDesktopLocation();
            this.Topmost= true;
            mainThreadSynContext = SynchronizationContext.Current;
            CheckUserName();
            CheckCacheFolder();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
       
            

        }

    

        // 设置窗口初始位置
        private void SetDesktopLocation()
        {
            double x = (SystemParameters.WorkArea.Size.Width / 2 - this.Width) / 2;
            double y = (SystemParameters.WorkArea.Size.Height - this.Height);
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left= x;
            this.Top = 0;

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }
        private void CheckCacheFolder()
        {
            if (!System.IO.Directory.Exists(".\\cache"))//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(".\\cache");
            }
        }
        private void CheckUserName()
        {
            string userName =  new DatabaseUnit().GetuserName();

            string userNameRegister = RWRegistry.GetRegistryValue();

            if (string.IsNullOrEmpty(userName)) 
            {
                if (string.IsNullOrEmpty(userNameRegister))
                {
                    Toast.Show(this, "用户名为空，请前往设置界面配置用户信息！", new ToastOptions { Icon = ToastIcons.Information, ToastMargin = new Thickness(2), Time = 5000, Location = ToastLocation.ScreenCenter });
                    Thread.Sleep(1000);
                    SettingWindowCreate();
                }
                else
                {
                    Toast.Show(this, $"程序已更新！已从注册表恢复用户“{userNameRegister}”", new ToastOptions { Icon = ToastIcons.Information, ToastMargin = new Thickness(2), Time = 5000, Location = ToastLocation.ScreenCenter });
                    new DatabaseUnit().UpdateUserName(userNameRegister);
                }

            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            var window = sender as Window;
            if (window != null)
            {
                // 获取屏幕尺寸  
                var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)window.Left, (int)window.Top));
                var screenWidth = screen.Bounds.Width;
                var screenHeight = screen.Bounds.Height;

                // 检查窗口是否贴边（这里以左边为例）  
                // 注意：你可能需要调整这个阈值（这里是10）以符合你的需求  
                if (window.Top < 5 && !isHide)
                {
                    // 隐藏窗口，这里通过最小化来实现  
                    window.Top = -window.Height + 10;
                    
                    isHide = true;

                    return;
                }

                // 类似地，你可以检查其他边缘（顶部、右侧、底部）  
                // ...  
            }

        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            var window = sender as Window;
            var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)window.Left, (int)window.Top));
            var screenWidth = screen.Bounds.Width;
            var screenHeight = screen.Bounds.Height;
            if (isHide)
            {
                window.Top = 0;
                isHide = false;
            }

        }

       
    }



}
