using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class AuthorInputType : InputObjectGraphType
{
    public AuthorInputType()
    {
        Name = "authorInput";
        Field<NonNullGraphType<IdGraphType>>("id");
        Field<StringGraphType>("firstName");
        Field<StringGraphType>("lastName");
        Field<ListGraphType<BlogPostInputType>>("blogs");
    }
}