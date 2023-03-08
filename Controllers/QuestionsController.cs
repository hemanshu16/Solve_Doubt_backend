using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_Discussion_Forum.Models;

namespace Online_Discussion_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DiscussionDbContext _context;
        private readonly IMapper _mapper;
        public QuestionsController(DiscussionDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions_()
        {
          if (_context.Questions_ == null)
          {
              return NotFound();
          }
            return await _context.Questions_.OrderBy(x => x.update_date).Reverse().ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions_DTO>> GetQuestions(long id)
        {
          if (await _context.Questions_.FindAsync(id) == null)
          {
              return NotFound();
          }
            var questions = await _context.Questions_.FindAsync(id);
            var questions_dto = _mapper.Map<Questions_DTO>(questions);
            
            var tags = _context.Tag_.Where(e => e.question_id == id)?.ToList() ;
            if(tags != null)
            {
                questions_dto.tag = tags;
            }
            if (questions == null)
            {
                return NotFound();
            }

            return questions_dto;
        }

        [HttpGet]
        [Route("GetQuestions")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            try
            {
                Request.Headers.TryGetValue("jwt", out var env);
                env = GetName(env);

                string email = env;
                var user = _context.User_.FirstOrDefault(e => e.Email == email);
                var questions = _context.Questions_.Where(e => e.user_id == user.Id).ToList();
                return questions;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(long id, Questions questions)
        {
            if (id != questions.Id)
            {
                return BadRequest();
            }
             Request.Headers.TryGetValue("jwt", out var env);
            env = GetName(env);
            
            string email = env;
            var user = _context.User_.FirstOrDefault(e => e.Email == email);
            questions.user_id = user.Id;
            questions.username = user.Name;
            questions.update_date = DateTime.Now;
            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
          if (_context.Questions_ == null)
          {
              return Problem("Entity set 'DiscussionDbContext.Questions_'  is null.");
          }
            _context.Questions_.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestions", new { id = questions.Id }, questions);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestions(long id)
        {
            if (_context.Questions_ == null)
            {
                return NotFound();
            }
            var questions = await _context.Questions_.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions_.Remove(questions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionsExists(long id)
        {
            return (_context.Questions_?.Any(e => e.Id == id)).GetValueOrDefault();
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
