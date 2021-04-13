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
    /// Controller relativo ao Modelo ProdutoEntregue.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class ProdutosEntreguesController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        public ProdutosEntreguesController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo ProdutoEntregue
        /// </summary>
        /// <returns>devolve view com lista de produtos doados e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            var appMovimentoBQDbContext = _context.ProdutosEntregues.Include(p => p.EntregaFamilia).Include(p => p.Produto).Include("EntregaFamilia.Familia");
            return View(await appMovimentoBQDbContext.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe ProdutoEntregue, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe ProdutoEntregue guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoEntregue = await _context.ProdutosEntregues
                .Include(p => p.EntregaFamilia)
                .Include(p => p.Produto)
                 .Include("EntregaFamilia.Familia")

                .FirstOrDefaultAsync(m => m.ID == id);
            if (produtoEntregue == null)
            {
                return NotFound();
            }

            return View(produtoEntregue);
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe ProdutoEntregue que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var produtoEntregue = await _context.ProdutosEntregues.FindAsync(id);


            if (produtoEntregue == null)
            {
                return NotFound();
            }
            ViewData["EntregaFamiliaID"] = new SelectList(_context.EntregaFamilias, "ID", "ID", produtoEntregue.EntregaFamiliaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "NomeProduto", produtoEntregue.ProdutoID);
            return View(produtoEntregue);
        }

        /// <summary>
        ///  POST: ProdutosEntregues/Edit
        ///  Método que edita objeto do tipo ProdutoEntregue que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        ///  <param name="id">id de objeto a ser alterado</param>
        /// <param name="produtoEntregue">objeto  da classe ProdutoEntregue editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantidade,ProdutoID,EntregaFamiliaID")] ProdutoEntregue produtoEntregue)
        {
            if (id != produtoEntregue.ID)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoEntregue);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoEntregueExists(produtoEntregue.ID))
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
            ViewData["EntregaFamiliaID"] = new SelectList(_context.EntregaFamilias, "EntregaFamiliaID", "ID", produtoEntregue.EntregaFamiliaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "NomeProduto", produtoEntregue.ProdutoID);
            return View(produtoEntregue);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe ProdutoEntregue selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe ProdutoEntregue</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoEntregue = await _context.ProdutosEntregues
                .Include(p => p.EntregaFamilia)
                .Include(p => p.Produto)
                .Include("EntregaFamilia.Familia")

                .FirstOrDefaultAsync(m => m.ID == id);
            if (produtoEntregue == null)
            {
                return NotFound();
            }

            return View(produtoEntregue);
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
            var produtoEntregue = await _context.ProdutosEntregues.FindAsync(id);
            _context.ProdutosEntregues.Remove(produtoEntregue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoEntregueExists(int id)
        {
            return _context.ProdutosEntregues.Any(e => e.ID == id);
        }
    }
}
