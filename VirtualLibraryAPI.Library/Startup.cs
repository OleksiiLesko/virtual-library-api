﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using VirtualLibraryAPI.Domain;
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
        private static string CreateConnectionString(IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = configuration["DefaultConnection:Server"];
            builder.InitialCatalog = configuration["DefaultConnection:DataBaseName"];
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            Configuration.Bind("DbHostName", connection);
            services.AddDbContextPool<ApplicationContext>(options =>
               options.UseSqlServer(CreateConnectionString(Configuration.GetSection("DefaultConnection"))));
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
