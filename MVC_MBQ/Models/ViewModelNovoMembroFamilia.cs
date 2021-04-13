using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe ViewModelNovoMembroFamilia com ResponseAPI de validação de email, verificação de data de nascimento, Membro de Família e Lista de Famílias. 
    /// </summary>
    public class ViewModelNovoMembroFamilia
    {
        /// <value>
        /// Objeto do tipo ResponseAPI
        /// </value>
        public ResponseAPI ResponsesAPI { get; set; }
        /// <value>
        /// Objeto do tipo MembroFamilia
        /// </value>
        public MembroFamilia MembroFamilia { get; set; }
        /// <value>
        /// Lista de Famílias
        /// </value>
        public List<Familia> Familias { get; set; }
        /// <value>
        /// Objeto com valor true atribuido para verificação de data de nascimento
        /// </value>
        public Boolean VerificaDataNascimento = true;

    }
}
