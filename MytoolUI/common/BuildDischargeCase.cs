using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MytoolUI.common
{
    internal class BuildDischargeCase
    {
        private string text;
        private string firstRecord;
        private string lastRecord;
        private string upperUpperRecord;
        private string ryBodyExam;
        private string ryResult;
        private string cureFunciton;
        private string pyhsicExam;
        private string ryDiagnose;
        private string cyDiagnose;
        private string healEducation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record">所有病程记录</param>
        /// <returns>首次病程记录</returns>
        public string[] GetFirstResult(string record)
        {
            string pattern = @"\d{4}-\d{2}-\d{2}[\s\S]\d{2}:\d{2}([.\s\S]*?)签名";
            MatchCollection collection = Regex.Matches(record, pattern);
            firstRecord = collection[0].ToString().Trim();
            lastRecord = collection[collection.Count - 1].ToString();
            string resultString = Regex.Split(collection[collection.Count - 1].ToString(), "辅助检查", RegexOptions.IgnoreCase)[0].Trim();
            lastRecord = Regex.Split(resultString, "处理", RegexOptions.IgnoreCase)[0].Trim();

            lastRecord = Regex.Replace(lastRecord, @"\d{4}-\d{2}-\d{2}[\s\S]\d{2}:\d{2}", "");
            lastRecord = lastRecord.Replace("日常病程记录", "").Replace("病程记录", "").Replace("刘益宏主治医师查房记录", "").Replace("张李副主任医师查房记录", "").Replace("周俊主治医师查房记录", "").Replace("签名", "");
            lastRecord = Regex.Replace(lastRecord, @"\n|\t|\r", "").Trim();
            return new string[] { firstRecord, lastRecord };

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record">所有病程记录</param>
        /// <returns>副主任医师查房记录</returns>
        private string GetUpperUpperRecord(string record)
        {
            string pattern, result;
            try
            {
                pattern = @"副主任医师查房记录([.\s\S]*?)诊疗计划";
                result = Regex.Match(record, pattern).ToString().Trim();
            }
            catch (Exception)
            {
                pattern = @"副主任医师查房记录([.\s\S]*?)患者目前治疗";
                result = Regex.Match(record, pattern).ToString().Trim();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record">首次病程记录</param>
        /// <returns>入院原因</returns>
        private string GetryResult(string record)
        {
            string pattern = @"因(.*?)于\d{4}年";
            string result = Regex.Match(record, pattern).ToString().Trim().Replace(":", "").Replace('"', ' ').Replace("。", "").Trim();
            result = Regex.Replace(result, @"于\d{4}年", "");
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record">首次病程记录</param>
        /// <returns>入院体格检查</returns>
        private string GetryBodyExam(string record)
        {
            string pattern, result;
            try
            {
                pattern = @"\d{1}.{1}体格检查([.\s\S]+?)\d{1}.{1}辅助检查";
                result = Regex.Match(record, pattern).ToString().Trim();
                result = Regex.Replace(result, @"\d{1}.{1}体格检查", "");
                result = Regex.Replace(result, @"\d{1}.{1}辅助检查", "");

            }
            catch (Exception)
            {
                pattern = @"\d{1}.{1}体检特点([.\s\S]+?)\d{1}.{1}辅助检查";
                result = Regex.Match(record, pattern).ToString().Trim();
                result = Regex.Replace(result, @"\d{1}.{1}体检特点", "");
                result = Regex.Replace(result, @"\d{1}.{1}辅助检查", "");
            }
            return result.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRecord">首次病程记录</param>
        /// <returns></returns>
        private string GetCureFunction(string record)
        {
            string pattern = @"诊疗计划([.\s\S]+?)健康宣教";
            string result = Regex.Match(record, pattern).ToString().Trim();
            result = this.RebuildCureFunction(result);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cure">未格式化首次病程记录诊疗计划</param>
        /// <returns></returns>
        private string RebuildCureFunction(string cure)
        {
            Console.WriteLine(cure);
            string pattern = @"1[、\.]([.\s\S]+?)2[、\.]([.\s\S]+?)3[、\.](.+)";
            MatchCollection collection = Regex.Matches(cure, pattern);

            Dictionary<string, string> cureDict = new Dictionary<string, string>();
            if (collection.Count > 0)
            {
                foreach (Match item in collection)
                {
                    GroupCollection groups = item.Groups;
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if (groups[i].ToString().Contains("护理常规"))
                        {
                            cureDict["basic"] = groups[i].ToString();
                        }
                        else if (groups[i].ToString().Contains("完善"))
                        {
                            cureDict["physic_check"] = groups[i].ToString();
                        }
                        else
                        {
                            cureDict["cure"] = groups[i].ToString().Substring(0, groups[i].ToString().Length - 1);
                        }
                    }
                }
                //string result = $"入院后予以{cureDict["basic"]}完善相关辅助检查<physic_check>{cureDict["cure"]}后病情好转，于今日出院。";
                string result = $"入院后予以{cureDict["basic"]}完善相关辅助检查,{cureDict["cure"]}后病情好转，于今日出院。";
                return result;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// old function,2023.08.18 forbidden;replaced by new function:GetPyhsicExamReuslt();
        /// </summary>
        /// <param name="record">副主任医师查房记录</param>
        /// <returns>辅助检查结果</returns>
        public string GetPyhsicExamResultForUpperUpper(string record)
        {
            string pattern, result;
            try
            {
                pattern = @"\d{1}.辅助检查([.\s\S]+?)三、诊断及依据";
                result = Regex.Match(record, pattern).ToString().Trim();


            }
            catch (Exception)
            {
                pattern = @"辅助检查：([.\s\S]+?)张李副主任医师查房后指示";
                result = Regex.Match(record, pattern).ToString().Trim();
            }
            Match match = Regex.Match(result, @"[；.。;,，]$");

            result = Regex.Replace(result, @"\d{1}.辅助检查", "");
            result = Regex.Replace(result, @"三、诊断及依据", "");
            result = Regex.Replace(result, @"张李副主任医师查房后指示", "");
            result = Regex.Replace(result, @"辅助检查：", "");
            result = Regex.Replace(result, @"\n|\t|\r", "");
            if (match.Length < 1)
            {
                return result;
            }
            else
            {
                return result.Substring(0, result.Length - 1) + "。";
            }
        }


        public string GetPyhsicExamReuslt(string allRecord)
        {
            Regex reg = new Regex(@"辅助检查[:|：](.+)");
            Regex regReplace = new Regex("辅助检查[:|：]");
            MatchCollection match = reg.Matches(allRecord);
            string result = "";
            for (int i = 0; i < match.Count; i++)
            {
                if (match[i].Value.Contains("无补充"))
                {
                    continue;
                }
                result += match[i].Value;
            }
            //MessageBox.Show(regReplace.Replace(result, ""));
            return regReplace.Replace(result,"");
        }
        /// <summary>
        /// 获取入院诊断信息
        /// </summary>
        /// <param name="record">首次病程记录</param>
        /// <returns></returns>
        private string GetryDiagnose(string record)
        {
            Match match;
            string result;
            match = Regex.Match(record, @"初步诊断：([.\s\S]+?)诊断依据");
            if (match.Length > 0)
            {
                result = Regex.Replace(match.ToString().Trim(), @"初步诊断：", "");
                result = Regex.Replace(result, "诊断依据", "");
            }
            else
            {
                match = Regex.Match(record, @"入院诊断：([.\s\S]+?)诊断依据");
                result = Regex.Replace(match.ToString().Trim(), @"入院诊断：", "");
                result = Regex.Replace(result, "诊断依据", "");
            }
            return Regex.Replace(result, @"\n|\t|\r", "");
        }
        /// <summary>
        /// 获取出院诊断信息
        /// ver1.0 尝试寻找修正诊断作为最新的诊断
        /// </summary>
        /// <param name="record">副主任医师查房记录</param>
        /// <returns></returns>
        private string GetcyDiagnose(string allRecord,string upperUpperRecord)
        {
            string result;
            result = SearchFixedDiagnose();  
            if (result == null || result == "")
            {
                result = Regex.Match(upperUpperRecord, @"主要诊断：([.\s\S]+?)诊断依据").ToString().Trim();
            }
            if (result == null || result == "")
            {
                result = Regex.Match(upperUpperRecord, @"患者目前诊断：([.\s\S]+?)").ToString().Trim();
            }
            //result = Regex.Replace(result, @"主要诊断：", "");
            //result = Regex.Replace(result, @"诊断依据", "");
            //result = Regex.Replace(result, @"患者目前诊断：", "");
            result = Regex.Replace(result, @"修正诊断：|修正诊断:|患者目前诊断：|诊断依据|主要诊断：|\n|\t|\r", "");
            //result = Regex.Replace(result, @"\n|\t|\r", "");

            return result;
            
            // 这个函数从所有的记录中匹配修正诊断作为结果返回
            string SearchFixedDiagnose()
            {
                MatchCollection fixedDiagnose = Regex.Matches(allRecord, @"修正诊断[：:]([.\s\S]+?)\n"); // ToString().Trim();
                return fixedDiagnose.Count == 0? null:fixedDiagnose[fixedDiagnose.Count - 1].ToString().Trim();
            }
        }

        public string HealthEducation(string diagnose)
        {
            Dictionary<string, string> healDict = new Dictionary<string, string>
            {
                ["COPD"] = "慢性阻塞性肺疾病健教处方：①.预防感冒，能有效地预防慢性阻塞性肺疾病的发生或急性发作；②.饮食宜清淡，忌辛辣荤腥；③.腹式呼吸具体方法：吸气时尽量使腹部隆起，呼气时尽力呼出使腹部凹下；每天锻炼2～3次，每次10～20分钟；④.避毒消敏：厨房居室应注意通风或装置脱排油烟机，应保持室内外环境的清洁卫生。⑤预防电信诈骗。",
                ["UA"] = "冠心病健教处方：①调节饮食，低盐、低脂、低糖、多纤维素，戒烟戒酒忌辛辣；保持适当体力活动，但不致发生疼痛症状为度；一般不需卧床休息；不宜用力排便，心脏负担加重可诱发心绞痛或发生意外。②遵出院医嘱服药；③预防电信诈骗。",
                ["DM"] = "糖尿病健教处方:①针以每天可供的总热量，以碳水化合物占50-60%，蛋白约占15%，脂肪约占25%为原则进行分配;并宜多吃一些富含高纤维的食物，按早餐占1/5，中、晚餐各占2/5的比例作分配②吃饭时间应尽量固定，并且要和用降血糖药物的时间配合好：胰岛素一般在吃饭前15－30分钟注射；磺脲类口服降糖药(如达美康、美吡达等）及中药一般在吃饭前半小时服；双胍类口服降糖药一般在吃饭后或吃饭中间服；a-糖苷酶抑制剂(如拜平）一般在吃第一口饭时同时嚼碎服；如果已吃过饭而忘了吃药、打胰岛素针，千万不要在饭后再补吃或注射，以免发生低血糖；已经吃药或注射过胰岛素，但因故吃饭延迟，可在原定的吃饭时间内加餐，但吃饭时应相应减去主食量;③糖尿病病人不能限制喝水，饭前喝一杯水后就可有饱胀感，同时也能起到减少进食量的作用;④市售的糖尿病食品多用荞麦或燕麦等制作，有的仅仅是含纤维素多些或未加糖而已，但仍然以粮食成分(碳水化合物）为主，食用时应相应计算进饮食计划总量中;⑤当外出旅游、参加重体力劳动或参加体育运动时，饭量可适当增加，或相应减少胰岛素以及口服降糖药的剂量;⑥预防电信诈骗。",
                ["NCZ"] = "做到有效预防最好遵守三大纪律、八项注意。三大纪律：第一，生活规律化。第二，饮食科学化。第三，文体活动经常化。八项注意：第一，保持血压正常。第二，保持正常体重。第三，保持正常血脂。第四，饮食平衡。第五，戒烟、控酒、减盐。第六，坚持适度体育锻炼。第七，讲求精神心理卫生。第八，保持良好心态，树立自我保护意识。预防电信诈骗。",
                ["HBP"] = "注意低盐饮食（每日在5克左右）。少吃高胆固醇、高甜、高脂肪食物。多食高蛋白、豆类、新鲜蔬菜瓜果等。2.不宜过度紧张、劳累及情绪激动。不吸烟，不饮酒、咖啡或浓茶。3.避免剧烈运动，适当参加体育活动，如保健操、气功、太极拳、散步。冠心病人夜间最好不独居一室。4.寒冷季节注意保暖，不宜户外活动过久。防止感冒，外出要戴口罩。5.定期测量血压，按时服用降压药物，不可随意停用或增大剂量。有头痛、恶心、呕吐、心悸、胸闷、心前区痛、视力模糊等症状，应及时就医。预防电信诈骗。",
                ["OTHER"] = "嘱患者低盐低脂饮食，防止跌倒，不宜过度紧张、劳累及情绪激动；不吸烟，不饮酒、咖啡或浓茶；避免剧烈运动，注意保暖，避免受凉，防止感染。预防电信诈骗。",
            };

            if (diagnose.Contains("慢性阻塞性"))
            {
                return healDict["COPD"];
            }
            else if (diagnose.Contains("冠状动脉粥样硬化"))
            {
                return healDict["UA"];
            }
            else if (diagnose.Contains("高血压"))
            {
                return healDict["HBP"];
            }
            else if (diagnose.Contains("糖尿病"))
            {
                return healDict["DM"];
            }
            else if (diagnose.Contains("冠状动脉粥样硬化"))
            {
                return healDict["UA"];
            }
            else if (diagnose.Contains("脑梗死") || diagnose.Contains("脑出血"))
            {
                return healDict["NCZ"];
            }
            else
            {
                return healDict["OTHER"];
            }
        }

        public string Start(string allRecord)
        {
            this.GetFirstResult(allRecord);
            this.upperUpperRecord = this.GetUpperUpperRecord(allRecord);
            this.ryResult = this.GetryResult(firstRecord);
            this.ryBodyExam = this.GetryBodyExam(firstRecord);
            this.cureFunciton = this.GetCureFunction(firstRecord);
            //this.pyhsicExam = this.GetPyhsicExamResultForUpperUpper(upperUpperRecord);

            this.pyhsicExam = this.GetPyhsicExamReuslt(allRecord);
            this.ryDiagnose = this.GetryDiagnose(firstRecord);
            this.cyDiagnose = this.GetcyDiagnose(allRecord,upperUpperRecord);

            this.healEducation = this.HealthEducation(cyDiagnose);   //cure_function.replace(\" < physic_check >\", physic_exam_result)
            string highBp = ",规律检测血压,按时服用降压药,勿自行调整药物剂量,出现头晕、头痛、肢体活动障碍、晕厥等情况及时就诊;";
            string t2DM = "规律检测血糖,按时使用降糖药物,出现心悸、冷汗等情况时及时测血糖避免低血糖发生;";
            string outText = $@"    入院情况: 患者{ryResult}入院。入院查体{ryBodyExam}
    初步诊断: {ryDiagnose}
    诊疗经过: {cureFunciton}
    住院期间相关辅助检查: {pyhsicExam}
    出院诊断: {cyDiagnose}
    出院情况: {lastRecord}
    出院医嘱:
    1.{healEducation}
    2.离院后继续服药:

    3.门诊随访,按时服药,勿自行调整药物剂量,如有不适，立即就诊。
      科室电话: 023 - 48623017
";

            return outText;
        }


    }
}
