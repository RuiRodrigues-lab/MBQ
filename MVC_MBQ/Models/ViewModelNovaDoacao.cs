using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe ViewModelNovaDoacao com Doacao, Lista de Voluntários, Lista de Eventos e Lista de Produtos. 
    /// </summary>
    public class ViewModelNovaDoacao
    {
        /// <value>
        /// Objeto do tipo Doação
        /// </value>
        public Doacao Doacao { get; set; }
        /// <value>
        /// Lista de Voluntários
        /// </value>
        public List<Voluntario> Voluntarios { get; set; }
        /// <value>
        /// Lista de Eventos
        /// </value>
        public List<Evento> Eventos { get; set; }
        /// <value>
        /// Lista de Produtos
        /// </value>
        public List<Produto> Produtos { get; set; }
  

    }
}
