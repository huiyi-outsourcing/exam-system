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
            Map(x => x.Description);
            Map(x => x.Manifestation);
            References(x => x.InjuredDegree);
            HasManyToMany(x => x.Categories).Cascade.All().Table("clinicalcase_category");
            HasManyToMany(x => x.InjuredAreas).Cascade.All().Table("clinicalcase_injuredarea");
            HasMany(x => x.COptions).Cascade.All();
            HasMany(x => x.MOptions).Cascade.All();
        }
    }
}
