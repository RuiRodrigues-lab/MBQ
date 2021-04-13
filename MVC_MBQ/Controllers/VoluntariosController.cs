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
    /// Controller relativo ao Modelo Voluntario.
    /// Fornece/Implementa todos os métodos relativos a acções CRUD
    /// </summary>
    public class VoluntariosController : Controller
    {
        /// <summary>
        /// Método que cria variável local com atribuição do objeto da classe AppMovimentoBDQContext
        /// </summary>
        private readonly AppMovimentoBQDbContext _context;

        public VoluntariosController(AppMovimentoBQDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método da view Index referente ao modelo Voluntario
        /// </summary>
        /// <returns>devolve view com lista de voluntarios inscritos e presentes em BD</returns>

        [Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voluntarios.ToListAsync());
        }

        ///<summary>
        ///Método que permite ler todos os detalhes de objeto da classe Voluntario, selecionado em view, cujos dados estão guardados em BD 
        ///</summary>
        ///<param name="id">ID do objeto da classe Voluntario guardado em BD</param>
        ///<returns>devolve view com todos os detalhes de um ID selecionado presente em BD</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntario = await _context.Voluntarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voluntario == null)
            {
                return NotFound();
            }

            return View(voluntario);
        }

        /// <summary>
        /// Método que devolve a view Create do modelo ViewModelNovoVoluntario
        /// </summary>
        /// <returns>devolve view com formulário para criar o objeto da classe ViewModelNovoVoluntario</returns>
        public IActionResult Create()
        {
            ViewModelNovoVoluntario viewModelNovoVoluntario = new ViewModelNovoVoluntario();
            return View(viewModelNovoVoluntario);
        }

        /// <summary>
        ///  POST: Voluntarios/Create
        ///  Método que adiciona objeto Voluntario adicionando à BD
        ///  Valida se email existe
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="voluntario">objeto  da classe Voluntario criado no Formulário</param>
        /// <returns>View Create com o objeto criado. Caso email nao validado retorna view com formulário contendo mensagem de email não validado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,DataNascimento,Morada,Cidade,Distrito,Codigo,Postal,Email,Descricao,ConsentimentoRGPD,Interno")] Voluntario voluntario)
        {
            HttpClient client = MyConvertHttpClient.Client;

            var email = voluntario.Email;
            var dataNascimento = voluntario.DataNascimento;

            string path = "?key=private_f9cea434f6d212d56da0091d19a97d71&email=" + email;

            string json = JsonConvert.SerializeObject(email);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) { return Redirect("/"); }
            string json_r = await response.Content.ReadAsStringAsync();
            ResponseAPI responseResult = JsonConvert.DeserializeObject<ResponseAPI>(json_r);

            ViewModelNovoVoluntario viewModelNovoVoluntario = new ViewModelNovoVoluntario();
            viewModelNovoVoluntario.ResponsesAPI = responseResult;

            if (dataNascimento > DateTime.Now)
            {
                viewModelNovoVoluntario.VerificaDataNascimento = false;
                return View(viewModelNovoVoluntario);
            }

            if (responseResult.Result != "valid")
            { return View(viewModelNovoVoluntario); }
            else
            {
                if (ModelState.IsValid)
                {

                    _context.Add(voluntario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                else
                    return View(viewModelNovoVoluntario);
            }
        }

        /// <summary>
        ///  Método que devolve a view Editar do objeto seleccionado da classe Voluntario que se encontra em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para editar</param>
        /// <returns>devolve view Edit com formulário para editar o objeto selecionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntario = await _context.Voluntarios.FindAsync(id);
            if (voluntario == null)
            {
                return NotFound();
            }
            return View(voluntario);
        }

        /// <summary>
        ///  POST: Voluntarios/Edit
        ///  Método que edita objeto do tipo Voluntario que se encontra na BD e atualiza a BD 
        ///  To protect from overposting attacks, enable the specific properties you want to bind to.
        ///  For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="id">id de objeto a ser alterado</param>
        /// <param name="voluntario">objeto  da classe Categoria editado no Formulário</param>
        /// <returns>View Edit com o objeto editado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,DataNascimento,Morada,Cidade,Distrito,Codigo,Postal,Email,Descricao,ConsentimentoRGPD,Interno")] Voluntario voluntario)
        {
            if (id != voluntario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voluntario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoluntarioExists(voluntario.ID))
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
            return View(voluntario);
        }

        /// <summary>
        /// Método que devolve a view Delete com objeto da classe Voluntario selecionado e que se encontra guardado em BD
        /// </summary>
        /// <param name="id">ID do objeto escolhido para apagar</param>
        /// <returns>devolve view com formulário para apagar o objeto selecionado da classe Voluntario</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voluntario = await _context.Voluntarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voluntario == null)
            {
                return NotFound();
            }

            return View(voluntario);
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
            var voluntario = await _context.Voluntarios.FindAsync(id);
            _context.Voluntarios.Remove(voluntario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoluntarioExists(int id)
        {
            return _context.Voluntarios.Any(e => e.ID == id);
        }
    }
}
