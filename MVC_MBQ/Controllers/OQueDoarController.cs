using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVC_MBQ.Data;
using MVC_MBQ.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVC_MBQ.Controllers
{
    /// <summary>
    /// Controller relativo ao Modelo OQueDoar.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class OQueDoarController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe IConfiguration
        /// </summary>
        private readonly IConfiguration _configuration;

        public OQueDoarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Método da view Index referente ao modelo OQueDoar
        /// </summary>
        /// <returns>devolve view com lista de entregas a famílias realizadas e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Método da view OQueDoar referente ao modelo OQueDoar
        /// </summary>
        /// <returns>devolve view com formulario para enviar mensagem a questionar o que doar  realizadas e presentes em BD</returns>
        public IActionResult OQueDoar()
        {
            return View();
        }

        /// <summary>
        /// Método da view OQueDoar referente ao modelo EmailFormModel
        /// </summary>
        /// <returns>devolve view com mensagem enviada a questionar o que doar  realizadas e presentes em BD</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OQueDoar(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {

                var email = model.Email;
                var body = "<p>Email From: {0} ({1})</p><p>Assunto: O que Doar?</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();

                    message.To.Add(new MailAddress(model.FromEmail));
                    message.Bcc.Add(new MailAddress("quentinhopapi@gmail.com"));  // replace with valid value 

                message.From = new MailAddress("quentinhopapi@gmail.com");  // replace with valid value
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "quentinhopapi@gmail.com",  // replace with valid value
                        Password = _configuration["PasswordEmail"] // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "	smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");

                }
            }
            return View(model);
        }
        /// <summary>
        /// Método que confirma envio de mensagem
        /// </summary>
        /// <returns>view Sent</returns>
        public ActionResult Sent()
        {
             return View();
        }

    }
}
