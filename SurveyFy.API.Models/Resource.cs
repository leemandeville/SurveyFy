using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Resource
    {
        public Resource()
        {
            Question = new HashSet<Question>();
            ScaleItem = new HashSet<ScaleItem>();
        }

        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public string Text { get; set; }

        public Language Language { get; set; }
        public ICollection<Question> Question { get; set; }
        public ICollection<ScaleItem> ScaleItem { get; set; }
    }
}
