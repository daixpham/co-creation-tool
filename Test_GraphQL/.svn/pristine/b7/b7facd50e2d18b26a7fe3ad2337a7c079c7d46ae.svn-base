
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class BlogPostType : ObjectGraphType<BlogPost>
{
    public BlogPostType()
    {
        Name = "BlogPost";
        Field(_ => _.Id, type:
        typeof(IdGraphType)).Description
       ("The Id of the Blog post.");
        Field(_ => _.Title).Description
        ("The title of the blog post.");
        Field(_ => _.Content).Description
        ("The content of the blog post.");
        Field(_ => _.Author, type: typeof(AuthorType)).Description("The Author of the blog post");

    }
}