using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_MBQ.Models;

namespace MVC_MBQ.Controllers
{
    /// <summary>
    /// Controller relativo ao Modelo Evento.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class EventoesController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        public EventoesController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Evento
        /// </summary>
        /// <returns>devolve view com lista de eventos realizados e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe Evento, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe Evento guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo Evento
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe Evento</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///  POST: Eventos/Create
        ///  Método que adiciona objeto Evento criado na BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="evento">objeto  da classe Evento criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NomeEvento,Telemovel,Email,Descricao,DataEvento")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Evento que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        /// <summary>
        ///  POST: Eventoes/Edit
        ///  Método que edita objeto do tipo Evento que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="evento">objeto  da classe Evento editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NomeEvento,Telemovel,Email,Descricao")] Evento evento)
        {
            if (id != evento.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.ID))
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
            return View(evento);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Evento selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Evento</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        /// <summary>
        /// Método que apaga objeto selecionado e atualiza a BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar </param>
        /// <returns>devolve view Index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.ID == id);
        }
    }
}
