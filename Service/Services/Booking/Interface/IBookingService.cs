namespace minimalistiskdotcom.Service.Services.Booking.Interface;

public interface IBookingService
{
    Task<IEnumerable<TimeSpan>> GetAvailableTimeSlotsAsync(DateTime date);
    Task<BookingModel> CreateBookingAsync(DateTime date, TimeSpan timeSlot, string email);
    Task<bool> ConfirmBookingAsync(string confirmationCode);
    Task<bool> CancelBookingAsync(string confirmationCode);
}

