using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net.Config;

namespace ExamResult {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            XmlConfigurator.Configure();

            base.OnStartup(e);
        }
    }
}
