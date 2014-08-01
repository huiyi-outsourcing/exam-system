using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExamSystem.entities;
using FluentNHibernate.Mapping;

namespace ExamSystem.mappings {
    public class ExamResultMap : ClassMap<ExamSystem.entities.ExamResult> {
        public ExamResultMap() {
            Id(x => x.Id);
            Map(x => x.Score);
            References(x => x.Occupation);
            References(x => x.User);
            Map(x => x.DateTime).CustomSqlType("date");
        }
    }
}
