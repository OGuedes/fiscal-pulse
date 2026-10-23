namespace FiscalPulse.Api.Domain;

public enum ObligationStatus { Pending, Completed }
public sealed class FiscalObligation
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public DateOnly DueDate { get; set; }
    public string Category { get; set; } = "";
    public ObligationStatus Status { get; set; }
    public string? DocumentName { get; set; }
}
public sealed record CompleteObligation(string DocumentName);
