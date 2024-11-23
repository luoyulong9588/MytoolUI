using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MytoolUI.common
{

    
    internal class FontText
    {
        private static System.Drawing.Text.PrivateFontCollection pfcc;

        public static System.Drawing.Text.PrivateFontCollection PFCC
        {
            get {
                return pfcc ?? LoadFont();
            }
            set {
                pfcc = value;
            }
        }
        public static bool JzFont { get; private set; } = false;
        public static System.Drawing.Text.PrivateFontCollection LoadFont()
        {
            if (!JzFont)
            {
                pfcc = new System.Drawing.Text.PrivateFontCollection();
                pfcc.AddFontFile(Environment.CurrentDirectory + "/resources/Fonts/iconfont.ttf");
                JzFont = true;
            }
            return pfcc;
        }

    }
}
