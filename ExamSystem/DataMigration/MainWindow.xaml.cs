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
using System.Threading;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using FluentNHibernate.Automapping;

using ExamSystem.entities;
using DataMigration.utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace DataMigration {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        ObservableCollection<FileCollection> _FileCollection = new ObservableCollection<FileCollection>();

        public ObservableCollection<FileCollection> FileCollection {
            get { return _FileCollection; }
            set { _FileCollection = value; }
        }

        public MainWindow() {
            InitializeComponent();
        }

        private void create_Click(object sender, RoutedEventArgs e) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory
                = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(cs => cs.FromAppSetting("conn")))
                .Mappings(m => m.AutoMappings
                    .Add(AutoMap.AssemblyOf<ExamSystem.entities.User>()
                    .Where(type => type.Namespace == "ExamSystem.entities")))
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            try {
                using (ISession session = sessionFactory.OpenSession()) {
                    using (ITransaction tran = session.BeginTransaction()) {
                        String[] occupations = new String[2] { "军医", "护士" };
                        foreach (String occupation in occupations) {
                            Occupation ocp = new Occupation() { Description = occupation };
                            session.SaveOrUpdate(ocp);
                        }

                        String[] categories = new String[6] { "分类组", "手术组", "重症抗休克组", "轻伤组", "化学武器组", "核武器组" };
                        foreach (String category in categories) {
                            Category cty = new Category() { Description = category };
                            session.SaveOrUpdate(cty);
                        }

                        String[] degrees = new String[3] { "重度", "中度", "轻度" };
                        foreach (String degree in degrees) {
                            InjuredDegree idegree = new InjuredDegree() { Degree = degree };
                            session.SaveOrUpdate(idegree);
                        }

                        String[] areas = new String[13] { "常见临床危象", "腹部损伤", "骨盆、泌尿生殖系统损伤", "急救", "脊椎损伤", "颈部损伤", "颅脑损伤", "面部损伤", "上肢骨、关节损伤", "烧伤", "外伤感染", "下肢骨、关节损伤", "胸部损伤" };
                        foreach (String area in areas) {
                            InjuredArea iarea = new InjuredArea() { Area = area };
                            session.SaveOrUpdate(iarea);
                        }
                        tran.Commit();
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("插入数据错误: " + ex.Message);
            }

            MessageBox.Show("数据库创建完成");
        }

        private void import_Click(object sender, RoutedEventArgs e) {
            _FileCollection.Clear();
            
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "XML-file (.xml)|*.xml";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == true) {
                foreach (String filename in dialog.FileNames) {
                    _FileCollection.Add(new FileCollection() { FilePath = filename, FileStatus = "等待处理" });
                }
            }

            PropertyHelper helper = new PropertyHelper();
            foreach (FileCollection fc in _FileCollection) {
                try {
                    helper.readFile(fc.FilePath);
                    fc.FileStatus = "成功";
                } catch (Exception ex) {
                    fc.FileStatus = "失败: " + ex.Message;
                }
            }
        }

        private void test_Click(object sender, RoutedEventArgs e) {
            //test_user_occupation();

            test_clinical_case();
        }

        private void test_clinical_case() {
            
                ISession session = PersistenceHelper.OpenSession();
                using (var transaction = session.BeginTransaction()) {
                    ClinicalCase cases = new ClinicalCase() { Manifestation = "abc", Description = "abc" };
                    session.SaveOrUpdate(cases);

                    Occupation ocp = new Occupation() { Description = "军医" };
                    session.SaveOrUpdate(ocp);

                    ClassificationOption co1 = new ClassificationOption() { Correct = true, Description = "co1", Occupation = ocp };
                    co1.ClinicalCase = cases;
                    ClassificationOption co2 = new ClassificationOption() { Correct = true, Description = "co2", Occupation = ocp };
                    co2.ClinicalCase = cases;

                    session.SaveOrUpdate(co1);
                    session.SaveOrUpdate(co2);


                    transaction.Commit();
                }
        }

        private void test_user_occupation() {
            try {
                ISession session = PersistenceHelper.OpenSession();
                using (ITransaction tran = session.BeginTransaction()) {
                    String[] occupations = new String[2] { "军医", "护士" };
                    for (int i = 1; i <= occupations.Length; ++i) {
                        Occupation ocp = new Occupation() { Description = occupations[i - 1] };
                        User user = new User { Name = "test" + i, Password = "abc", Username = "test" + i, Occupation = ocp };
                        session.SaveOrUpdate(user);
                        tran.Commit();
                        MessageBox.Show("user_occupation success.");
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class FileCollection {
        public string FilePath { get; set; }
        public string FileStatus { get; set; }
    }
}
