using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyFy.API.Models;

namespace SurveyFy.API.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionTypes")]
    public class QuestionTypesController : Controller
    {
        private readonly SurveyfyContext _context;

        public QuestionTypesController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/QuestionTypes
        [HttpGet]
        public IEnumerable<QuestionType> GetQuestionType()
        {
            return _context.QuestionType;
        }

        // GET: api/QuestionTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionType = await _context.QuestionType.SingleOrDefaultAsync(m => m.Id == id);

            if (questionType == null)
            {
                return NotFound();
            }

            return Ok(questionType);
        }

        // PUT: api/QuestionTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionType([FromRoute] int id, [FromBody] QuestionType questionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionTypeExists(id))
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

        // POST: api/QuestionTypes
        [HttpPost]
        public async Task<IActionResult> PostQuestionType([FromBody] QuestionType questionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.QuestionType.Add(questionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionType", new { id = questionType.Id }, questionType);
        }

        // DELETE: api/QuestionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionType = await _context.QuestionType.SingleOrDefaultAsync(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            _context.QuestionType.Remove(questionType);
            await _context.SaveChangesAsync();

            return Ok(questionType);
        }

        private bool QuestionTypeExists(int id)
        {
            return _context.QuestionType.Any(e => e.Id == id);
        }
    }
}