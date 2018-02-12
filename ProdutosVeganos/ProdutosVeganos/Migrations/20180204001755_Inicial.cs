using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProdutosVeganos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    TemGlutem = table.Column<bool>(nullable: false),
                    TemLactose = table.Column<bool>(nullable: false),
                    Vegano = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoBarra = table.Column<long>(nullable: false),
                    ContemDerivadosLeite = table.Column<bool>(nullable: false),
                    ContemGlutem = table.Column<bool>(nullable: false),
                    ContemLactose = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    PodeConterAnimais = table.Column<bool>(nullable: false),
                    PodeConterDerivadosLeite = table.Column<bool>(nullable: false),
                    PodeConterGlutem = table.Column<bool>(nullable: false),
                    PodeConterLactose = table.Column<bool>(nullable: false),
                    Site = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientesProdutos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientesProdutos", x => new { x.ProdutoId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_IngredientesProdutos_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientesProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_Nome",
                table: "Ingredientes",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientesProdutos_IngredienteId",
                table: "IngredientesProdutos",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CodigoBarra",
                table: "Produtos",
                column: "CodigoBarra");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientesProdutos");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
