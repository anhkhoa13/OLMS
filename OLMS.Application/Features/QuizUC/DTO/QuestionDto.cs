namespace OLMS.Application.Features.QuizUC.DTO;
public record QuestionDto
{
    public Guid? QuestionId { get; set; } // Nullable for new questions
    public required string Content { get; set; }
    public required string Type { get; set; } // "MultipleChoice" or "ShortAnswer"

    // Nullable properties depending on question type
    public List<string>? Options { get; set; }
    public int? CorrectOptionIndex { get; set; }
    public string? CorrectAnswer { get; set; }
}
