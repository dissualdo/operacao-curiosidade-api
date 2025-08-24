using WebApi.Models.Dto;
using WebApi.Models.Models.Users;
using WebApi.Application.UseCase.Users.Dto;

namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Use case responsible for retrieving users based on a dynamic filter.
    /// Delegates the actual query to <see cref="IUserRepository"/>.
    /// </summary>
    public interface IGetUserByFilterUseCase
    {
        /// <summary>
        /// Executes the search using the provided filter and returns the matching users.
        /// </summary>
        /// <param name="filter">
        /// A filter DTO containing optional criteria such as name, email, status, and
        /// (optionally) pagination parameters depending on your repository implementation.
        /// </param>
        /// <returns>
        /// A collection of <see cref="User"/> records that satisfy the filter conditions.
        /// </returns>
        /// <remarks>
        /// This method is a thin orchestration layer and should remain free of query logic.
        /// Prefer to keep filtering and projection in the repository/query layer.
        Task<IEnumerable<UserResponse>> ExecuteAsync(GetUserFilterDto filter);
    }
}
