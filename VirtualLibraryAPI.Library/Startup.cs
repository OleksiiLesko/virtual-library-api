using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Repository;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository.Repositories;
using Serilog;

namespace VirtualLibraryAPI.Library
{
    /// <summary>
    /// Settings configuration
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Settings of configuration and environment
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }
        /// <summary>
        /// Settings of configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Settings of environment
        /// </summary>
        public IHostEnvironment Environment { get; }
        /// <summary>
        /// Create database connection string
        /// </summary>
        /// <returns></returns>
        public  string CreateConnectionString(IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = configuration.GetSection("DbHostName")["Server"];
            builder.InitialCatalog = configuration.GetSection("DbHostName")["Database"];
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            builder.ConnectRetryCount = 1;

            return builder.ConnectionString;
        }
        /// <summary>
        /// Adding services to the collection of services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMagazine, Repository.Repositories.Magazine>();
            services.AddScoped<Models.Magazine>();
            services.AddScoped<IArticle, Repository.Repositories.Article>();
            services.AddScoped<Models.Article>();
            services.AddScoped<IBook, Repository.Repositories.Book>();
            services.AddScoped<Models.Book>();
            services.AddScoped<ICopy, Repository.Repositories.Copy>();
            services.AddScoped<Models.Copy>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            string connectionString = CreateConnectionString(Configuration);
            services.AddDbContextPool<ApplicationContext>(options =>
             options.UseSqlServer(connectionString ,b => b.MigrationsAssembly("VirtualLibraryAPI.Library")));
        }
        /// <summary>
        /// Responsible for building the application's request processing pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<Middleware>();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
