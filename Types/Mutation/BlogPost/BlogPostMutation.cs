using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;

public class BlogPostMutation : ObjectGraphType, IGraphMutationMarker
{
  public BlogPostMutation(BlogService blogService)
  {
    Field<BlogPostType>(
      "createBlog",
      arguments: new QueryArguments(
        new QueryArgument<NonNullGraphType<BlogPostInputType>> {Name = "blog"}
      ),
      resolve: context =>
      {
        var blogPost = context.GetArgument<BlogPost>("blog");
        return blogService.CreateBlogPost(blogPost);
      });

    Field<StringGraphType>(
      "deleteBlog",
      arguments: new QueryArguments(
        new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "blog_id"}
      ),
      resolve: context =>
      {
        var blogId = context.GetArgument<int>("blog_id");
        return blogService.DeleteBlog(blogId);
      });
  }
}