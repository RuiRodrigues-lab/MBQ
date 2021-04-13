using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe Inscrições
    /// </summary>
    public class Inscricao
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Inscrições.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda a Data de Inscrição
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataInscrição { get; set; }
        /// <value>
        /// Chave Estrangeira que guarda o ID de Voluntário
        /// </value>
        [ForeignKey("Voluntario")]
        public int VoluntarioID { get; set; }
        /// <value>
        /// Navegation Property que guarda Voluntário
        /// </value>
        public virtual Voluntario Voluntario { get; set; }
        /// <value>
        /// Chave Estrangeira que guarda o ID de Evento
        /// </value>
        [ForeignKey("Evento")]
        public int EventoID { get; set; }
        /// <value>
        /// Navegation Property que guarda Evento
        /// </value>
        public virtual Evento Evento { get; set; }
    }
}
