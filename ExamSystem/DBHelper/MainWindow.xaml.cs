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

using ExamSystem.entities;
using ExamSystem.utils;

using NHibernate;
using NHibernate.Criterion;

namespace DBHelper {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        private IList<InjuredArea> InjuredAreas = null;
        private IList<InjuredDegree> InjuredDegrees = null;
        
        public MainWindow() {
            InitializeComponent();

            InjuredAreas = PersistenceHelper.RetrieveAll<InjuredArea>();
            InjuredDegrees = PersistenceHelper.RetrieveAll<InjuredDegree>();

            cob_area.ItemsSource = InjuredAreas;
            cob_area.DisplayMemberPath = "Area";
            cob_area.SelectedIndex = 0;

            cob_degree.ItemsSource = InjuredDegrees;
            cob_degree.DisplayMemberPath = "Degree";
            cob_degree.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var cc = dg_results.SelectedItem as ClinicalCase;
            
        }

        private void btn_retrieve_Click(object sender, RoutedEventArgs e) {
            ICriteria criteria = PersistenceHelper.OpenSession().CreateCriteria(typeof(ClinicalCase));

            //criteria.Add(NHibernate.Criterion.Expression.Eq("Occupation", cob_occupation.SelectedItem));
            criteria.Add(NHibernate.Criterion.Expression.Eq("InjuredDegree", cob_degree.SelectedItem));

            IList<ClinicalCase> cases = new List<ClinicalCase>();
            String selected_area = (cob_area.SelectedItem as InjuredArea).Area;
            IList<ClinicalCase> tmp = criteria.List<ClinicalCase>();
            foreach (ClinicalCase c in tmp) {
                IList<InjuredArea> iarea = c.InjuredAreas;
                foreach (InjuredArea ia in iarea) {
                    if (ia.Area.Equals(selected_area)) {
                        cases.Add(c);
                        break;
                    }
                }
            }

            dg_results.ItemsSource = cases;
            dg_results.UpdateLayout();
        }
    }
}
