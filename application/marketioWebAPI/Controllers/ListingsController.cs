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
                var tags = await _context.ListingTags
                    .Where(lt => lt.ListingId == listings[i].Id)
                    .ToListAsync();

                var images = await _context.ListingImages
                    .Where(li => li.ListingId == listings[i].Id)
                    .ToListAsync();

                var user = await _context.Users
                    .Where(u => u.Id == listings[i].UserId)
                    .FirstOrDefaultAsync();

                var category = await _context.ListingCategories
                    .Where(lc => lc.Id == listings[i].CategoryId)
                    .FirstOrDefaultAsync();

                listings[i].ListingTags = tags;
                listings[i].ListingImages = images;
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
            // add listing
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            // get listing that was added
            var addedListing = await _context.Listings
                .Where(u => u.Id == listing.Id)
                .FirstOrDefaultAsync();

            // add tags
            foreach (var tag in listing.TagString)
            {
                _context.ListingTags.Add(new ListingTag()
                {
                    Name = tag,
                    ListingId = addedListing.Id,
                });
            }

            // add images
            for (int i = 0; i < listing.ImagesBase64.Length; i++)
            {
                _context.ListingImages.Add(new ListingImage()
                {
                    ListingId = addedListing.Id,
                    ImageAsBase64 = listing.ImagesBase64[i],
                    IsPrimaryImage = i == 0 ? true : false,
                    DateAdded = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
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
