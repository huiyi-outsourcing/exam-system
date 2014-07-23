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
        
        //private static ISessionFactory sessionFactory
        //    = Fluently.Configure()
        //    .Database(MySQLConfiguration.Standard.ConnectionString(cs => cs.FromAppSetting("conn")))
        //    .Mappings(m => m.AutoMappings
        //        .Add(AutoMap.AssemblyOf<ExamSystem.entities.User>()
        //        .Where(type => type.Namespace == "ExamSystem.entities")))
        //    .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
        //    .BuildSessionFactory();

        public MainWindow() {
            InitializeComponent();
        }

        private void create_Click(object sender, RoutedEventArgs e) {
            ISessionFactory sessionFactory
            = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(cs => cs.FromAppSetting("conn")))
            .Mappings(m => m.AutoMappings
                .Add(AutoMap.AssemblyOf<ExamSystem.entities.User>()
                .Where(type => type.Namespace == "ExamSystem.entities")))
            .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
            .BuildSessionFactory();

            using (ISession session = sessionFactory.OpenSession()) {
                using (ITransaction tran = session.BeginTransaction()) {
                    var doctor = new Occupation { Description = "医生" };
                    var nurse = new Occupation { Description = "护士" };
                    session.SaveOrUpdate(doctor);
                    session.SaveOrUpdate(nurse);

                    var category1 = new Category { Description = "分类组" };
                    var category2 = new Category { Description = "手术组" };
                    var category3 = new Category { Description = "重抗组" };
                    var category4 = new Category { Description = "轻伤组" };
                    var category5 = new Category { Description = "化学武器组" };
                    var category6 = new Category { Description = "核武器组" };
                    session.SaveOrUpdate(category1);
                    session.SaveOrUpdate(category2);
                    session.SaveOrUpdate(category3);
                    session.SaveOrUpdate(category4);
                    session.SaveOrUpdate(category5);
                    session.SaveOrUpdate(category6);

                    var degree1 = new InjuredDegree { Degree = "重度" };
                    var degree2 = new InjuredDegree { Degree = "中度" };
                    var degree3 = new InjuredDegree { Degree = "轻度" };
                    session.SaveOrUpdate(degree1);
                    session.SaveOrUpdate(degree2);
                    session.SaveOrUpdate(degree3);

                    var area1 = new InjuredArea { Area = "头颈部" };
                    var area2 = new InjuredArea { Area = "胸部" };
                    var area3 = new InjuredArea { Area = "腰腹部" };
                    var area4 = new InjuredArea { Area = "四肢" };
                    var area5 = new InjuredArea { Area = "生殖系统" };
                    var area6 = new InjuredArea { Area = "全身" };
                    session.SaveOrUpdate(area1);
                    session.SaveOrUpdate(area2);
                    session.SaveOrUpdate(area3);
                    session.SaveOrUpdate(area4);
                    session.SaveOrUpdate(area5);
                    session.SaveOrUpdate(area6);
                    tran.Commit();
                }
            }
        }

        private void import_Click(object sender, RoutedEventArgs e) {

        }
    }
}
