using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BreadCore.Data;
using BreadCore.Models;

namespace BreadCore.Controllers
{
    public class IndexModel : PageModel
    {
        private readonly BreadCore.Data.AppDbContext _context;

        public IndexModel(BreadCore.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Medewerker> Medewerker { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Medewerker != null)
            {
                Medewerker = await _context.Medewerker.ToListAsync();
            }
        }
    }
}
