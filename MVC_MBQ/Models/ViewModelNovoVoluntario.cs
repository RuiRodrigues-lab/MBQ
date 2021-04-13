using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    public class ViewModelNovoVoluntario
    {
        /// <value>
        /// Objeto do tipo ResponseAPI
        /// </value>
        public ResponseAPI ResponsesAPI { get; set; }
        /// <value>
        /// Objeto do tipo Voluntário
        /// </value>
        public Voluntario Voluntario { get; set; }
        /// <value>
        /// Objeto com valor true atribuido para verificação de data de nascimento
        /// </value>
        public Boolean VerificaDataNascimento = true;
    }
}
