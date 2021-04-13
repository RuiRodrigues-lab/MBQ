using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Membros de Família
    /// </summary>
    public class MembroFamilia
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Famílias.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda o nome do membro da Família.
        /// </value>
        public string? Nome { get; set; }
        /// <value>
        /// Propriedade que guarda o grau de parentesco do membro da Família.
        /// </value>
        public string? GrauParentesco { get; set; }
        /// <value>
        /// Propriedade que guarda a data de nascimento do membro da Família.
        /// </value>
        public DateTime DataNascimento { get; set; }
        /// <value>
        /// Propriedade que guarda a morada onde o membro da Família reside.
        /// </value>
        public string? Morada { get; set; }
        /// <value>
        /// Propriedade que guarda a cidade onde membro da Família reside.
        /// </value>
        public string? Cidade { get; set; }
        /// <value>
        /// Propriedade que guarda o Distrito onde membro da Família reside.
        /// </value>
        public string? Distrito { get; set; }
        /// <value>
        /// Propriedade que guarda os 1ºs 4 digitos do código postal onde membro da Família reside.
        /// </value>
        public int? Codigo { get; set; }
        /// <value>
        /// Propriedade que guarda os últimos 3 digitos do código postal onde membro da Família reside.
        /// </value>
        public int? Postal { get; set; }
        /// <value>
        /// Propriedade que guarda o email do membro da Família.
        /// </value>
        public string? Email { get; set; }
        /// <value>
        /// Propriedade que guarda descrição associada ao membro da Família.
        /// </value>
        public string? Descricao { get; set; }
        /// <value>
        /// Chave estrangeira. Propriedade que guarda o ID da Família.
        /// </value>
        [ForeignKey("Familia")]
        public int FamiliaID { get; set; }
        /// <value>
        /// Navegation property com dados de Famílias
        /// </value>
        public virtual Familia Familia { get; set; }
    }
}
