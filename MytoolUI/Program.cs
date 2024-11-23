using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MytoolUI
{
    static class Program
    {

        private static System.Threading.Mutex mutex;
        private static string userName;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //CreateDb();
            if (!CheckUserName())
            {
                MessageBox.Show("若要运行此应用程序 您必须首先安装 .NET Framework的以下版本之一：\r\nv6.0\r\n有关如何获取.NET Framework的适当版本的说明，请与应用程序开发者联系。", ".NET Framework初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (userName=="userName")
            {
                Application.Run(new UpdateMessage());
            }
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                if (args.Length == 0)
                {
                    Application.Run(new FormMainUI(mini:false));
                }
                else if (args[0] == "auto")
                {
                    Application.Run(new FormMainUI(mini:true));
                }
                else
                {
                    MessageBox.Show("参数错误");
                }
            }
            else
            {
                MessageBox.Show("程序已经在运行,请勿重复打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        static bool CheckUserName()
        {
            string checkString = "罗玉龙王雪玲刘益宏彭育欢朱庆霞李小琴userName张李张  李";
            userName = new DatabaseUnit().GetuserName();
            DateTime limitTime = DateTime.Parse("2023.12.01");
            DateTime currentTime = DateTime.Now;
            if (checkString.Contains(userName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 机器限制，通过对比机器码，如不符合则不能使用;
        /// </summary>
        /// <returns></returns>
        static bool CheckPermission()
        {
            DatabaseUnit db = new DatabaseUnit();
            string savedCode = db.GetRegisterCode();
            SoftReg soft = new SoftReg();

            string machineCode = soft.GetMNum();
            if (savedCode == "newUser")
            {
                db.SaveRegisterCode(machineCode);
            }
            else if (savedCode != machineCode)
            {
                MessageBox.Show("若要运行此应用程序 您必须首先安装 .NET Framework的以下版本之一：\r\nv6.0\r\n有关如何获取.NET Framework的适当版本的说明，请与应用程序开发者联系。", ".NET Framework初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }
    }
}
