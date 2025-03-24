using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace OLMS.Infrastructure.Database.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        public Task AddAsync(Material material)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        public Task<Material> GetByIdAsync(Guid id)
        {
            // Implementation here
            throw new NotImplementedException();
        }
    }
}