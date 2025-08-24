using System.ComponentModel.DataAnnotations;
using WebApi.Models.Enums;

namespace WebApi.Models.Dto.Authenticate
{
    /// <summary>
    /// Data transfer object (DTO) for user authentication.
    /// </summary>
    public class AuthenticateDto
    {
        /// <summary>
        /// User's email address or CPF used for login.
        /// </summary>
        [Required(ErrorMessage = "O campo E-mail ou CPF é obrigatório.")]
        public string? Login { get; set; }

        /// <summary>
        /// User's password, used for authentication.
        /// </summary>
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string? Password { get; set; }

    }
}
