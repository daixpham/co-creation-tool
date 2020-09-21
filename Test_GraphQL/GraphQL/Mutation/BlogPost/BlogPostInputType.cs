using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class BlogPostInputType : InputObjectGraphType
{
    public BlogPostInputType()
    {
        Name = "blogPostInput";
        Field<IdGraphType>("id");
        Field<StringGraphType>("title");
        Field<StringGraphType>("content");
        Field<AuthorInputType>("author");
        Field<IdGraphType>("authorId");
    }
}