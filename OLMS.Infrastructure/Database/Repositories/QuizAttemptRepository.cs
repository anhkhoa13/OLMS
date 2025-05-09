﻿using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class QuizAttemptRepository : Repository<QuizAttempt>, IQuizAttemptRepository {
    public QuizAttemptRepository(ApplicationDbContext context) : base(context) { }

    public Task<List<QuizAttempt>> GetAttemptsByStudentId(Guid studentId) {
        throw new NotImplementedException();
    }
    public async Task<List<QuizAttempt>> GetAttemptsByStudentAndQuizAsync(Guid studentId, Guid quizId) {
        return await _context.QuizAttempts
            .Where(a => a.StudentId == studentId && a.QuizId == quizId)
            .ToListAsync();
    }

}