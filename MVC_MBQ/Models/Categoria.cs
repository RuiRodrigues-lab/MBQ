using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe Categoria
    /// </summary>
    public class Categoria
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Categoria.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda o Nome das Categorias
        /// </value>
        public string NomeCategoria { get; set; }
        /// <value>
        /// Propriedade Navegation Property. Lista de tipo de Produtos.
        /// </value>
        public virtual List<Produto> Produtos { get; set; }


    }
}
