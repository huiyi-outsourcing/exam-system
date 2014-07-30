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
    /// MedicalExamSelectionControl.xaml 的交互逻辑
    /// </summary>
    public partial class MedicalExamSelectionControl : UserControl {
        private User user = null;

        public MedicalExamSelectionControl() {
            InitializeComponent();
        }

        public MedicalExamSelectionControl(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void enterShouShuZu(object sender, MouseButtonEventArgs e) {
            (Window.GetWindow(this) as MainWindow).setBody(new MedicalExamControl(user, "手术组"));
        }

        private void enterZhongZhengKangXiuKeZu(object sender, MouseButtonEventArgs e) {
            (Window.GetWindow(this) as MainWindow).setBody(new MedicalExamControl(user, "重症抗休克组"));
        }

        private void enterQingShangZu(object sender, MouseButtonEventArgs e) {
            (Window.GetWindow(this) as MainWindow).setBody(new MedicalExamControl(user, "轻伤组"));
        }

        private void enterHuaXueWuQiZu(object sender, MouseButtonEventArgs e) {
            (Window.GetWindow(this) as MainWindow).setBody(new MedicalExamControl(user, "化学武器组"));
        }

        private void enterHeWuQiZu(object sender, MouseButtonEventArgs e) {
            (Window.GetWindow(this) as MainWindow).setBody(new MedicalExamControl(user, "核武器组"));
        }
    }
}
