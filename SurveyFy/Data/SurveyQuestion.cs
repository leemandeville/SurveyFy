using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SurveyFy.Data
{
    public class SurveySection
    {
        #region Admin
        public static async Task<List<SurveyFy.API.Models.SurveySection>> GetSurveySections()
        {
            HttpClient client = new HttpClient();
            List<SurveyFy.API.Models.SurveySection> SurveySections = new List<API.Models.SurveySection>();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/SurveySections");
            if (response.IsSuccessStatusCode)
            {
                SurveySections = await response.Content.ReadAsAsync<List<SurveyFy.API.Models.SurveySection>>();
            }

            return SurveySections;
        }

        public static async Task<SurveyFy.API.Models.SurveySection> GetSurveySection(int id)
        {
            HttpClient client = new HttpClient();
            SurveyFy.API.Models.SurveySection SurveySection = new API.Models.SurveySection();
            HttpResponseMessage response = await client.GetAsync("http://api.surveyfy.co.uk/api/SurveySections/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                SurveySection = await response.Content.ReadAsAsync<SurveyFy.API.Models.SurveySection>();
            }

            return SurveySection;
        }

        public static async Task EditSurvey(SurveyFy.API.Models.SurveySection SurveySection)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync("http://api.surveyfy.co.uk/api/SurveySections/" + SurveySection.Id.ToString(), SurveySection);
            response.EnsureSuccessStatusCode();
        }

        public static async Task InsertSurveySection(SurveyFy.API.Models.SurveySection SurveySection)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://api.surveyfy.co.uk/api/SurveySections", SurveySection);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteSurveySection(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://api.surveyfy.co.uk/api/SurveySections/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }
    }
    #endregion
}