using Microsoft.EntityFrameworkCore;
using ProdutosVeganos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosVeganos
{
    public class DataService : IDataService
    {
        #region config banco e DataService
        private readonly Contexto _contexto;
        public DataService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public void InicializaBD()
        {
            this._contexto.Database.EnsureCreated();
            if (this._contexto.Produtos.Count() == 0)
            {
                //Cria ingredientes
                List<Ingrediente> ingredientes = new List<Ingrediente>
                {
                    new Ingrediente("Açucar", "Açucar refinado de cana", Vegan.Sim, false, false),
                    new Ingrediente("Cacau em pó", "pó de cacau", Vegan.Sim, false, false),
                    new Ingrediente("Vitamina D", "Vitamina geralmente extraida de animais", Vegan.Talvez, false, false),
                    new Ingrediente("Leite em pó", "Leite em pó desidratado", Vegan.Não, false, true)
                };
                foreach (var ingrediente in ingredientes)
                {
                    this._contexto.Ingredientes.Add(ingrediente);
                }
                this._contexto.SaveChanges();

                //Cria produtos
                List<Produto> produtos = new List<Produto>
                {
                    new Produto(7866123465857, "Nescau", "Achocolatado da nestle", "http://www.nescau.com.br", false, false, false, true, false, false, false)
                        .AdicionaIngrediente(GetIngredienteByNome("Açucar"))
                        .AdicionaIngrediente(GetIngredienteByNome("Cacau em pó"))
                        .AdicionaIngrediente(GetIngredienteByNome("Vitamina D")),
                    new Produto(7456985465548, "Açucar União", "Açucar refinado da marca união", "http://www.uniao.com.br", false, false, false, false, false, false, false)
                        .AdicionaIngrediente(GetIngredienteByNome("Açucar")),
                    new Produto(7459415489152, "Leite Ninho", "Leite ninho", "http://www.nestle.com.br", false, true, true, false, false, false, false)
                        .AdicionaIngrediente(GetIngredienteByNome("Açucar"))
                        .AdicionaIngrediente(GetIngredienteByNome("Leite em pó"))
                        .AdicionaIngrediente(GetIngredienteByNome("Vitamina D"))
                };
                foreach (var produto in produtos)
                {
                    this._contexto.Produtos.Add(produto);
                }

                this._contexto.SaveChanges();
            }
        }
        #endregion
        #region Produtos
        public List<Produto> GetProdutos()
        {
            //return this._contexto.Produtos.Include(ip => ip.Ingredientes).ThenInclude(i => i.Ingrediente).ToList();
            return this._contexto.Produtos.ToList();
        }
        public Produto GetProdutoByBarra(long barCode)
        {
            return this._contexto.Produtos
                .Include(ip => ip.Ingredientes)
                .ThenInclude(i => i.Ingrediente)
                .Where(p => p.CodigoBarra == barCode)
                .FirstOrDefault();
        }
        public List<Produto> GetProdutosByNome(string nome)
        {
            return this._contexto.Produtos.Where(i => i.Nome.Contains(nome)).ToList();
        }

        public Produto SetProduto(Produto produto)
        {
            //Verifica se o ingrendiente existe pelo ID ou pelo nome
            Produto produtoDB = (produto.CodigoBarra == 0 ? throw new Exception("Não existe produto sem código de barras") : GetProdutoByBarra(produto.CodigoBarra));
            //Atualiza ou cadastra novo ingrediente
            if (produtoDB != null)
            {
                produtoDB.ContemDerivadosLeite = produto.ContemDerivadosLeite;
                produtoDB.ContemGlutem = produto.ContemGlutem;
                produtoDB.ContemLactose = produto.ContemLactose;
                produtoDB.Descricao = produto.Descricao;
                if (produto.Ingredientes.Count > 0) { produtoDB.Ingredientes = produto.Ingredientes; } //os produtos só são atualizados se estiverem na lista                    
                produtoDB.Nome = produto.Nome;
                produtoDB.PodeConterAnimais = produto.PodeConterAnimais;
                produtoDB.PodeConterDerivadosLeite = produto.PodeConterDerivadosLeite;
                produtoDB.PodeConterGlutem = produto.PodeConterGlutem;
                produtoDB.PodeConterLactose = produto.PodeConterLactose;
                produtoDB.Site = produto.Site;
            }
            else
            {
                produtoDB = produto;
                this._contexto.Produtos.Add(produtoDB);
            }
            this._contexto.SaveChanges();
            return produtoDB;
        }
        public void Excluir(Produto produto)
        {
            this._contexto.Produtos.Attach(produto);
            this._contexto.Produtos.Remove(produto);
            this._contexto.SaveChanges();
        }
        #endregion
        #region Ingredientes
        public List<Ingrediente> GetIngredientes()
        {
            return this._contexto.Ingredientes.ToList();
        }
        public Ingrediente GetIngredienteById(int ingredienteId)
        {
            return this._contexto.Ingredientes.Include(i=>i.Produtos).Where(i => i.Id == ingredienteId).SingleOrDefault();
        }
        public Ingrediente GetIngredienteByNome(string nome)
        {
            return this._contexto.Ingredientes.Where(i => i.Nome == nome).FirstOrDefault();
        }
        public List<Ingrediente> GetIngredientesByNome(string nome)
        {
            return this._contexto.Ingredientes.Where(i => i.Nome.Contains(nome)).ToList();
        }
        public Ingrediente SetIngrediente(Ingrediente ingrediente)
        {
            //Verifica se o ingrendiente existe pelo ID ou pelo nome
            Ingrediente ingredienteDB = (ingrediente.Id==0 ? GetIngredienteByNome(ingrediente.Nome) : GetIngredienteById(ingrediente.Id));
            //Atualiza ou cadastra novo ingrediente
            if (ingredienteDB != null)
            {
                ingredienteDB.Descricao = ingrediente.Descricao;
                ingredienteDB.Nome = ingrediente.Nome;
                ingredienteDB.TemGlutem = ingrediente.TemGlutem;
                ingredienteDB.TemLactose = ingrediente.TemLactose;
                ingredienteDB.Vegano = ingrediente.Vegano;
            }
            else
            {
                ingredienteDB = ingrediente;
                this._contexto.Ingredientes.Add(ingredienteDB);
            }
            this._contexto.SaveChanges();
            return ingredienteDB;
        }
        public void Excluir(Ingrediente ingrediente)
        {
            this._contexto.Ingredientes.Attach(ingrediente);
            this._contexto.Ingredientes.Remove(ingrediente);
            this._contexto.SaveChanges();
        }
        #endregion
        #region IngredientesProduto
        public void Excluir(IngredientesProduto ingredientesProduto)
        {
            this._contexto.IngredientesProdutos.Attach(ingredientesProduto);
            this._contexto.IngredientesProdutos.Remove(ingredientesProduto);
            this._contexto.SaveChanges();
        }
        public void Adicionar(IngredientesProduto ingredientesProduto)
        {
            var ingredientesProdutoDb = this._contexto.IngredientesProdutos.FirstOrDefault(ip => ip.IngredienteId == ingredientesProduto.IngredienteId && ip.ProdutoId == ingredientesProduto.ProdutoId);
            if (ingredientesProdutoDb==null)
            {
                this._contexto.IngredientesProdutos.Add(ingredientesProduto);
                this._contexto.SaveChanges();
            }
        }
        #endregion
    }
}
