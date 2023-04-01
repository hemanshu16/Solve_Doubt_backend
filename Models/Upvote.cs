namespace Online_Discussion_Forum.Models
{
    public class Upvote
    {
        public long Id { get; set; }
        public long userid { get; set; }
        public long answerid { get; set; }
        public long questionid { get; set; }
    }


}
