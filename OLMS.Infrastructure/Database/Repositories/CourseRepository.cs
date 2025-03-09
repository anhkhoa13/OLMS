using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context) { }
}

