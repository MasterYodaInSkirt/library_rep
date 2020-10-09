using DataDB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : Controller
    {
        private ILibraryService _libraryServices;
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILogger<LibraryController> logger, ILibraryService libraryServices)
        {
            _logger = logger;
            _libraryServices = libraryServices;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var books = _libraryServices.GetAllBooks();
            return Ok(books);
        }
    }
}
