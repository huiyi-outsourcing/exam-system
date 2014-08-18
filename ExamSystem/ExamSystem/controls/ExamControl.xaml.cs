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
using System.Windows.Media.Effects;

namespace ExamSystem.controls {
    /// <summary>
    /// ExamControl.xaml 的交互逻辑
    /// </summary>
    public partial class ExamControl : UserControl {
        #region Properties
        private User user = null;
        private Exam exam = null;
        private bool flag = false;
        #endregion

        public ExamControl() {
            InitializeComponent();
        }

        public ExamControl(User user, String category, String reason) {
            InitializeComponent();
            this.user = user;
            exam = new Exam(user, category, reason);
            initLayout();
        }

        private void initLayout() { 
            // init clinical case list
            for (int i = 0; i < exam.Questions.Count; ++i) {
                Question q = exam.Questions.ElementAt(i);
                Border border = new Border() { SnapsToDevicePixels = true, BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(4), CornerRadius = new CornerRadius(5), Width = 40, Height = 40 };
                border.Effect = new DropShadowEffect() { Color = Colors.Black, BlurRadius = 16, ShadowDepth = 0, Opacity = 1 };

                TextBlock tb = new TextBlock() { Text = (i + 1).ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                border.Child = tb;
                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(10) };
                item.Content = border;
                item.Tag = q;

                qlist.Items.Add(item);
            }

            qlist.SelectedIndex = 0;

            // init content
            refreshQuestion(0);
        }

        private void refreshQuestion(int index) {
            Question q = exam.Questions[index];
            tb_description.Text = q.Description;
            tb_manifestation.Text = q.Manifestation;
            lb_options.Items.Clear();

            for (int i = 0; i < q.Options.Count; ++i) {
                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(10) };
                TextBlock tb = new TextBlock() { Text = q.Options[i].Description, TextWrapping = TextWrapping.Wrap };

                item.Content = tb;
                item.MouseDoubleClick += new MouseButtonEventHandler(next_question);

                if (q.SelectedOptions.Contains(i)) {
                    flag = true;
                    item.IsSelected = true;
                }
                lb_options.Items.Add(item);
            }

            //foreach (int idx in q.SelectedOptions) {
            //    lb_options.SelectedItems.Add(lb_options.Items[idx]);
            //}

            drawColor();
            flag = false;
        }

        private void drawColor() {
            foreach (ListBoxItem item in qlist.Items) {
                Border border = item.Content as Border;
                Question q = item.Tag as Question;

                if (q.SelectedOptions.Count > 0) {
                    border.Background = Brushes.Green;
                } else {
                    border.Background = Brushes.Transparent;
                }
            }
        }

        #region EventHandlers
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void qlist_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int index = qlist.SelectedIndex;
            flag = true;
            refreshQuestion(index);
        }

        private void lb_options_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (flag) {
                flag = false;
                return;
            }

            int index = qlist.SelectedIndex;
            ListBoxItem item = qlist.Items[index] as ListBoxItem;
            Question q = item.Tag as Question;

            q.SelectedOptions.Clear();

            foreach (object option in lb_options.SelectedItems) {
                int idx = lb_options.Items.IndexOf(option);
                q.SelectedOptions.Add(idx);
            }
        }

        private void lb_options_Selected(object sender, RoutedEventArgs e) {
            int index = qlist.SelectedIndex;
            ListBoxItem item = qlist.Items[index] as ListBoxItem;
            Question q = item.Tag as Question;

            if (!q.SelectedOptions.Contains(lb_options.SelectedIndex)) {
                q.SelectedOptions.Add(lb_options.SelectedIndex);
            } else {
                q.SelectedOptions.Remove(lb_options.SelectedIndex);
            }
        }

        private void prev_question(object sender, RoutedEventArgs e) {
            int index = qlist.SelectedIndex;
            if (index == 0) {
                MessageBox.Show("已经是第一题");
                return;
            }

            qlist.SelectedIndex = index - 1;
        }

        private void next_question(object sender, RoutedEventArgs e) {
            int index = qlist.SelectedIndex;
            if (index == qlist.Items.Count - 1) {
                if (MessageBox.Show("已到最后一题，是否提交试卷？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                    //showScore();
                }
            }
            qlist.SelectedIndex = index + 1;
        }

        private void submit_exam(object sender, RoutedEventArgs e) {
            int count = 0;
            foreach (Question q in exam.Questions) {
                if (q.SelectedOptions.Count > 0)
                    count++;
            }

            if (count != exam.Questions.Count) {
                if (MessageBox.Show("还有题目没有完成，确认提交？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                    ShowScore();
                    (gd_status.Children[0] as CountDownControl).stopTimer();
                }
            } else {
                ShowScore();
                (gd_status.Children[0] as CountDownControl).stopTimer();
            }
        }
        #endregion

        #region Methods
        private void ShowScore() {
            (Window.GetWindow(this) as MainWindow).setBody(new ExamResultControl(user, exam));
        }

        public void SubmitExam() {
            MessageBox.Show("考试时间到，自动提交试卷");
            ShowScore();
        }
        #endregion
    }
}
