using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVC_MBQ.Models;

namespace MVC_MBQ.Controllers
{
    /// <summary>
    /// Controller relativo ao Modelo  Newsletter.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class NewslettersController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe IConfiguration
        /// </summary>
        private readonly IConfiguration _configuration;

        public NewslettersController(AppMovimentoBQDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Newsletters
        /// </summary>
        /// <returns>devolve view com lista de emails subscritos para Newsletters realizados e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Newsletters.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe Newsletter, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe Newsletter guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo Newsletter
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe Newsletter</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///  POST: Newsletters/Create
        ///  Método que adiciona objeto Newsletter adicionando à BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="newsletter">objeto  da classe Newsletter criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ativo,Email,ConsentimentoRGPD")] Newsletter newsletter)
        {
            if(newsletter.Ativo == false && newsletter.ConsentimentoRGPD == false)
            {
                newsletter.Ativo = true;
                newsletter.ConsentimentoRGPD = true;
            } 
            if (ModelState.IsValid)
            {
                _context.Add(newsletter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsletter);
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Newsletter que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }
            return View(newsletter);
        }

        //// <summary>
        ///  POST: Newsletters/Edit
        ///  Método que edita objeto do tipo Newsletter que se encontra na BD e atualiza a BD 
        ///  Valida AntiforferyToken e apenas executa se validado
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="categoria">objeto  da classe Newsletter editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ativo,Email,ConsentimentoRGPD")] Newsletter newsletter)
        {
            if (id != newsletter.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsletter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsletterExists(newsletter.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsletter);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Newsletter selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Newsletter</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        /// <summary>
        /// Método que apaga objeto selecionado e atualiza a BD
        /// Valida AntiforferyToken e apenas executa se validado
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar </param>
        /// <returns>devolve view Index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsletter = await _context.Newsletters.FindAsync(id);
            _context.Newsletters.Remove(newsletter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Metodo que verifica se id seleccionado de Newsletter existe em BD
        /// </summary>
        /// <param name="id">ID de Newsletter selecionado</param>
        /// <returns></returns>
        private bool NewsletterExists(int id)
        {
            return _context.Newsletters.Any(e => e.ID == id);
        }

        [HttpGet]
        public IActionResult EnviarNewsletter()
        {
            return View();
        }

        /// <summary>
        /// Método que devolve a view EnviarNewsletter do modelo EmailFormModel para enviar mensagem a um contacto de email 
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe EmailFormModel</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnviarNewsletter(EmailFormModel model)
        {

            List<Newsletter> listaNewsletter = _context.Newsletters.ToList();

            if (ModelState.IsValid)
            {

                var email = model.Email;
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                foreach (Newsletter n in listaNewsletter)
                {
                    if (n.Ativo == true && n.ConsentimentoRGPD)
                        message.Bcc.Add(new MailAddress(n.Email)); 
                }
                message.From = new MailAddress("quentinhopapi@gmail.com");  
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "quentinhopapi@gmail.com",  
                        Password = _configuration["PasswordEmail"] 
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
        /// Método que devolve view de confirmação de envio
        /// </summary>
        /// <returns>view Sent que confirma envio</returns>
        public ActionResult Sent()
        {
            return View();
        }

    }



}


