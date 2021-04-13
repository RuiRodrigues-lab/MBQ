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
    /// Controller relativo ao Modelo EntregaFamilia.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class EntregaFamiliasController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        public EntregaFamiliasController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo EntregaFamilia
        /// </summary>
        /// <returns>devolve view com lista de entregas a famílias realizadas e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            var appMovimentoBQDbContext = _context.EntregaFamilias.Include(e => e.Familia);
            return View(await appMovimentoBQDbContext.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe EntregaFamilia, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe EntregaFamilia guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entregaFamilia = await _context.EntregaFamilias
                .Include(e => e.Familia)
                 .Include(d => d.ProdutosEntregues)
                .Include("ProdutosEntregues.Produto")

                .FirstOrDefaultAsync(m => m.ID == id);
            if (entregaFamilia == null)
            {
                return NotFound();
            }

            return View(entregaFamilia);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo EntregaFamilia com objeto viewModelNovaEntregaFamilia. 
        /// Atribui a viewModelNovaEntregaFamilia dados de famílias e produtos que se encontram em BD
        /// Instancia uma nova Lista do tipo ProdutoEntregue, vazia e instancia e adiciona um objeto ProdutoEntregue vazio a esta lista
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe viewModelNovaEntregaFamilia</returns>
        public IActionResult Create()
        {
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "ID");
            List<Familia> familias = _context.Familias.ToList();
            List<Produto> produtos = _context.Produtos.ToList();


            ViewModelNovaEntregaFamilia viewModelNovaEntregaFamilia = new ViewModelNovaEntregaFamilia();
            viewModelNovaEntregaFamilia.Familias = familias;
            viewModelNovaEntregaFamilia.Produtos = produtos;
            viewModelNovaEntregaFamilia.EntregaFamilia = new EntregaFamilia();
            viewModelNovaEntregaFamilia.EntregaFamilia.ProdutosEntregues = new List<ProdutoEntregue>();
            viewModelNovaEntregaFamilia.EntregaFamilia.ProdutosEntregues.Add(new ProdutoEntregue());


            return View(viewModelNovaEntregaFamilia);
        }

        /// <summary>
        ///  POST: Categorias/Create
        ///  Método que adiciona objeto EntregaFamilia e objetos ProdutoEntregue criados à BD
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="entregaFamilia">objeto  da classe EntregaFamilia criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataEntrega,FamiliaID,ProdutosEntregues")] EntregaFamilia entregaFamilia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entregaFamilia);

                foreach (ProdutoEntregue produtoEntregue in entregaFamilia.ProdutosEntregues)
                    _context.Add(produtoEntregue);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "ID", entregaFamilia.FamiliaID);
            return View(entregaFamilia);
        }

        /// <summary>
        /// Método que atribui ao viewModelNovaEntregaFamilia os dados da BD de Famílias e Produtos
        /// Instancia e adiciona à EntregaFamilia um novo objeto, sem dados, do tipo ProdutoEntregue
        /// </summary>
        /// <param name="viewModelNovaEntregaFamilia"> objeto do tipo viewModelNovaEntregaFamilia</param>
        /// <returns>devolve a view Create com o viewModelNovaEntregaFamilia com dados de famílias e produtos e um novo campo no formulário para adicionar novo ProdutoEntregue</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProdutoEntregue(ViewModelNovaEntregaFamilia viewModelNovaEntregaFamilia)
        {
            List<Familia> familias = _context.Familias.ToList();
            List<Produto> produtos = _context.Produtos.ToList();

            viewModelNovaEntregaFamilia.Familias = familias;
            viewModelNovaEntregaFamilia.Produtos = produtos;

            viewModelNovaEntregaFamilia.EntregaFamilia.ProdutosEntregues.Add(new ProdutoEntregue());

            return View("Create", viewModelNovaEntregaFamilia);

        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe EntregaFamilia que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entregaFamilia = await _context.EntregaFamilias.FindAsync(id);
            if (entregaFamilia == null)
            {
                return NotFound();
            }
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "Nome", entregaFamilia.FamiliaID);
            return View(entregaFamilia);
        }

        /// <summary>
        ///  POST: Categorias/Edit
        ///  Método que edita objeto do tipo EntregaFamilia que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="entregaFamilia">objeto  da classe EntregaFamilia editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataEntrega,FamiliaID")] EntregaFamilia entregaFamilia)
        {
            if (id != entregaFamilia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entregaFamilia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntregaFamiliaExists(entregaFamilia.ID))
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
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "Nome", entregaFamilia.FamiliaID);
            return View(entregaFamilia);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe EntregaFamilia selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe EntregaFamilia</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entregaFamilia = await _context.EntregaFamilias
                .Include(e => e.Familia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entregaFamilia == null)
            {
                return NotFound();
            }

            return View(entregaFamilia);
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
            var entregaFamilia = await _context.EntregaFamilias.FindAsync(id);
            _context.EntregaFamilias.Remove(entregaFamilia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntregaFamiliaExists(int id)
        {
            return _context.EntregaFamilias.Any(e => e.ID == id);
        }
    }
}
