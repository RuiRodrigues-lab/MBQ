using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_MBQ.Data;
using MVC_MBQ.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace MVC_MBQ.Controllers
{
    /// <summary>
    /// Controller relativo às páginas do website.
    /// </summary>

    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// variável local do tipo ILogger para gestão de interface
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// variável local to tipo Data.ApplicationDbContext
        /// Para gestão de identity
        /// </summary>
        private readonly Data.ApplicationDbContext _application;

        private UserManager<AppUser> userManager;

        /// <summary>
        /// Método que atribui a variaveis locais informação guardada na BD.
        /// </summary>
        /// <param name="logger">objeto do tipo ILogger. </param>
        /// <param name="application">objeto do tipo  Data.ApplicationDbContext.</param>
        /// <param name="userMgr">objeto do tipo UserManager.</param>
        public HomeController(ILogger<HomeController> logger, Data.ApplicationDbContext application, UserManager<AppUser> userMgr)
        {
            _logger = logger;
            _application = application;
            userManager = userMgr;
        }
        /// <summary>
        /// Método da view Index do website.
        /// Permite acesso de utilizadores não autenticados
        /// Obtém data de atualização do ficheiro xml
        /// </summary>
        /// <returns>view Index com data de atualização</returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            string path = Path.GetFullPath("MVC_MBQ.xml");
            var date = System.IO.File.GetLastWriteTime(path).ToString();

            ViewBag.date = date;

            return View();
        }

        /// <summary>
        /// Método da view PapiAppi do website.
        /// Obtém data de atualização do ficheiro xml
        /// Acesso apenas a utilizadores autenticados
        /// </summary>
        /// <returns>view da PapiAppi com data de atualização.Caso utilizador não autenticado devolve view _noAcess</returns>
        [Authorize(Roles = "Administrador, Gestor, Visitante")]
        public async Task<IActionResult> PapiApp()
        {
            string path = Path.GetFullPath("MVC_MBQ.xml");
            var date = System.IO.File.GetLastWriteTime(path).ToString();

            ViewBag.date = date;

            if (User.Identity.IsAuthenticated)
            {
                return View("PapiApp");
            }
            else
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = "É obrigatório iniciar sessão!";

                return View("_NoAcess", errorViewModel);
            }
        }

        /// <summary>
        /// Método da view Equipa do website.
        /// Acesso apenas a utilizadores autenticados
        /// </summary>
        /// <returns>view da Equipa. Caso utilizador não autenticado devolve view _noAcess</returns>
        public async Task<IActionResult> Equipa()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = "Necessário Login";

                return View("_NoAcess", errorViewModel);
            }
        }
        /// <summary>
        /// Método para Erro
        /// </summary>
        /// <returns>view com erro especificado</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Método que devolve view de Como Ajudar
        /// </summary>
        /// <returns>view de ComoAjudar</returns>
        public IActionResult ComoAjudar()
        {
            return View();
        }

        /// <summary>
        /// Método que devolve view de página pessoal de Ana Domingues
        /// </summary>
        /// <returns>view de AnaDomingues</returns>
        public IActionResult AnaDomingues()
        {
            return View();
        }
        /// <summary>
        /// Método que devolve view de página pessoal de Carlos Brito
        /// </summary>
        /// <returns>view de CarlosBrito</returns>
        public IActionResult CarlosBrito()
        {
            return View();
        }
        /// <summary>
        /// Método que devolve view de Contactos do MBQ
        /// </summary>
        /// <returns>devolve view de Contactos</returns>
        public IActionResult Contactos()
        {
            return View();
        }
        /// <summary>
        /// Método que devolve view da Política de  Privacidade
        /// </summary>
        /// <returns>devolve view de Privacidade</returns>
        public IActionResult Privacidade()
        {
            return View();
        }
        /// <summary>
        /// Método que devolve view de Campanha de Donativos realizadas pelo MBQ
        /// </summary>
        /// <returns>devolve view de CampanhaDonativos</returns>
        public IActionResult CampanhaDonativos()
        {
            return View();
        }
        /// <summary>
        /// Método que devolve view de página pessoal de João Silva
        /// </summary>
        /// <returns>view de JoaoSilva</returns>
        public IActionResult JoaoSilva()
        {
            return View();
        }
        /// <summary>
        ///  Método que devolve view de Quem Somos na qual explica o propósito do MBQ
        /// </summary>
        /// <returns>devolve view de QuemSomos</returns>
        public IActionResult QuemSomos()
        {
            return View();
        }
        /// <summary>
        ///  Método que devolve view de Tipos de Donativos na qual explica os tipos de donativos que MBQ aceitam
        /// </summary>
        /// <returns>devolve view de TiposDonativo</returns>
        public IActionResult TiposDonativo()
        {
            return View();
        }
        /// <summary>
        ///  Método que devolve view de Documentação técnica e de utilização da PapiAppi
        /// </summary>
        /// <returns>devolve view de Docs</returns>
        public IActionResult Docs()
        {
            return View();
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}
