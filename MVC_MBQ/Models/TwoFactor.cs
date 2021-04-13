using System.ComponentModel.DataAnnotations;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe TwoFactor. 
    /// </summary>
    public class TwoFactor
    {
        /// <value>
        /// Propriedade que guarda string de TwoFactorCode
        /// </value>
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
