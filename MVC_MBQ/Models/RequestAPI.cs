using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe RequestAPI - Dados enviados para API
    /// </summary>
    public class RequestAPI
    {
        /// <value>
        /// Propriedade que guarda o email a ser validado pela API
        /// </value>
        public string Mail { get; set; }

    }
}
