using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam {
    public class MedicalExam : Exam {
        protected override void LoadExam() {
            IList<ClinicalCase> cases = ExamHelper.RetrieveByCategory(category);
            foreach (ClinicalCase cc in cases) {
                Question q = new Question(cc, Question.EXAMSTATUS.MEDICAL, user.Occupation.Description);
                Questions.Add(q);
            }
        }

        public MedicalExam(User user, String category) {
            this.user = user;
            this.category = category;
            questions = new List<Question>();
            LoadExam();
        }
    }
}
