using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class ExamResult {
        public virtual long Id { get; protected set; }
        public virtual double Score { get; set; }
        public virtual Occupation Occupation { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime DateTime { get; set; }
    }
}
