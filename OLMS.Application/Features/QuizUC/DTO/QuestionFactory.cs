using OLMS.Domain.Entities.QuestionEntity;

namespace OLMS.Application.Features.QuizUC.DTO;

public static class QuestionFactory
{
    public static Question CreateQuestion(Guid id, QuestionCreateDto dto, Guid quizId)
    {
        if (dto.Type == "MultipleChoice")
        {
            if (dto.Options == null || dto.CorrectOptionIndex == null)
                throw new ArgumentException("Multiple choice questions require options and a correct option index.");

            return new MultipleChoiceQuestion(id, dto.Content, dto.Options, dto.CorrectOptionIndex.Value, quizId);
        }
        else if (dto.Type == "ShortAnswer")
        {
            if (string.IsNullOrEmpty(dto.CorrectAnswer))
                throw new ArgumentException("Short answer questions require a correct answer.");

            return new ShortAnswerQuestion(id, dto.Content, dto.CorrectAnswer, quizId);
        }

        throw new ArgumentException($"Unsupported question type: {dto.Type}");
    }
}
