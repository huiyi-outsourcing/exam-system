using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.utils.exam2 {
    public class Option {
        private String description;

        public String Description {
            get { return description; }
            set { description = value; }
        }

        private bool correct;

        public bool Correct {
            get { return correct; }
            set { correct = value; }
        }

        public Option(String des, bool correct = false) {
            description = des;
            this.correct = correct;
        }
    }
}
