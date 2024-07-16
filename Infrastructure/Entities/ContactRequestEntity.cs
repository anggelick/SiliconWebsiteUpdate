namespace Infrastructure.Entities;

public class ContactRequestEntity
{
    public int Id { get; set; }
    public string Sender { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? Service { get; set; }
}
