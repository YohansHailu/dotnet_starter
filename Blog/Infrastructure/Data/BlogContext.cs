using Blog.Data.Configurations;
using Blog.Models;

namespace Blog.Context;
public class BlogDbContext : DbContext
{


    public DbSet<User> User { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    // add constructure with option builder
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    // add configuration from the other files
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
    }


}
