using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam {
    public abstract class Exam {
        #region Properties
        private IList<Question> questions = null;
        private User user = null;

        public IList<Question> Questions {
            get { return questions; }
            set { questions = value; }
        }

        public User User {
            get { return user; }
            set { user = value; }
        }
        #endregion

        #region Constructor
        public Exam(User user) {
            this.user = user;
            LoadExam();
        }
        #endregion

        protected virtual void LoadExam();

        public virtual double GetScore();
    }
}
