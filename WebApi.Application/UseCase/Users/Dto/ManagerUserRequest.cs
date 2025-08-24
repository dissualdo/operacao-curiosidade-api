using WebApi.Models.Models.Users;

namespace WebApi.Application.UseCase.Users.Dto
{
    /// <summary>
    /// Data Transfer Object (DTO) for managing user creation or update requests.
    /// Encapsulates user personal data and curiosity-related information.
    /// </summary>
    public class ManagerUserRequest
    {
        /// <summary>
        /// User identifier (primary key).
        /// Optional: provided only when updating an existing user.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Age of the user (optional).
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Full name of the user.
        /// Required in the first registration step.
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// User's email address.
        /// Must be unique in the system and is required for registration.
        /// </summary> 
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's residential or contact address (optional).
        /// </summary> 
        public string? Address { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the user is currently active in the system.
        /// Defaults to true for newly created users.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Additional free-text notes or information about the user (optional).
        /// </summary> 
        public string? OtherInfo { get; set; }

        /// <summary>
        /// User's interests, stored as free text (optional).
        /// </summary> 
        public string? Interests { get; set; }

        /// <summary>
        /// User's feelings, stored as free text (optional).
        /// </summary> 
        public string? Feelings { get; set; }

        /// <summary>
        /// Maps the current DTO into a <see cref="User"/> entity,
        /// including associated <see cref="Curiosity"/> data.
        /// </summary>
        /// <returns>A <see cref="User"/> entity populated with this DTO data.</returns>
        public User ToEntry()
        {
            var id = this.Id.HasValue ? this.Id.Value : 0;

            return new User()
            {
                Id = id,
                Age = this.Age,
                Name = this.Name,
                Email = this.Email,
                Address = this.Address,
                IsActive = this.IsActive,
                Curiosity = new Curiosity()
                {
                    Feelings = this.Feelings,
                    OtherInfo = this.OtherInfo,
                    Interests = this.Interests
                }
            };
        }
    }
}
