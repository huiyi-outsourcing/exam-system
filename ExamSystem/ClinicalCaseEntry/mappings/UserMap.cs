﻿using System;
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
            Map(x => x.SecurityCode);
            References(x => x.Occupation).Cascade.None();
            HasMany(x => x.Results).Cascade.All();
        }
    }
}
