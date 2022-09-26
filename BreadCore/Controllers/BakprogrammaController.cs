using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BreadCore.Data;
using BreadCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BreadCore.Controllers
{
    public class BakprogrammaController : Controller
    {
        private readonly AppDbContext _context;

        public BakprogrammaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> BakprogrammaBeheren()
        {
            return _context.Bakprogramma != null ?
            View(await _context.Bakprogramma.ToListAsync()) :
            Problem("Entity set 'AppDbContext.Bakprogramma'  is null.");
        }
         public IActionResult BroodEnBakprogrammaBeheren()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Naam")] Bakprogramma bakprogramma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bakprogramma);
                await _context.SaveChangesAsync();
                return RedirectToAction("BakprogrammaBeheren");
            }
            return View("BakprogrammaBeheren");
        }

        // GET: Medewerkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bakprogramma == null)
            {
                return NotFound();
            }

            var bakprogramma = await _context.Bakprogramma.FindAsync(id);
            if (bakprogramma == null)
            {
                return NotFound();
            }
            return View(bakprogramma);
        }

        // POST: Medewerkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Naam")] Bakprogramma bakprogramma)
        {
            if (id != bakprogramma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bakprogramma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BakprogrammaExists(bakprogramma.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("BakprogrammaBeheren");
            }
            return View(bakprogramma);
        }
        private bool BakprogrammaExists(int id)
        {
            return (_context.Bakprogramma?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
