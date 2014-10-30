﻿using System;
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

namespace ExamSystem.windows {
    /// <summary>
    /// ExitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExitWindow : Window {
        private Window window;

        public ExitWindow(Window window) {
            InitializeComponent();
            this.window = window;
        }

        private void btn_confirm_Click_1(object sender, RoutedEventArgs e) {
            this.Close();
            window.Close();
        }

        private void btn_cancel_Click_1(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
