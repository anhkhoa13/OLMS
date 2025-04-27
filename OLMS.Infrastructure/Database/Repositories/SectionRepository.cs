using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class SectionRepository(ApplicationDbContext context) : Repository<Section>(context), ISectionRepository
{

}

