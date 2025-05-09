﻿using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

public interface IQuizAttemptRepository : IRepository<QuizAttempt>
{
    Task<List<QuizAttempt>> GetAttemptsByStudentId(Guid studentId);
    Task<List<QuizAttempt>> GetAttemptsByStudentAndQuizAsync(Guid studentId, Guid quizId);

}
