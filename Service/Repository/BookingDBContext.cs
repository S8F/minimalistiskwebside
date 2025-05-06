using Microsoft.EntityFrameworkCore;

namespace minimalistiskdotcom.Service.Repository
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<BookingModel> Bookings { get; set; }
    }
}
