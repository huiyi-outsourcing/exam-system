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

using ExamSystem.entities;
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
            log.Info("enter logincontrol");
        }

        private void login_Click(object sender, RoutedEventArgs e) {
            String occupation;
            if (rb_doctor.IsChecked == true) {
                occupation = "军医";
            } else {
                occupation = "护士";
            }
            User user = authenticate(tb_name.Text.Trim(), tb_securitycode.Text.Trim(), occupation);
            if (user != null) {
                String category = "";
                if (rb_ssz.IsChecked == true)
                    category = "手术室(组)";
                if (rb_zsz.IsChecked == true)
                    category = "抗休克室(组)";
                if (rb_qsz.IsChecked == true)
                    category = "伤病员室(组)";
                if (rb_hwqz.IsChecked == true)
                    category = "核武器伤伤病员(组)";
                if (rb_flz.IsChecked == true)
                    category = "分类组";
                if (rb_hxwqz.IsChecked == true)
                    category = "化学武器伤伤病员室(组)";

                MainWindow main = new MainWindow();
                main.User = user;
                main.Category = category;
                main.setBody(new MainControl(user, category));
                main.Show();
                Window parent = Window.GetWindow(this);
                parent.Close();
            } else {
                MessageBox.Show("姓名或保障卡号错误");
            }
        }

        private User authenticate(String name, String securitycode, String occupation) {
            IList<User> user = PersistenceHelper.RetrieveByProperty<User>("SecurityCode", tb_securitycode.Text.Trim());
            if (user.Count == 0) {
                return null;
            }

            if (user[0].Name.Equals(name) && user[0].Occupation.Description.Equals(occupation)) {
                return user[0];
            } else {
                return null;
            }
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
