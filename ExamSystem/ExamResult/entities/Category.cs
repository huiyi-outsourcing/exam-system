using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class Category {

        #region Properties
        public virtual long Id { get; protected set; }
        public virtual String Description { get; set; }

        public virtual IList<ClinicalCase> ClinicalCases { get; set; }
        #endregion

        #region Constructor
        public Category() {
            ClinicalCases = new List<ClinicalCase>();
        }
        #endregion
    }
}
