using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_Discussion_Forum.Models;

namespace Online_Discussion_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly DiscussionDbContext _context;

        public AnswersController(DiscussionDbContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answers>>> GetAnswers_()
        {
          if (_context.Answers_ == null)
          {
              return NotFound();
          }
            return await _context.Answers_.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answers>> GetAnswers(long id)
        {
          if (_context.Answers_ == null)
          {
              return NotFound();
          }
            var answers = await _context.Answers_.FindAsync(id);

            if (answers == null)
            {
                return NotFound();
            }

            return answers;
        }

        [HttpGet]
        [Route("GetAnswers")]
        public async Task<ActionResult<IEnumerable<Answers>>> GetAnswersBYId(long id)
        {
            try
            {
               
                var answers = _context.Answers_.Where(e => e.Question_id == id).ToList();
                return answers;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswers(long id, Answers answers)
        {
            if (id != answers.Id)
            {
                return BadRequest();
            }

            _context.Entry(answers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswersExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Answers>> PostAnswers(Answers answers)
        {
          if (_context.Answers_ == null)
          {
              return Problem("Entity set 'DiscussionDbContext.Answers_'  is null.");
          }
            answers.Update_date = DateTime.Now;
            try
            {
                Request.Headers.TryGetValue("jwt", out var env);
                env = GetName(env);

                string email = env;
                var user = _context.User_.FirstOrDefault(e => e.Email == email);
                answers.username = user.Name;
                answers.User_id = user.Id;
                _context.Answers_.Add(answers);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAnswers", new { id = answers.Id }, answers);
            }
            catch (DbUpdateConcurrencyException)
            {
                return  NoContent();
            }
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswers(long id)
        {
            if (_context.Answers_ == null)
            {
                return NotFound();
            }
            var answers = await _context.Answers_.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }

            _context.Answers_.Remove(answers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswersExists(long id)
        {
            return (_context.Answers_?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string GetName(string token)
        {
            string secret = "This is my best Key";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Identity.Name;
        }
    }
}
