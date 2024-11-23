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
    /// LabelInput.xaml 的交互逻辑
    /// </summary>
    public partial class LabelInput : UserControl
    {
        public LabelInput()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string Text
        {
            set => SetValue(labelTextProperty, value);
            get => (string)GetValue(labelTextProperty);
        }

        public string Value
        {
            set => SetValue(textValue, value);
            get => (string)GetValue(textValue);
        }

        public static readonly DependencyProperty labelTextProperty =
            DependencyProperty.Register("labeltext", typeof(string), typeof(LabelInput), new PropertyMetadata(null));
       
        public static readonly DependencyProperty textValue =
                DependencyProperty.Register("value", typeof(string), typeof(LabelInput), new PropertyMetadata(null));
    }
}
