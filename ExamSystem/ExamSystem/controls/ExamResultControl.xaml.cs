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
using ExamSystem.entities;
using System.Windows.Media.Effects;

namespace ExamSystem.controls {
    /// <summary>
    /// ExamResultControl.xaml 的交互逻辑
    /// </summary>
    public partial class ExamResultControl : UserControl {
        private User user = null;
        private Exam exam = null;

        public ExamResultControl() {
            InitializeComponent();
        }

        public ExamResultControl(User user, Exam exam) {
            MessageBox.Show("您本次训练的成绩为" + Math.Round(exam.GetScore(), 2) + "分，本次成绩已计入系统，方便上级查询！点确定按钮后，做错的题目题号按钮将变为红色，请点击相应题号后，会出现该题的正确答案，请比照正确答案进行纠错学习！");
            InitializeComponent();
            this.user = user;
            this.exam = exam;

            initLayout();
        }

        #region Layout
        private void initLayout() {
            // Calculate exam score
            CalculateResult();

            // init clinical case list with corresponding tag color
            // red: wrong
            // green: right
            for (int i = 0; i < exam.Questions.Count; ++i) {
                Question q = exam.Questions.ElementAt(i);
                Border border = new Border() { SnapsToDevicePixels = true, BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(4), CornerRadius = new CornerRadius(5), Width = 40, Height = 40 };
                if (q.IsCorrect()) {
                    border.Background = Brushes.Green;
                } else {
                    border.Background = Brushes.Red;
                }

                border.Effect = new DropShadowEffect() { Color = Colors.Black, BlurRadius = 16, ShadowDepth = 0, Opacity = 1 };

                TextBlock tb = new TextBlock() { Text = (i + 1).ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                border.Child = tb;
                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(10) };
                item.Content = border;
                item.Tag = q;

                qlist.Items.Add(item);
            }

            qlist.SelectedIndex = 0;
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

                if (q.SelectedOptions.Contains(i)) {
                    item.IsSelected = true;
                }
                item.IsEnabled = false;
                lb_options.Items.Add(item);
            }

            tb_number.Text = index + 1 + "";
            // set answers textblock
            tb_answers.Text = q.GetAnswers();
        }
        #endregion

        #region EventHandlers
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void qlist_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int index = qlist.SelectedIndex;
            refreshQuestion(index);
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
                MessageBox.Show("已到最后一题");
                return;
            }
            qlist.SelectedIndex = index + 1;
        }
        #endregion

        #region Helper methods
        private void CalculateResult() {
            double score = exam.GetScore();
            tb_score.Text = Math.Round(score, 2).ToString();

            ExamResult result = new ExamResult();
            result.Occupation = user.Occupation;
            result.Score = score;
            result.User = user;
            result.DateTime = DateTime.Now;
            utils.PersistenceHelper.Save<ExamResult>(result);
        }

        private void again_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("您确定要继续训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                MainWindow window = Window.GetWindow(this) as MainWindow;
                window.setBody(new ExamControl(user, exam.Category, exam.Reason));
            }
        }
        #endregion
    }
}
