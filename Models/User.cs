using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Discussion_Forum.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Userid { get; set; } = null;
        public string Email { get; set; } = "Email";
        public string? Name { get; set; } = "Name";
        public DateTime? Date { get; set; } = DateTime.MinValue;
        public byte[] passwordHash { get; set; } = null;
        public byte[] passwordSalt { get; set; } = null;
     }

     public class UserDto
    {
        public long Id { get; set; }
        public string? Userid { get; set; } = null;
        public string Email { get; set; } = "Email";
        public string? Name { get; set; } = "Name";
        public DateTime? Date { get; set; } = DateTime.MinValue;

    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();
        }
    }

    public class LoginDetails
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
