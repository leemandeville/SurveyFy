using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class SurveySection
    {
        public SurveySection()
        {
            SurveySectionQuestion = new HashSet<SurveySectionQuestion>();
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public int DisplayOrder { get; set; }

        public Survey Survey { get; set; }
        public ICollection<SurveySectionQuestion> SurveySectionQuestion { get; set; }
    }
}
