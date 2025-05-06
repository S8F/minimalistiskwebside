using System.ComponentModel.DataAnnotations;

public class BookingModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan TimeSlot { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public bool IsConfirmed { get; set; } = false;

    public string ConfirmationCode { get; set; }

    public bool IsCancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
