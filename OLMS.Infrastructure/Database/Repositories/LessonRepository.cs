using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class LessonRepository(ApplicationDbContext context) : Repository<Lesson>(context), ILessonRepository
{

}

