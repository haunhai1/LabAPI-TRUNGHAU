using LabAPI.Models.Domain;
using LabAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace LabAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookstoreServices _BookService;
        private readonly ILogger<AuthorsController> _logger; // Inject ILogger

        public AuthorsController(IBookstoreServices BookService, ILogger<AuthorsController> logger)
        {
            _BookService = BookService;
            _logger = logger; // Inject ILogger
        }
        [HttpGet]
        public async Task<IActionResult> getAllAuthors()
        {
            try
            {
                var authors = await _BookService.getAllAuthors();
                _logger.LogInformation($"Retrieved {authors.Count} book from the database.");
                return StatusCode(StatusCodes.Status200OK, authors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving book: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving Publishers.");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetIDAuthors(int id, bool includeAuthors = false)
        {
            try
            {
                Authors authors = await _BookService.GetIDAuthors(id, includeAuthors);
                _logger.LogInformation($"Retrieved Name: {authors.FullName} authors from the database.");
                if (authors == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
                }
                return StatusCode(StatusCodes.Status200OK, authors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving authors with ID {id}: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving student.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Books>> AddAuthors(Authors authors)
        {
            try
            {
                var dbauthors = await _BookService.AddAuthors(authors);
                if (dbauthors == null)
                {
                    _logger.LogError($"{authors.FullName} could not be added.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{authors.FullName} could not be added.");
                }

                _logger.LogInformation($"{authors.FullName} added successfully.");
                return CreatedAtAction("Getauthors", new { id = authors.AuthorsId }, authors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding student.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUpdateAuthorsBooks(int id, Authors authors)
        {
            try
            {
                if (id != authors.AuthorsId)
                {
                    return BadRequest();
                }

                Authors dbauthors = await _BookService.UpdateAuthors(authors);

                if (dbauthors == null)
                {
                    _logger.LogError($"{authors.FullName} could not be updated.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{authors.FullName} could not be updated.");
                }

                _logger.LogInformation($"{authors.FullName} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating authors: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating student.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthors(int id)
        {
            try
            {
                var authors = await _BookService.GetIDAuthors(id, false);
                (bool status, string message) = await _BookService.DeleteAuthors(authors);

                if (status == false)
                {
                    _logger.LogError($"Error deleting authors: {message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }

                _logger.LogInformation($"authors with ID {id} deleted successfully.");
                return StatusCode(StatusCodes.Status200OK, authors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting authors with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting student.");
            }
        }
    }
}