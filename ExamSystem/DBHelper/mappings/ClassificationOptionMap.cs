using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class ClassificationOptionMap : ClassMap<ClassificationOption> {
        public ClassificationOptionMap() {
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.Correct);
            References(x => x.Occupation);
            References(x => x.ClinicalCase);
        }
    }
}
