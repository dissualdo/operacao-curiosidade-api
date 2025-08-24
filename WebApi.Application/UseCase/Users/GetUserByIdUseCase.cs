using WebApi.Models.Dto;
using WebApi.Models.Repositories;
using WebApi.Application.UseCase.Users.Dto;

namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Use case responsible for retrieving a single user by its identifier.
    /// </summary>
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdUseCase"/> class.
        /// </summary>
        /// <param name="userRepository">Repository abstraction used to query users.</param>
        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
        public async Task<UserByIdResponse?> ExecuteAsync(long id)
        {
            // Query the repository using a typed filter DTO (Id equality).
            var users = await _userRepository.GetByFilterAsync(new GetUserFilterDto { Id = id });

            // Map the first (and only expected) record to the response model.
            return users
                .Select(u => new UserByIdResponse(u))
                .FirstOrDefault();
        }
    }
}
