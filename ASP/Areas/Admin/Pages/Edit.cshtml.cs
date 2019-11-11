using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.DAL.Data;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ASP.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ASP.DAL.Data.ApplicationDbContext _context;
        private IHostingEnvironment _environment;


        public EditModel(ASP.DAL.Data.ApplicationDbContext context, IHostingEnvironment env)
        {
            _environment = env; 
            _context = context;
        }

        [BindProperty]
        public Boots Boots { get; set; }

        [BindProperty] public IFormFile image { get; set; }

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
           ViewData["BootsGroupId"] = new SelectList(_context.BootsGroups, "BootsGroupId", "GroupName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string path = ""; 
            // предыдущее изображение
            string previousImage = String.IsNullOrEmpty(Boots.Image) ? "" : Boots.Image;
            if (image != null)
            { // новый файл изображения
              Boots.Image = Boots.BootsId +  Path.GetExtension(image.FileName); 
              // путь для нового файла изображения
              path = Path.Combine(_environment.WebRootPath,  "images", Boots.Image);
            } 



            _context.Attach(Boots).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                if (image != null)
                {
                    // если было предыдущее изображение
                    if (!String.IsNullOrEmpty(previousImage))
                    {
                        // если файл существует
                        var fileInfo = _environment.WebRootFileProvider.GetFileInfo("/Images/" + previousImage);
                        if (fileInfo.Exists)
                        {
                            var oldPath = Path.Combine(_environment.WebRootPath, "images", previousImage);
                            // удалить предыдущее изображение
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        // сохранить новое изображение
                        await image.CopyToAsync(stream);
                    }

             
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BootsExists(Boots.BootsId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BootsExists(Boots.BootsId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        private bool BootsExists(int id)
        {
            return _context.Bootses.Any(e => e.BootsId == id);
        }
    }
}
