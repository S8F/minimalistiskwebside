using minimalistiskdotcom.Service.Interfaces;
using minimalistiskdotcom.Service.Services.Booking.Interface;
using minimalistiskdotcom.Service.Services.Email.Interface;
namespace minimalistiskdotcom.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IEmailService _emailService;

        private readonly TimeSpan StartTime = new(9, 0, 0);  // 9:00
        private readonly TimeSpan EndTime = new(19, 0, 0);   // 19:00
        private readonly TimeSpan Interval = new(1, 0, 0);   // 1 hour slots

        public BookingService(IBookingRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<TimeSpan>> GetAvailableTimeSlotsAsync(DateTime date)
        {
            var existingBookings = await _repository.GetBookingsByDateAsync(date);

            var allSlots = Enumerable.Range(0, (int)((EndTime - StartTime).TotalHours))
                .Select(i => StartTime.Add(TimeSpan.FromHours(i)))
                .ToList();

            var takenSlots = existingBookings.Select(b => b.TimeSlot).ToHashSet();

            return allSlots.Where(slot => !takenSlots.Contains(slot));
        }

        public async Task<BookingModel> CreateBookingAsync(DateTime date, TimeSpan timeSlot, string email)
        {
            var availableSlots = await GetAvailableTimeSlotsAsync(date);

            if (!availableSlots.Contains(timeSlot))
                throw new InvalidOperationException("Time slot is already taken");

            var confirmationCode = Guid.NewGuid().ToString();

            var booking = new BookingModel
            {
                Date = date.Date,
                TimeSlot = timeSlot,
                Email = email,
                ConfirmationCode = confirmationCode,
                IsConfirmed = false
            };

            await _repository.AddBookingAsync(booking);
            await _repository.SaveChangesAsync();

            await _emailService.SendConfirmationEmailAsync(booking);

            return booking;
        }

        public async Task<bool> ConfirmBookingAsync(string confirmationCode)
        {
            var booking = await _repository.GetBookingByConfirmationCodeAsync(confirmationCode);
            if (booking == null || booking.IsCancelled)
                return false;

            booking.IsConfirmed = true;
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(string confirmationCode)
        {
            var booking = await _repository.GetBookingByConfirmationCodeAsync(confirmationCode);
            if (booking == null || booking.IsCancelled)
                return false;

            booking.IsCancelled = true;
            await _repository.SaveChangesAsync();
            return true;
        }
    }

}
