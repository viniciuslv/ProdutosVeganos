using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProdutosVeganos.Models
{
    [DataContract]
    public class Produto
    {
        [DataMember]
        public int Id { get; set; }
        [Required]
        [Range(999999999999, 9999999999999, ErrorMessage = "Código de barras maior que o permitido" )]
        [DataMember]
        public long CodigoBarra { get; set; }
        [Required]
        [DataMember]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nome deve ter entre 1 e 100 caracteres.")]
        public string Nome { get; set; }
        [DataMember]
        [StringLength(1000)]
        public string Descricao { get; set; }
        [DataMember]
        public string Site { get; set; }
        [DataMember]
        public bool ContemGlutem { get; set; }
        [DataMember]
        public bool ContemLactose { get; set; }
        [DataMember]
        public bool ContemDerivadosLeite { get; set; }
        [DataMember]
        public bool PodeConterGlutem { get; set; }
        [DataMember]
        public bool PodeConterLactose { get; set; }
        [DataMember]
        public bool PodeConterDerivadosLeite { get; set; }
        [DataMember]
        public bool PodeConterAnimais { get; set; }
        [DataMember]
        public List<IngredientesProduto> Ingredientes { get; set; }

        public Produto()
        {
            Ingredientes = new List<IngredientesProduto>();
        }
        public Produto(long codigoBarra, string nome, string descricao, string site) : this()
        {
            CodigoBarra = codigoBarra;
            Nome = nome;
            Descricao = descricao;
            Site = site;
        }
        public Produto(long codigoBarra, string nome, string descricao, string site,
            bool contemGlutem, bool contemLactose, bool contemDerivadosLeite,
            bool podeConterGlutem, bool podeConterLactose, bool podeConterDerivadosLeite, bool podeConterAnimais) :this(codigoBarra, nome, descricao, site)
        {
            this.ContemGlutem = contemGlutem;
            this.ContemLactose = contemLactose;
            this.ContemDerivadosLeite = contemDerivadosLeite;

            this.PodeConterGlutem = podeConterGlutem;
            this.PodeConterLactose = podeConterLactose;
            this.PodeConterDerivadosLeite = podeConterDerivadosLeite;
            this.PodeConterAnimais = podeConterAnimais;

        }

        public Produto AdicionaIngrediente(Ingrediente ingrediente)
        {
            if (!this.Ingredientes.Exists(ip => ip.IngredienteId == ingrediente.Id))
            {
                IngredientesProduto ip = new IngredientesProduto(this, ingrediente);
                this.Ingredientes.Add(ip);
            }
            return this;
        }

        public Produto RemoveIngrediente(Ingrediente ingrediente)
        {
            this.Ingredientes.Remove(new IngredientesProduto(this, ingrediente));
            return this;
        }

        public override bool Equals(System.Object obj)
        {
            var produto = obj as Produto;
            if (produto == null)
            {
                return false;
            }
            return this.Id == produto.Id && this.Nome == produto.Nome;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

    }
}
