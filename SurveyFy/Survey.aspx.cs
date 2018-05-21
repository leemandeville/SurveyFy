using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Surveyfy.UserControls;
using System.Web.UI.HtmlControls;

namespace SurveyFy
{
    public partial class Survey : Page
    {
        List<SurveyFy.API.Models.Question> questions { get; set; }
        List<SurveyFy.API.Models.Answer> answers { get; set; }

        SurveyFy.API.Models.Respondent respondent {get;set;}

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(TaskAsync));
            //List<SurveyFy.API.Models.Question> questions = new List<API.Models.Question>();
            //SurveyFy.API.Models.Question q = new API.Models.Question();
            //q.Id = 1;
            //questions.Add(q);
            //q = new API.Models.Question();
            //q.Id = 2;
            //questions.Add(q);

            //QuestionRepeater.DataSource = questions;
            //QuestionRepeater.DataBind();
            //GridView1.DataSource = questions;

            //GridView1.DataBind();
            Page.ExecuteRegisteredAsyncTasks();
        }

        private async Task TaskAsync()
        {
            Guid uid = new Guid();
            if (Request.QueryString["uid"] != null)
            {
                uid = new Guid(Request.QueryString["uid"].ToString());
            }
            Guid sid = new Guid();
            if (Request.QueryString["sid"] != null)
            {
                sid = new Guid(Request.QueryString["sid"].ToString());
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            List<SurveyFy.API.Models.SurveySection> surveySections = await SurveyFy.Data.SurveySection.GetSurveySections();

            Guid pid;
            if (Request.QueryString["pid"] != null)
            {
               pid = new Guid(Request.QueryString["pid"].ToString());
            }
            else
            {
                SurveyFy.API.Models.SurveySection surveySection = await SurveyFy.Data.Survey.GetFirstSection(sid);
                pid = surveySection.Guid;
            }

            questions = await SurveyFy.Data.Survey.GetQuestionsForSurvey(sid);
            answers = await SurveyFy.Data.Survey.GetAnswersForSurvey(sid, uid);

            SurveyFy.API.Models.SurveyTaker surveyTaker = await SurveyFy.Data.SurveyTaker.GetSurveyTaker(uid);
            respondent = await SurveyFy.Data.Respondent.GetRespondentBySurveyTaker(uid);

            if (respondent.Id == 0)
            {
                respondent = new SurveyFy.API.Models.Respondent();
                respondent.SurveyTakerId = surveyTaker.Id;
                respondent.StatusId = (int)Surveyfy.Enums.RespondentStatus.Started;
                await SurveyFy.Data.Respondent.InsertRespondent(respondent);
                respondent = await SurveyFy.Data.Respondent.GetRespondentBySurveyTaker(uid);
            }

            hdnRespondent.Value = respondent.Id.ToString();

            SectionRepeater.DataSource = surveySections;
            SectionRepeater.DataBind();

            //QuestionRepeater.DataSource = questions;
            //QuestionRepeater.DataBind();
            //GridView1.DataSource = questions;
            //GridView1.DataBind();
        }

        protected void SectionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            QuestionContainer questionContainer = (QuestionContainer)e.Item.FindControl("QuestionContainer1");
            SurveyFy.API.Models.SurveySection surveySection = (SurveyFy.API.Models.SurveySection)e.Item.DataItem;
            //HiddenField pid = (HiddenField)e.Item.FindControl("pidHiddenField");
           // HtmlControl containerdiv = (HtmlControl)e.Item.FindControl("containerdiv");
            if (e.Item.ItemIndex == 0)
            {
                questionContainer.IsFirst = true;
            }

            List<SurveyFy.API.Models.Question> sectionQuestions = new List<API.Models.Question>();
            foreach (SurveyFy.API.Models.Question question in questions)
            {
                foreach (SurveyFy.API.Models.SurveySectionQuestion surveySectionQuestion in question.SurveySectionQuestion)
                {
                    if (surveySectionQuestion.SurveySection.Guid == surveySection.Guid)
                    {
                        sectionQuestions.Add(question);
                    }
                }
            }
            questionContainer.Questions = sectionQuestions;//questions.Where(x => x.SurveySectionQuestion.Any(y => y.SurveySection.Guid == Guid.Parse(pid.Value))).ToList();
            questionContainer.Answers = answers;
            questionContainer.Respondent = respondent;
            questionContainer.Bind();
        }
    }
}