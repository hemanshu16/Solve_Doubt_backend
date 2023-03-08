namespace Online_Discussion_Forum.Models
{
    public class Answers
    {
        public long Id { get; set; }
        public long? Question_id { get; set; }
        public long? User_id { get; set; }
        public string? username { get; set; }
        public string? Answer { get; set; }
        public int? Upvotes { get; set; }

        public DateTime? Update_date { get; set; } 
    }
}
