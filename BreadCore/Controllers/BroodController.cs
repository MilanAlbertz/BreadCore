using BreadCore.Data;
using BreadCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        public async Task<IActionResult> BroodAanmaken()
        { 
            return View();
        }
    }
}
