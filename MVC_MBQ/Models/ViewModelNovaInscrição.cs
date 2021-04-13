using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe ViewModelNovaInscricao com Inscrição, Lista de Voluntários, Lista de Eventos. 
    /// </summary>
    public class ViewModelNovaInscricao

    {
        /// <value>
        /// Objeto do tipo Inscrição
        /// </value>
        public Inscricao Inscricao { get; set; }
        /// <value>
        /// Lista de Voluntários
        /// </value>
        public List<Voluntario> Voluntarios { get; set; }
        /// <value>
        /// Lista de Eventos
        /// </value>
        public List<Evento> Eventos { get; set; }

    }
}
