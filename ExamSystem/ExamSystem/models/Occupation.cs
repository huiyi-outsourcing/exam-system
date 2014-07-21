using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.models {
    public class Occupation { 
        #region Declarations
        private int id;
        private Status status;
        #endregion

        #region Constructor
        public Occupation() { }
        #endregion

        #region Properties
        public virtual int ID {
            get { return id; }
            set { id = value; }
        }

        public Status Status {
            get { return status; }
            set { status = value; }
        }
        #endregion
    }

    public enum Status { 
        Doctor,
        Nurse
    }
}
