using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.entities {
    public class User {
        public virtual long Id { get; protected set; }
        public virtual String Name { get; set; }
        public virtual String Username { get; set; }
        public virtual String Password { get; set; }
        public virtual Occupation Occupation { get; set; }
    }
}
