using ProdutosVeganos.Models;
using System.Collections.Generic;

namespace ProdutosVeganos
{
    public interface IDataService
    {
        void InicializaBD();

        //Produtos
        List<Produto> GetProdutos();
        Produto GetProdutoByBarra(long barCode);
        List<Produto> GetProdutosByNome(string nome);
        Produto SetProduto(Produto produto);
        void Excluir(Produto produto);

        //Ingredientes
        List<Ingrediente> GetIngredientes();
        Ingrediente GetIngredienteById(int ingredienteId);
        Ingrediente GetIngredienteByNome(string nome);
        List<Ingrediente> GetIngredientesByNome(string nome);
        Ingrediente SetIngrediente(Ingrediente ingrediente);
        void Excluir(Ingrediente produto);

        //IngredientesProduto
        void Excluir(IngredientesProduto ingredientesProduto);
        void Adicionar(IngredientesProduto ingredientesProduto);
    }
}