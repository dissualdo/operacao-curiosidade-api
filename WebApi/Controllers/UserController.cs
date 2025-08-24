using WebApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UseCase.Users;
using WebApi.Application.UseCase.Users.Dto;

namespace WebApi.Controllers
{
    /// <summary>
    /// Exposes endpoints for querying users registered in the system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IManagerUserUseCase _managerUserUseCase;
        private readonly IGetUserByIdUseCase _getUserByIdUseCase;
        private readonly IGetUserByFilterUseCase _getUserByFilterUseCase;

     
        public UserController(
            ILogger<UserController> logger,
            IManagerUserUseCase managerUserUseCase,
            IGetUserByIdUseCase getUserByIdUseCase,
            IGetUserByFilterUseCase getUserByFilterUseCase)
        {
            _logger = logger;
            _managerUserUseCase = managerUserUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getUserByFilterUseCase = getUserByFilterUseCase;
        }

        /// <summary>
        /// Retrieves users that match the given filter.
        /// </summary>
        /// <param name="filter">Filter with optional criteria (e.g., name, email, status, pagination).</param>
        /// <returns>A list of users that satisfy the filter conditions.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WebApi.Models.Models.Users.User>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUser([FromQuery] GetUserFilterDto filter)
        {
            if (filter is null)
                return BadRequest("Filter object cannot be null.");

            try
            {
                // Delegates to use case; repository performs the actual query.
                var userFounds = await _getUserByFilterUseCase.ExecuteAsync(filter);
                return Ok(userFounds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve users.");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Retrieves a user by its unique identifier.
        /// </summary>
        /// <param name="id">User identifier (primary key).</param>
        /// <returns>
        /// <see cref="IActionResult"/> with:
        /// - 200 OK and the user payload when found;
        /// - 404 Not Found when no user matches the id;
        /// - 500 Internal Server Error when an unexpected error occurs.
        /// </returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(UserByIdResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserById(long id)
        {
            try
            {
                var userFound = await _getUserByIdUseCase.ExecuteAsync(id);

                if (userFound is null)
                    return NotFound($"User with id {id} was not found.");

                return Ok(userFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve user with id {UserId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        /// <summary>
        /// Creates a user based on the payload.
        /// </summary>
        /// <param name="request">DTO containing the user data to create.</param>
        /// <param name="ct">Cancellation token propagated by the HTTP request.</param>
        /// <returns>
        /// <see cref="IActionResult"/> with:
        /// - 200 OK and the persisted user payload (<see cref="UserResponse"/>) on success;
        /// - 400 Bad Request when the request model is invalid;
        /// - 500 Internal Server Error when an unexpected error occurs.
        /// </returns>
        [HttpPost()]  
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostUser(
            [FromBody] ManagerUserRequest request,
            CancellationToken ct)  {
            try
            {
                // Basic model validation guard (if the controller/class is not annotated with [ApiController]).
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Execute use case (handles create/update based on Id).
                var result = await _managerUserUseCase.ExecuteAsync(request);

                // For simplicity, returning 200 OK for both create/update.
                // If you implement GET by Id, consider returning CreatedAtAction when request.Id is null/0.
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Expected domain/validation errors -> 400
                _logger.LogWarning(ex, "Validation/argument error managing user.");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Unexpected errors -> 500
                _logger.LogError(ex, "Failed to manage user (create/update).");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        /// <summary>
        /// Updates a user based on the payload.
        /// </summary>
        /// <param name="request">DTO containing the user data to  update.</param>
        /// <param name="ct">Cancellation token propagated by the HTTP request.</param>
        /// <returns>
        /// <see cref="IActionResult"/> with:
        /// - 200 OK and the persisted user payload (<see cref="UserResponse"/>) on success;
        /// - 400 Bad Request when the request model is invalid;
        /// - 500 Internal Server Error when an unexpected error occurs.
        /// </returns>
        [HttpPost("{id:long}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostUserEdit(
            long id,
            [FromBody] ManagerUserRequest request,
            CancellationToken ct)
        {
            try
            {
                // Basic model validation guard (if the controller/class is not annotated with [ApiController]).
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                request.Id = id;

                // Execute use case (handles create/update based on Id).
                var result = await _managerUserUseCase.ExecuteAsync(request);

                // For simplicity, returning 200 OK for both create/update.
                // If you implement GET by Id, consider returning CreatedAtAction when request.Id is null/0.
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Expected domain/validation errors -> 400
                _logger.LogWarning(ex, "Validation/argument error managing user.");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Unexpected errors -> 500
                _logger.LogError(ex, "Failed to manage user (create/update).");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }


    }
}