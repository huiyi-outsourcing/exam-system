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
    /// RegisterControl.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterControl : UserControl {
        public RegisterControl() {
            InitializeComponent();
        }

        private void register_Click(object sender, RoutedEventArgs e) {

        }

        private void return_Click(object sender, RoutedEventArgs e) {
            HelperWindow window = Window.GetWindow(this) as HelperWindow;
            window.setBody(new controls.LoginControl());
        }

        private void exit_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("您确定要退出本程序吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                Window.GetWindow(this).Close();
            }
        }
    }
}
