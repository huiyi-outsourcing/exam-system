using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// AuthControl.xaml 的交互逻辑
    /// </summary>
    public partial class AuthControl : UserControl {
        public AuthControl() {
            InitializeComponent();

            machine_code.Text = utils.AuthHelper.getMachineID();
        }

        private void exit_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("您确定要退出本程序吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                Window.GetWindow(this).Close();
            }
        }

        private void authorize_Click(object sender, RoutedEventArgs e) {
            String authcode = auth_code.Text;
            String id = machine_code.Text.ToString();

            if (utils.AuthHelper.authorize(id, authcode)) {
                writeRegistry(authcode);
                HelperWindow window = Window.GetWindow(this) as HelperWindow;
                window.setBody(new controls.LoginControl());
            } else {
                MessageBox.Show("错误的验证码");
            }
        }

        private void writeRegistry(String authcode) {
            RegistrySecurity rs = new RegistrySecurity();

            // Allow the current user to read and delete the key. 
            //
            string user = Environment.UserDomainName + "\\" + Environment.UserName;

            rs.AddAccessRule(new RegistryAccessRule(user,
            RegistryRights.ReadKey | RegistryRights.Delete,
            InheritanceFlags.None,
            PropagationFlags.None,
            AccessControlType.Allow));

            Registry.LocalMachine.CreateSubKey("SOFTWARE\\ExamSystem", RegistryKeyPermissionCheck.ReadWriteSubTree);

            RegistryKey SimuTraining = Registry.LocalMachine.CreateSubKey("SOFTWARE\\ExamSystem");
            SimuTraining.SetValue("authcode", authcode);
            SimuTraining.Close();
        }
    }
}
