using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Produtos Doados
    /// </summary>
    public class ProdutoDoado
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Produtos Doados.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda a quantidade de produto doado.
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
        /// Chave Estrangeira. Propriedade que guarda o ID de Doação.
        /// </value>
        [ForeignKey("Doacao")]
        public int DoacaoID { get; set; }
        /// <value>
        /// Navegation Property que guarda a doação.
        /// </value>
        public virtual Doacao Doacao { get; set; }
    }
}
