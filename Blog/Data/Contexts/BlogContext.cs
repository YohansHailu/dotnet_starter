using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Context;
public class BlogDbContext : DbContext
{

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    // add constructure with option builder
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
          .HasMany(p => p.Comments).WithOne(c => c.Post)
          .HasForeignKey(c => c.PostId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
          .HasOne(c => c.Post)
          .WithMany(p => p.Comments)
          .HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);
    }
}
