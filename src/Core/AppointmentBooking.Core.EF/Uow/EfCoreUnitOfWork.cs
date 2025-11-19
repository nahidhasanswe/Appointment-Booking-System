using AppointmentBooking.Core.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AppointmentBooking.Core.EF.Uow;

/// <summary>
/// Implements Unit of work for Entity Framework.
/// </summary>
public class EfCoreUnitOfWork<TDbContext> : UnitOfWork
    where TDbContext : DbContext
{
    private bool disposed = false;
    private readonly TDbContext _context;
    
    private IDbContextTransaction? _transaction;
    
    /// <summary>
    /// Creates a new <see cref="EfCoreUnitOfWork{TDbContext}"/>.
    /// </summary>
    public EfCoreUnitOfWork(TDbContext dbContext)
    {
        _context = dbContext;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposed)
            return;

        disposed = true;

        base.Dispose(disposing);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public override Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public override async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction?.CommitAsync(cancellationToken)!;
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public override async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _transaction?.RollbackAsync(cancellationToken)!;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}