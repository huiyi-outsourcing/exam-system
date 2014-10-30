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

namespace ExamSystem.windows {
    /// <summary>
    /// ContinueTrainingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ContinueTrainingWindow : Window {
        private ExamResultWindow window;

        public ContinueTrainingWindow(ExamResultWindow window) {
            InitializeComponent();
            this.window = window;
        }

        private void btn_confirm_Click_1(object sender, RoutedEventArgs e) {
            ReasonWindow reason = new ReasonWindow(window.Examresult.User, window.Examresult.Exam.Category);
            this.Close();
            reason.Show();
            window.Close();
        }

        private void btn_cancel_Click_1(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
