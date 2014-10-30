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
    /// ReminderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReminderWindow : Window {
        private MainWindow main;

        public ReminderWindow(MainWindow main) {
            InitializeComponent();
            this.main = main;
        }

        private void btn_confirm_Click_1(object sender, RoutedEventArgs e) {
            this.Close();
            main.SubmitExam();
        }

        private void btn_cancel_Click_1(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
