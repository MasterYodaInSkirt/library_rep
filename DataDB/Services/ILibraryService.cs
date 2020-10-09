using DataDB.Models;
using System.Collections.Generic;

namespace DataDB.Services
{
    public interface ILibraryService
    {
        void Add(Book book);

        List<Book> GetAllBook();
    }
}
