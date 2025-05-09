﻿using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface IExerciseRepository : IRepository<Exercise> {
    Task<Exercise?> GetByIdWithAttachmentsAsync(Guid id);
}