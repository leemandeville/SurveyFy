using System;
using System.Collections.Generic;

namespace SurveyFy.API.Models
{
    public partial class OrgNode
    {
        public OrgNode()
        {
            InverseParent = new HashSet<OrgNode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrgId { get; set; }
        public int? ParentId { get; set; }

        public Org Org { get; set; }
        public OrgNode Parent { get; set; }
        public ICollection<OrgNode> InverseParent { get; set; }
    }
}
