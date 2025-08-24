using WebApi.Models.Repositories;
using WebApi.Application.UseCase.Users.Dto;

namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Use case responsible for creating or updating a <see cref="User"/> entity.
    /// Decides between insert and update based on the presence of an identifier in the request.
    /// </summary>
    public class ManagerUserUseCase : IManagerUserUseCase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerUserUseCase"/> class.
        /// </summary>
        /// <param name="userRepository">
        /// Abstraction for persistence operations related to users.
        /// </param>
        public ManagerUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
        public async Task<UserResponse> ExecuteAsync(ManagerUserRequest request)
        {
            // Map DTO to entity (includes Curiosity nested data).
            var user = request.ToEntry();

            // Decide between insert or update by Id presence.
            if (user.Id == 0)
            {
                // Create new user.
                await _userRepository.AddAsync(user);
            }
            else
            {
                // Update existing user.
                await _userRepository.UpdateAsync(user);
            }

            // Return a lightweight response model for API output.
            return new UserResponse(user);
        }
    }
}
