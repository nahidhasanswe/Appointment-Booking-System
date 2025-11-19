using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Core.EF;

public sealed class DefaultDbContextProvider<TDbContext>(TDbContext dbContext) : IDbContextProvider<TDbContext>
    where TDbContext : DbContext
{
    public TDbContext GetDbContext()
    {
        return dbContext;
    }

    public void Dispose()
    {
        dbContext?.Dispose();
    }
}