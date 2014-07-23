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

namespace ExamSystem {
    /// <summary>
    /// HelperWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelperWindow : Window {
        public HelperWindow() {
            InitializeComponent();
        }

        #region Layout
        public void setBody(UserControl control) {
            body.Children.RemoveRange(0, body.Children.Count);
            body.Children.Add(control);
        }
        #endregion

        #region Event Handlers
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                this.Close();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            MessageBoxResult result = MessageBox.Show("您确定要退出本程序吗？", "提醒", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        #endregion
    }
}
