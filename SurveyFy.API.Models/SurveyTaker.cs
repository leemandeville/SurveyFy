using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class SurveyTaker
    {
        public SurveyTaker()
        {
            Respondent = new HashSet<Respondent>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }
        public ICollection<Respondent> Respondent { get; set; }
    }
}
