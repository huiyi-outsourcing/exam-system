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

using ExamSystem.utils.exam2;

namespace ExamSystem.controls {
    /// <summary>
    /// QuestionControl.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionControl : UserControl {
        private Question question;
        private bool flag;

        public QuestionControl(Question q, int index) {
            InitializeComponent();
            question = q;
            initLayout(index);
        }

        private void initLayout(int index) {
            tb_index.Text = "第" + (index + 1) + "题";
            tb_description.Text = question.Description;
            tb_manifestation.Text = question.Manifestation;

            for (int i = 0; i < question.Options.Count; ++i) {
                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(10),  };
                TextBlock tb = new TextBlock() { Text = question.Options[i].Description, TextWrapping = TextWrapping.Wrap, Foreground = Brushes.Black };

                item.Content = tb;
                
                if (question.SelectedOptions.Contains(i)) {
                    flag = true;
                    item.IsSelected = true;
                }
                lb_options.Items.Add(item);
            }

            flag = false;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void lb_options_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (flag) {
                flag = false;
                return;
            }

            question.SelectedOptions.Clear();
            foreach (object option in lb_options.SelectedItems) {
                int idx = lb_options.Items.IndexOf(option);
                question.SelectedOptions.Add(idx);
            }
        }
    }
}
