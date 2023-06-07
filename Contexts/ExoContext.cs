using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Exo.WebApi.Contexts
{

    // Responsável pela conexão com o BD
    public class ExoContext : DbContext
    {

        // Construtor
        public ExoContext()
        {
        }

        // DB Context
        public ExoContext(DbContextOptions<ExoContext> options) : base(options)
        {
        }

        // Método para configuração
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // String de conexão 
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-76HIR06F\\SQLEXPRESS;Initial Catalog=ExoApi;Integrated Security=True");


                // Exemplo 1 de string de conexão:
                // User ID=sa;Password=admin;Server=localhost;Database=ExoApi;Trusted_Connection=False;                
                // Exemplo 2 de string de conexão:
                // Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;

            }
        }

        // Aponta para a tabela  de Projetos
        public DbSet<Projeto> Projetos { get; set; }
        
        // Aponta para a tabela de Usuarios
        public DbSet<Usuario> Usuarios { get; set; }
    }
}