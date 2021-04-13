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
    /// Controller relativo ao Modelo ProdutosDoados.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class ProdutosDoadosController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        public ProdutosDoadosController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo ProdutosDoados
        /// </summary>
        /// <returns>devolve view com lista de Produtos Doados e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            var appMovimentoBQDbContext = _context.ProdutosDoados.Include(p => p.Doacao).Include(p => p.Produto);
            return View(await appMovimentoBQDbContext.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe ProdutoDoado, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe ProdutoDoado guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDoado = await _context.ProdutosDoados
                .Include(p => p.Doacao)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produtoDoado == null)
            {
                return NotFound();
            }

            return View(produtoDoado);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo ProdutoDoado
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <returns>devolve view com formulário para criar o objeto da classe ProdutoDoado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDoado = await _context.ProdutosDoados.FindAsync(id);
            if (produtoDoado == null)
            {
                return NotFound();
            }

            ViewData["DoacaoID"] = new SelectList(_context.Doacoes, "ID", "DataDoacao", produtoDoado.DoacaoID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "NomeProduto", produtoDoado.ProdutoID);
            return View(produtoDoado);
        }

        /// <summary>
        ///  POST: ProdutosDoados/Edit
        ///  Método que edita objeto do tipo ProdutoDoado que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="produtoDoado">objeto  da classe ProdutoDoado editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantidade,ProdutoID,DoacaoID")] ProdutoDoado produtoDoado)
        {
            if (id != produtoDoado.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoDoado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoDoadoExists(produtoDoado.ID))
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
            ViewData["DoacaoID"] = new SelectList(_context.Doacoes, "ID", "ID", produtoDoado.DoacaoID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", produtoDoado.ProdutoID);
            return View(produtoDoado);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe ProdutoDoado selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe ProdutoDoado</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDoado = await _context.ProdutosDoados
                .Include(p => p.Doacao)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produtoDoado == null)
            {
                return NotFound();
            }

            return View(produtoDoado);
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
            var produtoDoado = await _context.ProdutosDoados.FindAsync(id);
            _context.ProdutosDoados.Remove(produtoDoado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoDoadoExists(int id)
        {
            return _context.ProdutosDoados.Any(e => e.ID == id);
        }
    }
}
