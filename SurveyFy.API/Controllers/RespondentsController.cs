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
    public class RespondentsController : Controller
    {
        private readonly SurveyfyContext _context;

        public RespondentsController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/Respondents
        [HttpGet]
        public IEnumerable<Respondent> GetRespondent()
        {
            return _context.Respondent;
        }

        // GET: api/Respondents/5
        [Route("api/Respondents/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRespondent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respondent = await _context.Respondent.SingleOrDefaultAsync(m => m.Id == id);

            if (respondent == null)
            {
                return NotFound();
            }

            return Ok(respondent);
        }

        // GET: api/Respondents/5
        [Route("api/Respondents/surveytaker/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRespondentBySurveyTaker([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respondent = await
                (from r in _context.Respondent
                 from a in _context.Answer
                          .Where(a => r.Id == a.RespondentId)
                          .DefaultIfEmpty()
                 where r.SurveyTaker.Guid == id
                 select r).Include(a => a.Answer)
                                   .FirstOrDefaultAsync();

            if (respondent == null)
            {
                return NotFound();
            }

            return Ok(respondent);
        }

        // PUT: api/Respondents/5
        [Route("api/Respondents/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRespondent([FromRoute] int id, [FromBody] Respondent respondent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != respondent.Id)
            {
                return BadRequest();
            }

            _context.Entry(respondent).State = EntityState.Modified;

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

        // POST: api/Respondents
        [Route("api/Respondents")]
        [HttpPost]
        public async Task<IActionResult> PostRespondent([FromBody] Respondent respondent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Respondent.Add(respondent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRespondent", new { id = respondent.Id }, respondent);
        }

        // DELETE: api/Respondents/5
        [Route("api/Respondents/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRespondent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respondent = await _context.Respondent.SingleOrDefaultAsync(m => m.Id == id);
            if (respondent == null)
            {
                return NotFound();
            }

            _context.Respondent.Remove(respondent);
            await _context.SaveChangesAsync();

            return Ok(respondent);
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.Id == id);
        }        
    }
}