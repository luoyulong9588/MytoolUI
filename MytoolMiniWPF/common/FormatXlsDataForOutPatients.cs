using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MytoolMiniWPF.views;

namespace MytoolMiniWPF.common
{
    /// <summary>
    /// 用于门诊首页导出文件的格式化处理
    /// </summary>
    public  class FormatXlsDataForOutPatients
    {
        public Random rd = new Random();
        DatabaseForPatient dbInfo = new DatabaseForPatient();
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=.\config\address.db;Version=3;");
        

        /// <summary>
        /// 门诊首页填写格式化门诊日志函数
        /// </summary>
        /// <param name="dataTable"></param>
        public void StartFormatFile(DataTable dataTable)
        {

            if (dataTable == null)
            {
                UMessageBox.Show("错误！", "没有选择正确的文件！");
                return;
            }
            string doctorName, patientName, male, female, age, phone, vocation, idCard, workAddress, nowAddress, comeDate, diaseDate, isFirst, notFirst, bloodPressure, mainChef, blank, diagMemory, mainDrug;
            int rows = dataTable.Rows.Count;//获取行数
            int columns = dataTable.Columns.Count;
            string userName = new DatabaseUnit().GetuserName();
           //导出日期，现在不需存储 string outportDate = Convert.ToDateTime(dataTable.Rows[1][0].ToString().Split(' ')[0].Split(':')[1]).ToString("yyyy年MM月");
            object[,] arrycell = new object[rows - 7, 19];

            for (int i = 4; i < rows - 3; i++)
            {
                doctorName = dataTable.Rows[i][0].ToString();
                patientName = dataTable.Rows[i][1].ToString();
                male = dataTable.Rows[i][2].ToString();
                female = dataTable.Rows[i][3].ToString();
                age = dataTable.Rows[i][4].ToString();
                phone = dataTable.Rows[i][5].ToString();
                vocation = dataTable.Rows[i][6].ToString();
                idCard = dataTable.Rows[i][7].ToString();
                workAddress = dataTable.Rows[i][8].ToString();
                nowAddress = dataTable.Rows[i][9].ToString();
                comeDate = dataTable.Rows[i][10].ToString();
                diaseDate = dataTable.Rows[i][11].ToString();
                isFirst = dataTable.Rows[i][12].ToString();
                notFirst = dataTable.Rows[i][13].ToString();
                bloodPressure = dataTable.Rows[i][14].ToString();
                mainChef = dataTable.Rows[i][15].ToString();
                diagMemory = dataTable.Rows[i][17].ToString();
                blank = dataTable.Rows[i][16].ToString();
                mainDrug = dataTable.Rows[i][18].ToString();
                if (doctorName != userName)
                {
                    MessageBox.Show("doctorName dose not match the userName in the database;", "SQL/SQLite Error");
                    return;
                }

                if (phone == "")
                {
                    phone = randomPhone();
                }
                if (vocation == "" || vocation == "农民" || vocation.Contains("无职业") || vocation == "其他" || workAddress.Length < 2) //农民不存在工作地址  // 没有工作地址的只配当农民
                {
                    vocation = "农民";
                    workAddress = "无";
                }
                else if (vocation.Contains("休"))
                {
                    vocation = "退休人员";
                }
                else if (workAddress.Contains("医院") || workAddress.Contains("社区卫生") || workAddress.Contains("卫生院"))
                {
                    vocation = "医务人员";
                }
                else if (vocation.Contains("教学"))
                {
                    vocation = "教师";
                }
                else if (vocation.Contains("职员"))
                {
                    vocation = "干部职员";
                }

                if (idCard == "")
                {
                    idCard = "未带";
                    //    continue;  //  未带身份证的患者作移除操作；
                }

                nowAddress = FormatAddress(nowAddress);

                bloodPressure = randomBloodPressure();

                if (diaseDate == "")
                {
                    diaseDate = comeDate;
                }
                if (mainDrug == "") // 首先处理无医嘱的;
                {
                    mainDrug = "未就诊";
                    mainChef = "未就诊";
                    diagMemory = "未就诊";
                }

                if (mainDrug.Contains("新型冠状病毒核酸")) // 再处理核酸检测的;
                {
                    mainChef = "新型冠状病毒核酸筛查";
                }

                if (diagMemory == "")
                {
                    diagMemory = mainDrug;
                    if (mainChef == "")
                    {
                        mainChef = mainDrug;
                    }
                }
                if (mainChef == "")
                {
                    mainChef = randomChief(diagMemory);
                }

                string gender = male.Length > 0 ? "男" : "女";
                dbInfo.SaveInfoToDb(doctorName, patientName, gender, age, phone, vocation, idCard, workAddress, nowAddress, comeDate, diaseDate, bloodPressure, mainChef, diagMemory, mainDrug);
            }
        }
        private string FormatAddress(string address)
        {
            address = "重庆市綦江区" + address.Replace("重庆市", "").Replace("綦江区", "").Replace("綦江县", "").Replace("重庆", "").Replace("綦江", "").Replace(" ", "");

            //Regex cityRegex = new Regex(@"\S+綦江\S+[组社号队]")
            string villagePattern = @"\S+村\S+[组社号队]";// match format : xx村xx组 or xx村xx社 or xx村xx号 with out house number.
            string townPattern = @"\S+[镇]\S+[号]"; // match format: xx镇xx号.

            //1.不含村，含有 number-number的，则属于城镇;要先replace space
            if (Regex.IsMatch(address, @"\d-\d") && !Regex.IsMatch(address, @"[组村社号队镇]+"))
            {
                return address;
            }
            else if (Regex.IsMatch(address, villagePattern))
            {
                return new Regex(villagePattern).Match(address).Value;
            }
            else if (Regex.IsMatch(address, townPattern))
            {
                return new Regex(townPattern).Match(address).Value;
            }

            //2.剩下的2中情况，2.1 不含number-number含有村的【农村】；2.2 不含number-number不含有村的【可能城市】；2.3含有number-number含有村的【农村，错误address】；

            return formateRandomAddress();


        }
        public string randomPhone()
        {
            string[] dog = { "1", "213" };
            string[] arrayHead = { "13", "15", "17", "18" };
            string head = arrayHead[rd.Next(4)];
            string end = rd.Next(19010102, 997939299).ToString();
            //Console.WriteLine(string.Format("函数中{0}", end));
            return head + end;
        }

