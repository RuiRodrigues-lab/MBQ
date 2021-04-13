using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe Entrega a Famílias
    /// </summary>
    public class EntregaFamilia
    {
        /// <value>
        /// Chave Primária. Propriedade que guarda ID de Entrega a Famílias
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda as datas de Entrega a Famílias
        /// </value>
        public DateTime DataEntrega { get; set; }
        /// <value>
        /// Chave Estrangeira. Propriedade que guarda o ID de Famílias
        /// </value>
        [ForeignKey("Familia")]
        public int FamiliaID { get; set; }
        /// <value>
        /// Navegation Property que guarda dados de Famílias
        /// </value>
        public virtual Familia Familia { get; set; }
        /// <value>
        /// Propriedade que guarda Navegation Property lista de Produtos Entregues
        /// </value>
        public virtual List<ProdutoEntregue> ProdutosEntregues { get; set; }

    }
}
