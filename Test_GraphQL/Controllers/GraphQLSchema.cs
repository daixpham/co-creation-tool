using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class GraphQLDemoSchema : Schema, ISchema
{
    
    public GraphQLDemoSchema(IDependencyResolver resolver, BlogService blogService) : base(resolver)
    {
        Query = resolver.Resolve<CompositeQuery>();
        Mutation = resolver.Resolve<CompositeMutation>();
        Subscription = new BlogPostSubscriptions(blogService);
    }
}