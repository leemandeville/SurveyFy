using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SurveyFy.API.Models;

namespace SurveyFy.Data
{
    public class Answer
    {
        #region Admin
        public static async Task<List<SurveyFy.API.Models.Answer>> GetAnswers()
        {
            List<SurveyFy.API.Models.Answer> answers = new List<API.Models.Answer>();

            if (Surveyfy.Properties.Settings.Default.LocalHosted==true)
            {
                SurveyFy.API.Models.
            }
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/answers");
                if (response.IsSuccessStatusCode)
                {
                    answers = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.Answer>>();
                }
            }
            return answers;
        }

        public static async Task<SurveyFy.API.Models.Answer> GetAnswer(int id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.Answer answer = new API.Models.Answer();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/answers/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                answer = await response.Content.ReadAsAsync<SurveyFy.API.Models.Answer>();
            }

            return answer;
        }

        public static async Task EditAnswer(SurveyFy.API.Models.Answer answer)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/answers/" + answer.Id.ToString(), answer);
            response.EnsureSuccessStatusCode();
        }

        public static async Task InsertAnswer(SurveyFy.API.Models.Answer answer)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/answers", answer);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteAnswer(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://api.surveyfy.co.uk/api/answers/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }

        #endregion
    }
}