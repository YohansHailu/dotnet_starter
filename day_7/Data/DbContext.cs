using Microsoft.EntityFrameworkCore;
public class BlogAppContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Database=blogapp;Username=postgres;Password=postgres");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("----------------");
        Console.WriteLine("------model created----------");
        Console.WriteLine("----------------");
    }
}
