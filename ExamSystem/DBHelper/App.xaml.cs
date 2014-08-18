using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using ExamSystem.entities;
using ExamSystem.utils;

using log4net.Config;

namespace DBHelper {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            XmlConfigurator.Configure();

            ClinicalCase cli_case = PersistenceHelper.RetrieveAll<ClinicalCase>()[0];
            
            InputWindow input = new InputWindow(cli_case);
            input.Show();
        }
    }
}
