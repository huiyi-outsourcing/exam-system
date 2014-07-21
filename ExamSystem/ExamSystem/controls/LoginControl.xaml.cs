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

using ExamSystem.models;

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

        // view model for data context
        private viewmodel.LoginViewModel viewModel = null;

        public LoginControl() {
            InitializeComponent();

            this.viewModel = new viewmodel.LoginViewModel();
            this.DataContext = this.viewModel;
        }

        private void login_Click(object sender, RoutedEventArgs e) {
            System.Collections.IList siteList;
            ISessionFactory factory =
            new NHibernate.Cfg.Configuration().Configure("conf/hibernate.cfg.xml").BuildSessionFactory(); 

            using (ISession session = factory.OpenSession()) 
            { 
                ICriteria sc = session.CreateCriteria(typeof(User)); 
                siteList = sc.List();
                for (int i = 0; i < siteList.Count; ++i) {
                    User u = siteList[i] as User;
                    MessageBox.Show(u.ToString());
                }
                session.Close(); 
            }
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
