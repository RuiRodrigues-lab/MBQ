using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Eventos. Com interface IComparable
    /// </summary>
    public class Evento : IComparable
    {
        /// <summary>
        /// Chave Primária. Propriedade guarda o ID de Eventos
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda o nome do evento
        /// </value>
        public string NomeEvento { get; set; }
        /// <value>
        /// Propriedade que guarda o telemóvel da pessoa responsável pelo evento
        /// </value>
        public int? Telemovel { get; set; }
        /// <value>
        /// Propriedade que guarda o email da pessoa responsável pelo evento
        /// </value>
        public string Email { get; set; }
        /// <value>
        /// Propriedade que guarda a descrição do evento
        /// </value>
        public string Descricao { get; set; }
        /// <value>
        /// Propriedade que guarda a data do evento
        /// </value>
        public DateTime DataEvento { get; set; }
        /// <summary>
        /// Método que implementa a interface ordenando os eventos por ordem decrescente
        /// </summary>
        /// <param name="o">objeto evento a adicionar</param>
        /// <returns>Eventos ordenados</returns>
        public int CompareTo(object o)
        {
            Evento outroEvento = (Evento)o;
            return this.DataEvento.CompareTo(((Evento)o).DataEvento);
        }
        /// <value>
        /// Navegation property que guarda a lista das inscrições
        /// </value>
        public virtual List<Inscricao> Inscricoes { get; set; }
        /// <value>
        /// Navegation property que guarda a lista de doações.
        /// </value>
        public virtual List<Doacao> Doacoes { get; set; }

    }
}
