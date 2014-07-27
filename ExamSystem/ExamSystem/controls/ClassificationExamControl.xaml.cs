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
    /// ClassificationExamControl.xaml 的交互逻辑
    /// </summary>
    public partial class ClassificationExamControl : UserControl {
        #region Properties
        private User user = null;
        #endregion

        #region Constructor
        public ClassificationExamControl() {
            InitializeComponent();
        }

        public ClassificationExamControl(User user) {
            InitializeComponent();
            this.user = user;
        }
        #endregion
    }
}
