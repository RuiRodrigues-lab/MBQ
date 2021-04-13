using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Famílias
    /// </summary>
        public class Familia
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Famílias.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda o nome da Família.
        /// </value>
        public string Nome { get; set; }
        /// <value>
        /// Navegation Property lista de Entregas realizadas a famílias
        /// </value>
        public virtual List<EntregaFamilia> EntregasFamilia { get; set; }
        /// <value>
        /// Navegation Property lista de Membros de família
        /// </value>
        public virtual List<MembroFamilia> MembrosFamilia{ get; set; }


    }


}
