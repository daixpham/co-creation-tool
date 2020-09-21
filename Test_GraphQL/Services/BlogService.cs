using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL.Types;

public interface IBlogService{
    IObservable<BlogPost> GetLastestPostedBlog{get;}
    ConcurrentStack<BlogPost> AllBlogPost {get;}
}

public class BlogService : IBlogService
{
    private readonly BloggingContext _context;
    public  IObservable<BlogPost> GetLastestPostedBlog => this._blogPostStream.AsObservable();
    private readonly ISubject<List<BlogPost>> _allBlogPostStream = new ReplaySubject<List<BlogPost>>(1);
    private readonly Subject<BlogPost> _blogPostStream;
    public ConcurrentStack<BlogPost> AllBlogPost {get;}
    public BlogService(BloggingContext context)
    {
        AllBlogPost = new ConcurrentStack<BlogPost>();
        _blogPostStream = new Subject<BlogPost>();
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
        AllBlogPost.Push(blogPost);
        _blogPostStream.OnNext(blogPost);
        return blogPost;
    }

    public string UpdateBlogPost(BlogPost blogPost)
    {
        var result = "Done!";
        var _blogPost = _context.BlogPost.FirstOrDefault(blog => blog.Id == blogPost.Id);
        if (_blogPost == null)
        {
            result = $"Blog mit dem ID {blogPost.Id} nicht gefunden";
        }
        else
        {
            _context.BlogPost.Update(blogPost);
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