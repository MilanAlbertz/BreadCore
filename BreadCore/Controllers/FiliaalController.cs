using BreadCore.Data;
using BreadCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace BreadCore.Controllers
{
    public class FiliaalController : Controller
    {
        private readonly AppDbContext database;

        public FiliaalController(AppDbContext database)
        {
            this.database = database;  
        }

        public IActionResult FilialenBeheren()
        {
            IEnumerable<Filiaal> objFiliaalList = database.Filiaal;
            return View(objFiliaalList);
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || database.Filiaal == null)
            {
                return NotFound();
            }

            var medewerker = await database.Filiaal
                .Include(k => k.Medewerkers)
                .FirstOrDefaultAsync(m => m.FiliaalId == id);
           
            if (medewerker == null)
            {
                return NotFound();
            }

            return View(medewerker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FiliaalId, FiliaalNaam")] Filiaal filiaal)
        {
            if (ModelState.IsValid)
            {
                database.Add(filiaal);
                await database.SaveChangesAsync();
                return RedirectToAction("FilialenBeheren");
            }
            return View("FilialenBeheren");
        }

        // GET: Medewerkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || database.Filiaal == null)
            {
                return NotFound();
            }

            var filiaal = await database.Filiaal.FindAsync(id);
            if (filiaal == null)
            {
                return NotFound();
            }
            return View(filiaal);
        }

        // POST: Medewerkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FiliaalId,FiliaalNaam")] Filiaal filiaal)
        {
            if (id != filiaal.FiliaalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    database.Update(filiaal);
                    await database.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiliaalExists(filiaal.FiliaalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FilialenBeheren");
            }
            return View("FilialenBeheren");
        }
        private bool FiliaalExists(int id)
        {
            return (database.Medewerker?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
