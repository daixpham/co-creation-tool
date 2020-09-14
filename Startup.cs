using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using GraphQL.Http;
using Microsoft.EntityFrameworkCore;
using GraphQL.Instrumentation;
using Newtonsoft.Json;

namespace graphQL_test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();
            services.AddScoped<GraphqlErrors>();

            services.AddScoped<IGraphQueryMarker, AuthorQuery>();
            services.AddScoped<IGraphQueryMarker, BlogQuery>();

            services.AddScoped<AuthorService>();
            services.AddScoped<AuthorType>();
            services.AddScoped<AuthorInputType>();
            services.AddScoped<IGraphMutationMarker, AuthorMutation>();


            

            services.AddScoped<BlogService>();
            services.AddScoped<BlogPostType>();
            services.AddScoped<BlogPostInputType>();
            services.AddScoped<IGraphMutationMarker, BlogPostMutation>();

            //graphQL
            services.AddScoped<ISchema, GraphQLDemoSchema>();
            services.AddScoped<CompositeQuery>();
            services.AddScoped<CompositeMutation>();
            
            //DB
            services.AddScoped<BloggingContext>();
            services.AddDbContext<BloggingContext>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGraphiQl("/graphql");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
