using System.Collections.Generic;
using System;
using System.Linq;
public class BlogRepository {
    private readonly List<Author> authors = new List<Author>();
    private readonly List<BlogPost> posts = new List<BlogPost>();

    public BlogRepository()
    {
        
    }
    public List<BlogPost> GetAllBlogs()
    {
        return this.posts;
    }
    

}