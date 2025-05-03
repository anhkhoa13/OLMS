namespace OLMS.Domain.Entities.QuestionEntity;

public class ShortAnswerQuestion : Question
{
    public string CorrectAnswer { get; set; }
    public override QuestionType Type { get; protected set; } = QuestionType.ShortAnswer;

    public ShortAnswerQuestion(Guid id, string content, string correctAnswer, Guid quizId)
        : base(id, content, quizId)
    {
        CorrectAnswer = correctAnswer;
    }
    public static ShortAnswerQuestion Create(string content, Guid quizId, string correctAnswer) {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty", nameof(content));

        if (quizId == Guid.Empty)
            throw new ArgumentException("Quiz ID cannot be empty", nameof(quizId));

        if (string.IsNullOrWhiteSpace(correctAnswer))
            throw new ArgumentException("Correct answer cannot be empty", nameof(correctAnswer));

        return new ShortAnswerQuestion(Guid.NewGuid(), content, correctAnswer, quizId);
    }
    public void UpdateCorrectAnswer(string answer) {
        CorrectAnswer = answer;
    }
    public override bool IsCorrect(string answer)
    {
        return string.Equals(CorrectAnswer, answer, StringComparison.OrdinalIgnoreCase);
    }
}
