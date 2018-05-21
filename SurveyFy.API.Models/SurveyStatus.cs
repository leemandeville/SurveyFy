using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class SurveyStatus
    {
        public SurveyStatus()
        {
            Respondent = new HashSet<Respondent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Respondent> Respondent { get; set; }
    }
}
