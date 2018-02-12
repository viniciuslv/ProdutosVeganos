using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProdutosVeganos;
using ProdutosVeganos.Models;

namespace ProdutosVeganos.Test
{
    [TestClass]
    public class IngredienteTest
    {
        [TestMethod]
        public void CriarIngrediente()
        {
            Ingrediente ingrediente =
                new Ingrediente(
                    "A�ucar",
                    "Obitido da Cana de a�ucar, inicialmente tinha produtos de origgem animal no refino, hoje as empresas afirmam que o produto n�o utiliza de animais nos processos de fabrica��o",
                    Vegan.Sim,
                    false,
                    false);

            Assert.AreEqual("A�ucar", ingrediente.Nome, "Erro ao carregar o nome do ingrediente");
            Assert.AreNotEqual("Acucar", ingrediente.Nome);
            Assert.AreEqual("Obitido da Cana de a�ucar, inicialmente tinha produtos de origgem animal no refino, hoje as empresas afirmam que o produto n�o utiliza de animais nos processos de fabrica��o", ingrediente.Descricao);
            Assert.AreNotEqual("Obitido da Cana de acucar, inicialmente tinha produtos de origgem animal no refino, hoje as empresas afirmam que o produto n�o utiliza de animais nos processos de fabrica��o", ingrediente.Descricao);
            Assert.AreEqual(Vegan.Sim, ingrediente.Vegano);
            Assert.AreNotEqual(Vegan.N�o, ingrediente.Vegano);
            Assert.AreEqual(false, ingrediente.TemLactose);
            Assert.AreNotEqual(true, ingrediente.TemLactose);
            Assert.AreEqual(false, ingrediente.TemGlutem);
            Assert.AreNotEqual(true, ingrediente.TemGlutem);
        }


    }
}
