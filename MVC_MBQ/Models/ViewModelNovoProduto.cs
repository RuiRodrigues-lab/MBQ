using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe ViewModelNovoProduto com Produto e Lista de Categorias de produtos.
    /// </summary>
    public class ViewModelNovoProduto
    {
        /// <value>
        /// Objeto do tipo Produto
        /// </value>
        public Produto Produto { get; set; }
        /// <value>
        /// Lista de Categorias
        /// </value>
        public List<Categoria> Categorias { get; set; }
    }
}
