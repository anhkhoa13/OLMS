using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public class QuizAttempt : Entity, IAggregateRoot
{
    public Guid QuizAttempId { get; set; }
    public Guid StudentId { get; private set; }
    public Guid QuizId { get; private set; }
    public List<StudentAnswer> Answers { get; private set; } = new();
    public bool IsSubmitted { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public int Score { get; private set; }

    public QuizAttempt(Guid id, Guid studentId, Guid quizId) : base(id)
    {
        StudentId = studentId;
        QuizId = quizId;
    }

    public void AddAnswer(StudentAnswer answer) => Answers.Add(answer);
    public void Submit() {
        IsSubmitted = true;
        SubmittedAt = DateTime.UtcNow;
    } 
}
