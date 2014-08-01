using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam {
    public abstract class Exam {
        #region Properties
        protected IList<Question> questions = null;
        protected User user = null;
        protected String category = null;

        public String Category {
            get { return category; }
            set { category = value; }
        }

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
        #endregion

        protected abstract void LoadExam();

        public double GetScore() {
            double result = 0;
            foreach (Question q in questions) {
                double score = q.GetScore();
                result += q.GetScore() * 100 / questions.Count;
            }

            return result;
        }
    }
}
