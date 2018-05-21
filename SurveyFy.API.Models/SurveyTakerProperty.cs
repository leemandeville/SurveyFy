using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class SurveyTakerProperty
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int? PropertyOptionId { get; set; }
        public string TextValue { get; set; }

        public Property Property { get; set; }
        public PropertyOption PropertyOption { get; set; }
    }
}
