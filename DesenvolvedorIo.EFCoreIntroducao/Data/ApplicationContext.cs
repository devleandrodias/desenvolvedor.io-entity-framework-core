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

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(x =>
            {
                x.ToTable("Clientes");
                x.HasKey(x => x.Id);
                x.Property(x => x.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                x.Property(x => x.Telefone).HasColumnType("CHAR(11)");
                x.Property(x => x.CEP).HasColumnType("CHAR(8)").IsRequired();
                x.Property(x => x.Estado).HasColumnType("CHAR(2)").IsRequired();
                x.Property(x => x.Cidade).HasMaxLength(60).IsRequired();

                x.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
            });

            modelBuilder.Entity<Produto>(x =>
            {
                x.ToTable("Produto");
                x.HasKey(x => x.Id);
                x.Property(x => x.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                x.Property(x => x.Descricao).HasColumnType("VARCHAR(16)");
                x.Property(x => x.Valor).IsRequired();
                x.Property(x => x.TipoProduto).HasConversion<string>(); // Converte ENUM p/ string
            });

            modelBuilder.Entity<Pedido>(x =>
            {
                x.ToTable("Pedidos");
                x.HasKey(x => x.Id);
                x.Property(x => x.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                x.Property(x => x.Status).HasConversion<string>();
                x.Property(x => x.TipoFrete).HasConversion<int>();
                x.Property(x => x.Observacao).HasColumnType("VARCHAR(512)");

                x.HasMany(x => x.Itens)
                    .WithOne(x => x.Pedido)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PedidoItem>(x =>
            {
                x.ToTable("PedidoItens");
                x.HasKey(x => x.Id);
                x.Property(x => x.Quanitdade).HasDefaultValue(1).IsRequired();
                x.Property(x => x.Valor).IsRequired();
                x.Property(x => x.Desconto).IsRequired();
            });
        }
    }
}
