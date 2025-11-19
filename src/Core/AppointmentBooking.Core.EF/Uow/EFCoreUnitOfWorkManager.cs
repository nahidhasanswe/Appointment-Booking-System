using AppointmentBooking.Core.Uow;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AppointmentBooking.Core.EF.Uow;

public class EfCoreUnitOfWorkManager<TDbContext>(TDbContext dbContext, IMediator mediator) : IUnitOfWorkManager
    where TDbContext : DbContext
{
    public IUnitOfWork Begin()
    {
        return new EfCoreUnitOfWork<TDbContext>(dbContext, mediator);
    }
}