using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe User
    /// </summary>
    public class User
    {
        /// <value>
        /// Propriedade que guarda o username do utilizador
        /// </value>
        [Required]
        public string UserName { get; set; }

        /// <value>
        /// Propriedade que guarda e valida email do utilizador
        /// </value>
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        /// <value>
        /// Propriedade que guarda password do utilizador
        /// </value>
        [Required]
        public string Password { get; set; }

        /*public Country Country { get; set; }

        public int Age { get; set; }

        [Required]
        public string Salary { get; set; }*/
    }
}