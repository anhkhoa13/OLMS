namespace OLMS.Domain.Entities.QuestionEntity;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; private set; } = [];
    public int CorrectOptionIndex { get; private set; } 
    public override QuestionType Type { get; protected set; } = QuestionType.MultipleChoice;

    private MultipleChoiceQuestion() : base() { } // For EF Core

    public MultipleChoiceQuestion(Guid id, string content, List<string> options, int correctOptionIndex, Guid quizId)
        : base(id, content, quizId)
    {
        Options = options;
        CorrectOptionIndex = correctOptionIndex;
    }
    public static MultipleChoiceQuestion Create(string content, Guid quizId, List<string> options, int correctOptionIndex) {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty", nameof(content));

        if (quizId == Guid.Empty)
            throw new ArgumentException("Quiz ID cannot be empty", nameof(quizId));

        if (options == null || options.Count == 0)
            throw new ArgumentException("Options cannot be empty", nameof(options));

        if (correctOptionIndex < 0 || correctOptionIndex >= options.Count)
            throw new ArgumentException("Correct option index is out of range", nameof(correctOptionIndex));

        return new MultipleChoiceQuestion(Guid.NewGuid(), content, options, correctOptionIndex, quizId);
    }
    public override bool IsCorrect(string answer)
    {
        int result = -1;
        try
        {
            result = int.Parse(answer);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{answer}'");
        }
        return result == CorrectOptionIndex;
    }
}
