using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe Voluntário
    /// </summary>
    public class Voluntario
    {
        /// <value>
        /// Chave Primária.Propriedade que guarda ID de Voluntário.
        /// </value>
        [Key]
        public int ID { get; set; }
        /// <value>
        /// Propriedade que guarda o Nome do voluntário
        /// </value>
        public string? Nome { get; set; }
        /// <value>
        /// Propriedade que guarda a data de nascimento do voluntário
        /// </value>
        public DateTime DataNascimento { get; set; }
        /// <value>
        /// Propriedade que guarda a morada onde o voluntário reside.
        /// </value>
        public string? Morada { get; set; }
        /// <value>
        /// Propriedade que guarda a cidade onde voluntário reside.
        /// </value>
        public string? Cidade { get; set; }
        /// <value>
        /// Propriedade que guarda o Distrito onde Voluntário reside.
        /// </value>
        public string? Distrito { get; set; }
        /// <value>
        /// Propriedade que guarda os 1ºs 4 digitos do código postal onde voluntário reside.
        /// </value>
        public int? Codigo { get; set; }
        /// <value>
        /// Propriedade que guarda os últimos 3 digitos do código postal onde voluntário reside.
        /// </value>
        public int? Postal { get; set; }
        /// <value>
        /// Propriedade que guarda email do voluntário.
        /// </value>
        public string? Email { get; set; }
        /// <value>
        /// Propriedade que guarda descrição associada ao voluntário.
        /// </value>
        public string? Descricao { get; set; }
        /// <value>
        /// Propriedade que guarda se voluntário deu consentimento para utilização de contactos.
        /// </value>
        public bool ConsentimentoRGPD { get; set; }
        /// <value>
        /// Propriedade que guarda se voluntário faz parte de escuteiros ou não
        /// </value>
        public bool Interno { get; set; }
        /// <value>
        /// Navegation property com lista de inscrições
        /// </value>
        public virtual List<Inscricao> Inscricoes { get; set; }
        /// <value>
        /// Navegation property com lista de doações
        /// </value>
        public virtual List<Doacao> Doacoes { get; set; }

    }
}
