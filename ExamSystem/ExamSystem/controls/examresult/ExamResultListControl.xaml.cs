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

namespace ExamSystem.controls.examresult {
    /// <summary>
    /// ExamResultListControl.xaml 的交互逻辑
    /// </summary>
    public partial class ExamResultListControl : UserControl {
        private User user = null;
        private bool[] visited;
        SolidColorBrush Grey = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BDBDBD"));
        SolidColorBrush Red = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));

        public User User {
            get { return user; }
            set { user = value; }
        }
        private Exam exam = null;

        public Exam Exam {
            get { return exam; }
            set { exam = value; }
        }

        public ExamResultListControl(User user, Exam exam) {
            InitializeComponent();
            this.user = user;
            this.exam = exam;
            visited = new bool[exam.Questions.Count];
            for (int i = 0; i < visited.Length; ++i)
                visited[i] = false;

            initLayout();
        }

        private void initLayout() {
            // init clinical case list
            for (int i = 0; i < exam.Questions.Count; ++i) {
                Question q = exam.Questions.ElementAt(i);
                Border border = new Border() { CornerRadius = new CornerRadius(5), Width = 60, Height = 60, Background = q.IsCorrect() ? Brushes.LightGray : Red};
                border.Effect = new DropShadowEffect() { Color = Colors.Black, BlurRadius = 16, ShadowDepth = 0, Opacity = 1 };

                TextBlock tb = new TextBlock() { Text = (i + 1).ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Black };
                border.Child = tb;
                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(0, 2, 0, 2) };
                item.Content = border;
                item.Tag = q;

                item.MouseDoubleClick += new MouseButtonEventHandler(show_question);

                qlist.Items.Add(item);
            }

            qlist.SelectedIndex = 0;
        }

        private void show_question(object sender, RoutedEventArgs e) {
            int index = qlist.SelectedIndex;
            visited[index] = true;
            ExamResultWindow window = Window.GetWindow(this) as ExamResultWindow;
            window.setBody(new QuestionResultControl(exam.Questions.ElementAt(index), index));
            //window.toggleButton();
        }

        public void refresh() {
            for (int i = 0; i < qlist.Items.Count; ++i) {
                ListBoxItem item = qlist.Items[i] as ListBoxItem;
                Border border = item.Content as Border;
                Question q = item.Tag as Question;

                if (q.IsCorrect()) continue;
                if (visited[i]) {
                    border.Background = Brushes.LightGray;
                }
            }
        }

        private void btn_continue_Click(object sender, RoutedEventArgs e)
        {
            windows.ContinueTrainingWindow window = new windows.ContinueTrainingWindow(Window.GetWindow(this) as ExamResultWindow);
            window.ShowDialog();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            windows.ExitTrainingWindow window = new windows.ExitTrainingWindow(Window.GetWindow(this));
            window.ShowDialog();
        }
    }
}
