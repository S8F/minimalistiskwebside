namespace minimalistiskdotcom.Service.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<List<BookingModel>> GetBookingsByDateAsync(DateTime date);
        Task<BookingModel> GetBookingByConfirmationCodeAsync(string code);
        Task AddBookingAsync(BookingModel booking);
        Task SaveChangesAsync();
    }
}
