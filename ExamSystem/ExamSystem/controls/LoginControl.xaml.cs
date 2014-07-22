using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

using ExamSystem.models;
using ExamSystem.utils;

using log4net;
using log4net.Config;
using NHibernate;

namespace ExamSystem.controls {
    /// <summary>
    /// LoginControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoginControl : UserControl {
        // logger
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginControl));

        public LoginControl() {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e) {
            String message = validateInput();
            if (message.Length != 0) {
                MessageBox.Show(message);
                return;
            }

            if (authenticate(tb_username.Text.Trim(), pb_password.Password)) {
                MainWindow main = new MainWindow();
                main.setBody(new MainControl());
                main.Show();
                Window parent = Window.GetWindow(this);
                parent.Close();
            } else {
                MessageBox.Show("用户名或密码错误");
            }
        }

        private bool authenticate(String username, String password) {
            IList<User> user = PersistenceHelper.RetrieveByProperty<User>("username", tb_username.Text);
            if (user.Count == 0) {
                return false;
            }

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] source = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] target = provider.ComputeHash(source);

            String MD5Password = Convert.ToBase64String(target);

            if (MD5Password.Equals(user[0].Password)) {
                return true;
            } else {
                return false;
            }
        }

        private String validateInput() {
            String username = tb_username.Text.Trim();
            String password = pb_password.Password.Trim();

            StringBuilder builder = new StringBuilder();
            if (username.Length < 4 || username.Length > 10) {
                builder.Append("用户名应在4-10个字符之间\n");
            }

            if (password.Contains('\'') || password.Contains('*') || password.Contains('_') || password.Contains('(') || password.Contains(')')) {
                builder.Append("密码不能包含以下特殊字符：', *, _, (, ), 空格");
            }

            return builder.ToString();
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

    
    public class NotNullValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string)) {
                return new ValidationResult(false, "不能为空!");
            }

            return new ValidationResult(true, null);
        }
    }
}
