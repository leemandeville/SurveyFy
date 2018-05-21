using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            SurveySectionQuestion = new HashSet<SurveySectionQuestion>();
        }

        public int Id { get; set; }
        public int QuestionTypeId { get; set; }
        public int ResourceId { get; set; }
        public int? ScaleId { get; set; }
        public bool Required { get; set; }

        public Resource Resource { get; set; }
        public Scale Scale { get; set; }
        public ICollection<Answer> Answer { get; set; }
        public ICollection<SurveySectionQuestion> SurveySectionQuestion { get; set; }
    }
}
