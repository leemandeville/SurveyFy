using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class SurveySectionQuestion
    {
        public int SurveySectionQuestionId { get; set; }
        public int SurveySectionId { get; set; }
        public int QuestionId { get; set; }
        public int? DisplayOrder { get; set; }

        public Question Question { get; set; }
        public SurveySection SurveySection { get; set; }
    }
}
