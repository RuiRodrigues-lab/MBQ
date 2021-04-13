using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe AppMovimentoBQDbContext que herda da classe IdentityUser. 
    /// </summary>
    public class AppUser : IdentityUser
    {
        /*public Country Country { get; set; }

        public int Age { get; set; }

        [Required]
        public string Salary { get; set; }*/
    }
}
