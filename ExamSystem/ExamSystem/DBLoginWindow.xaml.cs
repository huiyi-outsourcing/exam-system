using ExamSystem.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
    /// DBLoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DBLoginWindow : Window {
        public DBLoginWindow() {
            InitializeComponent();
            String conn = ConfigurationManager.AppSettings["conn"];
            int begin = conn.IndexOf('=');
            int end = conn.IndexOf(';');
            String ip = conn.Substring(begin + 1, end - begin - 1);
            tb_ip.Text = ip;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Mouse.SetCursor(Cursors.Wait);
            String setting = ConfigurationManager.AppSettings["conn"];
            int begin = setting.IndexOf('=');
            int end = setting.IndexOf(';');
            String newip = setting.Replace(setting.Substring(begin + 1, end - begin - 1), tb_ip.Text.Trim());
            ConfigurationManager.AppSettings["conn"] = newip;

            try {
                PersistenceHelper.OpenSession();
            } catch (Exception) {
                Mouse.SetCursor(Cursors.Arrow);
                MessageBox.Show("无法连接数据库，请检查网络连接或联系管理员。");
                return;
            }

            Mouse.SetCursor(Cursors.Arrow);
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
            appSettings.Settings.Remove("conn");
            appSettings.Settings.Add("conn", newip);
            config.Save();

            HelperWindow window = new HelperWindow();
            window.setBody(new controls.LoginControl());
            window.Show();

            this.Close();
        }
    }
}
