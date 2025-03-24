using OLMS.Domain.Entities;

namespace OLMS.Domain.Repositories
{
    public interface IMaterialRepository
    {
        Task AddAsync(Material material);
        Task<Material> GetByIdAsync(Guid id);
    }
}