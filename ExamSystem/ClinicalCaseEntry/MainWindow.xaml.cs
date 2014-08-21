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

namespace ClinicalCaseEntry {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        #region Properties
        private IList<Category> categories;
        private IList<InjuredArea> areas;
        private IList<InjuredDegree> degrees;
        private IList<Occupation> occupations;
        private ClinicalCase cli_case = null;
        #endregion

        public MainWindow() {
            InitializeComponent();
            init();
        }

        #region Public Methods
        #endregion

        #region Private Methods
        private void init() {
            cli_case = new ClinicalCase();

            categories = PersistenceHelper.RetrieveAll<Category>();
            areas = PersistenceHelper.RetrieveAll<InjuredArea>();
            degrees = PersistenceHelper.RetrieveAll<InjuredDegree>();
            occupations = PersistenceHelper.RetrieveAll<Occupation>();

            cob_category.ItemsSource = categories;
            cob_category.DisplayMemberPath = "Description";
            cob_category.SelectedIndex = 0;

            cob_area.ItemsSource = areas;
            cob_area.DisplayMemberPath = "Area";

            cob_degree.ItemsSource = degrees;
            cob_degree.DisplayMemberPath = "Degree";

            coccupation.ItemsSource = occupations;
            coccupation.DisplayMemberPath = "Description";

            moccupation.ItemsSource = occupations;
            moccupation.DisplayMemberPath = "Description";

            dg_coptions.ItemsSource = cli_case.COptions;
            dg_moptions.ItemsSource = cli_case.MOptions;
        }
        #endregion

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            if (validate())
                save();
        }

        private bool validate() {
            if (tb_description.Text.Length == 0) {
                MessageBox.Show("受伤史不能为空");
                return false;
            }

            if (tb_manifestation.Text.Length == 0) {
                MessageBox.Show("临床表现不能为空");
                return false;
            }

            if (cob_category.SelectedItems.Count == 0) {
                MessageBox.Show("未选择病例分组");
                return false;
            }

            if (cob_area.SelectedItems.Count == 0) {
                MessageBox.Show("未选择受伤部位");
                return false;
            }

            if (cob_degree.SelectedIndex == -1) {
                MessageBox.Show("未选择受伤程度");
                return false;
            }

            if (lb_reason.SelectedIndex == -1) {
                MessageBox.Show("未选择伤势类型");
                return false;
            }

            if (cli_case.COptions.Count == 0) {
                MessageBox.Show("未输入分类处置选项");
                return false;
            }

            if (cli_case.MOptions.Count == 0) {
                MessageBox.Show("未输入医疗处置选项");
            }

            return true;
        }

        private void save() {
            //分组
            foreach (Category ct in cob_category.SelectedItems) {
                cli_case.Categories.Add(ct);
            }

            //伤部
            foreach (InjuredArea area in cob_area.SelectedItems) {
                cli_case.InjuredAreas.Add(area);
            }

            // 受伤史
            cli_case.Description = tb_description.Text.Trim();
            // 临床表现
            cli_case.Manifestation = tb_manifestation.Text.Trim();
            // 受伤程度
            cli_case.InjuredDegree = cob_degree.SelectedItem as InjuredDegree;

            // 伤势类型
            ListBoxItem it = lb_reason.SelectedItem as ListBoxItem;
            cli_case.Reason = it.Content.ToString();

            IList<ClassificationOption> ico = cli_case.COptions;
            cli_case.COptions = null;
            IList<MedicalOption> imo = cli_case.MOptions;
            cli_case.MOptions = null;

            PersistenceHelper.Save<ClinicalCase>(cli_case);

            // 分类处置
            foreach (ClassificationOption co in ico) {
                co.ClinicalCase = cli_case;
                PersistenceHelper.Save<ClassificationOption>(co);
            }
            // 医疗处置
            foreach (MedicalOption mo in imo) {
                mo.ClinicalCase = cli_case;
                PersistenceHelper.Save<MedicalOption>(mo);
            }

            MessageBox.Show("保存成功");
        }
    }
}
