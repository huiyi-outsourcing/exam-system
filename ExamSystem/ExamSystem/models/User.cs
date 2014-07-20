using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.models {
    public class User {
        #region Declarations
        private int id;
        private String name;
        private String username;
        private String password;
        private Occupation occupation;
        #endregion

        #region Constructor
        public User() { }
        #endregion

        #region Properties
        public virtual int ID {
            get { return id; }
            set { id = value; }
        }

        public virtual String Name {
            get { return name; }
            set { name = value; }
        }

        public virtual String Username {
            get { return username; }
            set { username = value; }
        }

        public virtual String Password {
            get { return password; }
            set { password = value; }
        }

        public virtual Occupation Occupation {
            get { return occupation; }
            set { occupation = value; }
        }
        #endregion
    }
}
