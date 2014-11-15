using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class ClinicalCaseMap : ClassMap<ClinicalCase> {
        public ClinicalCaseMap() {
            Id(x => x.Id);
            Map(x => x.Description).Length(500);
            Map(x => x.Manifestation).Length(500);
            Map(x => x.Reason);
            References(x => x.InjuredDegree);
            HasManyToMany(x => x.Categories).Cascade.All();
            HasManyToMany(x => x.InjuredAreas).Cascade.All();
            HasMany(x => x.COptions).Cascade.All().Inverse();
            HasMany(x => x.MOptions).Cascade.All().Inverse();
        }
    }
}
