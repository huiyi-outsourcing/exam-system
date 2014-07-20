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

using log4net;
using log4net.Config;

namespace ExamSystem.controls {
    /// <summary>
    /// LoginControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoginControl : UserControl {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginControl));

        public LoginControl() {
            InitializeComponent();
            log.Info("Enter LoginControl..");
        }

        private void login_Click(object sender, RoutedEventArgs e) {

        }

        private void register_Click(object sender, RoutedEventArgs e) {
            HelperWindow window = Window.GetWindow(this) as HelperWindow;
            window.setBody(new controls.RegisterControl());
        }

        private void exit_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("您确定要退出本程序吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                Window.GetWindow(this).Close();
            }
        }
    }
}
