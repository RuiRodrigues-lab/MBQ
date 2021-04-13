using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe AppMovimentoBQDbContext que herda da classe DbContext. Permite Mapear a BD
    /// </summary>
    public class AppMovimentoBQDbContext : DbContext
    {
        /// <summary>
        /// Detalhes de configuração de BD, para BD funcionar é importante inserir a string para BD SQL no appsettings.json, e o contexto no startup.cs: https://docs.microsoft.com/pt-pt/ef/core/cli/dbcontext-creation?tabs=vs.
        /// Para fazer migration/update é necessário especificar o DBcontext.
        /// Add-Migration InitialCreate -Context AppMovimentoBQDbContext
        ///Update-Database -Context AppMovimentoBQDbContext
        /// </summary>
        public AppMovimentoBQDbContext(DbContextOptions<AppMovimentoBQDbContext> options)
            : base(options)
        {
        }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Categorias
        /// </value>
        public DbSet<Categoria> Categorias { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Voluntarios
        /// </value>
        public DbSet<Voluntario> Voluntarios { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Eventos
        /// </value>
        public DbSet<Evento> Eventos { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Familias
        /// </value>
        public DbSet<Familia> Familias { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Produtos
        /// </value>
        public DbSet<Produto> Produtos { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Membros de Familias
        /// </value>
        public DbSet<MembroFamilia> MembroFamilias { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Inscrições
        /// </value>
        public DbSet<Inscricao> Inscricoes { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Produtos Entregues
        /// </value>
        public DbSet<ProdutoEntregue> ProdutosEntregues { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Produtos Doados
        /// </value>
        public DbSet<ProdutoDoado> ProdutosDoados { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Entregas realizadas a Famílias
        /// </value>
        public DbSet<EntregaFamilia> EntregaFamilias { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de Doações
        /// </value>
        public DbSet<Doacao> Doacoes { get; set; }
        /// <value>
        /// Propriedade DbSet que guarda repositório de dados referentes a Newsletters
        /// </value>
        public DbSet<Newsletter> Newsletters { get; set; }


        /// <summary>
        /// Método que faz o populate da Base de Dados
        /// </summary>
        /// <param name="modelBuilder">objeto do tipo ModelBuilder </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                        new Categoria() { ID = 1, NomeCategoria = "Lacticinios" },
                        new Categoria() { ID = 2, NomeCategoria = "Produtos de Limpeza" },
                        new Categoria() { ID = 3, NomeCategoria = "Conservas" },
                        new Categoria() { ID = 4, NomeCategoria = "Alimentos" },
                        new Categoria() { ID = 5, NomeCategoria = "Outros" }
                        );

            modelBuilder.Entity<Produto>().HasData(
                        new Produto() { ID = 1, NomeProduto = "Leite", QuantidadeMinima = 0, CategoriaID = 1 },
                        new Produto() { ID = 2, NomeProduto = "Papas", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 3, NomeProduto = "Bolachas", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 4, NomeProduto = "Fraldas", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 5, NomeProduto = "Toalhetes", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 6, NomeProduto = "Leite em pó", QuantidadeMinima = 0, CategoriaID = 1 },
                        new Produto() { ID = 7, NomeProduto = "Farinhas Lacteas", QuantidadeMinima = 0, CategoriaID = 1 },
                        new Produto() { ID = 8, NomeProduto = "Atum", QuantidadeMinima = 0, CategoriaID = 3 },
                        new Produto() { ID = 9, NomeProduto = "Salsichas", QuantidadeMinima = 0, CategoriaID = 3 },
                        new Produto() { ID = 10, NomeProduto = "Arroz", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 11, NomeProduto = "Massa", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 12, NomeProduto = "Feijao", QuantidadeMinima = 0, CategoriaID = 3 },
                        new Produto() { ID = 13, NomeProduto = "Azeite", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 14, NomeProduto = "Oleo", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 15, NomeProduto = "Biberao", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 16, NomeProduto = "Chuchas", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 17, NomeProduto = "Roupa", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 18, NomeProduto = "Sapatos", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 19, NomeProduto = "Brinquedos", QuantidadeMinima = 0, CategoriaID = 5 },
                        new Produto() { ID = 20, NomeProduto = "Cereais", QuantidadeMinima = 0, CategoriaID = 4 },
                        new Produto() { ID = 21, NomeProduto = "Champo", QuantidadeMinima = 0, CategoriaID = 2 },
                        new Produto() { ID = 22, NomeProduto = "Gel de duche", QuantidadeMinima = 0, CategoriaID = 2 },
                        new Produto() { ID = 23, NomeProduto = "Creme", QuantidadeMinima = 0, CategoriaID = 4 }
                        );


            modelBuilder.Entity<Voluntario>().HasData(
                        new Voluntario() { ID = 1, Interno = true, Nome = "Ana Domingues", DataNascimento = DateTime.Parse("01/03/1982"), Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "analmjdomingues@yahoo.fr", Descricao = null, ConsentimentoRGPD = true },
                        new Voluntario() { ID = 2, Interno = true, Nome = "David Domingues", DataNascimento = DateTime.Parse("21/07/1975").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "domingues_david@yahoo.fr", Descricao = null, ConsentimentoRGPD = true },
                        new Voluntario() { ID = 3, Interno = true, Nome = "Mariana Domingues", DataNascimento = DateTime.Parse("01/06/2004").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "mariana.j.domingues@gmail.com", Descricao = null, ConsentimentoRGPD = true },
                        new Voluntario() { ID = 4, Interno = true, Nome = "David Jesus Domingues", DataNascimento = DateTime.Parse("30/06/2008").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "domingues.s.david@gmail.com", Descricao = null, ConsentimentoRGPD = true }
                        );

            modelBuilder.Entity<Evento>().HasData(
                        new Evento() { ID = 1, NomeEvento = "1º Campanha Bebe Quentinho", Telemovel = 916505505, Email = "quentinhopapi@gmail.com", Descricao = "Primeira Campanha Bebe Quentinho para angariar alimentos e outros" },
                        new Evento() { ID = 2, NomeEvento = "2º Campanha Bebe Quentinho", Telemovel = 916505505, Email = "quentinhopapi@gmail.com", Descricao = "Segunda Campanha Bebe Quentinho para angariar alimentos e outros" },
                        new Evento() { ID = 3, NomeEvento = "3º Campanha Bebe Quentinho", Telemovel = 916505505, Email = "quentinhopapi@gmail.com", Descricao = "Terceira Campanha Bebe Quentinho para angariar alimentos e outros" },
                        new Evento() { ID = 4, NomeEvento = "4º Campanha Bebe Quentinho", Telemovel = 916505505, Email = "quentinhopapi@gmail.com", Descricao = "Quarta Campanha Bebe Quentinho para angariar alimentos e outros" },
                        new Evento() { ID = 5, NomeEvento = "Qualquer Hora, Qualquer Dia", Telemovel = 916505505, Email = "quentinhopapi@gmail.com", Descricao = "Angariação continua de alimentos, apenas é necessario agendar entrega" }
                        );

            modelBuilder.Entity<Familia>().HasData(
                        new Familia() { ID = 1, Nome = "Familia Domingues" },
                        new Familia() { ID = 2, Nome = "Familia Silva" },
                        new Familia() { ID = 3, Nome = "Familia Antunes" },
                        new Familia() { ID = 4, Nome = "Familia Salgueiro" },
                        new Familia() { ID = 5, Nome = "Familia Costa" },
                        new Familia() { ID = 6, Nome = "Familia Jardim" },
                        new Familia() { ID = 7, Nome = "Familia Alves" },
                        new Familia() { ID = 8, Nome = "Familia Ferreira" },
                        new Familia() { ID = 9, Nome = "Familia Rodrigues" },
                        new Familia() { ID = 10, Nome = "Familia Teste" }
                        );

            modelBuilder.Entity<MembroFamilia>().HasData(
                        new MembroFamilia() { ID = 1, Nome = "Ana Domingues", GrauParentesco = "Mãe", DataNascimento = DateTime.Parse("01/03/1982").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "analmjdomingues@yahoo.fr", Descricao = null, FamiliaID = 1 },
                        new MembroFamilia() { ID = 2, Nome = "David Domingues", GrauParentesco = "Pai", DataNascimento = DateTime.Parse("21/07/1975").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "domingues_david@yahoo.fr", Descricao = null, FamiliaID = 1 },
                        new MembroFamilia() { ID = 3, Nome = "Mariana Domingues", GrauParentesco = "Filha", DataNascimento = DateTime.Parse("01/06/2004").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "mariana.j.domingues@gmail.com", Descricao = null, FamiliaID = 1 },
                        new MembroFamilia() { ID = 4, Nome = "David Jesus Domingues", GrauParentesco = "Filho", DataNascimento = DateTime.Parse("30/06/2008").Date, Morada = "Rua Major Perestrelo da Conceição", Cidade = "Setúbal", Distrito = "Setúbal", Codigo = 2900, Postal = 550, Email = "domingues.s.david@gmail.com", Descricao = null, FamiliaID = 1 }
                        );

            modelBuilder.Entity<ProdutoDoado>().HasData(
                        new ProdutoDoado() { ID = 1, Quantidade = 3, ProdutoID = 1, DoacaoID = 1 },
                        new ProdutoDoado() { ID = 2, Quantidade = 5, ProdutoID = 20, DoacaoID = 1 }
                );


            modelBuilder.Entity<Doacao>().HasData(
                new Doacao()
                {
                    ID = 1,
                    DataDoacao = DateTime.Parse("27/01/2021"),
                    EventoID = 5,
                    VoluntarioID = 1,
                    ProdutosDoados = new List<ProdutoDoado>()
                }
                );

            modelBuilder.Entity<Inscricao>().HasData(
                new Inscricao() { ID = 1, DataInscrição = DateTime.Parse("13/02/2021"), VoluntarioID = 4, EventoID = 5 },
                new Inscricao() { ID = 2, DataInscrição = DateTime.Parse("13/02/2021"), VoluntarioID = 1, EventoID = 5 },
                new Inscricao() { ID = 3, DataInscrição = DateTime.Parse("13/02/2021"), VoluntarioID = 3, EventoID = 5 },
                new Inscricao() { ID = 4, DataInscrição = DateTime.Parse("13/02/2021"), VoluntarioID = 2, EventoID = 5 }
                );

            modelBuilder.Entity<ProdutoEntregue>().HasData(
                new ProdutoEntregue() { ID = 1, Quantidade = 3, ProdutoID = 1, EntregaFamiliaID = 1 },
                new ProdutoEntregue() { ID = 2, Quantidade = 5, ProdutoID = 20, EntregaFamiliaID = 1 }
                );

            modelBuilder.Entity<EntregaFamilia>().HasData(
                new EntregaFamilia()
                {
                    ID = 1,
                    DataEntrega = DateTime.Parse("14/02/2021"),
                    FamiliaID = 10,
                    ProdutosEntregues = new List<ProdutoEntregue>()
                }
                );

        }
    }
}
