using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Property
    {
        public Property()
        {
            PropertyOption = new HashSet<PropertyOption>();
            SurveyTakerProperty = new HashSet<SurveyTakerProperty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PropertyOption> PropertyOption { get; set; }
        public ICollection<SurveyTakerProperty> SurveyTakerProperty { get; set; }
    }
}
