using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MytoolMiniWPF.customControls
{
    /// <summary>
    /// LabelTextLeft.xaml 的交互逻辑
    /// </summary>
    public partial class LabelTextLeft : UserControl
    {
        public LabelTextLeft()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string LeftText
        {
            set => SetValue(leftTextProperty, value);
            get => (string)GetValue(leftTextProperty);
        }
        public string Value
        {
            set => SetValue(textValue, value);
            get => (string)GetValue(textValue);
        }

        public static readonly DependencyProperty leftTextProperty =
            DependencyProperty.Register("left_text", typeof(string), typeof(LabelText), new PropertyMetadata(null));
        public static readonly DependencyProperty textValue =
                DependencyProperty.Register("value", typeof(string), typeof(LabelText), new PropertyMetadata(null));

    }
}
