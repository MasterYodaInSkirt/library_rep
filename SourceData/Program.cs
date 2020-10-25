using DataDB;
using DataDB.Models;
using DataDB.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SourceData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = ConfigureServices(new ServiceCollection());
            var serviceProvider = services.BuildServiceProvider();

            var libraryService = serviceProvider.GetService<ILibraryService>();

            var libraryClient = RestService.For<ILibraryData>("https://anapioficeandfire.com/");

            await GetBookData(libraryClient, libraryService);
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.RegisterDataServices(config);

            services.AddTransient<ILibraryService, LibraryService>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        private static async Task<List<BookModel>> GetBooksData(ILibraryData libraryClient)
        {
            return await Policy
                  .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
                  .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                  .ExecuteAsync(async () => await libraryClient.GetAllBooksAsync());
        }

        private static async Task GetBookData(ILibraryData libraryClient, ILibraryService libraryService)
        {
            try
            {
                var books = await GetBooksData(libraryClient);
                foreach (var book in books)
                {
                    var bookmodel = new Book()
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Publisher = book.Publisher,
                        Country = book.Country,
                        Released = Convert.ToDateTime(book.Released)
                    };

                    libraryService.Add(bookmodel);
                }
            }
            catch (ApiException ex)
            {
                var content = ex.GetContentAs<Dictionary<String, String>>();
                Console.WriteLine(ex.StatusCode);
            }
        }
    }
}
