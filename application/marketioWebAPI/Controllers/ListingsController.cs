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
    public class ListingsController : ControllerBase
    {
        private readonly marketioContext _context;

        public ListingsController(marketioContext context)
        {
            _context = context;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listings = await _context.Listings
                .ToListAsync();

            //add images and tags user and category
            for (int i = 0; i < listings.Count; i++)
            {
                var user = await _context.Users
                    .Where(u => u.Id == listings[i].UserId)
                    .FirstOrDefaultAsync();

                // map tagstring to tags array
                if (listings[i].TagString != null)
                    listings[i].Tags = listings[i].TagString.Split(' ');

                var category = await _context.ListingCategories
                    .Where(lc => lc.Id == listings[i].CategoryId)
                    .FirstOrDefaultAsync();

                listings[i].ImagesBase64 = 
                    await _context.ListingImages.Where(li => li.ListingId == listings[i].Id).ToListAsync();
                //listings[i].ListingImages = images;
            }


            //var listings = await _context.Listings.ToListAsync();

            return listings;
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(int id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FindAsync(id);

            if (listing == null)
            {
                return NotFound();
            }

            return listing;
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(Listing listing)
        {
          if (_context.Listings == null)
          {
              return Problem("Entity set 'marketioContext.Listings'  is null.");
          }


            // ignore creating new category and user
            _context.ListingCategories.Attach(listing.Category);
            _context.Users.Attach(listing.User);

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            // add images
            if (listing.ImagesBase64 != null)
            {
                for (int i = 0; i < listing.ImagesBase64.Count; i++)
                {
                    listing.ImagesBase64[i].ListingId = listing.Id;
                    _context.Add(listing.ImagesBase64[i]);
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetListing", new { id = listing.Id }, listing);
        }

        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListingExists(int id)
        {
            return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
