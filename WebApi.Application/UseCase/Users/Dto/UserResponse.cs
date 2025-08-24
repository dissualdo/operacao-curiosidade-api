using WebApi.Models.Models.Users;

namespace WebApi.Application.UseCase.Users.Dto
{
    public class UserResponse
    {
        public long Id { get; set; }

        /// <summary>
        /// Full name of the user.
        /// Required in the first registration step.
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Age of the user (optional).
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// User's email address.
        /// Required and must be unique.
        /// </summary> 
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's address (optional).
        /// </summary> 
        public string? Address { get; set; }

        /// <summary>
        /// Indicates whether the user is active or inactive.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Timestamp of when the user record was created.
        /// Defaults to the current UTC date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp of the last update for the user record.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Foreign key to the authentication credentials.
        /// Used when local authentication is applied.
        /// </summary>
        public long? IdAuthentication { get; set; }

        /// <summary>
        /// Creates a new response DTO based on a <see cref="User"/> entity.
        /// </summary>
        public UserResponse(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            Name = user.Name;
            Age = user.Age;
            Email = user.Email;
            Address = user.Address;
            IsActive = user.IsActive;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
            IdAuthentication = user.IdAuthentication;
        }
    }
}
