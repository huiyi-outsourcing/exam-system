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

using ExamSystem.utils.exam2;

namespace ExamSystem.controls {
    /// <summary>
    /// QuestionControl.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionControl : UserControl {
        private Question question;
        //private bool flag;

        public QuestionControl(Question q, int index) {
            //this.Width = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualWidth;
            //this.Height = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight - 190;
            InitializeComponent();
            question = q;
            initLayout(index);
        }

        private void initLayout(int index) {
            var h = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight;
            var w = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualWidth;
            tb_index.Text = "第" + (index + 1) + "题";
            tb_description.Text = "    " + question.Description;
            tb_description.Width = w * 0.3;
            tb_manifestation.Text = "    " + question.Manifestation;
            tb_manifestation.Width = w * 0.3;
            //MessageBox.Show();

            for (int i = 0; i < question.Options.Count; ++i) {
                CheckBox cb = new CheckBox();
                cb.IsChecked = question.SelectedOptions.Contains(i) ? true : false;
                TextBlock tb = new TextBlock() { Text = question.Options[i].Description, TextWrapping = TextWrapping.Wrap, Foreground = Brushes.Black, HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch };
                tb.Width = w * 0.6;

                StackPanel p = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch, Margin = new Thickness(10, 15, 0, 15) };
                p.Children.Add(cb);
                p.Children.Add(tb);

                sp_options.Children.Add(p);
            }

            //for (int i = 0; i < question.Options.Count; ++i) {
            //    ListBoxItem item = new ListBoxItem() { Margin = new Thickness(10),  };
            //    TextBlock tb = new TextBlock() { Text = question.Options[i].Description, TextWrapping = TextWrapping.Wrap, Foreground = Brushes.Black };

            //    item.Content = tb;
                
            //    if (question.SelectedOptions.Contains(i)) {
            //        flag = true;
            //        item.IsSelected = true;
            //    }
            //    lb_options.Items.Add(item);
            //}

            //flag = false;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        public void Confirm() {
            for (int i = 0; i < sp_options.Children.Count; ++i) {
                StackPanel p = sp_options.Children[i] as StackPanel;
                CheckBox cb = p.Children[0] as CheckBox;
                if (cb.IsChecked == true) {
                    question.SelectedOptions.Add(i);
                }
            }
        }

        private void lb_options_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //if (flag) {
            //    flag = false;
            //    return;
            //}

            //question.SelectedOptions.Clear();
            //foreach (object option in lb_options.SelectedItems) {
            //    int idx = lb_options.Items.IndexOf(option);
            //    question.SelectedOptions.Add(idx);
            //}
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            Confirm();

            MainWindow parent = Window.GetWindow(this) as MainWindow;
            parent.Examlist.refresh();
            parent.setBody(parent.Examlist);
        }
    }
}
