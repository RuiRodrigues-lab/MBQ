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
    /// Controller relativo ao Modelo Categorias.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class CategoriasController : Controller
    {

        /// <value>
        /// Variável local do tipo AppMovimentoBQDbContext
        /// </value>
        private readonly AppMovimentoBQDbContext _context;

        /// <summary>
        /// Método que cria variável local e cujo objeto AppMovimentoBDQContext lhe é atribuído
        /// </summary>
        public CategoriasController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Categorias
        /// </summary>
        /// <returns>devolve view com lista de Categorias presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        /// <summary>
        /// Método que permite ler todos os detalhes de objeto da classe Categoria, selecionado em view, cujos dados estão guardados em BD 
        /// </summary>
        /// <param name="id">ID do objeto da classe Categoria guardado em BD</param>
        /// <returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo Categoria
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe Categoria</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///  POST: Categorias/Create
        ///  Método que adiciona objeto Categoria criado na BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="categoria">objeto  da classe Categoria criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NomeCategoria")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Categoria que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        /// <summary>
        ///  POST: Categorias/Edit
        ///  Método que edita objeto Categoria criado na BD e atualiza a BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="categoria">objeto  da classe Categoria editado no Formulário</param>
        /// <returns>View edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NomeCategoria")] Categoria categoria)
        {
            if (id != categoria.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.ID))
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
            return View(categoria);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Categoria selecionado e guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Categoria</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
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
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.ID == id);
        }
    }
}
