using OLMS.Domain.Entities;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> GetByCodeAsync(string code);     
}