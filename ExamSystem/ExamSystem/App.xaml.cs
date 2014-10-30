using ExamSystem.utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using log4net;
using log4net.Config;

namespace ExamSystem {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));

        protected override void OnStartup(StartupEventArgs e) {
            //// setup log4net
            //XmlConfigurator.Configure();

            // Get reference to the current Process
            Process thisProc = Process.GetCurrentProcess();
            // Check how many total processes have the same name as the current one
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1) {
                MessageBox.Show("程序已经在运行。");
                Application.Current.Shutdown();
                return;
            }

            // Check database connection
            try {
                PersistenceHelper.OpenSession();
            } catch (Exception) {
                MessageBox.Show("无法连接数据库，请检查网络连接或联系管理员。");
                return;
            }

            /*
            entities.User user = PersistenceHelper.RetrieveAll<entities.User>()[0];

            controls.ExamListControl elc = new controls.ExamListControl(user, "手术室(组)", "外伤");
            MainWindow main = new MainWindow(elc);
            main.setBody(new controls.QuestionControl(elc.Exam.Questions.ElementAt(0), 0));
            main.toggleButton();
            main.Show();
            */

            if (confirmAuthorization()) {
                HelperWindow window = new HelperWindow();
                window.setBody(new controls.LoginControl());
                window.Show();
            } else {
                HelperWindow window = new HelperWindow();
                controls.AuthControl auth = new controls.AuthControl();
                window.setBody(auth);
                window.Show();
            }

            //entities.User user = PersistenceHelper.RetrieveAll<entities.User>()[0];

            //MainWindow main = new MainWindow(new controls.ExamListControl(user, "手术室(组)", "外伤"));
            //main.Show();
            
            base.OnStartup(e);
        }

        private Boolean confirmAuthorization() {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey ExamSystem = key.OpenSubKey("SOFTWARE\\ExamSystem");
            log.Debug("查找key");
            if (ExamSystem != null && ExamSystem.GetValue("authcode") != null) {
                String id = AuthHelper.getMachineID();
                return AuthHelper.authorize(id, ExamSystem.GetValue("authcode").ToString());
            } else {
                return false;
            }
        }
    }
}
