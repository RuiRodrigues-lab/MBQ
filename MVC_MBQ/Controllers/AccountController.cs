using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Controllers
{
    /// <summary>
    /// Controller relativo à autenticação através de facebook.
    /// Fornece/Implementa todos os métodos relativos autenticação por facebook
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Método para fazer log in através de Facebook
        /// </summary>
        /// <returns> Devolve página de autenticação pelo log in</returns>
        [Route("facebook-login")]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("FacebookResponse") };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }
        /// <summary>
        /// Método de resposta de Facebook após log in 
        /// </summary>
        /// <returns>Devolve JSON result decorrente de autenticação realizada em Facebook</returns>
        [Route("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);
        }
    }

}