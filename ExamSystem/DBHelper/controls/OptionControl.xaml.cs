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

namespace DBHelper.controls {
    /// <summary>
    /// OptionControl.xaml 的交互逻辑
    /// </summary>
    public partial class OptionControl : UserControl {
        private String occupation;
        private ClinicalCase cli_case;

        public OptionControl() {
            InitializeComponent();
        }

        public OptionControl(String occupation, ClinicalCase cli_case) {
            InitializeComponent();

            this.occupation = occupation;
            this.cli_case = cli_case;
            tb_occupation.Text = occupation;

            if (occupation.Equals("军医"))

        }
    }
}
