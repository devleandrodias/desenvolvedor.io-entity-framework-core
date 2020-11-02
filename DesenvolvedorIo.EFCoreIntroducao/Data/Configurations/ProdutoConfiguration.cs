using DesenvolvedorIo.EFCoreIntroducao.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesenvolvedorIo.EFCoreIntroducao.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("VARCHAR(16)");
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.TipoProduto).HasConversion<string>();
            // Converte ENUM p/ string
        }
    }
}
