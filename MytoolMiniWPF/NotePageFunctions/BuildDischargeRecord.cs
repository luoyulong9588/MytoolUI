using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MytoolMiniWPF.common
{
    internal class BuildDischargeRecord
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
            lastRecord = Regex.Replace(lastRecord, @".*?查房记录|.*?病程记录","").Replace("签名", "");
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
                        string str = groups[i].ToString();
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
                            cureDict["cure"] = groups[i].ToString().Substring(0, groups[i].ToString().Length - 2);
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
                pattern = @"\d{1}.辅助检查([.\s\S]+?)三、主要诊断";
                result = Regex.Match(record, pattern).ToString().Trim();


            }
            catch (Exception)
            {
                pattern = @"辅助检查：([.\s\S]+?)张李副主任医师查房后指示";
                result = Regex.Match(record, pattern).ToString().Trim();
            }
            Match match = Regex.Match(result, @"[；.。;,，]$");

            result = Regex.Replace(result, @"\d{1}.辅助检查", "");
            result = Regex.Replace(result, @"三、主要诊断", "");
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

        /// <summary>
        /// 获取所有辅助检查结果
        /// </summary>
        /// <param name="allRecord"></param>
        /// <returns></returns>
        public string GetPyhsicExamReuslt(string allRecord)
        {
            Regex reg = new Regex(@"辅助检查[:|：](.+)");
            Regex regReplace = new Regex("辅助检查[:|：]");
            MatchCollection match = reg.Matches(allRecord);
            string result = "";
            for (int i = 0; i < match.Count; i++)
            {
                if (match[i].Value.Contains("无补充")|| match[i].Value.Contains("暂缺"))
                {
                    continue;
                }
                result += Regex.Replace(match[i].Value, @"\r?\n|\r", "");
            }
            return regReplace.Replace(result, "");
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
        private string GetcyDiagnose(string allRecord, string upperUpperRecord)
        {
            string result;
            result = SearchFixedDiagnose();

            string[] patterns = {
            @"三、主要诊断：([.\s\S]+?)四、鉴别诊断",
            @"主要诊断：([.\s\S]+?)诊断依据",
            @"目前诊断：([.\s\S]+?)诊断依据"
        };
            foreach (var pattern in patterns)
            {
                var match = Regex.Match(upperUpperRecord, pattern);
                if (!string.IsNullOrEmpty(match.Value))
                {
                    result = match.Groups[1].Value.Trim();
                    break;
                }
            }

            //if (result == null || result == "")
            //{
            //    result = Regex.Match(upperUpperRecord, @"三、主要诊断：([.\s\S]+?)四、鉴别诊断").ToString().Trim();
            //}

            //if (result == null || result == "")
            //{
            //    result = Regex.Match(upperUpperRecord, @"主要诊断：([.\s\S]+?)诊断依据").ToString().Trim();

            //}
            //if (result == null || result == "")
            //{
            //    result = Regex.Match(upperUpperRecord, @"目前诊断：([.\s\S]+?)诊断依据").ToString().Trim();

            //}

            //result = Regex.Replace(result, @"修正诊断：|修正诊断:|患者目前诊断：|诊断依据|主要诊断：|四、鉴别诊断|\n|\t|\r", "");

            //result = SearchaddDiagnose(result);

            if (!string.IsNullOrEmpty(result))
            {
                const string replacementPattern = @"修正诊断：|修正诊断:|患者目前诊断：|诊断依据|主要诊断：|四、鉴别诊断|\n|\t|\r";
                result = Regex.Replace(result, replacementPattern, "");
                result = SearchaddDiagnose(result);
            }

            return result;

            // 这个函数从所有的记录中匹配修正诊断作为结果返回
            string SearchFixedDiagnose()
            {
                MatchCollection fixedDiagnose = Regex.Matches(allRecord, @"修正诊断[：:]([.\s\S]+?)\n"); // ToString().Trim();
                return fixedDiagnose.Count == 0 ? null : fixedDiagnose[fixedDiagnose.Count - 1].ToString().Trim();
            }

            
            // 这个函数从所有记录中获取补充诊断并添加序号
            string SearchaddDiagnose(string currentDiagnose)
                {
                MatchCollection addedDiagnose = Regex.Matches(allRecord, @"补充诊断[：:]([.\s\S]+?)\n"); // ToString().Trim();
                if (addedDiagnose.Count == 0)
                {
                    return currentDiagnose;
                }
                string addedDiagString = "";
                int diagnoseCount = 1;
                foreach (Match match in addedDiagnose)
                {
                    string cleanedDiagnose = match.Groups[1].Value.Replace("补充诊断:", "").Replace("补充诊断：", "").Trim();                                                                                    // 检查是否包含序号（简单判断：是否以数字加点开头）
                    bool containsNumberedItems = Regex.IsMatch(cleanedDiagnose, @"^\d+\.");
                    if (!containsNumberedItems)
                    {
                        // 如果没有序号，则手动添加一个序号
                        cleanedDiagnose = $"{diagnoseCount++}.{cleanedDiagnose}";
                    }
                    addedDiagString = addedDiagString + " " + cleanedDiagnose; // 提取并修剪空白字符    
                }
                string input = currentDiagnose + addedDiagString;


                // 匹配数字和小数点后的内容
                string pattern = @"(\d+)\.(.+?)(?=\s*\d+\.|$)";
                Regex regex = new Regex(pattern);
           
                List<string> resultList = new List<string>();
                foreach (Match match in regex.Matches(input))
                {
                    // 获取匹配组中的第二个部分
                    string newDiagnose = match.Groups[2].Value.Trim();
                    if (resultList.Contains(newDiagnose)) // 去除重复的元素
                    {
                        continue;
                    }
                    else
                    {
                        resultList.Add(newDiagnose);
                    }
                }

                string newString = "";

                for (int i = 0; i < resultList.Count; i++)
                {
                    // 使用字符串插值来格式化编号和元素
                    string formattedItem = $"{i + 1}.{resultList[i]}";

                    // 如果是第一个元素，直接赋值给newString
                    // 否则，使用两个空格连接新的元素和已有的newString
                    if (i == 0)
                    {
                        newString = formattedItem;
                    }
                    else
                    {
                        newString += "  " + formattedItem;
                    }
                }


                return newString;
            }
        }

        public string HealthEducation(string diagnose)
        {
            var healthEducation = new HealthEducation();

            if (diagnose.Contains("慢性阻塞性")&& diagnose.Contains("高血压"))
            {
                return healthEducation.HEAD +  healthEducation.COPD +healthEducation.HBPBASE + healthEducation.BASE + healthEducation.END;
            }
            if (diagnose.Contains("慢性阻塞性") && diagnose.Contains("糖尿病"))
            {
                return healthEducation.HEAD + healthEducation.COPD + healthEducation.DMBASE + healthEducation.BASE + healthEducation.END;
            }
            if (diagnose.Contains("慢性阻塞性") && diagnose.Contains("冠状动脉"))
            {
                return healthEducation.HEAD + healthEducation.COPD + healthEducation.UABASE + healthEducation.BASE + healthEducation.END;
            }
            if (diagnose.Contains("糖尿病"))
            {
                return healthEducation.HEAD + healthEducation.DMBASE + healthEducation.BASE + healthEducation.END;
            }
            if (diagnose.Contains("高血压"))
            {
                return healthEducation.HEAD + healthEducation.HBPBASE + healthEducation.BASE + healthEducation.END;
            }
            if (diagnose.Contains("慢性阻塞性"))
            {
                return healthEducation.HEAD + healthEducation.COPD + healthEducation.END;
            }
            if (diagnose.Contains("冠状动脉粥样"))
            {
                return healthEducation.HEAD + healthEducation.UABASE + healthEducation.END;
            }
            if (diagnose.Contains("脑梗死") || diagnose.Contains("脑出血"))
            {
                return healthEducation.HEAD + healthEducation.APOPLEXY + healthEducation.END;
            }
            return healthEducation.HEAD + healthEducation.OTHER + healthEducation.END;
        }


        public string Start(string allRecord)
        {
            this.GetFirstResult(allRecord);
            this.upperUpperRecord = this.GetUpperUpperRecord(allRecord);
            this.ryResult = this.GetryResult(firstRecord);
            this.ryBodyExam = this.GetryBodyExam(firstRecord);
            this.cureFunciton = this.GetCureFunction(firstRecord);
            this.pyhsicExam = this.GetPyhsicExamReuslt(allRecord);
            this.ryDiagnose = this.GetryDiagnose(firstRecord);
            this.cyDiagnose = this.GetcyDiagnose(allRecord, upperUpperRecord);

            this.healEducation = this.HealthEducation(cyDiagnose);
            string outText = $@"    入院情况: 患者{ryResult}入院。入院查体{ryBodyExam}
    初步诊断: {ryDiagnose}
    诊疗经过: {cureFunciton}
    住院期间相关辅助检查: {pyhsicExam}
    出院诊断: {cyDiagnose}
    出院情况: {lastRecord}
    出院医嘱:
    1.{healEducation}
    2.离院后继续用药:

    3.门诊随访,按时服药,勿自行调整药物剂量,如有不适，立即就诊。
      科室电话: 023 - 48623017，全科医学科门诊：2楼3号诊室
";

            return outText;
        }


    }
    public class HealthEducation {
        public string HEAD = "健教处方:";
        public  string END = "注意预防电信诈骗。";
        public  string HBPBASE = "注意低盐饮食（每日在5克左右）；少吃高胆固醇、高甜、高脂肪食物；多食高蛋白、豆类、新鲜蔬菜瓜果等；";
        public  string DMBASE = "低盐低脂糖尿病饮食，少吃高胆固醇、高甜、高脂肪食物；多食高蛋白、豆类、新鲜蔬菜瓜果等；";
        public  string BASE = "不宜过度紧张、劳累及情绪激动；不吸烟，不饮酒、咖啡或浓茶；适量参加锻炼，不吸烟，不饮酒、咖啡或浓茶；";
        public  string UABASE = "调节饮食，低盐、低脂、低糖、多纤维素，戒烟戒酒忌辛辣；保持适当体力活动，但不致发生疼痛症状为度；一般不需卧床休息;控制血压、血糖、血脂，预防并发症；";
        public string APOPLEXY = "做到有效预防最好遵守三大纪律、八项注意。三大纪律：第一，生活规律化。第二，饮食科学化。第三，文体活动经常化。八项注意：第一，保持血压正常。第二，保持正常体重。第三，保持正常血脂。第四，饮食平衡。第五，戒烟、控酒、减盐。第六，坚持适度体育锻炼。第七，讲求精神心理卫生。第八，保持良好心态，树立自我保护意识。" ;
        public string COPD = "避免吸烟和接触有害空气，注意保暖，预防感冒，坚持呼吸锻炼和适量运动；腹式呼吸具体方法：吸气时尽量使腹部隆起，呼气时尽力呼出使腹部凹下；每天锻炼2～3次，每次10～20分钟；";
        public string OTHER = "调节饮食，低盐、低脂、低糖、多纤维素，戒烟戒酒忌辛辣；";
    }


}
