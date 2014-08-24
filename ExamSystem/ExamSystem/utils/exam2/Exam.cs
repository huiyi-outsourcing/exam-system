using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam2 {
    public class Exam {
        #region Properties
        protected IList<Question> questions = null;
        protected User user = null;
        protected String category = null;
        protected String reason = null;

        public String Reason {
            get { return reason; }
            set { reason = value; }
        }

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
        public Exam(User user, String category, String reason) {
            this.user = user;
            this.category = category;
            this.reason = reason;
            LoadExam();
        }
        #endregion

        protected void LoadExam() {
            questions = new List<Question>();
            IList<ClinicalCase> cases = ExamHelper.RetrieveByCategory(category, reason);
            if (cases.Count == 0)
                throw new Exception();

            foreach (ClinicalCase cc in cases) {
                if (!cc.Reason.Equals(reason))
                    continue;
                Question q = new Question(cc, category, user.Occupation.Description);
                questions.Add(q);
            }
        }

        public double GetScore() {
            double result = 0;
            foreach (Question q in questions) {
                double score = q.GetScore();
                result += q.GetScore() * 100;
            }

            return result / questions.Count;
        }
    }
}
