using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.models {
    public class ClinicalCase {
        
        #region Declarations
        private int id;
        private Category category;
        private InjuredArea injuredArea;
        private String description;
        private String manifestation;
        private Degree degree;

        private IList<ClassificationOption> coptions = new List<ClassificationOption>();
        private IList<MedicalOption> moptions = new List<MedicalOption>();
        #endregion

        #region Constructor
        public ClinicalCase() { }
        #endregion

        #region Properties
        public virtual int ID {
            get { return id; }
            set { id = value; }
        }

        public Category Category {
            get { return category; }
            set { category = value; }
        }

        public InjuredArea InjuredArea {
            get { return injuredArea; }
            set { injuredArea = value; }
        }

        public String Description {
            get { return description; }
            set { description = value; }
        }

        public String Manifestation {
            get { return manifestation; }
            set { manifestation = value; }
        }

        public Degree Degree {
            get { return degree; }
            set { degree = value; }
        }

        public IList<ClassificationOption> Coptions {
            get { return coptions; }
            set { coptions = value; }
        }

        public IList<MedicalOption> Moptions {
            get { return moptions; }
            set { moptions = value; }
        }
        #endregion
    }
}
