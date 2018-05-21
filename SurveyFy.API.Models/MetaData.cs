using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SurveyFy.API.Models
{
    public partial class ScaleItem
    {
        public object Name { get { return this.Resource.Text; } }
    }
}