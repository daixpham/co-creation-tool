using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class AuthorQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public AuthorQuery(AuthorService authorService)
    {

        Name = "AuthorQuery";
        int id = 0;
        Field<ListGraphType<AuthorType>>(
        name: "authors", resolve: context =>
         {
             return authorService.GetAllAuthors();
         });
        Field<AuthorType>(
            name: "author",
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
            resolve: context =>
            {
                id = context.GetArgument<int>("id");
                return authorService.GetAuthorById(id);
            }
        );
    }
}

