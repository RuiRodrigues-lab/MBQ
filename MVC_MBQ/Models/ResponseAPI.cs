using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe ResponseAPI - Dados recebidos da API
    /// </summary>
    public class ResponseAPI
    {
        /// <value>
        /// Propriedade que guarda o status de pedido
        /// </value>
        public string Status { get; set; }
        /// <value>
        /// Propriedade que guarda o resultado de pedido
        /// </value>
        public string Result { get; set; }
        /// <value>
        /// Propriedade que guarda a lista de Flags do pedido
        /// </value>
        public List<string> Flags { get; set; }
        /// <value>
        /// Propriedade que guarda a sugestão de correção do email verificado
        /// </value>
        public string SuggestedCorrection { get; set; }
        /// <value>
        /// Propriedade que guarda o tempo decorrido entre pedido e resposta
        /// </value>
        public int ExecutionTime { get; set; }
    }
}
