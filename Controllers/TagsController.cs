using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Discussion_Forum.Models;

namespace Online_Discussion_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly DiscussionDbContext _context;

        public TagsController(DiscussionDbContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tags>>> GetTag_()
        {
          if (_context.Tag_ == null)
          {
              return NotFound();
          }
            return await _context.Tag_.ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tags>> GetTags(long id)
        {
          if (_context.Tag_ == null)
          {
              return NotFound();
          }
            var tags = await _context.Tag_.FindAsync(id);

            if (tags == null)
            {
                return NotFound();
            }

            return tags;
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTags(long id, Tags tags)
        {
            if (id != tags.Id)
            {
                return BadRequest();
            }

            _context.Entry(tags).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagsExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tags>> PostTags(Tags tags)
        {
          if (_context.Tag_ == null)
          {
              return Problem("Entity set 'DiscussionDbContext.Tag_'  is null.");
          }
            _context.Tag_.Add(tags);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTags", new { id = tags.Id }, tags);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTags(long id)
        {
            if (_context.Tag_ == null)
            {
                return NotFound();
            }
            var tags = await _context.Tag_.FindAsync(id);
            if (tags == null)
            {
                return NotFound();
            }

            _context.Tag_.Remove(tags);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagsExists(long id)
        {
            return (_context.Tag_?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
