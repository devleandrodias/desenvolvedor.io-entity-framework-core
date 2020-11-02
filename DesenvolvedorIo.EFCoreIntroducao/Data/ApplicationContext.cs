using DesenvolvedorIo.EFCoreIntroducao.Data.Configurations;
using DesenvolvedorIo.EFCoreIntroducao.Domain;
using Microsoft.EntityFrameworkCore;

namespace DesenvolvedorIo.EFCoreIntroducao.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=dev_io_entity_framework_intro;User Id=sa;Password=m9F857JJXLhss2Mm;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Faz o ef core procurar no assembly todas as classes que implementan interface IEntityTypeConfiguration e mapeia elas
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
