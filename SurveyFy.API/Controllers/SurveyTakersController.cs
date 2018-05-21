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
    public class SurveyTakersController : Controller
    {
        private readonly SurveyfyContext _context;

        public SurveyTakersController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/SurveyTakers
        [Route("api/SurveyTakers")]
        [HttpGet]
        public IEnumerable<SurveyTaker> GetSurveyTaker()
        {
            return _context.SurveyTaker;
        }

        // GET: api/SurveyTakers/5
        [Route("api/SurveyTakers/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveyTaker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyTaker = await _context.SurveyTaker.SingleOrDefaultAsync(m => m.Id == id);

            if (surveyTaker == null)
            {
                return NotFound();
            }

            return Ok(surveyTaker);
        }

        [Route("api/SurveyTakers/guid/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveyTaker([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyTaker = await _context.SurveyTaker.SingleOrDefaultAsync(m => m.Guid == id);

            if (surveyTaker == null)
            {
                return NotFound();
            }

            return Ok(surveyTaker);
        }

        // PUT: api/SurveyTakers/5
        [Route("api/SurveyTakers/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyTaker([FromRoute] int id, [FromBody] SurveyTaker surveyTaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != surveyTaker.Id)
            {
                return BadRequest();
            }

            _context.Entry(surveyTaker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyTakerExists(id))
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

        // POST: api/SurveyTakers
        [Route("api/SurveyTakers")]
        [HttpPost]
        public async Task<IActionResult> PostSurveyTaker([FromBody] SurveyTaker surveyTaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SurveyTaker.Add(surveyTaker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveyTaker", new { id = surveyTaker.Id }, surveyTaker);
        }

        // DELETE: api/SurveyTakers/5
        [Route("api/SurveyTakers/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyTaker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyTaker = await _context.SurveyTaker.SingleOrDefaultAsync(m => m.Id == id);
            if (surveyTaker == null)
            {
                return NotFound();
            }

            _context.SurveyTaker.Remove(surveyTaker);
            await _context.SaveChangesAsync();

            return Ok(surveyTaker);
        }

        private bool SurveyTakerExists(int id)
        {
            return _context.SurveyTaker.Any(e => e.Id == id);
        }
    }
}