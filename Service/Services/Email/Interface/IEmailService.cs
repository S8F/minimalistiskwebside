namespace minimalistiskdotcom.Service.Services.Email.Interface
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(BookingModel booking);
        Task SendReminderEmailAsync(BookingModel booking);
    }
}
