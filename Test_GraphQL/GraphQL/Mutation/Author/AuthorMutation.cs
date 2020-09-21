using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;

public class AuthorMutation : ObjectGraphType, IGraphMutationMarker
{
    public AuthorMutation(AuthorService authorService)
    {
        Field<AuthorType>(
          "createAuthor",
          arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "author" }
          ),
          resolve: context =>
          {
            var author = context.GetArgument<Author>("author");
            return authorService.CreateAuthor(author);
          });

        Field<StringGraphType>(
          "updateAuthor",
          arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "authorInput" },
            new QueryArgument<ListGraphType<IntGraphType>> {Name="authorBlogPostList"}
          ),
          resolve: context =>
          {
            var authorInput = context.GetArgument<Author>("authorInput");
            var authorBlogPostList = context.GetArgument<List<int>>("authorBlogPostList");
            return authorService.UpdateAuthor(authorInput,authorBlogPostList);
          });
    }
}