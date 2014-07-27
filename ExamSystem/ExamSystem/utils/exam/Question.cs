using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;

namespace ExamSystem.utils.exam {
    public class Question {
        #region Properties
        private String description;
        private String manifestation;
        private IList<Option> options;
        private double score;
        public enum STATUS { EMPTY, DONE }
        private STATUS status;
        private ISet<int> answers;

        public ISet<int> Answers {
            get { return answers; }
            set { answers = value; }
        }

        public STATUS Status {
            get { return status; }
            set { status = value; }
        }

        public double Score {
            get { return score; }
            set { score = value; }
        }

        public IList<Option> Options {
            get { return options; }
            set { options = value; }
        }

        public String Manifestation {
            get { return manifestation; }
            set { manifestation = value; }
        }

        public String Description {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region Constructor
        public Question(ClinicalCase cli_case) {
            
        }
        #endregion
    }
}
