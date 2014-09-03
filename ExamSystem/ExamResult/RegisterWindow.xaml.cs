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
using DataMigration.utils;
using ExamResult.utils;
using System.Data;
using ExcelLibrary.SpreadSheet;

namespace ExamResult {
    /// <summary>
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window {
        private Dictionary<String, Occupation> occupations;

        public RegisterWindow() {
            InitializeComponent();
            occupations = new Dictionary<String, Occupation>();
            IList<Occupation> ocs = PersistenceHelper.RetrieveAll<Occupation>();
            foreach (Occupation o in ocs) {
                occupations.Add(o.Description, o);
            }
        }

        private void tb_register_Click(object sender, RoutedEventArgs e) {
            User user = new User();
            user.Name = tb_name.Text.Trim();
            user.SecurityCode = tb_securitycode.Text.Trim();

            if (!validate(user.SecurityCode)) {
                MessageBox.Show("该保障卡号已注册");
                return;
            }

            if (rb_doctor.IsChecked == true) {
                user.Occupation = occupations["军医"];
            } else {
                user.Occupation = occupations["护士"];
            }
            PersistenceHelper.Save<User>(user);
            MessageBox.Show("注册用户成功");
        }

        private bool validate(String securitycode) {
            if (PersistenceHelper.RetrieveByProperty<User>("SecurityCode", securitycode).Count == 0) {
                return true;
            } else {
                return false;
            }
        }

        private void tb_import_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Excel Worksheets 2003(*.xls)|*.xls";
            if (dialog.ShowDialog() == true) {
                String filepath = dialog.FileName;
                Workbook workbook = Workbook.Load(filepath);
                Worksheet worksheet = workbook.Worksheets[0];

                try {
                    for (int rowIndex = worksheet.Cells.FirstRowIndex; rowIndex <= worksheet.Cells.LastRowIndex; ++rowIndex) {
                        Row row = worksheet.Cells.GetRow(rowIndex);

                        if (PersistenceHelper.RetrieveByProperty<User>("SecurityCode", row.GetCell(1).StringValue).Count != 0) {
                            continue;    
                        }

                        User user = new User();
                        user.Name = row.GetCell(0).StringValue;
                        user.SecurityCode = row.GetCell(1).StringValue;
                        user.Occupation = occupations[row.GetCell(2).StringValue];
                        PersistenceHelper.Save<User>(user);
                    }
                } catch (Exception) {
                    MessageBox.Show("导入Excel出错");
                    return;
                }
                MessageBox.Show("导入成功");
            }
        }
    }
}
