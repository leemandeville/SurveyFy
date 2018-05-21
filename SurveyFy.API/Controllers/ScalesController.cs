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
    [Route("api/Scales")]
    public class ScalesController : Controller
    {
        private readonly SurveyfyContext _context;

        public ScalesController(SurveyfyContext context)
        {
            _context = context;
        }

        // GET: api/Scales
        [HttpGet]
        public IEnumerable<Scale> GetScale()
        {
            return _context.Scale;
        }

        // GET: api/Scales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scale = await _context.Scale.SingleOrDefaultAsync(m => m.Id == id);

            if (scale == null)
            {
                return NotFound();
            }

            return Ok(scale);
        }

        // PUT: api/Scales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScale([FromRoute] int id, [FromBody] Scale scale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scale.Id)
            {
                return BadRequest();
            }

            _context.Entry(scale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScaleExists(id))
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

        // POST: api/Scales
        [HttpPost]
        public async Task<IActionResult> PostScale([FromBody] Scale scale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Scale.Add(scale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScale", new { id = scale.Id }, scale);
        }

        // DELETE: api/Scales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scale = await _context.Scale.SingleOrDefaultAsync(m => m.Id == id);
            if (scale == null)
            {
                return NotFound();
            }

            _context.Scale.Remove(scale);
            await _context.SaveChangesAsync();

            return Ok(scale);
        }

        private bool ScaleExists(int id)
        {
            return _context.Scale.Any(e => e.Id == id);
        }
    }
}