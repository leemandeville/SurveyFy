using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class Org
    {
        public Org()
        {
            OrgNode = new HashSet<OrgNode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<OrgNode> OrgNode { get; set; }
    }
}
