using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using log4net;
using log4net.Config;

namespace DataMigration {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            // setup log4net
            XmlConfigurator.Configure();

            try {
                utils.PersistenceHelper.OpenSession();
            } catch (Exception ex) {
                MessageBox.Show("无法连接数据库，请检查网络连接或联系管理员。" + ex.Message);
                MessageBox.Show(ex.InnerException.Message);
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }
    }
}
