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
            // setup log4net
            XmlConfigurator.Configure();

            // Get reference to the current Process
            Process thisProc = Process.GetCurrentProcess();
            // Check how many total processes have the same name as the current one
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1) {
                MessageBox.Show("程序已经在运行。");
                Application.Current.Shutdown();
                return;
            }

            log.Debug("程序单例启动成功");

            try {
                PersistenceHelper.OpenSession();
            } catch (Exception) {
                MessageBox.Show("无法连接数据库，请检查网络连接或联系管理员。");
                Application.Current.Shutdown();
                return;
            }

            log.Debug("数据库检测成功");

            HelperWindow window = new HelperWindow();
            if (confirmAuthorization()) {
                log.Debug("验证成功");
                controls.LoginControl login = new controls.LoginControl();
                window.setBody(login);
            } else {
                controls.AuthControl auth = new controls.AuthControl();
                window.setBody(auth);
            }
            window.Show();

            //entities.User user = PersistenceHelper.RetrieveByProperty<entities.User>("Username", "doctor")[0];
            //MainWindow main = new MainWindow();
            //main.setBody(new controls.MedicalExamControl(user));
            //main.Show();
            
            base.OnStartup(e);
        }

        private Boolean confirmAuthorization() {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey SimuTraining = key.OpenSubKey("SOFTWARE\\ExamSystem");
            log.Debug("查找key");
            if (SimuTraining != null && SimuTraining.GetValue("authcode") != null) {
                String id = AuthHelper.getMachineID();
                return AuthHelper.authorize(id, SimuTraining.GetValue("authcode").ToString());
            } else {
                return false;
            }
        }
    }
}
