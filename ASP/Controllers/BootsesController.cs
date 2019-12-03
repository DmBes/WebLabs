using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.DAL.Data;
using ASP.DAL.Entities;

namespace ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootsesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BootsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bootses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boots>>> GetBootses(int? group)
        {
            return await _context.Bootses.Where(x=> !group.HasValue||x.BootsGroupId.Equals(group.Value) ).ToListAsync();
        }

        // GET: api/Bootses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Boots>> GetBoots(int id)
        {
            var boots = await _context.Bootses.FindAsync(id);

            if (boots == null)
            {
                return NotFound();
            }

            return boots;
        }

        // PUT: api/Bootses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoots(int id, Boots boots)
        {
            if (id != boots.BootsId)
            {
                return BadRequest();
            }

            _context.Entry(boots).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BootsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bootses
        [HttpPost]
        public async Task<ActionResult<Boots>> PostBoots(Boots boots)
        {
            _context.Bootses.Add(boots);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoots", new { id = boots.BootsId }, boots);
        }

        // DELETE: api/Bootses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Boots>> DeleteBoots(int id)
        {
            var boots = await _context.Bootses.FindAsync(id);
            if (boots == null)
            {
                return NotFound();
            }

            _context.Bootses.Remove(boots);
            await _context.SaveChangesAsync();

            return boots;
        }

        private bool BootsExists(int id)
        {
            return _context.Bootses.Any(e => e.BootsId == id);
        }
    }
}
