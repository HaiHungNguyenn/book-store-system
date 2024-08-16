using BMS.Services.BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Services.BookAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext() { }
    
    public DbSet<Book> Books { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("Book").HasKey(b => b.BookId);
        base.OnModelCreating(modelBuilder);
    }
}