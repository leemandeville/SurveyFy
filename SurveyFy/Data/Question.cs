using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SurveyFy.API.Models;

namespace SurveyFy.Data
{
    public class Question
    {
        #region Admin
        public static async Task<List<SurveyFy.API.Models.Question>> GetQuestions()
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.Question> questions = new List<API.Models.Question>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/questions");
            if (response.IsSuccessStatusCode)
            {
                questions = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.Question>>();
            }

            return questions;
        }

        public static async Task<SurveyFy.API.Models.Question> GetQuestion(int id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.Question question = new SurveyFy.API.Models.Question();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/questions/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                question = await response.Content.ReadAsAsync<SurveyFy.API.Models.Question>();
            }

            return question;
        }

        public static async Task EditQuestion(SurveyFy.API.Models.Question question)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/questions/" + question.Id.ToString(), question);
            response.EnsureSuccessStatusCode();
        }

        public static async Task InsertQuestion(SurveyFy.API.Models.Question question)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/questions", question);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteQuestion(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://api.surveyfy.co.uk/api/questions/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }

        #endregion
    }
}