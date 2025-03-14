﻿using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Course?> GetByCodeAsync(string code)
    {
        var course = await _context.Courses.Include(c => c.Enrollments)
                                            .SingleOrDefaultAsync(c => c.Code.Value == code);
        return course;
    }

    public override async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Courses.Include(c => c.Enrollments)
                                     .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}

