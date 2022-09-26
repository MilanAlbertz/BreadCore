using BreadCore.Data;
using BreadCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using NuGet.Protocol;

namespace BreadCore.Controllers
{
    public class BroodController : Controller
    {
        private readonly AppDbContext database;
        public BroodController(AppDbContext database)
        {
            this.database = database;
        }

        public async Task<IActionResult> GebakkenBroodInvoeren(int? id)
        {
            if (id == null || database.Bakprogramma == null)
            {
                return NotFound();
            }

            var bakprogramma = await database.Bakprogramma
                .Include(k => k.BroodTypes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bakprogramma == null)
            {
                return NotFound();
            }
            return View(bakprogramma);
        }
        public IActionResult GebakkenBakprogrammaKiezen()
        {
            int aantalBakprogrammas = database.Bakprogramma.Count();
            ViewBag.Message = aantalBakprogrammas.ToString();
            return View();
        }
        public async Task<IActionResult> BroodOpsplitsen(List<int> BroodTypeID, List<int> HoeveelheidGebakken, string BedienerNr, string Wachtwoord, int Bakprogramma)
        {
            int loop = BroodTypeID.Count();
            for (int i = 0; i < loop; i++)
            {
                string alleenBedienerNr = BedienerNr.Replace("bedienerNr: ", " ");
                int IntBedienerNr = Int16.Parse(alleenBedienerNr);
                string alleenWachtwoord = Wachtwoord.Replace("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: ", " ");
                int IntWachtwoord = Int16.Parse(alleenWachtwoord);
                var werkendFiliaal = database.Medewerker
                    .Where(d => d.BedienerNr == IntBedienerNr)
                    .Where(d => d.Wachtwoord == IntWachtwoord).FirstOrDefault();
                var record = database.Brood
                    .Where(d => d.TijdGebakken == DateTime.Now.Date)
                    .Where(d => d.BroodTypeID == BroodTypeID[i])
                    .Where(d => d.GebakkenFiliaalId == werkendFiliaal.FiliaalId);
                if (record.Count() > 0)
                {
                    var recordUpdate = record.FirstOrDefault();
                    recordUpdate.HoeveelheidGebakken += HoeveelheidGebakken[i];
                    database.Brood.Update(recordUpdate);
                    await database.SaveChangesAsync();
                }
                else
                {
                    Brood nieuwBrood = new Brood();
                    nieuwBrood.BroodTypeID = BroodTypeID[i];
                    nieuwBrood.HoeveelheidGebakken = HoeveelheidGebakken[i];
                    nieuwBrood.GebakkenFiliaalId = werkendFiliaal.FiliaalId;
                    nieuwBrood.MedewerkerId = IntBedienerNr;
                    nieuwBrood.TijdGebakken = DateTime.Now.Date;
                    nieuwBrood.Bakprogramma = Bakprogramma;
                    if (ModelState.IsValid)
                    {
                        database.Brood.Add(nieuwBrood);
                        await database.SaveChangesAsync();
                    }
                }
            }
            int aantalBakprogrammas = database.Bakprogramma.Count();
            ViewBag.Message = aantalBakprogrammas.ToString();
            return View("GebakkenBakprogrammaKiezen");
        }

