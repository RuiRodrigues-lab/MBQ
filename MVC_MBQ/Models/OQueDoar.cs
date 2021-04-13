using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe O que Doar. Dados não guardados em BD
    /// </summary>
    public class OQueDoar 
    {
        /// <value>
        /// Propriedade que guarda o nome de quem está a enviar mensagem
        /// </value>
        public string Nome { get; set; }
        /// <value>
        /// Propriedade que guarda o apelido de quem está a enviar mensagem
        /// </value>
        public string Apelido { get; set; }
        /// <value>
        /// Propriedade que guarda o email de quem está a enviar mensagem
        /// </value>
        public string Email { get; set; }
        /// <value>
        /// Propriedade que guarda o número de telefone de quem está a enviar mensagem
        /// </value>
        public int Contacto { get; set; }
    }
}
