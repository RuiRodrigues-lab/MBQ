using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Produtos Entregues
    /// </summary>
    public class ProdutoEntregue
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Produtos Entregues.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda a quantidade de produto entregue.
        /// </value>
        public int Quantidade { get; set; }
        /// <value>
        /// Chave Estrangeira. Propriedade que guarda o ID de Produto.
        /// </value>
        [ForeignKey("Produto")]
        public int ProdutoID { get; set; }
        /// <value>
        /// Navegation Property que guarda o tipo de produto.
        /// </value>
        public virtual Produto Produto { get; set; }
        /// <value>
        /// Chave Estrangeira. Propriedade que guarda o ID de Entrega realizada à Família.
        /// </value>
        [ForeignKey("EntregaFamilia")]
        public int EntregaFamiliaID { get; set; }
        /// <value>
        /// Navegation Property que guarda a entrega realizada à Família.
        /// </value>
        public virtual EntregaFamilia EntregaFamilia { get; set; }
     
    }
}
