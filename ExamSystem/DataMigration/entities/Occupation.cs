using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class Occupation {
        public virtual long Id { get; set; }
        public virtual String Description { get; set; }
        public virtual IList<User> Staff { get; set; }
        public virtual IList<ClassificationOption> COptions { get; set; }
        public virtual IList<MedicalOption> MOptions { get; set; }

        public Occupation() {
            Staff = new List<User>();
            COptions = new List<ClassificationOption>();
            MOptions = new List<MedicalOption>();
        }
    }
}
