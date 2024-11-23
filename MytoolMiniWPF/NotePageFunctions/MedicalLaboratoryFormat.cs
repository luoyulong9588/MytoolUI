using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MytoolMiniWPF.common
{
    internal class MedicalLaboratoryFormat
    {
        // private string _orignText;

        //   private string _formattedText;
        public string OrignText { get; set; }
        public string FormattedText { get; set; }
        public MedicalLaboratoryFormat()
        {

        }
        public string Start(string originText)
        {
            this.OrignText = originText;

            this.OrignText = Regex.Replace(this.OrignText, @"\*(?!\d)", "").Trim();
            //2.移除末尾型号；
            this.OrignText = Regex.Replace(this.OrignText, @"\*$", "");

            //3.替换*为×
            this.OrignText = Regex.Replace(this.OrignText, @"(?<=\d)\*(?=\d)", "×");

            //移除检验中的英文标识符；
            this.OrignText = Regex.Replace(this.OrignText, @"[^\u4e00-\u9fa5]+[\:\：]", ":");

            // 在中英文之间添加标点符号
            this.OrignText = Regex.Replace(this.OrignText, @"([a-zA-Z])([\u4e00-\u9fa5])(?!值)", "$1、$2");

            //特殊情况，ph值中间会被添加、
            this.FormattedText = this.OrignText.Replace("ph、值", "ph值").Replace("C、反应","C反应").Replace("D、二聚","D-二聚").Replace("T、波", "T波");
            return FormattedText;
        }
    }
        
}
