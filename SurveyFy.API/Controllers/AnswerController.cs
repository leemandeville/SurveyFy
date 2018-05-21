using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyFy.API.Models;
using System.Web.Http.Cors;


namespace SurveyFy.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Answers")]
    [EnableCors(origins: "http://api.surveyfy.co.uk", headers: "*", methods: "*")]
    public class AnswerController : Controller
    {
        private readonly SurveyfyContext _context;

        public AnswerController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/Answer
        [HttpGet]
        public IEnumerable<Answer> GetAnswer()
        {
            return _context.Answer;
        }

        // GET: api/Answer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answer = await _context.Answer.SingleOrDefaultAsync(m => m.Id == id);

            if (answer == null)
            {
                return NotFound();
            }

            return Ok(answer);
        }

        // PUT: api/Answers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer([FromRoute] int id, [FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != answer.Id)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answer
        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Answer deleteAnswer = await _context.Answer.SingleOrDefaultAsync(m => m.RespondentId == answer.RespondentId && m.QuestionId == answer.QuestionId);
            if ( deleteAnswer != null)
            {
                _context.Answer.Remove(deleteAnswer);
                await _context.SaveChangesAsync();
            }

            _context.Answer.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Answer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answer = await _context.Answer.SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();

            return Ok(answer);
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(e => e.Id == id);
        }

        private bool AnswerExists(int respondentId ,int questionId)
        {
            return _context.Answer.Any(e => e.QuestionId == questionId && e.RespondentId == respondentId);
        }
    }
}