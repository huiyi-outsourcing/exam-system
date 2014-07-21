using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.controls.viewmodel {
    public class LoginViewModel {
        private string username = string.Empty;
        private string password = string.Empty;
        
        public string Username {
            get { return username; }
            set { username = value; }
        }
        
        public string Password {
            get { return password; }
            set { password = value; }
        }
    }
}
