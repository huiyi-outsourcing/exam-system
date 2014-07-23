using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class InjuredAreaMap : ClassMap<InjuredArea> {
        public InjuredAreaMap() {
            Id(x => x.Id);
            Map(x => x.Area);
            HasMany(x => x.ClinicalCases).Inverse();
        }
    }
}
