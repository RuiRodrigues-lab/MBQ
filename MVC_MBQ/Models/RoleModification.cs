using System.ComponentModel.DataAnnotations;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe RoleModification
    /// </summary>
    public class RoleModification
    {
        /// <value>
        /// Propriedade que guarda o nome do Role criado
        /// </value>
        [Required]
        public string RoleName { get; set; }
        /// <value>
        /// Propriedade que guarda o ID do Role criado
        /// </value>
        public string RoleId { get; set; }
        /// <value>
        /// Propriedade que guarda IDs adicionados
        /// </value>
        public string[] AddIds { get; set; }
        /// <value>
        /// Propriedade que guarda IDs apagados
        /// </value>
        public string[] DeleteIds { get; set; }
    }
}