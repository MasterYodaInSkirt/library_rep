using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceData
{
    interface ILibraryData
    {
        [Get("/api/books")]
        Task<List<BookModel>> GetAllBooksAsync();
    }
}
