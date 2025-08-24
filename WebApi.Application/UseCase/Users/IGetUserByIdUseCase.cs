using WebApi.Application.UseCase.Users.Dto;

namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Use case responsible for retrieving a single user by its identifier.
    /// </summary>
    public interface IGetUserByIdUseCase
    {
        /// <summary>
        /// Retrieves a user by id using the repository filter API and maps it to <see cref="UserResponse"/>.
        /// </summary>
        /// <param name="id">Unique identifier of the target user.</param>
        /// <returns>
        /// A <see cref="UserResponse"/> when found; otherwise, <c>null</c>.
        /// </returns>
        /// <remarks>
        /// Keeps the logic intentionally minimal for the scope of the technical test.
        /// Controller/action should translate a <c>null</c> return into HTTP 404.
        /// </remarks>
         Task<UserByIdResponse?> ExecuteAsync(long id);
    }
}
