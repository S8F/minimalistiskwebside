using Microsoft.EntityFrameworkCore;
using minimalistiskdotcom.Service.Repository.Interface;

namespace minimalistiskdotcom.Service.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Task<List<BookingModel>> GetBookingsByDateAsync(DateTime date) =>
            _context.Bookings
                .Where(b => b.Date.Date == date.Date && !b.IsCancelled)
                .ToListAsync();

        public Task<BookingModel> GetBookingByConfirmationCodeAsync(string code) =>
            _context.Bookings.FirstOrDefaultAsync(b => b.ConfirmationCode == code);

        public Task AddBookingAsync(BookingModel bookingModel)
        {
            return _context.Bookings.AddAsync(bookingModel).AsTask();
        }

        public Task SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}
