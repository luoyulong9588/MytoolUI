using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// RegexToolWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegexToolWindow : Window
    {
        private bool isUpdating = false; // 标志位，用于防止递归
        public RegexToolWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TestInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatchResults();
        }

        private void RegexInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatchResults();
        }
        // 显示帮助弹窗
        private void ShowHelpPopup(object sender, RoutedEventArgs e)
        {
            HelpPopup.IsOpen = true;
        }
        private void UpdateMatchResults()
        {
            if (isUpdating) return; // 如果已经在更新，直接返回
            isUpdating = true;

            try
            {
                // 清空匹配结果列表
                MatchResultList.Items.Clear();

                // 获取用户输入
                string regexPattern = RegexInput.Text;
                string testText = new TextRange(TestRichTextBox.Document.ContentStart, TestRichTextBox.Document.ContentEnd).Text;

                if (string.IsNullOrWhiteSpace(regexPattern) || string.IsNullOrWhiteSpace(testText))
                    return;

                try
                {
                    // 使用正则表达式匹配
                    Regex regex = new Regex(regexPattern);
                    MatchCollection matches = regex.Matches(testText);

                    // 清除现有的高亮
                    ClearRichTextBoxHighlight();

                    // 遍历每个匹配结果并高亮
                    foreach (Match match in matches)
                    {
                        HighlightText(match.Index, match.Length);
                        MatchResultList.Items.Add($"匹配内容: '{match.Value}' 位置: {match.Index}-{match.Index + match.Length - 1}");
                    }

                    if (matches.Count == 0)
                    {
                        MatchResultList.Items.Add("未匹配到任何结果。");
                    }
                }
                catch (Exception ex)
                {
                    MatchResultList.Items.Add($"正则表达式错误: {ex.Message}");
                }
            }
            finally
            {
                isUpdating = false; // 确保标志位被重置
            }
        }

        // 高亮 RichTextBox 中的匹配文本
        private void HighlightText(int startIndex, int length)
        {
            TextPointer start = GetTextPointerAtOffset(TestRichTextBox.Document.ContentStart, startIndex);
            TextPointer end = GetTextPointerAtOffset(start, length);

            if (start != null && end != null)
            {
                TextRange range = new TextRange(start, end);
                range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
                range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            }
        }

        // 清除 RichTextBox 中的高亮
        private void ClearRichTextBoxHighlight()
        {
            TextRange range = new TextRange(TestRichTextBox.Document.ContentStart, TestRichTextBox.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.BackgroundProperty, null); // 清除背景
            range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black); // 恢复默认字体颜色
        }

        // 根据字符偏移量获取 TextPointer
        private TextPointer GetTextPointerAtOffset(TextPointer start, int offset)
        {
            TextPointer navigator = start;
            int currentIndex = 0;

            while (navigator != null)
            {
                if (navigator.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string text = navigator.GetTextInRun(LogicalDirection.Forward);
                    if (currentIndex + text.Length >= offset)
                    {
                        return navigator.GetPositionAtOffset(offset - currentIndex);
                    }

                    currentIndex += text.Length;
                }

                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }

            return null;
        }

        private void TestRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMatchResults();
        }

        private void showHelpBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowHelpPopup(sender, e);
        }
    }
}
