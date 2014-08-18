using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class ClinicalCase {

        #region Properties
        public virtual long Id { get; protected set; }
        public virtual String Description { get; set; }
        public virtual String Manifestation { get; set; }
        public virtual InjuredDegree InjuredDegree { get; set; }
        public virtual String Reason { get; set; }

        public virtual IList<Category> Categories { get; set; }
        public virtual IList<InjuredArea> InjuredAreas { get; set; }
        public virtual IList<ClassificationOption> COptions { get; set; }
        public virtual IList<MedicalOption> MOptions { get; set; }
        #endregion

        #region Constructor
        public ClinicalCase() {
            Categories = new List<Category>();
            InjuredAreas = new List<InjuredArea>();
            COptions = new List<ClassificationOption>();
            MOptions = new List<MedicalOption>();
        }
        #endregion
    }
}
