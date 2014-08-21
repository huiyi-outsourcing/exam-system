using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class MedicalOption {

        #region Properties
        public virtual long Id { get; protected set; }
        public virtual String Description { get; set; }
        public virtual Boolean Correct { get; set; }

        public virtual Occupation Occupation { get; set; }
        public virtual ClinicalCase ClinicalCase { get; set; }
        #endregion
    }
}
