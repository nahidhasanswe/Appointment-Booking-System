using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Core.EF;

public interface IDbContextProvider<out TDbContext>
    where TDbContext : DbContext
{
    TDbContext GetDbContext();
}