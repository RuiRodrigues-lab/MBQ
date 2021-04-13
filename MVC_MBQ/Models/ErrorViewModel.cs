using System;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe de Erros referentes ao identity
    /// </summary>
    public class ErrorViewModel
    {
        /// <value>
        /// Propriedade que guarda o ID requerido
        /// </value>
        public string RequestId { get; set; }
        /// <value>
        /// Propriedade que retorna valor True/False validando a existência de Request ID. 
        /// </value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
