using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam {
    public class ClassificationExam : Exam {
        protected override void LoadExam() {
            IList<ClinicalCase> cases = ExamHelper.RetrieveByInjuredArea();
            foreach (ClinicalCase cc in cases) {
                Question q = new Question(cc, Question.EXAMSTATUS.CLASSIFICATION, User.Occupation.Description);
                Questions.Add(q);
            }
        }
    }
}
