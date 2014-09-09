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

using DataMigration.utils;
using NHibernate;
using NHibernate.Criterion;
using ExamResult.utils;

namespace ExamResult {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        private IList<ExamSystem.entities.ExamResult> results = null;

        public MainWindow() {
            InitializeComponent();
        }

        private void btn_retrieve_Click(object sender, RoutedEventArgs e) {
            // Create a criteria object with the specified criteria
            //ICriteria criteria = session.CreateCriteria(typeof(T));
            //criteria.Add(Expression.Eq(propertyName, propertyValue));

            //// get matching objects
            //IList<T> machingObjects = criteria.List<T>();

            //return machingObjects;

            ICriteria criteria = PersistenceHelper.OpenSession().CreateCriteria(typeof(ExamSystem.entities.ExamResult));
            if (chb_name.IsChecked == true) {
                IList<ExamSystem.entities.User> users = PersistenceHelper.RetrieveByProperty<ExamSystem.entities.User>("SecurityCode", tb_securitycode.Text.Trim());
                if (users.Count != 0)
                    criteria.Add(NHibernate.Criterion.Expression.Eq("User", users[0]));
                else
                    criteria.Add(NHibernate.Criterion.Expression.Eq("User", null));
            }

            if (chb_occupation.IsChecked == true) {
                if (cob_occupation.SelectedIndex == -1) {
                    MessageBox.Show("请选择相应的职业");
                    return;
                }

                long id = cob_occupation.SelectedIndex + 1;
                ExamSystem.entities.Occupation op = PersistenceHelper.RetrieveByProperty<ExamSystem.entities.Occupation>("Id", id)[0];
                criteria.Add(NHibernate.Criterion.Expression.Eq("Occupation", op));
            }

            if (chb_date.IsChecked == true) {
                DateTime dt = (DateTime)dp_date.SelectedDate;
                DateTime am = new DateTime(dt.Year, dt.Month, dt.Day);
                DateTime pm = new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1);
                criteria.Add(NHibernate.Criterion.Expression.Between("DateTime", am, pm));
                //MessageBox.Show(dt.ToString());
            }

            IList<ExamSystem.entities.ExamResult> objects = criteria.List<ExamSystem.entities.ExamResult>();
            results = objects;
            dg_results.ItemsSource = objects;
        }

        private void btn_export_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Excel Worksheets 2003(*.xls)|*.xls";
            if (dialog.ShowDialog() == true) {
                String filepath = dialog.FileName;
                ExcelHelper2 excel = new ExcelHelper2();
                excel.SaveToExcel(filepath, results);
                MessageBox.Show("保存成功");
            }
        }

        private void btn_register_Click(object sender, RoutedEventArgs e) {
            Window register = new RegisterWindow();
            register.Show();
        }

        private void btn_change_password_Click(object sender, RoutedEventArgs e) {
            ChangePasswordWindow window = new ChangePasswordWindow();
            window.ShowDialog();
        }
    }
}
