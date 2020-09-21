using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


public class BloggingContext : DbContext
{
    public IConfiguration Configuration { get; }
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
    {

    }
    public DbSet<Author> Author { get; set; }
    public DbSet<BlogPost> BlogPost { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        
    }
    
    protected override void OnConfiguring (DbContextOptionsBuilder options)
        => options.UseNpgsql( "Host=localhost;Port=5432;Username=postgres;Password=CDpJOaCr1v0FQLzUSnVM;Database=rc-regman-co-creation;");
    

}

