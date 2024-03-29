﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Discussion_Forum.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.Intrinsics.X86;

namespace Online_Discussion_Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DiscussionDbContext _context;
        private readonly IConfiguration _Iconfiguraion;
        private readonly IMapper _mapper;
        public UsersController(DiscussionDbContext context, IConfiguration iconfiguraion, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _Iconfiguraion = iconfiguraion;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser_()
        {
          if (_context.User_ == null)
          {
              return NotFound();
          }
            return await _context.User_.ToListAsync();
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("byid")]
        public async Task<ActionResult<UserDto>> GetUser()
        {
          if (_context.User_ == null)
          {
              return NotFound();
          }
          long id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.User_.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var user_dto = _mapper.Map<UserDto>(user);

            return user_dto;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutUser( UserDto user1)
        {
            
            long id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.User_.FindAsync(id);
            user.Name = user1.Name;
            user.image_url = user1.image_url;
            user.about = user1.about;
            if (user == null)
            {
                return NotFound();
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        [Authorize]
        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UserDto user1)
        {
            long id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.User_.FindAsync(id);
            if(user == null)
            {
                return BadRequest();
            }
            if(!VerifyPasswordHash(user1.Name, user.passwordHash, user.passwordSalt))
            {
                return BadRequest("Old Password is Not Correct");
            }
            CreatePasswordHash(user1.Email, out byte[] passwordHash, out byte[] passwordSalt);
            _context.Update(user);
            user.passwordSalt = passwordSalt;
            user.passwordHash = passwordHash;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.User_ == null)
          {
              return Problem("Entity set 'DiscussionDbContext.User_'  is null.");
          }
            _context.User_.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.User_ == null)
            {
                return NotFound();
            }
            var user = await _context.User_.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User_.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.User_?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        private async Task<bool> UserExistswithemail(string email)
        {
            var user = await _context.User_.FirstOrDefaultAsync(e => e.Email == email);
            return user != null;
        }

        [HttpGet]
        [Route("UserName")]
        public async Task<ActionResult<User>> UserDetails()
        {
            
            User user;
            try
            {
                Request.Headers.TryGetValue("jwt", out var env);
                env = GetEmail(env);
                string email = env;
                user = _context.User_.FirstOrDefault(e => e.Email == email);
                if(user == null) { return BadRequest("Some Thing Went Wrong Please Try Again Later"); }
            }
            catch (Exception ex)
            {
                return BadRequest("Provide Authentication Key");
            }
            return user;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<long> Register(UserDto user, [FromHeader] string Password)
        {
            bool exist = await UserExistswithemail(user.Email);
            if(exist)
            {
                return -1;
            }
            User user1 = _mapper.Map<User>(user);
            CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
            user1.passwordSalt = passwordSalt;
            user1.passwordHash = passwordHash;
            _context.User_.Add(user1);
            await _context.SaveChangesAsync();

            return user1.Id;
        }
        
        [HttpPost]
        [Route("Login")]
        public async Task<String?> Login(LoginDetails details)
        {                       
            
            var user = await _context.User_.FirstOrDefaultAsync(e => e.Email == details.Email);
            if(user == null)
            {
                return "UserName or Password is Wrong";
            }
            else if (!VerifyPasswordHash(details.Password,  user.passwordHash, user.passwordSalt))
            {
                return "UserName or Password is Wrong";
            }
            return  CreateToken(user);
        }


        private String CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
            var appSettingToken = _Iconfiguraion.GetSection("AppSettings:Token").Value;
            if(appSettingToken == null)
            {
                throw new Exception("Appsetting Token is null");
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingToken));
            SigningCredentials signingCredentials = 
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string GetEmail(string token)
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
