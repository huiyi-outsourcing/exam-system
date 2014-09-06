using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ExamSystem.entities;
using ExamSystem.utils.exam2;

namespace ExamSystem.controls.examresult {
    /// <summary>
    /// QuestionResultControl.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionResultControl : UserControl {
        private Question question;

        public QuestionResultControl(Question q, int index) {
            InitializeComponent();
            question = q;
            initLayout(index);
        }

        private void initLayout(int index) {
            tb_index.Text = "第" + (index + 1) + "题";
            tb_description.Text = question.Description;
            tb_manifestation.Text = question.Manifestation;

            for (int i = 0; i < question.Options.Count; ++i) {
                TextBlock tb = new TextBlock() { Text = question.Options[i].Description, TextWrapping = TextWrapping.Wrap, Foreground = Brushes.Black, FontSize = 25, Margin = new Thickness(0, 0, 0, 20) };
                sp_options.Children.Add(tb);
            }

            String selected = "您选择的答案为： ";
            for (int i = 0; i < question.SelectedOptions.Count; ++i) { 
                char tmp = (char)(question.SelectedOptions.ElementAt(i) + 'A');
                selected += tmp;
            }

            tb_selected.Text = selected;

            String correct = "正确答案为：";
            for (int i = 0; i < question.Options.Count; ++i) {
                if (question.Options[i].Correct) {
                    char tmp = (char)(i + 'A');
                    correct += tmp;
                }
            }

            tb_correct.Text = correct;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
