using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProdutosVeganos.Models
{
    [DataContract]
    public enum Vegan : int
    {
        Sim = 0,
        Talvez = 1,
        Não = 2
    };
    [DataContract]
    public class Ingrediente
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nome deve ter entre 1 e 100 caracteres.")]
        public string Nome { get; set; }
        [DataMember]
        [Required]
        [StringLength(1000)]
        public string Descricao { get; set; }
        [DataMember]
        public Vegan Vegano { get; set; }
        [DataMember]
        public bool TemGlutem { get; set; }
        [DataMember]
        public bool TemLactose { get; set; }
        [DataMember]
        public List<IngredientesProduto> Produtos { get; set; }


        public Ingrediente()
        {
        }
        public Ingrediente(int id)
        {
            Id = id;
        }
        public Ingrediente(string nome, string descricao, Vegan vegano, bool temGlutem, bool temLactose)
        {
            Nome = nome;
            Descricao = descricao;
            Vegano = vegano;
            TemGlutem = temGlutem;
            TemLactose = temLactose;
        }
        
        public override bool Equals(System.Object obj)
        {
            var ingrediente = obj as Ingrediente;
            if (ingrediente == null)
            {
                return false;
            }
            return this.Id == ingrediente.Id && this.Nome == ingrediente.Nome;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }


    }
}
