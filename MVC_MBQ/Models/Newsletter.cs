using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Newsletter
    /// </summary>
    public class Newsletter
    {
        /// <value>
        /// Chave Primária. Propriedade que guarda dados de ID de Newsletters
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda se subscrição para newsletter se encontra ativa ou não
        /// </value>
        public bool Ativo { get; set; }
        /// <value>
        /// Propriedade que guarda email que subscreveu newsletter
        /// </value>
        public string Email { get; set; }
        /// <value>
        /// Propriedade que guarda se foi dado ou não consentimento para utilização de dados mediante regras de RGPD
        /// </value>
        public bool ConsentimentoRGPD { get; set; }



    }
}
