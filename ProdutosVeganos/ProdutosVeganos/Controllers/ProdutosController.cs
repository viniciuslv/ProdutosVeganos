using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProdutosVeganos.Models;

namespace ProdutosVeganos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IDataService _dataService;
        public ProdutosController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public IActionResult Index()
        {
            List<Produto> produtos = _dataService.GetProdutos();
            return View(produtos);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public IActionResult Index(Produto produto)
        {
            List<Produto> produtos;
            if (produto.CodigoBarra == 0)
            {
                //consulta por nome
                produtos = _dataService.GetProdutosByNome(produto.Nome);
            }
            else
            {
                //consulta por código
                produtos = new List<Produto>();
                var produtoDb = _dataService.GetProdutoByBarra(produto.CodigoBarra);
                if (produtoDb!= null)
                {
                    produtos.Add(produtoDb);
                }
            }
            return View(produtos);
        }

        public IActionResult Detalhe(long id)
        {
            //O id na verdade é o código do barra
            Produto produto = _dataService.GetProdutoByBarra(id);
            return View(produto);
        }

        [HttpGet]
        public IActionResult Cadastro(long id)
        {
            //O id na verdade é o código do barra
            Produto produto = _dataService.GetProdutoByBarra(id);
            return View(produto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro(Produto produto)
        {
            var produtoDb = _dataService.SetProduto(produto);
            return RedirectToAction("Detalhe", new { id = produtoDb.CodigoBarra });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IngredientesProduto(Produto produto)
        {
            var produtoDb = _dataService.SetProduto(produto);
            return View(produtoDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void RemoverIngrediente([FromBody]IngredientesProduto input)
        {
            _dataService.Excluir(input);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AdicionarIngrediente([FromBody]IngredientesProduto input)
        {
            _dataService.Adicionar(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public List<Ingrediente> ConsultarIngredientes([FromBody]Ingrediente input)
        {
            return _dataService.GetIngredientesByNome(input.Nome);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ExcluirProduto([FromBody]Produto produto)
        {
            _dataService.Excluir(produto);
        }

    }
}