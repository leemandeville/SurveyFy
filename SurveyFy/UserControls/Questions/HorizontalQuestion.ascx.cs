using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Surveyfy.UserControls.Questions
{

    public partial class HorizontalQuestion : System.Web.UI.UserControl
    {
        public SurveyFy.API.Models.Question Question { get; set; }
        public SurveyFy.API.Models.Answer Answer { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            QuestionLabel.Text = Question.Resource.Text;
            ScaleList.DataSource = Question.Scale.ScaleItem;
            ScaleList.DataTextField = "Name";
            ScaleList.DataValueField = "Id";
            ScaleList.DataBind();
            ScaleList.Attributes.Add("data-QID", Question.Id.ToString());
            ScaleList.Attributes.Add("data-Qtype", Question.QuestionTypeId.ToString());

            if (Answer != null && ScaleList.Items.FindByValue(Answer.Value.ToString()) != null)
            {
                ScaleList.SelectedValue = Answer.Value.ToString();
            }
        }
    }
}