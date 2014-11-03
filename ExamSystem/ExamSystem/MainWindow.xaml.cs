using ExamSystem.entities;
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

using ExamSystem.controls;

namespace ExamSystem {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        #region Properties
        private ExamListControl examlist = null;

        public ExamListControl Examlist {
            get { return examlist; }
            set { examlist = value; }
        }
        #endregion

        #region Constructor
        public MainWindow() {
            InitializeComponent();
        }

        public MainWindow(ExamListControl examlist) {
            InitializeComponent();
            this.examlist = examlist;
            setBody(examlist);
        }
        #endregion

        #region Layout
        public void setBody(UserControl control) {
            body.Children.RemoveRange(0, body.Children.Count);
            body.Children.Add(control);
        }
        #endregion

        #region EventHandlers
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                if (MessageBox.Show("您确定要结束本次训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                    this.Close();
                }
            }
        }

        private void mainPage_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("您确定要结束本次训练吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                ReasonWindow reason = new ReasonWindow(examlist.User, examlist.Category);
                this.Close();
                reason.Show();
            }
        }

        private void submit_Click(object sender, RoutedEventArgs e) {
            ReminderWindow window = new ReminderWindow(this);
            window.ShowDialog();
        }
        #endregion

        public void SubmitExam()
        {
            ExamResultMainControl erc = new ExamResultMainControl(examlist.User, examlist.Exam);
            setBody(erc);
        }

        //private void confirm_Click(object sender, RoutedEventArgs e) {
        //    QuestionControl qc = body.Children[0] as QuestionControl;
        //    qc.Confirm();

        //    examlist.refresh();
        //    setBody(examlist);
        //    toggleButton();
        //}

        //public void toggleButton() {
        //    if (btn_confirm.Visibility == Visibility.Hidden) {
        //        btn_confirm.Visibility = Visibility.Visible;
                
        //        btn_submit.Visibility = Visibility.Hidden;
        //    } else {
        //        btn_confirm.Visibility = Visibility.Hidden;
        //        btn_submit.Visibility = Visibility.Visible;
        //    }
        //}
    }
}
