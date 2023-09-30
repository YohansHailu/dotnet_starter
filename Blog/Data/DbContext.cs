using Microsoft.EntityFrameworkCore;

public class BlogDbContext : DbContext
{

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blog_database.db");
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
          .HasForeignKey(c => c.PostId)
          .OnDelete(DeleteBehavior.NoAction);
    }
}
