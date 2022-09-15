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

namespace BreadCore.Controllers
{
    public class MedewerkersController : Controller
    {
        private readonly AppDbContext _context;

        public MedewerkersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Medewerkers

        // GET: Medewerkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medewerker == null)
            {
                return NotFound();
            }

            var medewerker = await _context.Medewerker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medewerker == null)
            {
                return NotFound();
            }

            return View(medewerker);
        }

        // GET: Medewerkers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medewerkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BedienerNr,Wachtwoord,Rol,FiliaalId")] Medewerker medewerker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medewerker);
                await _context.SaveChangesAsync();
                return RedirectToAction("MedewerkersBeheren");
            }
            return View("MedewerkersBeheren");
        }

        // GET: Medewerkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medewerker == null)
            {
                return NotFound();
            }

            var medewerker = await _context.Medewerker.FindAsync(id);
            if (medewerker == null)
            {
                return NotFound();
            }
            return View(medewerker);
        }

        // POST: Medewerkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BedienerNr,Wachtwoord,Rol")] Medewerker medewerker)
        {
            if (id != medewerker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medewerker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedewerkerExists(medewerker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medewerker);
        }

        // GET: Medewerkers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medewerker == null)
            {
                return NotFound();
            }

            var medewerker = await _context.Medewerker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medewerker == null)
            {
                return NotFound();
            }

            return View(medewerker);
        }

        // POST: Medewerkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medewerker == null)
            {
                return Problem("Entity set 'AppDbContext.Medewerker'  is null.");
            }
            var medewerker = await _context.Medewerker.FindAsync(id);
            if (medewerker != null)
            {
                _context.Medewerker.Remove(medewerker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedewerkerExists(int id)
        {
            return (_context.Medewerker?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [Authorize]
        public IActionResult Medewerker()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(int bedienerNr, int wachtwoord, string returnUrl)
        {
            foreach (var medewerker in _context.Medewerker)
            {
                if (medewerker.BedienerNr == bedienerNr & medewerker.Wachtwoord == wachtwoord)
                {
                    if (medewerker.Rol == "Medewerker")
                    {
                        return View("Medewerker");
                    }
                    if (medewerker.Rol == "Manager")
                    {
                        return View("Manager");
                    }
                    if (medewerker.Rol == "Systeem Beheerder")
                    {
                        return View("SysteemBeheerder");
                    }
                }
            }
            TempData["Error"] = "Error. BedienerNr of Wachtwoord is fout.";
            return View("login");
        } 

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        public async Task<IActionResult> MedewerkersBeheren(string returnUrl)
        {
            return _context.Medewerker != null ?
                        View(await _context.Medewerker.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Medewerker'  is null.");
        }
    }
}
