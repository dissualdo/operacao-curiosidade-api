using WebApi.Models.Dto;
using WebApi.Models.Shared;
using WebApi.Models.Models.Users;

namespace WebApi.Models.Repositories
{
    /// <summary>
    /// Interface defining the contract for user repository operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The <see cref="User"/> object if found; otherwise, null.</returns>
        Task<User> GetByIdAsync(long id);

        /// <summary>
        /// Retrieves a list of users based on the provided filter criteria asynchronously.
        /// </summary>
        /// <param name="filter">The filtering criteria for retrieving users.</param>
        /// <returns>A paginated list of users matching the filter criteria.</returns>
        Task<IEnumerable<User>> GetByFilterAsync(GetUserFilterDto filter);

        /// <summary>
        /// Retrieves a list of users based on the provided filter criteria asynchronously.
        /// </summary>
        /// <param name="filter">The filtering criteria for retrieving users.</param>
        /// <returns>A paginated list of users matching the filter criteria.</returns>
        Task<User?> GetByLoginAsync(GetUserFilterDto filter);

        /// <summary>
        /// Adds a new user to the database asynchronously.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> AddAsync(User user);

        /// <summary>
        /// Updates an existing user in the database asynchronously.
        /// </summary>
        /// <param name="user">The user entity containing updated information.</param>
        /// <returns>The number of affected rows.</returns>
        /// <exception cref="AppException">Thrown if the user is not found.</exception>
        Task<int> UpdateAsync(User user);
    }
}
