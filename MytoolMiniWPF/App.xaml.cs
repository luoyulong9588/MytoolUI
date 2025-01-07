using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Globalization;

namespace MytoolMiniWPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private const string MutexName = "CQMU.OfficeTool.MytoolUI"; // 定义互斥锁名称  
        private Mutex mutex;

        public App(){
            // 设置中文区域
            var cultureInfo = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool createdNew;
            mutex = new Mutex(true, MutexName, out createdNew); // 尝试创建互斥锁  

            if (!createdNew) // 如果互斥锁已存在，说明有其他实例在运行  
            {
                MessageBox.Show("Another instance of the application is already running.");
                Shutdown(); // 退出当前实例  
                Environment.Exit(1);
            }
            else
            {
                // 正常启动应用程序的逻辑  
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            mutex?.ReleaseMutex(); // 释放互斥锁  
            mutex?.Close(); // 关闭互斥锁句柄  
        }
    }
}
