using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreadCore.Data;
using BreadCore.Models;

namespace BreadCore.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BroodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BroodController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brood>>> GetIndividueelFiliaal(string filiaalNaam, DateTime beginDatum, DateTime eindDatum)
        {
            return await _context.Brood
                .Include(b => b.GebakkenFiliaal)
                .Include(b => b.Medewerker)
                .Include(b => b.BroodType)
                .Where(b => b.TijdGebakken >= beginDatum)
                .Where(b => b.TijdGebakken <= eindDatum)

                .ToListAsync();
        }

        /*
        // GET: api/Brood/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brood>> GetBrood(int id)
        {
            var brood = await _context.Brood.FindAsync(id);

            if (brood == null)
            {
                return NotFound();
            }

            return brood;
        }

        // PUT: api/Brood/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrood(int id, Brood brood)
        {
            if (id != brood.Id)
            {
                return BadRequest();
            }

            _context.Entry(brood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BroodExists(id))
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

        // POST: api/Brood
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brood>> PostBrood(Brood brood)
        {
            _context.Brood.Add(brood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrood", new { id = brood.Id }, brood);
        }

        // DELETE: api/Brood/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrood(int id)
        {
            var brood = await _context.Brood.FindAsync(id);
            if (brood == null)
            {
                return NotFound();
            }

            _context.Brood.Remove(brood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BroodExists(int id)
        {
            return _context.Brood.Any(e => e.Id == id);
        }*/
    }
}
