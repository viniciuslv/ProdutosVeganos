using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProdutosVeganos.Models;

namespace ProdutosVeganos
{
    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<IngredientesProduto> IngredientesProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientesProduto>()
                .HasKey(ip => new { ip.ProdutoId, ip.IngredienteId });

            modelBuilder.Entity<IngredientesProduto>()
                .HasOne(ip => ip.Produto)
                .WithMany(ing => ing.Ingredientes)
                .HasForeignKey(pi => pi.ProdutoId);

            modelBuilder.Entity<IngredientesProduto>()
                .HasOne(ip => ip.Ingrediente)
                .WithMany(pd => pd.Produtos)
                .HasForeignKey(ii => ii.IngredienteId);

            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.CodigoBarra);

            modelBuilder.Entity<Ingrediente>()
                .HasIndex(i => i.Nome);
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {

        }


    }
}
