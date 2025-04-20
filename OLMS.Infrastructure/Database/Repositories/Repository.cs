using OLMS.Domain.Repositories;
using OLMS.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace OLMS.Infrastructure.Database.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _context;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) {
        try {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        } catch (Exception ex) {
            // Log the exception (using your logging mechanism)
            Console.WriteLine($"An error occurred while fetching the entity by id: {ex.Message}");

            // You can also log the full exception for more details
            // Logger.LogError(ex, "Error in GetByIdAsync method.");

            // Optionally, rethrow the exception or return null depending on your error handling strategy
            throw new ApplicationException($"An error occurred while fetching the entity with id {id}.", ex);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
