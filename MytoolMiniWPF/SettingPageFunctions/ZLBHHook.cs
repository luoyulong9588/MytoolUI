using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// 中联hook update;这是为中联ZLBH系统作出的补丁。
    /// 原系统会频繁弹出扶贫提醒窗口，严重影响工作效率，通过下列函数功能实现hook掉扶贫提醒
    /// 但zlbh.exe的更新功能同时被屏蔽掉了，如果需要更新，程序提供一个原版的zlbh.exe供更新用
    /// 更新完后切换到hook版本即可。
    /// </summary>
    public partial class Settings
    {
        static string processName = "ZLBH";
        private string destPath = "D:\\ZLBH\\ZLBH.exe";
        private string destDllPath = "D:\\ZLBH\\扶贫提醒.dll";


        private void button_origon_Click(object sender, RoutedEventArgs e)
        {
            warningInfo();
            FileInfo origonApp = new FileInfo(Environment.CurrentDirectory + "\\config\\origin\\ZLBH.exe");
            CheckFileExists();
            origonApp.CopyTo(destPath);
            runZLBH();
        }

        private void button_hooked_Click(object sender, RoutedEventArgs e)
        {
            warningInfo();
            FileInfo origonApp = new FileInfo(Environment.CurrentDirectory + "\\config\\hook\\ZLBH.exe");
            FileInfo origondll = new FileInfo(Environment.CurrentDirectory + "\\config\\hook\\扶贫提醒.dll");
            CheckFileExists();
            origonApp.CopyTo(destPath);
            origondll.CopyTo(destDllPath);
            runZLBH();
        }
        private bool check_app_running()
        {
            if (System.Diagnostics.Process.GetProcessesByName("ZLBH").ToList().Count > 0)
            {
                return true;
            }
            return false;
        }
        private void kill_zlbh_process()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(processName);
            foreach (System.Diagnostics.Process p in process)
            {
                p.Kill();
            }
        }
        private void warningInfo()
        {
            if (check_app_running())
            {
                MessageBoxResult result = MessageBox.Show("zlbh.exe is still running, if continue,it will force kill the process", "warning", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    kill_zlbh_process();
                }
            }
        }
        private void runZLBH()
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(destPath);
        }

        private void CheckFileExists()
        {

            Thread.Sleep(1000);
            if (File.Exists(destPath))
            {
                File.Delete(destPath);
            }
            if (File.Exists(destDllPath))
            {
                File.Delete(destDllPath);
            }
        }
    }
}
