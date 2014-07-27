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
using System.Windows.Threading;

namespace ExamSystem.controls {
    /// <summary>
    /// CoundDownControl.xaml 的交互逻辑
    /// </summary>
    public partial class CountDownControl : UserControl {
        private CountDownHelper cd;
        private DispatcherTimer timer;

        public CountDownControl() {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);

            cd = new CountDownHelper(40 * 60);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e) {
            if (cd.ProcessCountDown()) {
                tb_hour.Text = cd.GetHour();
                tb_minute.Text = cd.GetMinute();
                tb_second.Text = cd.GetSecond();
            } else {
                timer.Stop();
            }
        }
    }

    public class CountDownHelper {
        private int _TotalSecond;
        public int TotalSecond {
            get { return _TotalSecond; }
            set { _TotalSecond = value; }
        }

        public CountDownHelper(int totalSecond) {
            _TotalSecond = totalSecond;
        }

        public bool ProcessCountDown() {
            if (_TotalSecond == 0) {
                return false;
            } else {
                _TotalSecond--;
                return true;
            }
        }

        public string GetHour() {
            return String.Format("{0:D2}", (_TotalSecond / 3600));
        }

        public string GetMinute() {
            return String.Format("{0:D2}", (_TotalSecond % 3600) / 60);
        }

        public string GetSecond() {
            return String.Format("{0:D2}", _TotalSecond % 60);
        }
    }
}
