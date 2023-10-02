using Microsoft.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        modelBuilder.Entity<Post>()
        .HasMany(p => p.Comments).WithOne(c => c.Post)
        .HasForeignKey(c => c.PostId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
          .HasOne(p => p.User)
          .WithMany(u => u.Posts)
          .HasForeignKey(p => p.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
          .HasOne(c => c.Post)
          .WithMany(p => p.Comments)
          .HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
          .HasOne(c => c.User)
          .WithMany(u => u.Comments)
          .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
          .HasMany(u => u.Posts)
          .WithOne(p => p.User)
          .HasForeignKey(p => p.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
          .HasMany(u => u.Comments)
          .WithOne(c => c.User)
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Post>()
            .Ignore(p => p.User);

        modelBuilder.Entity<Comment>()
            .Ignore(p => p.User);



    }

}
