using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe EmailFormModel que herda de classe Newsletter
    /// </summary>
    public class EmailFormModel : Newsletter
    {
        /// <value>
        /// Propriedade que guarda dados de Assunto de mensagem a enviar
        /// </value>
        [Required, Display(Name = "Assunto")]
        public string Subject { get; set; }
        /// <value>
        /// Propriedade que guarda nome de quem provém a mensagem
        /// </value>
        [Required, Display(Name = "Nome")]
        public string FromName { get; set; }
        /// <value>
        /// Propriedade que guarda a email de origem da mensagem
        /// </value>
        [Required, Display(Name = "Email"), EmailAddress]
        public string FromEmail { get; set; }
        /// <value>
        /// Propriedade que guarda  a mensagem enviada
        /// </value>
        [Required]
        public string Message { get; set; }

    }
}
