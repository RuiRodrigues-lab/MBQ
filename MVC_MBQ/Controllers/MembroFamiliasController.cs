using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_MBQ.Models;
using Newtonsoft.Json;

namespace MVC_MBQ.Controllers
{
    /// <summary>
    /// Controller relativo ao Modelo MembroFamilia.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class MembroFamiliasController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        /// <param name="context">objeto do tipo AppMovimentoBQDbContext</param>
        public MembroFamiliasController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        // <summary>
        /// Método da view Index referente ao modelo MembroFamilia
        /// </summary>
        /// <returns>devolve view com lista de Membros de famílias inscritos e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            var appMovimentoBQDbContext = _context.MembroFamilias.Include(m => m.Familia);
            return View(await appMovimentoBQDbContext.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe MembroFamilia, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe MembroFamilia guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membroFamilia = await _context.MembroFamilias
                .Include(m => m.Familia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (membroFamilia == null)
            {
                return NotFound();
            }

            return View(membroFamilia);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo ViewModelNovoMembroFamilia
        /// obtém dados de Famílias e envia para view lista de Famílias
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe ViewModelNovoMembroFamilia</returns>
        public IActionResult Create()
        {
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "Nome");
            List<Familia> listaFamilia = _context.Familias.ToList();
            ViewModelNovoMembroFamilia novoViewModelMembro = new ViewModelNovoMembroFamilia();
            novoViewModelMembro.Familias = listaFamilia;
            return View(novoViewModelMembro);
        }

        /// <summary>
        ///  POST: MembroFamilias/Create
        ///  Método que adiciona objeto MembroFamilia adicionando à BD
        ///  Valida através de API se email existe e é válido ou não. 
        ///  Valida data de nascimento para inferior a data atual
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="membroFamilia">objeto  da classe MembroFamilia criado no Formulário</param>
        /// <returns>View Create com o objeto membroFamilia criado. Caso email não seja válido/existente devolve view create com alerta</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,GrauParentesco,DataNascimento,Morada,Cidade,Distrito,Codigo,Postal,Email,Descricao,FamiliaID")] MembroFamilia membroFamilia)
        {
            HttpClient client = MyConvertHttpClient.Client;

            var email = membroFamilia.Email;
            var dataNascimento = membroFamilia.DataNascimento;

            string path = "?key=private_f9cea434f6d212d56da0091d19a97d71&email=" + email;

            string json = JsonConvert.SerializeObject(email);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) { return Redirect("/"); }
            string json_r = await response.Content.ReadAsStringAsync();
            ResponseAPI responseResult = JsonConvert.DeserializeObject<ResponseAPI>(json_r);

            ViewModelNovoMembroFamilia novoViewModelMembro = new ViewModelNovoMembroFamilia();
            novoViewModelMembro.ResponsesAPI = responseResult;
            List<Familia> listaFamilia = _context.Familias.ToList();
            novoViewModelMembro.Familias = listaFamilia;

            if (dataNascimento > DateTime.Now)
            {
                novoViewModelMembro.VerificaDataNascimento = false;
                //novoViewModelMembro.ResponsesAPI = null;
                return View(novoViewModelMembro); 
            }
            else
            if (responseResult.Result != "valid")
            { return View(novoViewModelMembro); }
            else
            if (ModelState.IsValid)
            {
                _context.Add(membroFamilia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "Nome", membroFamilia.FamiliaID);
            return View(membroFamilia);
        }

        // <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe MembroFamilia que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membroFamilia = await _context.MembroFamilias.FindAsync(id);
            if (membroFamilia == null)
            {
                return NotFound();
            }
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "ID", membroFamilia.FamiliaID);
            return View(membroFamilia);
        }

        /// <summary>
        ///  POST: MembroFamilias/Edit
        ///  Método que edita objeto do tipo MembroFamilia que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="membroFamilia">objeto  da classe MembroFamilia editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,GrauParentesco,DataNascimento,Morada,Cidade,Distrito,Codigo,Postal,Email,Descricao,FamiliaID")] MembroFamilia membroFamilia)
        {
            if (id != membroFamilia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membroFamilia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembroFamiliaExists(membroFamilia.ID))
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
            ViewData["FamiliaID"] = new SelectList(_context.Familias, "ID", "ID", membroFamilia.FamiliaID);
            return View(membroFamilia);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe MembroFamilia selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe MembroFamilia</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membroFamilia = await _context.MembroFamilias
                .Include(m => m.Familia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (membroFamilia == null)
            {
                return NotFound();
            }

            return View(membroFamilia);
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
            var membroFamilia = await _context.MembroFamilias.FindAsync(id);
            _context.MembroFamilias.Remove(membroFamilia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembroFamiliaExists(int id)
        {
            return _context.MembroFamilias.Any(e => e.ID == id);
        }
    }
}
