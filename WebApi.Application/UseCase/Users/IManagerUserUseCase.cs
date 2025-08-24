using WebApi.Application.UseCase.Users.Dto;

namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Use case responsible for creating or updating a <see cref="User"/> entity.
    /// Decides between insert and update based on the presence of an identifier in the request.
    /// </summary>
    public interface IManagerUserUseCase
    {
        /// <summary>
        /// Executes the manage-user flow.
        /// If <see cref="ManagerUserRequest.Id"/> is null or zero, a new user is created.
        /// Otherwise, the existing user is updated.
        /// </summary>
        /// <param name="request">
        /// DTO containing user data to be created or updated.
        /// </param>
        /// <returns>
        /// A <see cref="UserResponse"/> representing the persisted user state.
        /// </returns>
        /// <remarks>
        /// This method is intentionally simple to match the scope of the technical test:
        /// validation and conflict checks (e.g., unique email) should be handled upstream
        /// or added here in future iterations if required.
        /// </remarks>
        Task<UserResponse> ExecuteAsync(ManagerUserRequest request);
    }
}
