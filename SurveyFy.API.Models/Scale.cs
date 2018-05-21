using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Scale
    {
        public Scale()
        {
            Question = new HashSet<Question>();
            ScaleItem = new HashSet<ScaleItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Question> Question { get; set; }
        public ICollection<ScaleItem> ScaleItem { get; set; }
    }
}
