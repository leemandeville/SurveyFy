using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Survey
    {
        public Survey()
        {
            SurveySection = new HashSet<SurveySection>();
            SurveyTaker = new HashSet<SurveyTaker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }

        public ICollection<SurveySection> SurveySection { get; set; }
        public ICollection<SurveyTaker> SurveyTaker { get; set; }
    }
}
