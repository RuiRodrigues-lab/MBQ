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
    /// Controller relativo ao Modelo Inscricao.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class InscricoesController : Controller
    {
        private readonly AppMovimentoBQDbContext _context;

        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        public InscricoesController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Inscricao
        /// </summary>
        /// <returns>devolve view com lista de inscrições realizadas e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            var appMovimentoBQDbContext = _context.Inscricoes.Include(i => i.Evento).Include(i => i.Voluntario);
            return View(await appMovimentoBQDbContext.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe Inscricao, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe Inscricao guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes
                .Include(i => i.Evento)
                .Include(i => i.Voluntario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo ViewModelNovaInscricao
        /// obtém dados da BD e atribui a lista de eventos e voluntarios
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe ViewModelNovaInscricao</returns>
        public IActionResult Create()
        {
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento");
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome");
            List<Evento> eventos = _context.Eventos.ToList();
            List<Voluntario> voluntarios = _context.Voluntarios.ToList();

            ViewModelNovaInscricao viewModelNovaInscricao = new ViewModelNovaInscricao();
            viewModelNovaInscricao.Eventos = eventos;
            viewModelNovaInscricao.Voluntarios = voluntarios;


            return View(viewModelNovaInscricao);
        }

        /// <summary>
        ///  POST: Inscricoes/Create
        ///  Método que adiciona objeto Inscricao adicionando à BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="inscricao">objeto  da classe Inscricao criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataInscrição,VoluntarioID,EventoID")] Inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento", inscricao.EventoID);
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome", inscricao.VoluntarioID);
            return View(inscricao);
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Inscricao que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound();
            }
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento", inscricao.EventoID);
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome", inscricao.VoluntarioID);
            return View(inscricao);
        }

        /// <summary>
        ///  POST: Inscricoes/Edit
        ///  Método que edita objeto do tipo Inscricao que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="inscricao">objeto  da classe Inscricao editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataInscrição,VoluntarioID,EventoID")] Inscricao inscricao)
        {
            if (id != inscricao.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscricaoExists(inscricao.ID))
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
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "ID", inscricao.EventoID);
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "ID", inscricao.VoluntarioID);
            return View(inscricao);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Inscricao selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Inscricao</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes
                .Include(i => i.Evento)
                .Include(i => i.Voluntario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
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
            var inscricao = await _context.Inscricoes.FindAsync(id);
            _context.Inscricoes.Remove(inscricao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscricaoExists(int id)
        {
            return _context.Inscricoes.Any(e => e.ID == id);
        }
    }
}
