using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosVeganos.Models
{
    public class IngredientesProduto
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int IngredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; }

        public IngredientesProduto()
        {
        }
        public IngredientesProduto(Produto produto, Ingrediente ingrediente)
        {
            Produto = produto;
            Ingrediente = ingrediente;
        }
    }
}
