using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Models;
using marketioWebAPI.Data;

namespace marketioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingCategoriesController : ControllerBase
    {
        private readonly marketioContext _context;

        public ListingCategoriesController(marketioContext context)
        {
            _context = context;
        }

        // GET: api/ListingCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListingCategory>>> GetListingCategories()
        {
          if (_context.ListingCategories == null)
          {
              return NotFound();
          }
            return await _context.ListingCategories.ToListAsync();
        }

        // GET: api/ListingCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListingCategory>> GetListingCategory(int id)
        {
          if (_context.ListingCategories == null)
          {
              return NotFound();
          }
            var listingCategory = await _context.ListingCategories.FindAsync(id);

            if (listingCategory == null)
            {
                return NotFound();
            }

            return listingCategory;
        }

        // PUT: api/ListingCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListingCategory(int id, ListingCategory listingCategory)
        {
            if (id != listingCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(listingCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingCategoryExists(id))
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

        // POST: api/ListingCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListingCategory>> PostListingCategory(ListingCategory listingCategory)
        {
            if (_context.ListingCategories == null)
            {
                return Problem("Entity set 'marketioContext.ListingCategories'  is null.");
            }

            // check if category already exists
            if (_context.ListingCategories.Any(c => c.Name.ToLower() == listingCategory.Name.ToLower()))
            {
                return BadRequest("Category already exists.");
            }

            _context.ListingCategories.Add(listingCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListingCategory", new { id = listingCategory.Id }, listingCategory);
        }

        // DELETE: api/ListingCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListingCategory(int id)
        {
            if (_context.ListingCategories == null)
            {
                return NotFound();
            }
            var listingCategory = await _context.ListingCategories.FindAsync(id);
            if (listingCategory == null)
            {
                return NotFound();
            }

            _context.ListingCategories.Remove(listingCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListingCategoryExists(int id)
        {
            return (_context.ListingCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
