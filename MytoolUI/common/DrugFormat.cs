using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MytoolUI.common
{
    internal class DrugFormat
    {

        public string Start(string drugInfo)
        {
            string str = drugInfo;
            string[] strArrary = str.Split('\n');
            string outMessage = "\t" + new string('-', 40);
            int nameLenth = 0;
            List<string[]> drugList = new List<string[]>();
            foreach (var item in strArrary)
            {
                if (!item.Contains("------------"))
                {

                    try
                    {
                        string[] newArrary = item.Split('；');
                        string drugName = newArrary[0].Split(' ')[0].Trim();
                        string drugSingleQuantity = newArrary[1].Replace("每次：", "").Trim();
                        string drugWay = newArrary[2].Replace("用法:", "").Trim();
                        if (nameLenth < drugName.Length)
                        {
                            nameLenth = drugName.Length;
                        }
                        drugList.Add(new string[] {drugName,drugSingleQuantity,drugWay});
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            foreach (var item in drugList)
            {
                string name = item[0].PadRight(nameLenth);
                string single = item[1];
                string way = item[2];
                outMessage = outMessage + "\r\n\t" + name + " \t" + single + "\t" + way ;
            }
            return outMessage + "\r\n\t" + new string('-', 40);
        }
    }
}
