using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Language
    {
        public Language()
        {
            Resource = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Resource> Resource { get; set; }
    }
}
