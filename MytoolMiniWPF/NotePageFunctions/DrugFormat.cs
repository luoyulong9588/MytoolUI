using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions.Configuration;

namespace MytoolMiniWPF.common
{
    internal class DrugFormat
    {
        private string pattern = @"([\u4e00-\u9fa5]+.+\))\s.+每次：(.+)；用法:([\u4e00-\u9fa5]+)，(.+)";
        private string patternNew = @"([\u4e00-\u9fa5]+.+\)?)\s.+每次：(.+)；用法:([\u4e00-\u9fa5]+)，(.+)";
        List<string> drugInfoList = new List<string>();
        public string Start(string drugInfo)
        {
            string str = Regex.Replace(drugInfo, "[（）]", m => m.Value == "（" ? "(" : ")");

            foreach (var item in str.Split('\n'))
            {
                string lineContet = Regex.Replace(item, @"^\(.*?\)", "");
                Match match = Regex.Match(lineContet, pattern);
                Match matchNew = Regex.Match(lineContet, patternNew);
                if (!match.Success&&!matchNew.Success)
                {
                    continue;
                }
                Match matchSuccess = match.Success ?  match: matchNew;
                string drugName = matchSuccess.Groups[1].Value.Trim();
                string drugDose = matchSuccess.Groups[2].Value.Trim();
                string drugUsage = matchSuccess.Groups[3].Value.Trim();
                string drugFreqency = matchSuccess.Groups[4].Value.Trim();
                string[] drug = { drugName, drugDose, drugUsage, drugFreqency };
                drugInfoList.Add(string.Join(" ", drug));
            }
        //       int maxLength = drug.Max(p => p.Length);
        //     string alignedInfo = string.Join("\t", drug.Select(p => p.PadRight(maxLength)));
     
            return Format();
        }
        public string Format()
        {
            string result="";
            int maxLengthName = 0;
            int maxLengthDose = 0;
            int maxLengthUsage = 0;


            string[] sentences = {
            "羧甲司坦口服溶液 10ml 口服 每天三次",
            "孟鲁司特钠片 10mg 口服 每天一次",
            "氯化钠注射液 5 ml 静脉注射 一次性",
            "注射用呋塞米 20mg 静脉注射 一次性"
        };

            // 获取每个部分的最大长度，用于对齐  
            int maxLength1 = drugInfoList.Max(s => s.Split(' ')[0].Length);
            int maxLength2 = drugInfoList.Max(s => s.Split(' ')[1].Length);
            int maxLength3 = drugInfoList.Max(s => s.Split(' ')[2].Length);
            int maxLength4 = drugInfoList.Max(s => s.Split(' ')[3].Length);
            int length = 0;

            // 对每个句子进行处理  
            foreach (var sentence in drugInfoList)
            {
                // 按空格分割句子，使用制表符对齐  
                string[] parts = sentence.Split(' ');
                string alignedSentence = $"    {parts[0].PadRight(maxLength1, ' ')}\t{parts[1].PadRight(maxLength2,' ')}\t{parts[2].PadRight(maxLength3,' ')}\t{parts[3].PadRight(maxLength4,' ')}";
                length = alignedSentence.Length>length? alignedSentence.Length : length;
                result =  result +  alignedSentence + "\n";
            }
            string line = new string('-', 65);
            result =  line + "\n" + result+ line;


            return result.Trim();
        }
    }
}
