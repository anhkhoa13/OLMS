using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.QuestionEntity;
public enum QuestionType
{
    MultipleChoice,
    TrueFalse,
    ShortAnswer
}
public abstract class Question : Entity
{
    public string Content { get; set; } = default!;
    public abstract QuestionType Type { get; protected set; }
    public Guid QuizId { get; set; }
    public bool IsDeleted { get; set; } // Soft delete flag
    protected Question() : base() { } // For EF Core

    protected Question(Guid id, string content, Guid quizId) : base(id)
    {
        Content = content;
        QuizId = quizId;
    }
    public abstract bool IsCorrect(string answer);
}
