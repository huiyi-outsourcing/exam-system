using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class MedicalOptionMap : ClassMap<MedicalOption> {
        public MedicalOptionMap() {
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.Correct);
            References(x => x.Case);
        }
    }
}
