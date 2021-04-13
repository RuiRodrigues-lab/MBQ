using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_MBQ.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEvento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telemovel = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEvento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Familias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsentimentoRGPD = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Voluntarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo = table.Column<int>(type: "int", nullable: true),
                    Postal = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsentimentoRGPD = table.Column<bool>(type: "bit", nullable: false),
                    Interno = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voluntarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeMinima = table.Column<int>(type: "int", nullable: false),
                    CategoriaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntregaFamilias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FamiliaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregaFamilias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntregaFamilias_Familias_FamiliaID",
                        column: x => x.FamiliaID,
                        principalTable: "Familias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembroFamilias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrauParentesco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo = table.Column<int>(type: "int", nullable: true),
                    Postal = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamiliaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroFamilias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MembroFamilias_Familias_FamiliaID",
                        column: x => x.FamiliaID,
                        principalTable: "Familias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doacoes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDoacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventoID = table.Column<int>(type: "int", nullable: false),
                    VoluntarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doacoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Doacoes_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doacoes_Voluntarios_VoluntarioID",
                        column: x => x.VoluntarioID,
                        principalTable: "Voluntarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInscrição = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoluntarioID = table.Column<int>(type: "int", nullable: false),
                    EventoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Voluntarios_VoluntarioID",
                        column: x => x.VoluntarioID,
                        principalTable: "Voluntarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosEntregues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProdutoID = table.Column<int>(type: "int", nullable: false),
                    EntregaFamiliaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosEntregues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProdutosEntregues_EntregaFamilias_EntregaFamiliaID",
                        column: x => x.EntregaFamiliaID,
                        principalTable: "EntregaFamilias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosEntregues_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosDoados",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProdutoID = table.Column<int>(type: "int", nullable: false),
                    DoacaoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosDoados", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProdutosDoados_Doacoes_DoacaoID",
                        column: x => x.DoacaoID,
                        principalTable: "Doacoes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosDoados_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "ID", "NomeCategoria" },
                values: new object[,]
                {
                    { 1, "Lacticinios" },
                    { 2, "Produtos de Limpeza" },
                    { 3, "Conservas" },
                    { 4, "Alimentos" },
                    { 5, "Outros" }
                });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "ID", "DataEvento", "Descricao", "Email", "NomeEvento", "Telemovel" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Primeira Campanha Bebe Quentinho para angariar alimentos e outros", "quentinhopapi@gmail.com", "1º Campanha Bebe Quentinho", 916505505 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Segunda Campanha Bebe Quentinho para angariar alimentos e outros", "quentinhopapi@gmail.com", "2º Campanha Bebe Quentinho", 916505505 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Terceira Campanha Bebe Quentinho para angariar alimentos e outros", "quentinhopapi@gmail.com", "3º Campanha Bebe Quentinho", 916505505 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quarta Campanha Bebe Quentinho para angariar alimentos e outros", "quentinhopapi@gmail.com", "4º Campanha Bebe Quentinho", 916505505 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angariação continua de alimentos, apenas é necessario agendar entrega", "quentinhopapi@gmail.com", "Qualquer Hora, Qualquer Dia", 916505505 }
                });

            migrationBuilder.InsertData(
                table: "Familias",
                columns: new[] { "ID", "Nome" },
                values: new object[,]
                {
                    { 10, "Familia Teste" },
                    { 9, "Familia Rodrigues" },
                    { 8, "Familia Ferreira" },
                    { 7, "Familia Alves" },
                    { 6, "Familia Jardim" },
                    { 2, "Familia Silva" },
                    { 4, "Familia Salgueiro" },
                    { 3, "Familia Antunes" },
                    { 1, "Familia Domingues" },
                    { 5, "Familia Costa" }
                });

            migrationBuilder.InsertData(
                table: "Voluntarios",
                columns: new[] { "ID", "Cidade", "Codigo", "ConsentimentoRGPD", "DataNascimento", "Descricao", "Distrito", "Email", "Interno", "Morada", "Nome", "Postal" },
                values: new object[,]
                {
                    { 3, "Setúbal", 2900, true, new DateTime(2004, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "mariana.j.domingues@gmail.com", true, "Rua Major Perestrelo da Conceição", "Mariana Domingues", 550 },
                    { 1, "Setúbal", 2900, true, new DateTime(1982, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "analmjdomingues@yahoo.fr", true, "Rua Major Perestrelo da Conceição", "Ana Domingues", 550 },
                    { 2, "Setúbal", 2900, true, new DateTime(1975, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "domingues_david@yahoo.fr", true, "Rua Major Perestrelo da Conceição", "David Domingues", 550 },
                    { 4, "Setúbal", 2900, true, new DateTime(2008, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "domingues.s.david@gmail.com", true, "Rua Major Perestrelo da Conceição", "David Jesus Domingues", 550 }
                });

            migrationBuilder.InsertData(
                table: "Doacoes",
                columns: new[] { "ID", "DataDoacao", "EventoID", "VoluntarioID" },
                values: new object[] { 1, new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 });

            migrationBuilder.InsertData(
                table: "EntregaFamilias",
                columns: new[] { "ID", "DataEntrega", "FamiliaID" },
                values: new object[] { 1, new DateTime(2021, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 });

            migrationBuilder.InsertData(
                table: "Inscricoes",
                columns: new[] { "ID", "DataInscrição", "EventoID", "VoluntarioID" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 },
                    { 4, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2 },
                    { 2, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 3, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "MembroFamilias",
                columns: new[] { "ID", "Cidade", "Codigo", "DataNascimento", "Descricao", "Distrito", "Email", "FamiliaID", "GrauParentesco", "Morada", "Nome", "Postal" },
                values: new object[,]
                {
                    { 4, "Setúbal", 2900, new DateTime(2008, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "domingues.s.david@gmail.com", 1, "Filho", "Rua Major Perestrelo da Conceição", "David Jesus Domingues", 550 },
                    { 3, "Setúbal", 2900, new DateTime(2004, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "mariana.j.domingues@gmail.com", 1, "Filha", "Rua Major Perestrelo da Conceição", "Mariana Domingues", 550 },
                    { 2, "Setúbal", 2900, new DateTime(1975, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "domingues_david@yahoo.fr", 1, "Pai", "Rua Major Perestrelo da Conceição", "David Domingues", 550 },
                    { 1, "Setúbal", 2900, new DateTime(1982, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Setúbal", "analmjdomingues@yahoo.fr", 1, "Mãe", "Rua Major Perestrelo da Conceição", "Ana Domingues", 550 }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ID", "CategoriaID", "NomeProduto", "QuantidadeMinima" },
                values: new object[,]
                {
                    { 19, 5, "Brinquedos", 0 },
                    { 18, 5, "Sapatos", 0 },
                    { 17, 5, "Roupa", 0 },
                    { 16, 5, "Chuchas", 0 },
                    { 15, 5, "Biberao", 0 },
                    { 5, 5, "Toalhetes", 0 },
                    { 4, 5, "Fraldas", 0 },
                    { 20, 4, "Cereais", 0 },
                    { 14, 4, "Oleo", 0 },
                    { 13, 4, "Azeite", 0 },
                    { 11, 4, "Massa", 0 },
                    { 10, 4, "Arroz", 0 },
                    { 3, 4, "Bolachas", 0 },
                    { 2, 4, "Papas", 0 },
                    { 12, 3, "Feijao", 0 },
                    { 9, 3, "Salsichas", 0 },
                    { 8, 3, "Atum", 0 },
                    { 22, 2, "Gel de duche", 0 },
                    { 21, 2, "Champo", 0 },
                    { 7, 1, "Farinhas Lacteas", 0 },
                    { 6, 1, "Leite em pó", 0 },
                    { 23, 4, "Creme", 0 },
                    { 1, 1, "Leite", 0 }
                });

            migrationBuilder.InsertData(
                table: "ProdutosDoados",
                columns: new[] { "ID", "DoacaoID", "ProdutoID", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 1, 3 },
                    { 2, 1, 20, 5 }
                });

            migrationBuilder.InsertData(
                table: "ProdutosEntregues",
                columns: new[] { "ID", "EntregaFamiliaID", "ProdutoID", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 1, 3 },
                    { 2, 1, 20, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_EventoID",
                table: "Doacoes",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_VoluntarioID",
                table: "Doacoes",
                column: "VoluntarioID");

            migrationBuilder.CreateIndex(
                name: "IX_EntregaFamilias_FamiliaID",
                table: "EntregaFamilias",
                column: "FamiliaID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_EventoID",
                table: "Inscricoes",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_VoluntarioID",
                table: "Inscricoes",
                column: "VoluntarioID");

            migrationBuilder.CreateIndex(
                name: "IX_MembroFamilias_FamiliaID",
                table: "MembroFamilias",
                column: "FamiliaID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaID",
                table: "Produtos",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosDoados_DoacaoID",
                table: "ProdutosDoados",
                column: "DoacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosDoados_ProdutoID",
                table: "ProdutosDoados",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosEntregues_EntregaFamiliaID",
                table: "ProdutosEntregues",
                column: "EntregaFamiliaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosEntregues_ProdutoID",
                table: "ProdutosEntregues",
                column: "ProdutoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricoes");

            migrationBuilder.DropTable(
                name: "MembroFamilias");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "ProdutosDoados");

            migrationBuilder.DropTable(
                name: "ProdutosEntregues");

            migrationBuilder.DropTable(
                name: "Doacoes");

            migrationBuilder.DropTable(
                name: "EntregaFamilias");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Voluntarios");

            migrationBuilder.DropTable(
                name: "Familias");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
