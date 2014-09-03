using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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

namespace ExamResult {
    /// <summary>
    /// ChangePasswordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePasswordWindow : Window {
        public ChangePasswordWindow() {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            // 确定按钮
            if (pb_password.Password.Equals(pb_confirm.Password)) {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
                AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
                appSettings.Settings.Remove("admin");
                appSettings.Settings.Add("admin", md5(pb_confirm.Password));
                config.Save();
                this.Close();
            } else {
                MessageBox.Show("两次输入密码不一致，请重新输入");
            }
        }

        private String md5(String original) {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] source = System.Text.Encoding.UTF8.GetBytes(original);
            byte[] target = provider.ComputeHash(source);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < target.Length; ++i) {
                sb.Append(target[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            // 取消按钮
            this.Close();
        }
    }
}
