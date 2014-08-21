using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class InjuredDegree {

        #region Properties
        public virtual long Id { get; protected set; }
        public virtual String Degree { get; set; }

        public virtual IList<ClinicalCase> ClinicalCases { get; set; }
        #endregion

        #region Constructor
        public InjuredDegree() {
            ClinicalCases = new List<ClinicalCase>();
        }
        #endregion
    }
}
