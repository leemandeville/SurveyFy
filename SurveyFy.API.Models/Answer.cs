using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int RespondentId { get; set; }
        public int QuestionId { get; set; }
        public int? Value { get; set; }
        public string ValueText { get; set; }

        public Question Question { get; set; }
        public Respondent Respondent { get; set; }
    }
}
