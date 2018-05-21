using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class PropertyOption
    {
        public PropertyOption()
        {
            SurveyTakerProperty = new HashSet<SurveyTakerProperty>();
        }

        public int Id { get; set; }
        public int? PropertyId { get; set; }
        public string Name { get; set; }
        public int? ParentPropertyOptionId { get; set; }
        public int Value { get; set; }

        public Property Property { get; set; }
        public ICollection<SurveyTakerProperty> SurveyTakerProperty { get; set; }
    }
}
