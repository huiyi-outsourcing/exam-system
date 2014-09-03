using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;

namespace ExamResult {
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window {
        public LoginWindow() {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            String password = pb_password.Password;

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] source = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] target = provider.ComputeHash(source);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < target.Length; ++i) {
                sb.Append(target[i].ToString("x2"));
            }

            
            String auth = sb.ToString();
            if (auth.Equals(ConfigurationManager.AppSettings["admin"])) {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            } else {
                MessageBox.Show("用户名或密码错误");
            }
        }
    }
}
