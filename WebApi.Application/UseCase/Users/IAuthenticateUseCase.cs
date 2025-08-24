using WebApi.Models.Dto;
using WebApi.Models.Dto.Authenticate;

namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Defines the contract for user authentication use case.
    /// </summary>
    public interface IAuthenticateUseCase
    {
        /// <summary>
        /// Executes the authentication process asynchronously.
        /// </summary>
        /// <param name="user">The authentication data containing login and password.</param>
        /// <returns>An <see cref="AuthenticateResponse"/> object containing user details.</returns>
        Task<AuthenticateResponse> ExecuteAsync(AuthenticateDto user);
    }
}
