using Microsoft.EntityFrameworkCore;

namespace DesenvolvedorIo.EFCoreIntroducao.Data
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=dev_io_entity_framework_intro;User Id=sa;Password=m9F857JJXLhss2Mm;");
        }        
    }
}
