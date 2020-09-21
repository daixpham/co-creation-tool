using System.Collections.Generic;
using System;
using System.Linq;
public class AuthorRepository {
  
    //private readonly List<BlogPost> posts = new List<BlogPost>();

    public List<Author> GetAllAuthors()
    {
        return null;
    }
    public Author GetAuthorById(int id)
    {
        return null;
    }
    public List<BlogPost> GetPostsByAuthor(int id)
    {
        return null;
    }

    public Author CreateAuthor(BloggingContext _context , Author _author){
        _context.Author.Add(_author);
        return _author;
    }
}