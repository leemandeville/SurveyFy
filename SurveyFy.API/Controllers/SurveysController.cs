using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyFy.API.Models;
using Microsoft.AspNetCore.Cors;

namespace SurveyFy.API.Controllers
{
    [Produces("application/json")]
    //[Route("api/Surveys")]
    public class SurveysController : Controller
    {
        private readonly SurveyfyContext _context;

        public SurveysController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/Surveys
        [Route("api/surveys")]
        [HttpGet]
        public IEnumerable<Survey> GetSurvey()
        {
            return _context.Survey;
        }

        // GET: api/Surveys/5
        [Route("api/surveys/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var survey = await _context.Survey.SingleOrDefaultAsync(m => m.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // PUT: api/Surveys/5
        [Route("api/surveys/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey([FromRoute] int id, [FromBody] Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.Id)
            {
                return BadRequest();
            }

            _context.Entry(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Surveys
        [Route("api/surveys")]
        [HttpPost]
        public async Task<IActionResult> PostSurvey([FromBody] Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Survey.Add(survey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurvey", new { id = survey.Id }, survey);
        }

        // DELETE: api/Surveys/5
        [Route("api/surveys/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var survey = await _context.Survey.SingleOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            _context.Survey.Remove(survey);
            await _context.SaveChangesAsync();

            return Ok(survey);
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.Id == id);
        }

        // GET: api/surveys/4/questions
        [Route("api/surveys/{sid}/questions")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyQuestions(Guid sid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questions = await (from ss in _context.SurveySection
                                   join sq in _context.SurveySectionQuestion on ss.Id equals sq.SurveySectionId
                                   join s in _context.Survey on ss.SurveyId equals s.Id
                                   join q in _context.Question on sq.QuestionId equals q.Id
                                   where s.Guid == sid
                                   orderby sq.DisplayOrder ascending
                                   select q).Distinct().Include(r => r.Resource)
                                   .Include(sc => sc.Scale)
                                   .Include(si => si.Scale.ScaleItem)
                                   .ThenInclude(sir => sir.Resource)
                                   .Include(sq => sq.SurveySectionQuestion)
                                   .ThenInclude(ss => ss.SurveySection)
                                   .ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // GET: api/surveys/4/questions
        [Route("api/surveys/{sid}/{uid}/answers")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyAnswers(Guid sid, Guid uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answers = await (from a in _context.Answer
                                 join r in _context.Respondent on a.RespondentId equals r.Id
                                 join st in _context.SurveyTaker on r.SurveyTakerId equals st.Id
                                 join q in _context.Question on a.QuestionId equals q.Id
                                 join sq in _context.SurveySectionQuestion on q.Id equals sq.QuestionId
                                 join ss in _context.SurveySection on sq.SurveySectionId equals ss.Id
                                 join s in _context.Survey on ss.SurveyId equals s.Id
                                   where st.Guid == uid
                                   && s.Guid == sid
                                 select a)
                                   .ToListAsync();

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }
    }
}