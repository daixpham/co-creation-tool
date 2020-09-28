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
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.GraphiQL;
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
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowMyOrigin", builder => builder.WithOrigins("http://localhost:3000"));
                });

            services.AddGraphQL(options =>{
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
                
            }).AddDataLoader().AddGraphTypes();

            services.AddSingleton<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<GraphqlErrors>();

            services.AddSingleton<IGraphQueryMarker, AuthorQuery>();
            services.AddSingleton<IGraphQueryMarker, BlogQuery>();

            services.AddSingleton<AuthorService>();
            services.AddSingleton<AuthorType>();
            services.AddSingleton<AuthorInputType>();
            services.AddSingleton<IGraphMutationMarker, AuthorMutation>();

            services.AddSingleton<BlogService>();
            services.AddSingleton<BlogPostType>();
            services.AddSingleton<BlogPostInputType>();
            services.AddSingleton<BlogPostSubscriptions>();
            services.AddSingleton<IGraphMutationMarker, BlogPostMutation>();

            //graphQL
            services.AddSingleton<ISchema, GraphQLDemoSchema>();
            services.AddSingleton<CompositeQuery>();
            services.AddSingleton<CompositeMutation>();

            //DB
            services.AddSingleton<DbContextOptions<BloggingContext>>();
   
            services.AddSingleton<BloggingContext>();
            services.AddDbContext<BloggingContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddControllers();

            services.Configure<GraphqlErrors>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGraphiQl("/graphql");
            }

            app.UseWebSockets();
            //app.UseGraphQL<GraphQLDemoSchema>("/graphql");
            app.UseGraphQLWebSockets<GraphQLDemoSchema>("/graphql");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseGraphiQLServer(new GraphiQLOptions
            {
                Path = "/ui/graphiql",
                GraphQLEndPoint = "/graphql",
            });
            app.UseAuthorization();
            app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().WithOrigins("http://localhost:3000").AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
