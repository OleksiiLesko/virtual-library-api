using VirtualLibraryAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using Host = Microsoft.Extensions.Hosting.Host;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using VirtualLibraryAPI.Library.Services;
using Microsoft.AspNetCore;
using VirtualLibraryAPI.Domain.Models;

namespace VirtualLibraryAPI.Library
{
    public class Program
    {
        /// <summary>
        /// Argument name for console 
        /// </summary>
        private const string CONSOLE_ARG_NAME = "--console";
        /// <summary>
        /// Starting method 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //path to content root
            var pathToContentRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(pathToContentRoot!);
            try
            {
                Log.Information("Starting up...");
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                string connectionString = DatabaseUtils.CreateConnectionString(configuration);
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.UseSqlServer(connectionString,b => b.MigrationsAssembly("VirtualLibraryAPI.Library"));
                using (var context = new ApplicationContext(optionsBuilder.Options))
                {
                     //context.Database.Migrate();


                    var book = new Books
                    {
                        Name = "The Great Gatsby",
                        Author = "F. Scott Fitzgerald",
                        ISBN = "978-3-16-148410-0",
                        Type = ItemType.Book,
                        PublishingDate = new DateTime(1925, 4, 10),
                        Publisher = "Charles Scribner's Sons"
                    };
                    context.Books.Add(book);

                    var item = new Items
                    {
                        Name = "The Great Gatsby",
                        ItemID = book.ItemID,
                        Type = ItemType.Book
                    };
                    context.Items.Add(item);


                    var magazine = new Magazines
                    {
                        Name = "National Geographic",
                        IssueNumber = 257,
                        Type = ItemType.Magazine,
                        PublishingDate = new DateTime(2015, 11, 1),
                        Publisher = "National Geographic Society"
                    };
                    context.Magazines.Add(magazine);


                    var article = new Articles
                    {
                        Name = "The Science of Sleep",
                        Author = "Karen Wright",
                        MagazinesIssueNumber = "January/February 2017",
                        MagazineName = "Scientific American Mind",
                        Type = ItemType.Article,
                        PublishingDate = new DateTime(2017, 1, 1),
                        Publisher = "Scientific American, a division of Springer Nature America, Inc."
                    };
                    context.Articles.Add(article);

                    // Add new copy to table Copies
                    var copy = new Copies
                    {
                        ItemID = book.ItemID,
                        CopyID = 1,
                        Type = ItemType.Copy,
                        PublishingDate = book.PublishingDate,
                        Publisher = book.Publisher
                    };
                    context.Copies.Add(copy);
                    context.SaveChanges();
                }

                IHost host = CreateHostBuilder(args).Build();
                var isService = !Debugger.IsAttached && !args.ToList().Contains(CONSOLE_ARG_NAME);
                if (isService)
                {
                    Log.Information("Running as a service");
                    host.RunAsService();
                }
                else
                {
                    Log.Information("Running as console");
                    host.Run();
                }

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        /// <summary>
        /// Create host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
        }
    }
}
