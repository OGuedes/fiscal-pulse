using FiscalPulse.Api.Data;
using FiscalPulse.Api.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var connection = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<FiscalDbContext>(options =>
{
    if (string.IsNullOrWhiteSpace(connection)) options.UseInMemoryDatabase("fiscal-pulse-demo");
    else options.UseNpgsql(connection);
});
var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FiscalDbContext>();
    db.Database.EnsureCreated();
    DemoData.Seed(db);
}

app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "fiscal-pulse-api" }));
app.MapGet("/api/dashboard", async (FiscalDbContext db) => Results.Ok(new
{
    company = "Ateliê Aurora Ltda.",
    pending = await db.Obligations.CountAsync(x => x.Status == ObligationStatus.Pending),
    nextDue = await db.Obligations.AsNoTracking().Where(x => x.Status == ObligationStatus.Pending).OrderBy(x => x.DueDate).FirstAsync()
}));
app.MapGet("/api/obligations", async (FiscalDbContext db) =>
    Results.Ok(await db.Obligations.AsNoTracking().OrderBy(x => x.DueDate).ToListAsync()));
app.MapPatch("/api/obligations/{id:guid}/complete", async (Guid id, CompleteObligation input, FiscalDbContext db) =>
{
    var item = await db.Obligations.FindAsync(id);
    if (item is null) return Results.NotFound();
    item.Status = ObligationStatus.Completed;
    item.DocumentName = input.DocumentName;
    await db.SaveChangesAsync();
    return Results.Ok(item);
});
app.Run();
public partial class Program;
