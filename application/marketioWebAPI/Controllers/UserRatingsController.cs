using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Models;
using marketioWebAPI.Data;
using Microsoft.AspNetCore.Identity;

namespace marketioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingsController : ControllerBase
    {
        private readonly marketioContext _context;

        public UserRatingsController(marketioContext context)
        {
            _context = context;
        }

        // GET: api/UserRatings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRating>>> GetUserRatings()
        {
          if (_context.UserRatings == null)
          {
              return NotFound();
          }
            return await _context.UserRatings.ToListAsync();
        }

        // GET: api/UserRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRating>> GetUserRating(int id)
        {
          if (_context.UserRatings == null)
          {
              return NotFound();
          }
            var userRating = await _context.UserRatings.FindAsync(id);

            if (userRating == null)
            {
                return NotFound();
            }

            return userRating;
        }

        // PUT: api/UserRatings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRating(int id, UserRating userRating)
        {
            if (id != userRating.Id)
            {
                return BadRequest();
            }

            _context.Entry(userRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRatingExists(id))
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

        // POST: api/UserRatings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRating>> PostUserRating(UserRating userRating)
        {
          if (_context.UserRatings == null)
          {
              return Problem("Entity set 'marketioContext.UserRatings'  is null.");
          }

            userRating.User = null;

            _context.UserRatings.Add(userRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserRatings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRating(int id)
        {
            if (_context.UserRatings == null)
            {
                return NotFound();
            }
            var userRating = await _context.UserRatings.FindAsync(id);
            if (userRating == null)
            {
                return NotFound();
            }

            _context.UserRatings.Remove(userRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRatingExists(int id)
        {
            return (_context.UserRatings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
