using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe Doação
    /// </summary>
    public class Doacao
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Doações.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda Data de Doação.
        /// </value>
        public DateTime DataDoacao { get; set; }
        /// <value>
        /// Propriedade que guarda chave estrangeira Id de Evento.
        /// </value>
        [ForeignKey("Evento")]
        public int EventoID { get; set; }
        /// <summary>
        /// Propriedade que guarda Navegation Property Evento
        /// </summary>
        public virtual Evento Evento { get; set; }
        /// <value>
        /// Propriedade que guarda chave estrangeira Id de Voluntário.
        /// </value>
        [ForeignKey("Voluntario")]
        public int VoluntarioID { get; set; }
        /// <value>
        /// Navegation Property Voluntário
        /// </value>
        public virtual Voluntario Voluntario { get; set; }
        /// <value>
        /// Navegation Property lista de Produtos Doados
        /// </value>
        public virtual List<ProdutoDoado> ProdutosDoados { get; set; }

      
    }
}
