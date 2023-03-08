using AutoMapper;

namespace Online_Discussion_Forum.Models
{
    public class Questions
    {
        public long  Id { get; set; }
        public string? title { get; set; } 
        public string? description { get; set; }
        public string? username { get; set; }
        public long? user_id { get; set; }
        public DateTime? update_date { get; set; }
    }

    public class Questions_DTO
    {
        public long Id { get; set; }
        public string? title { get; set; }
        public string? username { get; set; }
        public string? description { get; set; }
        public long? user_id { get; set; }
        public DateTime? update_date { get; set; }

        public List<Tags> tag { get; set; }
    }

    public class Question_Profile : Profile
    {
        public Question_Profile()
        {
            CreateMap<Questions, Questions_DTO>().ReverseMap();
        }
    }
}

