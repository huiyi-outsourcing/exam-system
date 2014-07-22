using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ExamSystem.utils.validations {
    public class UsernameValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            String str = value as String;
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str)) {
                return new ValidationResult(false, "用户名不能为空！");
            }

            if (str.Length > 15 || str.Length < 4) {
                return new ValidationResult(false, "用户名长度应在4-15之间");
            }

            return new ValidationResult(true, null);
        }
    }

    public class PasswordValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
            String str = value as String;
            if (str.Length > 20 || str.Length < 6) {
                return new ValidationResult(false, "密码长度应在6-20之间");
            }

            if (str.Contains('\'') || str.Contains('*') || str.Contains('_') || str.Contains(' ')) {
                return new ValidationResult(false, "密码中不能存在', *, _, 空格特殊字符");
            }

            return new ValidationResult(true, null);
        }
    }
}
