using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class BlogQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public BlogQuery(BlogService blogService)
    {
        Name = "BlogQuery";
        int id = 0;
        Field<ListGraphType<BlogPostType>>(
        name: "blogs", resolve: context =>
         {
             return blogService.GetAllBlogs();
         });
        Field<BlogPostType>(
            name: "blog",
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
            resolve: context =>
            {
                id = context.GetArgument<int>("id");
                return blogService.GetBlogById(id);
            }
        );
    }
}