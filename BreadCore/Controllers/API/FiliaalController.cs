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
    public class FiliaalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FiliaalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Filiaal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filiaal>>> GetFiliaal()
        {
            return await _context.Filiaal.ToListAsync();
        }

        /*
         // GET: api/Filiaal/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Filiaal>> GetFiliaal(int id)
         {
             var filiaal = await _context.Filiaal.FindAsync(id);

             if (filiaal == null)
             {
                 return NotFound();
             }

             return filiaal;
         }

         // PUT: api/Filiaal/5
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPut("{id}")]
         public async Task<IActionResult> PutFiliaal(int id, Filiaal filiaal)
         {
             if (id != filiaal.FiliaalId)
             {
                 return BadRequest();
             }

             _context.Entry(filiaal).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!FiliaalExists(id))
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

         // POST: api/Filiaal
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<Filiaal>> PostFiliaal(Filiaal filiaal)
         {
             _context.Filiaal.Add(filiaal);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetFiliaal", new { id = filiaal.FiliaalId }, filiaal);
         }

         // DELETE: api/Filiaal/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteFiliaal(int id)
         {
             var filiaal = await _context.Filiaal.FindAsync(id);
             if (filiaal == null)
             {
                 return NotFound();
             }

             _context.Filiaal.Remove(filiaal);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool FiliaalExists(int id)
         {
             return _context.Filiaal.Any(e => e.FiliaalId == id);
         }
        */
    }
}
