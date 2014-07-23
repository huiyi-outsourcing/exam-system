using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class InjuredDegreeMap : ClassMap<InjuredDegree> {
        public InjuredDegreeMap() {
            Id(x => x.Id);
            Map(x => x.Degree);
            HasMany(x => x.ClinicalCases).Inverse();
        }
    }
}
