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
    public class DeleteModel : PageModel
    {
        private readonly ASP.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(ASP.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Boots = await _context.Bootses.FindAsync(id);

            if (Boots != null)
            {
                _context.Bootses.Remove(Boots);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