        public async Task<IActionResult> DervingBroodInvoeren(int? id)
        {
            if (id == null || database.Bakprogramma == null)
            {
                return NotFound();
            }

            var bakprogramma = await database.Bakprogramma
                .Include(k => k.BroodTypes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bakprogramma == null)
            {
                return NotFound();
            }
            return View(bakprogramma);
        }
        public IActionResult DervingBakprogrammaKiezen()
        {
            int aantalBakprogrammas = database.Bakprogramma.Count();
            ViewBag.Message = aantalBakprogrammas.ToString();
            return View();
        }
        public async Task<IActionResult> DervingBroodOpsplitsen(List<int> BroodTypeID, List<int> HoeveelheidDerving, int Bakprogramma, string BedienerNr, string Wachtwoord)
        {
            string alleenBedienerNr = BedienerNr.Replace("bedienerNr: ", " ");
            int IntBedienerNr = Int16.Parse(alleenBedienerNr);
            string alleenWachtwoord = Wachtwoord.Replace("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: ", " ");
            int IntWachtwoord = Int16.Parse(alleenWachtwoord);
            var werkendFiliaal = database.Medewerker
                .Where(d => d.BedienerNr == IntBedienerNr)
                .Where(d => d.Wachtwoord == IntWachtwoord).FirstOrDefault();

            foreach (var brood in database.Brood
                .Where(d => d.TijdGebakken == DateTime.Now.Date)
                .Where(d => d.Bakprogramma == Bakprogramma)
                .Where(d => d.GebakkenFiliaalId == werkendFiliaal.FiliaalId))
            {
                if (ModelState.IsValid && brood.HoeveelheidDerving == null)
                {
                    brood.HoeveelheidDerving = HoeveelheidDerving[0];
                    database.Brood.Update(brood);
                    HoeveelheidDerving.Remove(HoeveelheidDerving[0]);
                }
            }
            await database.SaveChangesAsync();
            int aantalBakprogrammas = database.Bakprogramma.Count();
            ViewBag.Message = aantalBakprogrammas.ToString();
            return View("DervingBakprogrammaKiezen");
        }
        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult FiliaalKiezen()
        {
            return View();
        }

        public IActionResult GegevensIndividueelFiliaal(string FiliaalNaam, DateTime EersteDatum, DateTime TweedeDatum)
        {
            var Filiaal = database.Filiaal
            .Where(d => d.FiliaalNaam == FiliaalNaam).FirstOrDefault();
            var FiliaalId = Filiaal.FiliaalId;
            List<Brood> broden = new List<Brood>();
            foreach (var brood in database.Brood
                .Where(d => d.GebakkenFiliaalId == FiliaalId)
                .Where(d => d.TijdGebakken >= EersteDatum)
                .Where(d => d.TijdGebakken <= TweedeDatum))
                
            {
                broden.Add(brood);
            }
            return View(broden);
        }

        public async Task<IActionResult> BroodBeheren()
        {
            return database.BroodType != null ?
            View(await database.BroodType.ToListAsync()) :
            Problem("Entity set 'AppDbContext.BroodType'  is null.");
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medewerkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Code,BakprogrammaId")] BroodType broodType)
        {
            if (ModelState.IsValid)
            {
                database.Add(broodType);
                await database.SaveChangesAsync();
                return RedirectToAction("BroodBeheren");
            }
            return View("BroodBeheren");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || database.BroodType == null)
            {
                return NotFound();
            }

            var broodType = await database.BroodType.FindAsync(id);
            if (broodType == null)
            {
                return NotFound();
            }
            return View(broodType);
        }

        // POST: Medewerkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BroodTypeID,Type,Code,BakprogrammaId")] BroodType broodType)
        {
            if (id != broodType.BroodTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    database.Update(broodType);
                    await database.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BroodTypeExists(broodType.BroodTypeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Broodbeheren");
            }
            return View("BroodBeheren");
        }
        private bool BroodTypeExists(int id)
        {
            return (database.BroodType?.Any(e => e.BroodTypeID == id)).GetValueOrDefault();
        }
            
        public IActionResult DatumKiezenAlleFilialen()
        {
            return View();
        }
        public IActionResult GegevensAlleFilialen(DateTime EersteDatum, DateTime TweedeDatum)
        {
            List<Brood> broden = new List<Brood>();
            foreach (var brood in database.Brood
                .Where(d => d.TijdGebakken >= EersteDatum)
                .Where(d => d.TijdGebakken <= TweedeDatum))
            {
                broden.Add(brood);
            }
            return View(broden);
        }
    }
}
