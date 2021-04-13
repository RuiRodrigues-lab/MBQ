using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Produtos
    /// </summary>
    public class Produto
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Produtos.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda o nome da Produto.
        /// </value>
        public string NomeProduto { get; set; }
        /// <value>
        /// Propriedade que guarda a quantidade mínima requerida para o tipo de produto.
        /// </value>
        public int QuantidadeMinima { get; set; }
        /// <value>
        /// Chave estrangeira.Guarda dados de ID de Categoria
        /// </value>
        [ForeignKey("Categoria")]
        public int CategoriaID { get; set; }
        /// <value>
        /// Navegation Property de categorias de produtos
        /// </value>
        public virtual Categoria Categoria { get; set; }
        /// <value>
        /// Navegation Property lista de produtos entregues a famílias
        /// </value>
        public virtual List<ProdutoEntregue> ProdutosEntregues { get; set; }
        /// <value>
        /// Navegation Property lista de doações realizadas
        /// </value>
        public virtual List<ProdutoDoado> ProdutosDoados { get; set; }

    }
}
