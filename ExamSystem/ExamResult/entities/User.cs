using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class User {
        public virtual long Id { get; protected set; }
        public virtual String Name { get; set; }
        public virtual String SecurityCode { get; set; }
        public virtual Occupation Occupation { get; set; }
        public virtual IList<ExamResult> Results { get; set; }

        public User() {
            Results = new List<ExamResult>();
        }
    }
}
