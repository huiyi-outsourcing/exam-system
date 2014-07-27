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
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace ExamSystem.controls {
    /// <summary>
    /// MedicalExamControl.xaml 的交互逻辑
    /// </summary>
    public partial class MedicalExamControl : UserControl {
        #region Properties
        private User user = null;
        private IList<ClinicalCase> cases = null;
        #endregion

        #region Constructor
        public MedicalExamControl() {
            InitializeComponent();
        }

        public MedicalExamControl(User user) {
            InitializeComponent();
            this.user = user;
            cases = ExamHelper.RetrieveByInjuredArea();
            initLayout();
        }
        #endregion

        #region Layout
        private void initLayout() { 
            // init clinical case list
            for (int i = 0; i < cases.Count; ++i) {
                ClinicalCase cli_case = cases[i];
                Border border = new Border() { SnapsToDevicePixels = true, BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(4), CornerRadius = new CornerRadius(5), Width = 40, Height = 40 };
                border.Effect = new DropShadowEffect() { Color = Colors.Black, BlurRadius = 16, ShadowDepth = 0, Opacity = 1 };

                TextBlock tb = new TextBlock() { Text = (i + 1).ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                border.Child = tb;
                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(10) };
                item.Content = border;
                item.Tag = cli_case;

                qlist.Items.Add(item);
            }

            // init content
            refreshClinicalCase(0);
        }

        private void refreshClinicalCase(int index) {
            ClinicalCase cli_case = cases[index];
            tb_description.Text = cli_case.Description;
            tb_manifestation.Text = cli_case.Manifestation;
        }

        private void drawColor() {
            //foreach (ListBoxItem item in qlist.Items) {
            //    Border border = item.Content as Border;
            //    Question q = item.Tag as Question;
            //    if (q.Status == Question.STATUS.DOUBT) {
            //        border.Background = Brushes.OrangeRed;
            //    } else {
            //        if (q.SelectedOption == -1)
            //            border.Background = Brushes.Transparent;
            //        else
            //            border.Background = Brushes.Green;
            //    }
            //}
        }
        #endregion

        #region EventHandlers
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void qlist_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int index = qlist.SelectedIndex;
            ListBoxItem item = qlist.Items[index] as ListBoxItem;

        }

        private void lb_options_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void prev_question(object sender, RoutedEventArgs e) {
        }

        private void next_question(object sender, RoutedEventArgs e) {
        }

        private void submit_exam(object sender, RoutedEventArgs e) {
        }
        #endregion
    }
}
