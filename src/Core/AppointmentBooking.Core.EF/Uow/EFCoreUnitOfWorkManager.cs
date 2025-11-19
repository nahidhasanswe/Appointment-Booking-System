using AppointmentBooking.Core.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AppointmentBooking.Core.EF.Uow;

public class EFCoreUnitOfWorkManager<TDbContext>(TDbContext dbContext) : IUnitOfWorkManager
    where TDbContext : DbContext
{
    public IUnitOfWork Begin()
    {
        return new EfCoreUnitOfWork<TDbContext>(dbContext);
    }
}