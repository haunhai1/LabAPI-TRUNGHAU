using LabAPI.Models.Domain;
using LabAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace LabAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookstoreServices _BookService;
        private readonly ILogger<BooksController> _logger; // Inject ILogger

        public BooksController(IBookstoreServices BookService, ILogger<BooksController> logger)
        {
            _BookService = BookService;
            _logger = logger; // Inject ILogger
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var book = await _BookService.GetAllBooks();
                _logger.LogInformation($"Retrieved {book.Count} book from the database.");
                return StatusCode(StatusCodes.Status200OK, book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving book: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving Publishers.");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetIdBooks(int id, bool includeBooks = false)
        {
            try
            {
                Books book = await _BookService.GetIdBooks(id, includeBooks);
                _logger.LogInformation($"Retrieved Name: {book.Title} book from the database.");
                if (book == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
                }
                return StatusCode(StatusCodes.Status200OK, book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving book with ID {id}: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving student.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Books>> AddBooks(Books books)
        {
            try
            {
                var dbBooks = await _BookService.AddBooks(books);
                if (dbBooks == null)
                {
                    _logger.LogError($"{books.Title} could not be added.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{books.Title} could not be added.");
                }

                _logger.LogInformation($"{books.Title} added successfully.");
                return CreatedAtAction("GetBooks", new { id = books.BooksId }, books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding student.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooks(int id, Books Books)
        {
            try
            {
                if (id != Books.PublishersId)
                {
                    return BadRequest();
                }

                Books dbBooks = await _BookService.UpdateBooks(Books);

                if (dbBooks == null)
                {
                    _logger.LogError($"{Books.Title} could not be updated.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{Books.Title} could not be updated.");
                }

                _logger.LogInformation($"{Books.Title} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating student.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            try
            {
                var books = await _BookService.GetIdBooks(id, false);
                (bool status, string message) = await _BookService.DeleteBooks(books);

                if (status == false)
                {
                    _logger.LogError($"Error deleting student: {message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }

                _logger.LogInformation($"Student with ID {id} deleted successfully.");
                return StatusCode(StatusCodes.Status200OK, books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting student with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting student.");
            }
        }
    }
}
