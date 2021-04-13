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
    /// Controller relativo ao Modelo Familia.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class FamiliasController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        public FamiliasController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Familia
        /// </summary>
        /// <returns>devolve view com lista de famílias presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Familias.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe Familia, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe Familia guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familia = await _context.Familias
                .Include(m => m.MembrosFamilia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (familia == null)
            {
                return NotFound();
            }

            return View(familia);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo Familia
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe Familia</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///  POST: Familias/Create
        ///  Método que adiciona objeto Familia criado na BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="familia">objeto  da classe Familia criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(familia);
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Familia que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familia = await _context.Familias.FindAsync(id);
            if (familia == null)
            {
                return NotFound();
            }
            return View(familia);
        }

        /// <summary>
        ///  POST: Familias/Edit
        ///  Método que edita objeto do tipo  Familia que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="familia">objeto  da classe Familia editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome")] Familia familia)
        {
            if (id != familia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamiliaExists(familia.ID))
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
            return View(familia);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Familia selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Familia</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familia = await _context.Familias
                .FirstOrDefaultAsync(m => m.ID == id);
            if (familia == null)
            {
                return NotFound();
            }

            return View(familia);
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
            var familia = await _context.Familias.FindAsync(id);
            _context.Familias.Remove(familia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamiliaExists(int id)
        {
            return _context.Familias.Any(e => e.ID == id);
        }
    }
}
