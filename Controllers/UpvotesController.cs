using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Online_Discussion_Forum.Models;

namespace Online_Discussion_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpvotesController : ControllerBase
    {
        private readonly DiscussionDbContext _context;


        public UpvotesController(DiscussionDbContext context)
        {
            _context = context;
        }

        // GET: api/Upvotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Upvote>>> GetUpvote_()
        {
          if (_context.Upvote_ == null)
          {
              return NotFound();
          }
            return await _context.Upvote_.ToListAsync();
        }

        // GET: api/Upvotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Upvote>> GetUpvote(long id)
        {
          if (_context.Upvote_ == null)
          {
              return NotFound();
          }
            var upvote = await _context.Upvote_.FindAsync(id);

            if (upvote == null)
            {
                return NotFound();
            }

            return upvote;
        }

        // PUT: api/Upvotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpvote(long id, Upvote upvote)
        {
            if (id != upvote.Id)
            {
                return BadRequest();
            }

            _context.Entry(upvote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpvoteExists(id))
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

        // POST: api/Upvotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Upvote>> PostUpvote(Upvote upvote)
        {
          if (_context.Upvote_ == null)
          {
              return Problem("Entity set 'DiscussionDbContext.Upvote_'  is null.");
          }
            _context.Upvote_.Add(upvote);
            upvote.userid = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var answer = await _context.Answers_.FindAsync
                (upvote.answerid);
            if(answer == null)
            {
                return BadRequest();
            }
            _context.Answers_.Update(answer); 
            if(answer.Upvotes == null)
            {
                answer.Upvotes = 1;
            }
            else
            {
                answer.Upvotes = answer.Upvotes + 1;
            }
            await _context.SaveChangesAsync();
            
           
            return CreatedAtAction("GetUpvote", new { id = upvote.Id }, upvote);
        }

        // DELETE: api/Upvotes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUpvote(long id)
        {
            if (_context.Upvote_ == null)
            {
                return NotFound();
            }
            var upvote =  _context.Upvote_.Where(e => e.answerid == id).ToList();
            if (upvote == null && upvote.Count == 1)
            {
                return NotFound();
            }


            _context.Upvote_.Remove(upvote[0]);
            var answer = await _context.Answers_.FindAsync(upvote[0].answerid);
            if (answer == null)
            {
                return BadRequest();
            }
            _context.Answers_.Update(answer);
            if (answer.Upvotes == null || answer.Upvotes == 0)
            {
                answer.Upvotes = 0;
            }
            else
            { 
                answer.Upvotes = answer.Upvotes - 1;
            }
            await _context.SaveChangesAsync();
            

            return NoContent();
        }

        private bool UpvoteExists(long id)
        {
            return (_context.Upvote_?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
