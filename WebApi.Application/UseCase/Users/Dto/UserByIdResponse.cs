using WebApi.Models.Models.Users;

namespace WebApi.Application.UseCase.Users.Dto
{
    public class UserByIdResponse: UserResponse
    {
        /// <summary>
        /// Additional free-text information about the user.
        /// </summary>
        public string? OtherInfo { get; set; }

        /// <summary>
        /// User's interests, stored as free text.
        /// </summary>
        public string? Interests { get; set; }

        /// <summary>
        /// User's feelings, stored as free text.
        /// </summary>
        public string? Feelings { get; set; }

        public UserByIdResponse(User user): base(user) {

            if (user is not null && user.Curiosity is not null)
            {
                Feelings = user.Curiosity.Feelings;
                OtherInfo = user.Curiosity.OtherInfo;
                Interests = user.Curiosity.Interests;
            }
        }
    }
}
