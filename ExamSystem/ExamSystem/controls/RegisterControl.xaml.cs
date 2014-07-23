using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ExamSystem.utils;

namespace ExamSystem.controls {
    /// <summary>
    /// RegisterControl.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterControl : UserControl {
        public RegisterControl() {
            InitializeComponent();
        }

        private void register_Click(object sender, RoutedEventArgs e) {
            // validate input
            String message = validate();
            if (message != null) {
                MessageBox.Show(message);
                return;
            }
            
            // register
            String username = tb_username.Text.Trim();
            String password = pb_password.Password;
            String name = tb_name.Text.Trim();

            User user = new User();
            user.Name = name;
            user.Username = username;
            user.Password = EncryptHelper.encrypt(password);
            Occupation occupation = new Occupation();
            if ((bool)rb_doctor.IsChecked) {
                occupation.Id = 1;
                occupation.Description = "医生";
            } else {
                occupation.Id = 2;
                occupation.Description = "护士";
            }
            user.Occupation = occupation;

            PersistenceHelper.Save<User>(user);

            MessageBox.Show("注册成功");
            HelperWindow window = Window.GetWindow(this) as HelperWindow;
            window.setBody(new LoginControl());
        }

        private String validate() {
            // validate input
            String username = tb_username.Text.Trim();
            if (username.Length < 4 || username.Length > 10)
                return "用户名应在4-10个字符之间\n";

            if (username.Contains('\'')
                || username.Contains('#')
                || username.Contains('*')
                || username.Contains(' ')
                || username.Contains('_')) {
                return "用户名中不应包含', #, *, _, 空格等特殊字符";
            }

            String password = pb_password.Password;
            String confirm = pb_confirm.Password;
            if (!password.Equals(confirm)) {
                return "两次输入密码不一致";
            }

            if (password.Contains('\'')
                || password.Contains('#')
                || password.Contains('*')
                || password.Contains(' ')
                || password.Contains('_')) {
                return "密码中不应包含', #, *, _, 空格等特殊字符";
            }

            // check if username is used
            IList<User> userList = PersistenceHelper.RetrieveByProperty<User>("Username", username);
            if (userList.Count != 0) {
                return "该用户名已被注册，请输入其他用户名。";
            }

            return null;
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
