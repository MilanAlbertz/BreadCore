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
    public class MedewerkersController : Controller
    {
        public int? bedienerNummer { get; set; }
        public int? filiaalNummer { get; set; }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,BedienerNr,Wachtwoord,Rol,FiliaalId")] Medewerker medewerker)
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
                return RedirectToAction("MedewerkersBeheren");
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
            return RedirectToAction("MedewerkersBeheren");
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
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(int bedienerNr, int wachtwoord, string returnUrl)
        {
            foreach (var medewerker in _context.Medewerker.Where(d => d.BedienerNr == bedienerNr))
            {
                if (medewerker.BedienerNr == bedienerNr & medewerker.Wachtwoord == wachtwoord)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("bedienerNr", bedienerNr.ToString()));
                    claims.Add(new Claim("wachtwoord", wachtwoord.ToString()));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, bedienerNr.ToString()));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return Redirect(returnUrl);
                }
            }
            TempData["Error"] = "Helaas. Bedienernummer of Wachtwoord is fout.";
            return View("Login");
        }
        [Authorize]
        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [Authorize]
        public IActionResult SysteemBeheerder(string returnUrl)
        {
            return View();
        }
        public async Task<IActionResult> MedewerkersBeheren(string returnUrl)
        {
            return _context.Medewerker != null ?
            View(await _context.Medewerker.ToListAsync()) :
            Problem("Entity set 'AppDbContext.Medewerker'  is null.");
        }
        [Authorize]
        public IActionResult Manager()
        {
            return View();
        }
    }
}
