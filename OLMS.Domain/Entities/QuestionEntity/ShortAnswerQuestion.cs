namespace OLMS.Domain.Entities.QuestionEntity;

public class ShortAnswerQuestion : Question
{
    public string CorrectAnswer { get; private set; }
    public override QuestionType Type { get; protected set; } = QuestionType.ShortAnswer;

    public ShortAnswerQuestion(Guid id, string content, string correctAnswer, Guid quizId)
        : base(id, content, quizId)
    {
        CorrectAnswer = correctAnswer;
    }
    public override bool IsCorrect(string answer)
    {
        return string.Equals(CorrectAnswer, answer, StringComparison.OrdinalIgnoreCase);
    }
}
