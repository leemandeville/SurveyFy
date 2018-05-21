using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SurveyFy.API.Models;

namespace SurveyFy.Data
{
    public class SurveyTaker
    {
        #region Admin
        public static async Task<List<SurveyFy.API.Models.SurveyTaker>> GetSurveyTakers()
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.SurveyTaker> surveyTakers = new List<API.Models.SurveyTaker>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveyTakers");
            if (response.IsSuccessStatusCode)
            {
                surveyTakers = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.SurveyTaker>>();
            }

            return surveyTakers;
        }

        public static async Task<SurveyFy.API.Models.SurveyTaker> GetSurveyTaker(int id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.SurveyTaker surveyTaker = new API.Models.SurveyTaker();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveyTakers/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                surveyTaker = await response.Content.ReadAsAsync<SurveyFy.API.Models.SurveyTaker>();
            }

            return surveyTaker;
        }

        public static async Task<SurveyFy.API.Models.SurveyTaker> GetSurveyTaker(Guid id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.SurveyTaker surveyTaker = new API.Models.SurveyTaker();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/surveyTakers/guid/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                surveyTaker = await response.Content.ReadAsAsync<SurveyFy.API.Models.SurveyTaker>();
            }

            return surveyTaker;
        }

        public static async Task EditSurveyTaker(SurveyFy.API.Models.SurveyTaker surveyTaker)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/surveyTakers/" + surveyTaker.Id.ToString(), surveyTaker);
            response.EnsureSuccessStatusCode();
        }

        public static async Task InsertSurveyTaker(SurveyFy.API.Models.SurveyTaker surveyTaker)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/surveyTakers", surveyTaker);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteRespondent(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://api.surveyfy.co.uk/api/surveyTakers/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }

        #endregion
    }
}