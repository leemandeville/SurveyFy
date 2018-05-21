using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Respondent
    {
        public Respondent()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int StatusId { get; set; }
        public int? SurveyTakerId { get; set; }

        public RespondentStatus Status { get; set; }
        public SurveyTaker SurveyTaker { get; set; }
        public ICollection<Answer> Answer { get; set; }
    }
}
