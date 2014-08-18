using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ExamSystem.entities;
using ExamSystem.utils;

namespace DBHelper {
    /// <summary>
    /// InputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputWindow : Window {
        private IList<Category> categories;
        private IList<InjuredArea> areas;
        private IList<InjuredDegree> degrees;
        private IList<Occupation> occupations;
        private ClinicalCase cli_case;

        public InputWindow() {
            InitializeComponent();
        }

        public InputWindow(ClinicalCase cli_case) {
            InitializeComponent();
            this.cli_case = cli_case;
            init();
        }

        private void init() {
            categories = PersistenceHelper.RetrieveAll<Category>();
            areas = PersistenceHelper.RetrieveAll<InjuredArea>();
            degrees = PersistenceHelper.RetrieveAll<InjuredDegree>();
            occupations = PersistenceHelper.RetrieveAll<Occupation>();

            cob_category.ItemsSource = categories;
            cob_category.DisplayMemberPath = "Description";
            for (int i = 0; i < categories.Count; ++i) {
                foreach (Category ca in cli_case.Categories) {
                    if (ca.Description.Equals(categories[i].Description)) {
                        cob_category.SelectedItems.Add(cob_category.Items[i]);
                    }
                }
            }
            cob_area.ItemsSource = areas;
            cob_area.DisplayMemberPath = "Area";
            for (int i = 0; i < areas.Count; ++i) {
                foreach (InjuredArea area in cli_case.InjuredAreas) {
                    if (area.Area.Equals(areas[i].Area)) {
                        cob_area.SelectedItems.Add(cob_area.Items[i]);
                    }
                }
            }
            cob_degree.ItemsSource = degrees;
            cob_degree.DisplayMemberPath = "Degree";
            for (int i = 0; i < degrees.Count; ++i) {
                if (cli_case.InjuredDegree.Degree.Equals(degrees[i].Degree)) {
                    cob_degree.SelectedIndex = i;
                }
            }

            tb_description.Text = cli_case.Description;
            tb_manifestation.Text = cli_case.Manifestation;

            occupation.ItemsSource = occupations;
            occupation.DisplayMemberPath = "Description";
            dg_coptions.ItemsSource = cli_case.COptions;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            PersistenceHelper.Delete<ClinicalCase>(cli_case);
        }
    }
}
