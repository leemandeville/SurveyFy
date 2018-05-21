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
    public class SurveySectionsController : Controller
    {
        private readonly SurveyfyContext _context;

        public SurveySectionsController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/SurveySections
        [Route("api/SurveySections")]
        [HttpGet]
        public IEnumerable<SurveySection> GetSurveySection()
        {
            return _context.SurveySection.OrderBy(x => x.DisplayOrder);
        }

        // GET: api/SurveySections/5
        [Route("api/SurveySections/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveySection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveySection = await _context.SurveySection.SingleOrDefaultAsync(m => m.Id == id);

            if (surveySection == null)
            {
                return NotFound();
            }

            return Ok(surveySection);
        }

        // PUT: api/SurveySections/5
        [Route("api/SurveySections/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveySection([FromRoute] int id, [FromBody] SurveySection surveySection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != surveySection.Id)
            {
                return BadRequest();
            }

            _context.Entry(surveySection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveySectionExists(id))
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

        // POST: api/SurveySections
        [Route("api/SurveySections")]
        [HttpPost]
        public async Task<IActionResult> PostSurveySection([FromBody] SurveySection surveySection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SurveySection.Add(surveySection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveySection", new { id = surveySection.Id }, surveySection);
        }

        // DELETE: api/SurveySections/5
        [Route("api/SurveySections/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveySection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveySection = await _context.SurveySection.SingleOrDefaultAsync(m => m.Id == id);
            if (surveySection == null)
            {
                return NotFound();
            }

            _context.SurveySection.Remove(surveySection);
            await _context.SaveChangesAsync();

            return Ok(surveySection);
        }

        private bool SurveySectionExists(int id)
        {
            return _context.SurveySection.Any(e => e.Id == id);
        }

        // GET: api/surveys/4/questions
        [Route("api/SurveySections/first/{sid}")]
        [HttpGet]
        public async Task<IActionResult> GetFirstSection(Guid sid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveySection = await (from ss in _context.SurveySection
                                 join s in _context.Survey on ss.SurveyId equals s.Id
                                 where s.Guid == sid
                                 orderby ss.DisplayOrder ascending
                                 select ss).FirstOrDefaultAsync();

            if (surveySection == null)
            {
                return NotFound();
            }

            return Ok(surveySection);
        }

        // GET: api/surveys/4/questions
        [Route("api/SurveySections/next/{sid}/{pid}")]
        [HttpGet]
        public async Task<IActionResult> GetNextSection(Guid sid,Guid pid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveySection = await (from ss in _context.SurveySection
                                       join s in _context.Survey on ss.SurveyId equals s.Id
                                       where s.Guid == sid
                                       orderby ss.DisplayOrder ascending
                                       select ss).FirstOrDefaultAsync();

            if (surveySection == null)
            {
                return NotFound();
            }

            return Ok(surveySection);
        }
    }
}