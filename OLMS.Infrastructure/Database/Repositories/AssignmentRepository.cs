using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class AssignmentRepository(ApplicationDbContext context) : Repository<Assignment>(context), IAssignmentRepository
{
    
}

