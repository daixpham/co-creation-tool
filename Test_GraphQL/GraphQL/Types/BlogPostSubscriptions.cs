  
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
public class BlogPostSubscriptions : ObjectGraphType<object>
    {
       private BlogService _blogServices ; 
        public BlogPostSubscriptions(BlogService blogServices)
        {
            _blogServices = blogServices;
           AddField(new EventStreamFieldType {
               Name ="blogPostAdded",
               Type = typeof(BlogPostType),
               Resolver = new FuncFieldResolver<BlogPost>(ResolveBlogPost),
               Subscriber = new EventStreamResolver<BlogPost>(Subscriber)
           });
        }

        private BlogPost ResolveBlogPost( ResolveFieldContext context){
            return context.Source as BlogPost ;
        }

        private IObservable<BlogPost> Subscriber(ResolveEventStreamContext context){
            return _blogServices.GetLastestPostedBlog;
        }
    }