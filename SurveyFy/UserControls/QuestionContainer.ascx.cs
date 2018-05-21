using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Surveyfy.UserControls.Questions;
using System.Web.UI.HtmlControls;

namespace Surveyfy.UserControls
{
    public partial class QuestionContainer : System.Web.UI.UserControl
    {
        [Bindable(true)]
        public SurveyFy.API.Models.Respondent Respondent { get; set; }
        public List<SurveyFy.API.Models.Question> Questions { get; set; }
        public List<SurveyFy.API.Models.Answer> Answers { get; set; }

        public bool IsFirst { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Bind()
        {
            if (IsFirst)
            {
                containerdiv.Attributes.Add("class", "item active");
            }
            hdnRespondent.Value = Respondent.Id.ToString();
            if (Questions != null)
            {
                int currentQuestionTypeId = 0;
                Table table = null;
                foreach (SurveyFy.API.Models.Question question in Questions)
                {
                    SurveyFy.API.Models.Answer answer = new SurveyFy.API.Models.Answer();
                    if (Answers != null)
                    {
                        answer = Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                    }
                    if ((currentQuestionTypeId == 0 || currentQuestionTypeId != question.QuestionTypeId) && (question.QuestionTypeId == 2 || question.QuestionTypeId == 7))
                    {
                        if (table != null)
                        {
                            QuestionPlaceholder.Controls.Add(table);
                        }
                        table = new Table();
                    }
                    if ((currentQuestionTypeId == 0 || currentQuestionTypeId != question.QuestionTypeId) && (question.QuestionTypeId == 3 || question.QuestionTypeId == 8) && table == null)
                    {
                        table = new Table();
                    }

                    if ((currentQuestionTypeId == 2 && question.QuestionTypeId == 8) || (currentQuestionTypeId == 7 && question.QuestionTypeId == 3))
                    {
                        if (table != null)
                        {
                            QuestionPlaceholder.Controls.Add(table);
                        }
                        table = new Table();
                    }
                    switch (question.QuestionTypeId)
                    {
                        case 1:
                            HorizontalQuestion horizontalQuestion = LoadControl("~/usercontrols/Questions/HorizontalQuestion.ascx") as HorizontalQuestion;
                            horizontalQuestion.Answer = answer;
                            horizontalQuestion.Question = question;
                            QuestionPlaceholder.Controls.Add(horizontalQuestion);
                            break;
                        case 2:
                            foreach (TableRow tr in CreateStackedRows(question, answer, true, true, question.QuestionTypeId))
                            {
                                table.Rows.Add(tr);
                            }
                            break;
                        case 3:      
                            foreach (TableRow tr in CreateStackedRows(question, answer, false, true, question.QuestionTypeId))
                            {
                                table.Rows.Add(tr);
                            }
                            break;
                        case 4:
                            VerticalQuestion verticalQuestion = LoadControl("~/usercontrols/Questions/VerticalQuestion.ascx") as VerticalQuestion;
                            verticalQuestion.Answer = answer;
                            verticalQuestion.Question = question;
                            QuestionPlaceholder.Controls.Add(verticalQuestion);
                            break;
                        case 5:
                            DropDownQuestion dropDownQuestion = LoadControl("~/usercontrols/Questions/DropDownQuestion.ascx") as DropDownQuestion;
                            dropDownQuestion.Answer = answer;
                            dropDownQuestion.Question = question;
                            QuestionPlaceholder.Controls.Add(dropDownQuestion);
                            break;
                        case 6:
                            HorizontalCheckQuestion horizontalCheckQuestion = LoadControl("~/usercontrols/Questions/HorizontalCheckQuestion.ascx") as HorizontalCheckQuestion;
                            horizontalCheckQuestion.Answer = answer;
                            horizontalCheckQuestion.Question = question;
                            QuestionPlaceholder.Controls.Add(horizontalCheckQuestion);
                            break;
                        case 7:
                            foreach (TableRow tr in CreateStackedRows(question, answer, true, false, question.QuestionTypeId))
                            {
                                table.Rows.Add(tr);
                            }
                            break;
                        case 8:
                            foreach (TableRow tr in CreateStackedRows(question, answer, false, false, question.QuestionTypeId))
                            {
                                table.Rows.Add(tr);
                            }
                            break;
                        case 9:
                            TextQuestion textQuestion = LoadControl("~/usercontrols/Questions/TextQuestion.ascx") as TextQuestion;
                            textQuestion.Answer = answer;
                            textQuestion.Question = question;
                            QuestionPlaceholder.Controls.Add(textQuestion);
                            break;
                        case 10:
                            BigTextQuestion bigTextQuestion = LoadControl("~/usercontrols/Questions/BigTextQuestion.ascx") as BigTextQuestion;
                            bigTextQuestion.Answer = answer;
                            bigTextQuestion.Question = question;
                            QuestionPlaceholder.Controls.Add(bigTextQuestion);
                            break;
                    }

                    if ((currentQuestionTypeId == 0 || currentQuestionTypeId != question.QuestionTypeId) && (currentQuestionTypeId != 2 && question.QuestionTypeId != 3 && currentQuestionTypeId != 7 && question.QuestionTypeId != 8) || question == Questions.Last())
                    {
                        if (table != null)
                        {
                            QuestionPlaceholder.Controls.Add(table);
                        }
                    }
                    currentQuestionTypeId = question.QuestionTypeId;
                }
            }
        }

        public List<TableRow> CreateStackedRows(SurveyFy.API.Models.Question question, SurveyFy.API.Models.Answer answer, bool showHeader,bool isRadio, int questionType)
        {
            List<TableRow> TableRows = new List<TableRow>();
            TableCell tableCell;
            if (showHeader == true)
            {
                TableHeaderRow th = new TableHeaderRow();
                tableCell = new TableCell();
                th.Cells.Add(tableCell);

                foreach (SurveyFy.API.Models.ScaleItem scale in question.Scale.ScaleItem)
                {
                    tableCell = new TableCell();
                    tableCell.Text = scale.Resource.Text;
                    th.Cells.Add(tableCell);
                }
                TableRows.Add(th);
            }

            TableRow tr = new TableRow();
            tableCell = new TableCell();
            tableCell.Text = question.Resource.Text;
            tr.Cells.Add(tableCell);
            tr.Attributes.Add("data-QID", question.Id.ToString());
            tr.Attributes.Add("data-Qtype", questionType.ToString());

            foreach (SurveyFy.API.Models.ScaleItem scale in question.Scale.ScaleItem)
            {
                tableCell = new TableCell();

                if (isRadio == true)
                {
                    RadioButton rb = new RadioButton();
                    rb.GroupName = "QID" + question.Id.ToString();
                    rb.Attributes.Add("value", scale.Id.ToString());
                    //rb.Attributes.Add("data-QID", "QID" + question.Id.ToString());
                    //rb.Attributes.Add("data-Qtype", questionType.ToString());

                    if (answer != null && scale.Id == answer.Value)
                    {
                        rb.Checked = true;
                    }
                    tableCell.Controls.Add(rb);
                }
                else
                {
                    CheckBox cb = new CheckBox();
                    cb.InputAttributes.Add("value", scale.Id.ToString());

                    if (answer != null && scale.Id == answer.Value)
                    {
                        cb.Checked = true;
                    }
                    tableCell.Controls.Add(cb);
                }

                tr.Cells.Add(tableCell);
            }
            TableRows.Add(tr);

            return TableRows;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var controls = Page.Controls;
            foreach (Control c in Page.Controls)
            {
                HtmlControl wc = (HtmlControl)c;
                string value = wc.Attributes["data-QID"];
            }
        }
    }
}