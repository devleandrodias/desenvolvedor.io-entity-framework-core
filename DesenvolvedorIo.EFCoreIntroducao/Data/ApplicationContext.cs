using DesenvolvedorIo.EFCoreIntroducao.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesenvolvedorIo.EFCoreIntroducao.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(x => x.AddConsole());

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Server=127.0.0.1,1433;Database=dev_io_entity_framework_intro;User Id=sa;Password=m9F857JJXLhss2Mm;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Faz o ef core procurar no assembly todas as classes que implementan interface IEntityTypeConfiguration e mapeia elas
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        }
    }
}
