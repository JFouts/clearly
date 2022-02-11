using KingdomDeathTools.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace KingdomDeathTools.Api.Startup;

public class DataSource : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Settlement>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Survivor>()
            .HasKey(x => x.Id);
            
        modelBuilder.Entity<Survivor>()
            .HasOne<Settlement>()
            .WithMany()
            .HasForeignKey(x => x.SettlementId);

        base.OnModelCreating(modelBuilder);
    }
}