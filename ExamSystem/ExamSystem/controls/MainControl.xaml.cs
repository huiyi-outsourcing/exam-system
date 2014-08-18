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

namespace ExamSystem.controls {
    /// <summary>
    /// MainControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainControl : UserControl {
        private User user = null;
        private String category;

        public MainControl() {
            InitializeComponent();
        }

        public MainControl(User user, String category) {
            InitializeComponent();
            this.category = category;
            this.user = user;
        }

        private void enterLocal(object sender, MouseButtonEventArgs e) {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new ExamControl(user, category, "地方伤"));
        }

        private void enterArmy(object sender, MouseButtonEventArgs e) {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new ExamControl(user, category, "战伤"));
        }
    }
}
