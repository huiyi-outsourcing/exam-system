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

namespace ExamSystem.controls {
    /// <summary>
    /// MainControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainControl : UserControl {
        public MainControl() {
            InitializeComponent();
        }

        private void enterClassificationExam(object sender, MouseButtonEventArgs e) {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new ClassificationExamControl());
        }

        private void enterMedicalExam(object sender, MouseButtonEventArgs e) {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setBody(new MedicalExamControl());
        }
    }
}
