using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosVeganos.Models;

namespace ProdutosVeganos.Controllers
{
    public class IngredientesController : Controller
    {
        private readonly IDataService _dataService;
        public IngredientesController(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public IActionResult Index()
        {
            List<Ingrediente> ingredientes = _dataService.GetIngredientes();
            return View(ingredientes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Ingrediente ingrediente)
        {
            var ingredientes = _dataService.GetIngredientesByNome(ingrediente.Nome);
            return View(ingredientes);
        }

        public IActionResult Detalhe(int id)
        {
            Ingrediente ingrediente = _dataService.GetIngredienteById(id);
            return View(ingrediente);
        }

        [HttpGet]
        public IActionResult Cadastro(int id)
        {
            Ingrediente ingrediente = _dataService.GetIngredienteById(id);
            return View(ingrediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro(Ingrediente ingredientePost)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var ingrediente = _dataService.SetIngrediente(ingredientePost) ;
                return RedirectToAction("Detalhe", new { id = ingredientePost.Id });
            }
            else
            {
                return RedirectToAction("Cadastro");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ExcluirIngrediente([FromBody]Ingrediente ingrediente)
        {
            _dataService.Excluir(ingrediente);
        }
    }
}