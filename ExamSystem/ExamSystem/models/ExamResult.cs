using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.models {
    public class ExamResult {

        #region Declarations
        private int id;
        private User user;
        private DateTime time;
        private double score;
        #endregion

        #region Constructor
        public ExamResult() { }
        #endregion

        #region Properties
        public virtual int ID {
            get { return id; }
            set { id = value; }
        }

        public User User {
            get { return user; }
            set { user = value; }
        }

        public DateTime Time {
            get { return time; }
            set { time = value; }
        }

        public double Score {
            get { return score; }
            set { score = value; }
        }
        #endregion
    }
}
