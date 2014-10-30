using ExamSystem.entities;
using ExamSystem.utils.exam2;
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

namespace ExamSystem.controls {
    /// <summary>
    /// ExamResultMainControl.xaml 的交互逻辑
    /// </summary>
    public partial class ExamResultMainControl : UserControl {
        private User user = null;
        private Exam exam = null;

        public ExamResultMainControl(User user, Exam exam) {
            InitializeComponent();
            this.user = user;
            this.exam = exam;

            double score = exam.GetScore();
            tb_score.Text = Math.Round(score, 2).ToString();

            ExamResult result = new ExamResult();
            result.Occupation = user.Occupation;
            result.Score = Math.Round(score, 2);
            result.User = user;
            result.DateTime = DateTime.Now;
            utils.PersistenceHelper.Save<ExamResult>(result);
        }

        private void specific_result(object sender, MouseButtonEventArgs e) {
            
        }

        private void continue_training(object sender, MouseButtonEventArgs e) {
            if (MessageBox.Show("您确定要继续训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                ReasonWindow window = new ReasonWindow(user, exam.Category);
                Window.GetWindow(this).Close();
                window.Show();
            }
        }

        private void exit_training(object sender, MouseButtonEventArgs e) {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            ExamResultWindow window = new ExamResultWindow(new controls.examresult.ExamResultListControl(user, exam));
            window.Show();
            Application.Current.MainWindow = window;
            Window.GetWindow(this).Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            windows.ExitTrainingWindow window = new windows.ExitTrainingWindow(Window.GetWindow(this));
            window.ShowDialog();
            //if (MessageBox.Show("您确定要结束本次训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
            //    Window.GetWindow(this).Close();
            //}
        }

    }
}
