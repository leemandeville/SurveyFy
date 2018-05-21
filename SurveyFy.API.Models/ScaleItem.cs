using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class ScaleItem
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int ScaleId { get; set; }
        public int? Calculation { get; set; }
        public int? OrderValue { get; set; }

        public Resource Resource { get; set; }
        public Scale Scale { get; set; }
    }
}
