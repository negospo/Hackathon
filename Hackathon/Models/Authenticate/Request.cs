using System.ComponentModel.DataAnnotations;

namespace Hackathon.Models.Authenticate
{
    public class Request
    {
        /// <summary>
        /// Usuário
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
