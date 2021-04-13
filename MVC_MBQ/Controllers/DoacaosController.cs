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
    /// Controller relativo ao Modelo Doacao.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class DoacaosController : Controller
    {
        /// <value>
        /// Variável local do tipo AppMovimentoBQDbContext
        /// </value>
        private readonly AppMovimentoBQDbContext _context;

        /// <summary>
        /// Método que atribui o objeto da classe AppMovimentoBDQContext a variável local
        /// </summary>
        public DoacaosController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Doacao
        /// </summary>
        /// <returns>devolve view com lista de Doações presentes em BD</returns>
        
        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            var appMovimentoBQDbContext = _context.Doacoes.Include(d => d.Evento).Include(d => d.Voluntario);
            return View(await appMovimentoBQDbContext.ToListAsync());
        }

        /// <summary>
        /// Método que permite ler todos os detalhes de objeto da classe Doacao, selecionado em view, cujos dados estão guardados em BD 
        /// </summary>
        /// <param name="id">ID do objeto da classe Doacao guardado em BD</param>
        /// <returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doacao = await _context.Doacoes
                .Include(d => d.Evento)
                .Include(d => d.Voluntario)
                .Include(d => d.ProdutosDoados)
                .Include("ProdutosDoados.Produto")

                .FirstOrDefaultAsync(m => m.ID == id);
            if (doacao == null)
            {
                return NotFound();
            }

            return View(doacao);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo Doacao com objeto viewModelNovaDoacao. 
        /// Atribui a viewModelNovaDoacao dados de voluntarios, eventos e produtos que se encontram em BD
        /// Instancia uma nova Lista do tipo ProdutoDoado, vazia e instancia e adiciona um objeto ProdutoDoado vazio a esta lista
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe viewModelNovaDoacao</returns>
        public IActionResult Create()
        {
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento");
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome");

            List<Voluntario> voluntarios = _context.Voluntarios.ToList();
            List<Evento> eventos = _context.Eventos.ToList();
            List<Produto> produtos = _context.Produtos.ToList();

            ViewModelNovaDoacao viewModelNovaDoacao = new ViewModelNovaDoacao();
            viewModelNovaDoacao.Voluntarios = voluntarios;
            viewModelNovaDoacao.Eventos = eventos;
            viewModelNovaDoacao.Produtos = produtos;
            viewModelNovaDoacao.Doacao = new Doacao();
            viewModelNovaDoacao.Doacao.ProdutosDoados = new List<ProdutoDoado>();
            viewModelNovaDoacao.Doacao.ProdutosDoados.Add(new ProdutoDoado());

            return View(viewModelNovaDoacao);

        }

        /// <summary>
        ///  POST: Doacões/Create
        ///  Método que adiciona objeto Doacao criado e objetos ProdutoDoado à  BD
        ///  Adiciona objetos do tipo ProdutoDoado à BD, ao objeto do tipo Doacao criado
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="doacao">objeto  da classe Doacao criado no Formulário</param>
        /// <returns>View Create com o objeto criado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataDoacao,EventoID,VoluntarioID,ProdutosDoados")] Doacao doacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doacao);
                foreach (ProdutoDoado produtoDoado in doacao.ProdutosDoados)
                    _context.Add(produtoDoado);


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento", doacao.EventoID);
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome", doacao.VoluntarioID);
            return View(doacao);
        }
        /// <summary>
        /// Método que atribui ao viewModelNovaDoacao os dados da BD de Voluntarios, Eventos, Produtos 
        /// Instancia e adiciona à doacao um novo objeto, sem dados, do tipo ProdutoDoado
        /// </summary>
        /// <param name="viewModelNovaDoacao"> objeto do tipo ViewModelNovaDoacao</param>
        /// <returns>devolve a view Create com o viewModelNovaDoacao com dados de Voluntarios, Eventos e Produtos e um novo campo no formulário para adicionar novo ProdutoDoado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProdutoDoado(ViewModelNovaDoacao viewModelNovaDoacao)
        {
            List<Voluntario> voluntarios = _context.Voluntarios.ToList();
            List<Evento> eventos = _context.Eventos.ToList();
            List<Produto> produtos = _context.Produtos.ToList();

            viewModelNovaDoacao.Voluntarios = voluntarios;
            viewModelNovaDoacao.Eventos = eventos;
            viewModelNovaDoacao.Produtos = produtos;

            viewModelNovaDoacao.Doacao.ProdutosDoados.Add(new ProdutoDoado());

            return View("Create", viewModelNovaDoacao);

        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Doacao que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doacao = await _context.Doacoes.FindAsync(id);
            if (doacao == null)
            {
                return NotFound();
            }
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento", doacao.EventoID);
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome", doacao.VoluntarioID);
            return View(doacao);
        }

        /// <summary>
        ///  POST: Doacaos/Edit
        ///  Método que edita objeto do tipo Doacao que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="doacao">objeto  da classe Doacao editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataDoacao,EventoID,VoluntarioID")] Doacao doacao)
        {
            if (id != doacao.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoacaoExists(doacao.ID))
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
            ViewData["EventoID"] = new SelectList(_context.Eventos, "ID", "NomeEvento", doacao.EventoID);
            ViewData["VoluntarioID"] = new SelectList(_context.Voluntarios, "ID", "Nome", doacao.VoluntarioID);
            return View(doacao);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Doacao selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Doacao</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doacao = await _context.Doacoes
                .Include(d => d.Evento)
                .Include(d => d.Voluntario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doacao == null)
            {
                return NotFound();
            }

            return View(doacao);
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
            var doacao = await _context.Doacoes.FindAsync(id);
            _context.Doacoes.Remove(doacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoacaoExists(int id)
        {
            return _context.Doacoes.Any(e => e.ID == id);
        }
    }
}
