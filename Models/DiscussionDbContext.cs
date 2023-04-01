using Microsoft.EntityFrameworkCore;

namespace Online_Discussion_Forum.Models
{
    public class DiscussionDbContext : DbContext
    {
        public DiscussionDbContext(DbContextOptions<DiscussionDbContext> options) : base(options) { }
        public DbSet<Answers> Answers_ { get; set; } = null!;
        public DbSet<Questions> Questions_ { get; set; } = null!;
        public DbSet<User> User_ { get; set; } = null!;
        public DbSet<Tags> Tag_ { get; set; } = null!;

        public DbSet<Upvote> Upvote_ { get; set; } = null!;
    }

}
