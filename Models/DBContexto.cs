using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class DBContexto : DbContext
    {
        public DBContexto()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Conexao.Dados);
            }
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
