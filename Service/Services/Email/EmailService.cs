using MailKit.Net.Smtp;
using MimeKit;
using minimalistiskdotcom.Service.Services.Email.Interface;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendConfirmationEmailAsync(BookingModel booking)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
        message.To.Add(MailboxAddress.Parse(booking.Email));
        message.Subject = "Confirm your booking";

        var confirmUrl = $"{_configuration["AppSettings:BaseUrl"]}/booking/confirm?code={booking.ConfirmationCode}";
        var cancelUrl = $"{_configuration["AppSettings:BaseUrl"]}/booking/cancel?code={booking.ConfirmationCode}";

        message.Body = new TextPart("html")
        {
            Text = $@"
                <h2>Confirm your booking</h2>
                <p>Date: {booking.Date:yyyy-MM-dd}, Time: {booking.TimeSlot}</p>
                <p><a href='{confirmUrl}'>Click here to confirm</a></p>
                <p><a href='{cancelUrl}'>Cancel booking</a></p>"
        };

        await SendAsync(message);
    }

    public async Task SendReminderEmailAsync(BookingModel booking)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
        message.To.Add(MailboxAddress.Parse(booking.Email));
        message.Subject = "Booking Reminder";

        message.Body = new TextPart("html")
        {
            Text = $"<p>This is a reminder for your booking on {booking.Date:yyyy-MM-dd} at {booking.TimeSlot}</p>"
        };

        await SendAsync(message);
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), true);
        await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
