using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

using ExamSystem.entities;

namespace ExamSystem.mappings {
    public class UserMap : ClassMap<User>{
        public UserMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Username);
            Map(x => x.Password);
            References(x => x.Occupation).Cascade.None();
        }
    }
}
