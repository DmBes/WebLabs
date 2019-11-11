using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.DAL.Data;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ASP.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ASP.DAL.Data.ApplicationDbContext _context;
        private IHostingEnvironment _environment;


        public CreateModel(ASP.DAL.Data.ApplicationDbContext context,
            IHostingEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
        ViewData["BootsGroupId"] = new SelectList(_context.BootsGroups, "BootsGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public Boots Boots { get; set; }


        [BindProperty] 
        public IFormFile image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bootses.Add(Boots);
            await _context.SaveChangesAsync();

            if (image != null)
            { Boots.Image = Boots.BootsId + Path.GetExtension(image.FileName); 
                var path = Path.Combine(_environment.WebRootPath, "images", Boots.Image);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }; 
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}