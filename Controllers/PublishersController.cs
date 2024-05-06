using LabAPI.Models.Domain;
using LabAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IBookstoreServices _BookService;
        private readonly ILogger<PublishersController> _logger; // Inject ILogger

        public PublishersController(IBookstoreServices BookService, ILogger<PublishersController> logger)
        {
            _BookService = BookService;
            _logger = logger; // Inject ILogger
        }
        [HttpGet]
        public async Task<IActionResult> getAllStudent()
        {
            try
            {
                var Publishers = await _BookService.GetAllPublishers();
                _logger.LogInformation($"Retrieved {Publishers.Count} Publishers from the database.");
                return StatusCode(StatusCodes.Status200OK, Publishers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving Publishers: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving Publishers.");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetIdPublishers(int id, bool includePublishers = false)
        {
            try
            {
                Publishers publishers = await _BookService.GetIdPublishers(id, includePublishers);
                _logger.LogInformation($"Retrieved Name: {publishers.Name} Publishers from the database.");
                if (publishers == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
                }
                return StatusCode(StatusCodes.Status200OK, publishers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving Publishers with ID {id}: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving student.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Publishers>> AddPublishers(Publishers publishers)
        {
            try
            {
                var dbpublishers = await _BookService.AddPublishers(publishers);
                if (dbpublishers == null)
                {
                    _logger.LogError($"{publishers.Name} could not be added.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{publishers.Name} could not be added.");
                }

                _logger.LogInformation($"{publishers.Name} added successfully.");
                return CreatedAtAction("GetStudents", new { id = publishers.PublishersId }, publishers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding student.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublishers(int id, Publishers publishers)
        {
            try
            {
                if (id != publishers.PublishersId)
                {
                    return BadRequest();
                }

                Publishers dbpublishers = await _BookService.UpdatePublishers(publishers);

                if (dbpublishers == null)
                {
                    _logger.LogError($"{publishers.Name} could not be updated.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{publishers.Name} could not be updated.");
                }

                _logger.LogInformation($"{publishers.Name} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating student.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublishers(int id)
        {
            try
            {
                var publishers = await _BookService.GetIdPublishers(id, false);
                (bool status, string message) = await _BookService.DeletePublishers(publishers);

                if (status == false)
                {
                    _logger.LogError($"Error deleting student: {message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }

                _logger.LogInformation($"Student with ID {id} deleted successfully.");
                return StatusCode(StatusCodes.Status200OK, publishers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting student with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting student.");
            }
        }
    }
}