        /// </summary>
        /// 产生綦江区内的随机地址
        /// <param name="isStr">是否地址字符串</param>
        /// <returns>xx市xx区xx街道/镇xx社区/村xx组/门牌号</returns>
        /// 
        public (string city ,string streetName,string villageName,string endName) randomAddress()
        {
            /*
            isStr:地址字符串；否则：
            */

            /*string city = "重庆市綦江区";
            string[] arrayContry = { "古南镇","古南街道","文龙街道","三江街道","金桥镇",
                                        "石角镇","东溪镇","赶水镇","打通镇","石壕镇",
                                        "永新镇","三角镇","隆盛镇","郭扶镇","篆塘镇",
                                        "丁山镇","安稳镇","扶欢镇","永城镇","新盛镇",
                                        "中峰镇","横山镇"};
            string contry = arrayContry[rd.Next(arrayContry.Length)];
            return city + contry;*/

            string city = "重庆市綦江区";
            int streetId = 500110002;
            string streetName = "文龙街道";
            string villageName = "版画院社区";
            string endName;
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select id,name from street_qj", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int count = 0;
            //Random random = new Random();
            int maxCont = new Random(Guid.NewGuid().GetHashCode()).Next(0, 23);

            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString());
                count++;
                if (count >= maxCont)
                {
                    streetId = int.Parse(reader[0].ToString());
                    streetName = reader[1].ToString();
                    command = new SQLiteCommand($"select name from village_qj where id ={streetId}", m_dbConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        villageName = reader[0].ToString();
                    }
                    break;
                }

            }
            m_dbConnection.Close();
            if (villageName.Contains("村"))
            {
                //endName = $"{new Random(Guid.NewGuid().GetHashCode()).Next(1, 8)}组{new Random(Guid.NewGuid().GetHashCode()).Next(1, 99)}号";
                endName = $"{new Random(Guid.NewGuid().GetHashCode()).Next(1, 8)}组{new Random(Guid.NewGuid().GetHashCode()).Next(1, 50)}号";
            }
            else
            {
                endName = $"{new Random(Guid.NewGuid().GetHashCode()).Next(1, 99)}号{new Random(Guid.NewGuid().GetHashCode()).Next(1, 13)}-{new Random(Guid.NewGuid().GetHashCode()).Next(1, 8)}";
            }

             return   (city, streetName, villageName, endName);
          

          

        }

        /// <summary>
        /// 随机血压生产
        /// </summary>
        /// <returns></returns>

        public string formateRandomAddress()
        {
            var addr = randomAddress();
            return addr.city + addr.streetName + addr.villageName + addr.endName;
        }
        public string randomBloodPressure()
        {
            int sbp, dbp;
            while (true)
            {
                sbp = rd.Next(94, 140);
                dbp = rd.Next(60, 90);
                if (sbp % 2 == 0 && dbp % 2 == 0 && (sbp - dbp) > 30 && (sbp - dbp) < 60)
                {
                    break;
                }
            }
            return string.Format(@"{0}/{1}mmHg", sbp, dbp);
        }

        public string randomHeight(string gender)
        {
            if (gender =="男")
            {
                return rd.Next(165, 173).ToString();
            }
            else
            {
                return rd.Next(155, 165).ToString();
            }
        }
        public string randomWeight(string gender)
        {
            if (gender == "男")
            {
                return rd.Next(50,60).ToString();
            }
            else
            {
                return rd.Next(45, 55).ToString();
            }
        }
        public string randomTempture()
        {
            return "36." + rd.Next(0, 9).ToString();
        }
        public string randomPulse()
        {
            return rd.Next(65, 95).ToString();
        }
        public string randomRespiration()
        {
            return rd.Next(17,20).ToString();
        }
        private string randomChief(string checkString)
        {
            List<string> data = new DatabaseUnit().GetrandomChief(checkString);
            if (data.Count > 0)
            {
                string result = data[rd.Next(data.Count)].ToString();
                return result;
            }
            else
            {
                return checkString;
            }
        }


    }
}
