using FiscalPulse.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace FiscalPulse.Api.Data;
public sealed class FiscalDbContext(DbContextOptions<FiscalDbContext> options) : DbContext(options)
{
    public DbSet<FiscalObligation> Obligations => Set<FiscalObligation>();
}
public static class DemoData
{
    public static readonly Guid PendingId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static void Seed(FiscalDbContext db)
    {
        if (db.Obligations.Any()) return;
        db.Obligations.AddRange(
            new FiscalObligation { Id = PendingId, Title = "Enviar notas fiscais", DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)), Category = "Fiscal", Status = ObligationStatus.Pending },
            new FiscalObligation { Id = Guid.NewGuid(), Title = "Conferir folha", DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(8)), Category = "Pessoal", Status = ObligationStatus.Pending });
        db.SaveChanges();
    }
}
