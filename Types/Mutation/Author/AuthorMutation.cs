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
            new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "authorInput" }
          ),
          resolve: context =>
          {
              var authorInput = context.GetArgument<Author>("authorInput");
            
              return authorService.UpdateAuthor(authorInput);
          });
    }
}