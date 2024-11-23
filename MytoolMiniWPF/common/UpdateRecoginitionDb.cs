using MytoolMiniWPF.views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MytoolMiniWPF.common
{
    internal static class UpdateRecoginitionDb
    {

        public static void Update(string processName)
        {
            try
            {
                // 获取当前应用程序的运行目录
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string subfolder = "医检互认日志处理";
                
                string exePath = Path.Combine(currentDirectory, subfolder, processName);

                // 检查文件是否存在
                if (File.Exists(exePath))
                {
                    // 启动test.exe
                    Process.Start(exePath);
                }
                else
                {
                    UMessageBox.Show($"无法找到文件: {exePath}", "错误");
                }
            }
            catch (Exception ex)
            {
                UMessageBox.Show($"出现错误: {ex.Message}", "错误");
            }

        }

    }
}
