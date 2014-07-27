using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class CategoryMap : ClassMap<Category> {
        public CategoryMap() {
            Id(x => x.Id);
            Map(x => x.Description);
            HasManyToMany(x => x.ClinicalCases).Cascade.All().Inverse();
        }
    }
}
