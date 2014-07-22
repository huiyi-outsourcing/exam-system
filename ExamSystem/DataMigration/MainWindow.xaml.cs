using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using FluentNHibernate.Automapping;

using ExamSystem.entities;

namespace DataMigration {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        private static ISessionFactory sessionFactory
            = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(cs => cs.FromAppSetting("conn")))
            .Mappings(m => m.AutoMappings
                .Add(AutoMap.AssemblyOf<ExamSystem.entities.User>()
                .Where(type => type.Namespace == "ExamSystem.entities")))
            .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))
            .BuildSessionFactory();

        public MainWindow() {
            InitializeComponent();
        }

        private void create_Click(object sender, RoutedEventArgs e) {
            var doctor = new Occupation { Description = "医生" };
            var nurse = new Occupation { Description = "护士" };

            using (ISession session = sessionFactory.OpenSession()) {
                using (ITransaction tran = session.BeginTransaction()) {
                    session.SaveOrUpdate(doctor);
                    session.SaveOrUpdate(nurse);
                    tran.Commit();
                }
            }
        }

        private void import_Click(object sender, RoutedEventArgs e) {

        }
    }
}
