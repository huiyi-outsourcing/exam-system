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
using ExamSystem.controls;

namespace ExamSystem {
    /// <summary>
    /// ReasonWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReasonWindow : Window {
        private User user = null;

        public User User {
            get { return user; }
            set { user = value; }
        }
        private String category = null;

        public String Category {
            get { return category; }
            set { category = value; }
        }

        public ReasonWindow(User user, String category) {
            InitializeComponent();
            this.user = user;
            this.category = category;
        }

        private void enterLocal(object sender, MouseButtonEventArgs e) {
            ExamListControl elc = null;
            try {
                elc = new ExamListControl(user, category, "外伤");
            } catch (Exception) {
                MessageBox.Show("数据库中无病例");
                return;
            }
            MainWindow window = new MainWindow(elc);
            window.Show();
            this.Close();
        }

        private void enterArmy(object sender, MouseButtonEventArgs e) {
            ExamListControl elc = null;
            try {
                elc = new ExamListControl(user, category, "战伤");
            } catch (Exception) {
                MessageBox.Show("数据库中无病例");
                return;
            }
            MainWindow window = new MainWindow(elc);
            window.Show();
            this.Close();
        }

        #region EventHandlers
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                this.Close();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("您确定要结束本次训练吗？", "提醒", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
                this.Close();
        }
        #endregion
    }
}
