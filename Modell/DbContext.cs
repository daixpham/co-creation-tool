using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
    {

    }
    public DbSet<Author> Author { get; set; }
    public DbSet<BlogPost> BlogPost { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        
    }

}

