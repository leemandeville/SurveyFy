using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SurveyFy.API.Models;

namespace SurveyFy.Data
{
    public class Survey
    {
        #region Admin
        public static async Task<List<SurveyFy.API.Models.Survey>> GetSurveys()
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.Survey> surveys = new List<API.Models.Survey>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveys");
            if (response.IsSuccessStatusCode)
            {
                surveys = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.Survey>>();
            }

            return surveys;
        }

        public static async Task<SurveyFy.API.Models.Survey> GetSurvey(int id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.Survey survey = new API.Models.Survey();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveys/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                survey = await response.Content.ReadAsAsync<SurveyFy.API.Models.Survey>();
            }

            return survey;
        }

        public static async Task EditSurvey(SurveyFy.API.Models.Survey survey)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/surveys/" + survey.Id.ToString(), survey);
            response.EnsureSuccessStatusCode();
        }

        public static async Task InsertSurvey(SurveyFy.API.Models.Survey survey)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/surveys", survey);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteSurvey(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://api.surveyfy.co.uk/api/surveys/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }

        #endregion

        public static async Task<List<SurveyFy.API.Models.Question>> GetQuestionsForSurvey(Guid sid)
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.Question> questions = new List<SurveyFy.API.Models.Question>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveys/" + sid.ToString() + "/questions");
            if (response.IsSuccessStatusCode)
            {
                questions = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.Question>>();
            }

            return questions;
        }

        public static async Task<SurveyFy.API.Models.SurveySection> GetFirstSection(Guid sid)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.SurveySection surveySection = new SurveyFy.API.Models.SurveySection();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/SurveySections/first/" + sid.ToString());
            if (response.IsSuccessStatusCode)
            {
                surveySection = await response.Content.ReadAsAsync<SurveyFy.API.Models.SurveySection>();
            }

            return surveySection;
        }

        public static async Task<List<SurveyFy.API.Models.Answer>> GetAnswersForSurvey(Guid sid, Guid uid)
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.Answer> answers = new List<SurveyFy.API.Models.Answer>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveys/" + sid.ToString() + "/" + uid.ToString() + "/answers");
            if (response.IsSuccessStatusCode)
            {
                answers = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.Answer>>();
            }

            return answers;
        }

        public static async Task SaveAnswer(SurveyFy.API.Models.Answer answer, bool isNew)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            if (isNew)
            {
                response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/answers", answer);
            }
            else
            {
                response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/answers/" + answer.Id.ToString(), answer);
            }
            response.EnsureSuccessStatusCode();
        }
    }
}