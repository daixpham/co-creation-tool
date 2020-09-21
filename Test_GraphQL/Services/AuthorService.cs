using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
public class AuthorService
{

    private readonly BloggingContext _context;
    private BlogService _blogService;
    public GraphqlErrors _errors;

    public AuthorService(BloggingContext context, BlogService blogService, GraphqlErrors errors)
    {
        _context = context;
        _blogService = blogService;
        _errors = errors;
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

    public string UpdateAuthor(Author authorInput, List<int> authorBlogPostList)
    {
        var result = "Done!";
        var author = _context.Author.FirstOrDefault(a => a.Id == authorInput.Id);
        if (author == null)
        {
            throw new ArgumentException($"Nutzer mit der ID-{authorInput.Id} wurde nicht gefunden");
           //result = $"Nutzer mit der ID-{authorInput.Id} wurde nicht gefunden";
        }
        else
        {

            _context.Entry(author).State = EntityState.Detached;
            _context.Author.Update(authorInput);
            if (authorBlogPostList?.Count > 0)
            {
                UpdateAuthorOfBlogPost(authorInput.Id, authorBlogPostList);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }
        }

        return result;
    }

    public void UpdateAuthorOfBlogPost(int authorId, List<int> authorBlogPostList)
    {

        for (int i = 0; i < authorBlogPostList?.Count; i++)
        {
            var blogPost = _context.BlogPost.Find(authorBlogPostList[i]);
            if (blogPost == null)
            {
                _errors.Errors.Add("Post wurde nicht gefunden");
            }
            else
            {
                blogPost.AuthorId = authorId;
                _blogService.UpdateBlogPost(blogPost);
            }
        }
    }
}