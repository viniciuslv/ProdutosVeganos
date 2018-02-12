﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ProdutosVeganos;
using ProdutosVeganos.Models;
using System;

namespace ProdutosVeganos.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20180204001755_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProdutosVeganos.Models.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("TemGlutem");

                    b.Property<bool>("TemLactose");

                    b.Property<int>("Vegano");

                    b.HasKey("Id");

                    b.HasIndex("Nome");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("ProdutosVeganos.Models.IngredientesProduto", b =>
                {
                    b.Property<int>("ProdutoId");

                    b.Property<int>("IngredienteId");

                    b.HasKey("ProdutoId", "IngredienteId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("IngredientesProdutos");
                });

            modelBuilder.Entity("ProdutosVeganos.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CodigoBarra");

                    b.Property<bool>("ContemDerivadosLeite");

                    b.Property<bool>("ContemGlutem");

                    b.Property<bool>("ContemLactose");

                    b.Property<string>("Descricao")
                        .HasMaxLength(1000);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("PodeConterAnimais");

                    b.Property<bool>("PodeConterDerivadosLeite");

                    b.Property<bool>("PodeConterGlutem");

                    b.Property<bool>("PodeConterLactose");

                    b.Property<string>("Site");

                    b.HasKey("Id");

                    b.HasIndex("CodigoBarra");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ProdutosVeganos.Models.IngredientesProduto", b =>
                {
                    b.HasOne("ProdutosVeganos.Models.Ingrediente", "Ingrediente")
                        .WithMany("Produtos")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProdutosVeganos.Models.Produto", "Produto")
                        .WithMany("Ingredientes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
