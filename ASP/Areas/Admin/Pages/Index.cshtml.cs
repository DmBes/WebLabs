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
    public class IndexModel : PageModel
    {
        private readonly ASP.DAL.Data.ApplicationDbContext _context;

        public IndexModel(ASP.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Boots> Boots { get;set; }

        public async Task OnGetAsync()
        {
            Boots = await _context.Bootses
                .Include(b => b.Group).ToListAsync();
        }
    }
}
