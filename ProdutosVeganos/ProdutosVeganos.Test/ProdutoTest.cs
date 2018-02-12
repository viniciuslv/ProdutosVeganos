using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProdutosVeganos.Models;
using System.Collections.Generic;

namespace ProdutosVeganos.Test
{
    [TestClass]
    public class ProdutoTest
    {
        private List<Ingrediente> ingredientes;
        private List<Ingrediente> produtos;
        [TestInitialize]
        public void AntesDoTeste()
        {
            ingredientes = new List<Ingrediente>();
            Ingrediente ingrediente =
                new Ingrediente(
                    "Açucar",
                    "Obitido da Cana de açucar, inicialmente tinha produtos de origgem animal no refino, hoje as empresas afirmam que o produto não utiliza de animais nos processos de fabricação",
                    Vegan.Sim,
                    false,
                    false);
            ingredientes.Add(ingrediente);
            
        }
        [TestCleanup]
        public void DepoisDoTeste()
        {
            ingredientes = null;
        }

        [TestMethod]
        public void CriarProduto()
        {
            Produto produto =
                new Produto(
                        12345679,
                        "Açucar União 1kg",
                        "Açucar união de 1 quilo",
                        "www.acucaruniao.com.br"
                    );

            Assert.AreEqual(produto.CodigoBarra, 12345679);
            Assert.AreEqual(produto.Nome, "Açucar União 1kg");
            Assert.AreEqual(produto.Descricao, "Açucar união de 1 quilo");
            Assert.AreEqual(produto.Site, "www.acucaruniao.com.br");
        }


    }
}
