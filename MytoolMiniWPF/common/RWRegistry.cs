using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MytoolMiniWPF.common
{
    public static class RWRegistry
    {
        public static void WriteRegistryValue(string userName)
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MytoolUI\\Data", true);

                    if (rk == null)
                    {
                        rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\MytoolUI\\Data");
                    }
                using (rk)
                {
                    rk.SetValue("UserName", userName);
                }
                
                }
            catch(Exception ex  )
            {
                Console.Write(ex.Message);
            }
        
        }
        public static string GetRegistryValue()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MytoolUI\\Data", true);
            if (rk !=null)
            {
                object value = rk.GetValue("UserName");
                if (value!=null)
                {
                    return value.ToString();
                }
                return null;
            }
            return null;
        }
    }
}
