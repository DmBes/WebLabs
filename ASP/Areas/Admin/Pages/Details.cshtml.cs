using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.DAL.Data;
using ASP.DAL.Entities;

namespace ASP.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ASP.DAL.Data.ApplicationDbContext _context;

        public DetailsModel(ASP.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Boots Boots { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Boots = await _context.Bootses
                .Include(b => b.Group).FirstOrDefaultAsync(m => m.BootsId == id);

            if (Boots == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
