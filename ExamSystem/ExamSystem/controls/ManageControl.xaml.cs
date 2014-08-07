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
    /// ManageControl.xaml 的交互逻辑
    /// </summary>
    public partial class ManageControl : UserControl {
        private User user = null;
        private String type = "";
        public ManageControl() {
            InitializeComponent();
        }

        public ManageControl(User user, String type) {
            this.user = user;
            this.type = type;
            InitializeComponent();
        }

        private void enterClassificationExam(object sender, MouseButtonEventArgs e) {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new ClassificationExamControl(user));
        }

        private void enterMedicalExam(object sender, MouseButtonEventArgs e) {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new MedicalExamSelectionControl(user, type));
        }
    }


}
