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
    /// ExamListControl.xaml 的交互逻辑
    /// </summary>
    public partial class ExamListControl : UserControl {
        #region Properties
        private User user = null;

        public User User {
            get { return user; }
            set { user = value; }
        }
        private String category = null;

        public String Category {
            get { return category; }
            set { category = value; }
        }
        private Exam exam = null;

        public Exam Exam {
            get { return exam; }
            set { exam = value; }
        }
        private SolidColorBrush DarkYellow;
        private SolidColorBrush Blue;
        #endregion

        public ExamListControl() {
            InitializeComponent();
        }

        public ExamListControl(User user, String category, String reason) {
            InitializeComponent();
            this.user = user;
            this.category = category;
            exam = new Exam(user, category, reason);
            Blue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#548DD3"));
            DarkYellow = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5")); 
            initLayout();
        }

        private void initLayout() {
            // init clinical case list
            for (int i = 0; i < exam.Questions.Count; ++i) {
                Question q = exam.Questions.ElementAt(i);
                Border border = new Border() { CornerRadius = new CornerRadius(5), Width = 60, Height = 60, Background = Blue };
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
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new QuestionControl(exam.Questions.ElementAt(index), index));
        }

        public void refresh() {
            foreach (ListBoxItem item in qlist.Items) {
                Border border = item.Content as Border;
                Question q = item.Tag as Question;

                if (q.SelectedOptions.Count > 0) {
                    border.Background = DarkYellow;
                } else {
                    border.Background = Blue;
                }
            }
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Window.GetWindow(this) as MainWindow;
            ReminderWindow window = new ReminderWindow(main);
            window.ShowDialog();
        }

        private void btn_return_Click(object sender, RoutedEventArgs e)
        {
            ReasonWindow reason = new ReasonWindow(user, category);
            reason.Show();
            Window.GetWindow(this).Close();
        }
    }
}
