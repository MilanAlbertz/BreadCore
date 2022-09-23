using BreadCore.Data;
using BreadCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            //bepaal programma
            //maak lijst met brood entiteiten
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
            //geef lijst door aan view
            return View(bakprogramma);
        }
        public IActionResult GebakkenBakprogrammaKiezen()
        {
            return View();
        }
        public async Task<IActionResult> BroodOpsplitsen(List<int> BroodTypeID, List<int> HoeveelheidGebakken)
        {
            for (int i = 0; i < BroodTypeID.Count; i++)
            {
                Brood brood = new Brood();
                brood.BroodTypeID = BroodTypeID[i];
                brood.HoeveelheidGebakken = HoeveelheidGebakken[i];
                brood.GebakkenFiliaalId = 1;
                brood.MedewerkerId = 2;
                brood.TijdGebakken = DateTime.Now;
                await BroodAanmaken(brood);
            }
            return View("GebakkenBakprogrammaKiezen");
        }

        public async Task<IActionResult> BroodAanmaken(Brood brood)
        {
            if (ModelState.IsValid)
            {
                database.Brood.Add(brood);
                await database.SaveChangesAsync();
                return View();
            }
            return View();
        }
    }
}
