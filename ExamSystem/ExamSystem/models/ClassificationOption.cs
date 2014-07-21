using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.models {
    public class ClassificationOption {

        #region Declarations
        private int id;
        private ClinicalCase cliCase;
        private Occupation occupation;
        private bool correct;
        #endregion

        #region Constructor
        public ClassificationOption() { }
        #endregion

        #region Properties
        public virtual int ID {
            get { return id; }
            set { id = value; }
        }

        public ClinicalCase CliCase {
            get { return cliCase; }
            set { cliCase = value; }
        }

        public Occupation Occupation {
            get { return occupation; }
            set { occupation = value; }
        }

        public bool Correct {
            get { return correct; }
            set { correct = value; }
        }
        #endregion
    }
}
