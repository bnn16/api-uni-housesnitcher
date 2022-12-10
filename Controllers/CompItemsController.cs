using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompApi.Models;

namespace CompApi.Controllers
{
    [Route("api/CompItems")]
    [ApiController]
    public class CompItemsController : ControllerBase
    {
        private readonly CompContext _context;

        public CompItemsController(CompContext context)
        {
            _context = context;
        }

        // GET: api/CompItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompItem>>> GetCompItems()
        {
          if (_context.CompItems == null)
          {
              return NotFound();
          }
            return await _context.CompItems.ToListAsync();
        }

        // GET: api/CompItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompItem>> GetCompItem(long id)
        {
          if (_context.CompItems == null)
          {
              return NotFound();
          }
            var compItem = await _context.CompItems.FindAsync(id);

            if (compItem == null)
            {
                return NotFound();
            }

            return compItem;
        }
        // POST: api/CompItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompItem>> PostCompItem(CompItem compItem)
        {
          if (_context.CompItems == null)
          {
              return Problem("Entity set 'CompContext.CompItems'  is null.");
          }
            _context.CompItems.Add(compItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompItem), new { id = compItem.Id }, compItem);
        }

        // DELETE: api/CompItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompItem(long id)
        {
            if (_context.CompItems == null)
            {
                return NotFound();
            }
            var compItem = await _context.CompItems.FindAsync(id);
            if (compItem == null)
            {
                return NotFound();
            }

            _context.CompItems.Remove(compItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompItemExists(long id)
        {
            return (_context.CompItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
