using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
public class AuthorService
{

    private readonly BloggingContext _context;

    public AuthorService(BloggingContext context)
    {
        _context = context;
    }
    public List<Author> GetAllAuthors()
    {
        return _context.Author.ToList();
    }
    public Author GetAuthorById(int id)
    {
        var author = _context.Author.Find(id);
        var blogs = new List<BlogPost>(); 
        blogs = _context.BlogPost.Where(b => b.AuthorId == id).ToList();
        author.Blogs = blogs;
        return author;
    }



    public Author CreateAuthor(Author _author)
    {
        _context.Author.Add(_author);
        _context.SaveChanges();
        return _author;
    }

    public string UpdateAuthor(Author authorInput){
        var result = "";
        _context.Author.Update(authorInput);
        _context.SaveChanges(); 
        return result;
    }
}