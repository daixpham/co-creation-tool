using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Name = "Author";
            Field(_ => _.Id).Description("Author's Id.");
            Field(_ => _.FirstName).Description("First name of the author");
            Field(_ => _.LastName).Description("Last name of the author");
            Field(_ => _.Blogs, type:typeof(ListGraphType<BlogPostType>));
        }
    }