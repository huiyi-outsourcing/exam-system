using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class OccupationMap : ClassMap<Occupation> {
        public OccupationMap() {
            Id(x => x.Id);
            Map(x => x.Description);
            HasMany(x => x.Staff).Cascade.All().Inverse();
            HasMany(x => x.COptions).Cascade.All().Inverse();
            HasMany(x => x.MOptions).Cascade.All().Inverse();
            HasMany(x => x.Results).Cascade.All().Inverse();
        }
    }
}
