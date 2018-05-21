using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Surveyfy
{
    /// <summary>
    /// Summary description for SurveyFySurvey
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SurveyFySurvey : System.Web.Services.WebService
    {

        [WebMethod]
        public async System.Threading.Tasks.Task InsertAnswerAsync(SurveyFy.API.Models.Answer answer)
        {
            await SurveyFy.Data.Answer.InsertAnswer(answer);
        }
    }
}