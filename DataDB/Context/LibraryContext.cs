using DataDB.Models;
using Microsoft.EntityFrameworkCore;

namespace DataDB.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LGECBFC\\MSSQLSERVER01;Initial Catalog=DBData;Integrated Security = True");
        }
    }
}
