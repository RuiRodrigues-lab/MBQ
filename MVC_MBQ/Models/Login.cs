using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    /// <summary>
    /// Classe Login
    /// </summary>
    public class Login
    {
        /// <value>
        /// Propriedade que guarda email de login
        /// </value>
        [Required]
        public string Email { get; set; }
        /// <value>
        /// Propriedade que guarda password de log in
        /// </value>
        [Required]
        public string Password { get; set; }
        /// <value>
        /// Propriedade que guarda o URL mediante Login
        /// </value>
        public string ReturnUrl { get; set; }
    }
}