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
using System.Windows.Shapes;

namespace ExamSystem {
    /// <summary>
    /// ExamResultListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExamResultWindow : Window {
        private controls.examresult.ExamResultListControl examresult;

        public controls.examresult.ExamResultListControl Examresult {
            get { return examresult; }
            set { examresult = value; }
        }

        public ExamResultWindow(controls.examresult.ExamResultListControl examresult) {
            InitializeComponent();
            this.examresult = examresult;
            setBody(examresult);
        }

        public void setBody(UserControl control) {
            body.Children.RemoveRange(0, body.Children.Count);
            body.Children.Add(control);
        }

        //public void toggleButton() {
        //    if (btn_confirm.Visibility == Visibility.Hidden) {
        //        btn_confirm.Visibility = Visibility.Visible;
        //        //btn_main.Visibility = Visibility.Hidden;
        //        btn_continue.Visibility = Visibility.Hidden;
        //        btn_exit.Visibility = Visibility.Hidden;
        //    } else {
        //        btn_confirm.Visibility = Visibility.Hidden;
        //        //btn_main.Visibility = Visibility.Visible;
        //        btn_continue.Visibility = Visibility.Visible;
        //        btn_exit.Visibility = Visibility.Visible;
        //    }
        //}

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                if (MessageBox.Show("您确定要退出吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                    this.Close();
                }
            }
        }

        private void btn_main_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("您确定要结束本次训练并返回首页吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                ReasonWindow reason = new ReasonWindow(examresult.User, examresult.Exam.Category);
                this.Close();
                reason.Show();
            }
        }

        private void btn_continue_Click(object sender, RoutedEventArgs e) {
            windows.ContinueTrainingWindow window = new windows.ContinueTrainingWindow(this);
            window.ShowDialog();

            //if (MessageBox.Show("您确定要继续训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
            //    ReasonWindow reason = new ReasonWindow(examresult.User, examresult.Exam.Category);
            //    this.Close();
            //    reason.Show();
            //}
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e) {
            windows.ExitTrainingWindow window = new windows.ExitTrainingWindow(this);
            window.ShowDialog();
            //if (MessageBox.Show("您确定要结束本次训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
            //    this.Close();
            //}
        }

        //private void btn_confirm_Click(object sender, RoutedEventArgs e) {
        //    setBody(examresult);
        //    toggleButton();
        //    examresult.refresh();
        //}
    }
}
