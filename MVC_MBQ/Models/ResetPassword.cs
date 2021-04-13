using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    /// <summary>
    /// Classe Reset Passord - permite fazer reset a passowrd para log in
    /// </summary>
    public class ResetPassword
    {
        /// <value>
        /// Propriedade que guarda password
        /// </value>
        [Required]
        public string Password { get; set; }
        /// <value>
        /// Propriedade que guarda password confirmada e compara com propriedade Password devolvendo mensagem de erro caso não sejam iguais.
        /// </value>
        [Compare("Password", ErrorMessage = "A palavra-passe e a confirmação da palavra passe não correspondem.")]
        public string ConfirmPassword { get; set; }
        /// <value>
        /// Propriedade que guarda email do utilizador
        /// </value>
        public string Email { get; set; }
        /// <value>
        /// Propriedade que guarda Token gerado
        /// </value>
        public string Token { get; set; }
    }
}