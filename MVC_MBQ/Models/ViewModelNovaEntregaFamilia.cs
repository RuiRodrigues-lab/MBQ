using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe ViewModelNovaEntregaFamilia com Entrega a Familia, Lista de Famílias e Lista de Produtos. 
    /// </summary>
    public class ViewModelNovaEntregaFamilia

    {
        /// <value>
        /// Objeto EntregaFamilia
        /// </value>
        public EntregaFamilia EntregaFamilia { get; set; }
        /// <value>
        /// Lista de Famílias
        /// </value>
        public List<Familia> Familias { get; set; }
        /// <value>
        /// Lista de Produtos
        /// </value>
        public List<Produto> Produtos { get; set; }


    }
}
