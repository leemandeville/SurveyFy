using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SurveyFy.API.Models;

namespace SurveyFy.Data
{
    public class Respondent
    {
        #region Admin
        public static async Task<List<SurveyFy.API.Models.Respondent>> GetRespondents()
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.Respondent> respondents = new List<API.Models.Respondent>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/respondents");
            if (response.IsSuccessStatusCode)
            {
                respondents = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.Respondent>>();
            }

            return respondents;
        }

        public static async Task<SurveyFy.API.Models.Respondent> GetRespondent(int id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.Respondent respondent = new API.Models.Respondent();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/respondents/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                respondent = await response.Content.ReadAsAsync<SurveyFy.API.Models.Respondent>();
            }

            return respondent;
        }

        public static async Task<SurveyFy.API.Models.Respondent> GetRespondentBySurveyTaker(Guid id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.Respondent respondent = new API.Models.Respondent();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/respondents/surveytaker/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                respondent = await response.Content.ReadAsAsync<SurveyFy.API.Models.Respondent>();
            }

            return respondent;
        }

        public static async Task EditRespondent(SurveyFy.API.Models.Respondent respondent)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/respondents/" + respondent.Id.ToString(), respondent);
            response.EnsureSuccessStatusCode();
        }

        public static async Task InsertRespondent(SurveyFy.API.Models.Respondent respondent)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/respondents", respondent);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteRespondent(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://api.surveyfy.co.uk/api/respondents/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }

        #endregion
    }
}