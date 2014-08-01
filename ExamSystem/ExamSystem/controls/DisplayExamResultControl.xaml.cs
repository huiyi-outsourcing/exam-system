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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ExamSystem.entities;
using ExamSystem.utils;

namespace ExamSystem.controls {
    /// <summary>
    /// DisplayExamResultControl.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayExamResultControl : UserControl {
        public DisplayExamResultControl() {
            IList<Category> categories = PersistenceHelper.RetrieveAll<Category>();
            cb_category.ItemsSource = categories;
            cb_category.SelectedValuePath = "Description";
            cb_category.DisplayMemberPath = "Description";
            InitializeComponent();
        }

        private void bn_inqury_Click(object sender, RoutedEventArgs e) {

        }

        private void bn_inqury_by_category_Click(object sender, RoutedEventArgs e) {

            cb_category.ItemsSource = null;
        }
    }
}
