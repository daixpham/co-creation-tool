using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL.Types;
public class BlogService
{
    private readonly BloggingContext _context;

    public BlogService(BloggingContext context)
    {
        _context = context;
    }
    public List<BlogPost> GetAllBlogs()
    {
        var results = new List<BlogPost>(_context.BlogPost.ToList());
        for (int i = 0; i < results.Count; i++)
        {
            results[i].Author = _context.Author.Find(results[i].AuthorId);
        }
        return results;
    }
    public BlogPost GetBlogById(int id)
    {
        var result = _context.BlogPost.Find(id);
        var author = _context.Author.Find(result.AuthorId);
        result.Author = author;
        return result;
    }

    public BlogPost CreateBlogPost(BlogPost blogPost)
    {
        _context.BlogPost.Add(blogPost);
        _context.SaveChanges();
        return blogPost;
    }

    public string DeleteBlog(int Id)
    {
        var result = "Done!";
        var blog = _context.BlogPost.FirstOrDefault(blog => blog.Id == Id);
        if (blog == null)
        {
            result = $"Blog mit dem ID {Id} nicht gefunden";
        }
        else
        {
            _context.BlogPost.Remove(blog);
            _context.SaveChanges();
        }
        return result;
    }
}