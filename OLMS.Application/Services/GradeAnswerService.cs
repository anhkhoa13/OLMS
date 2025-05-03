

using OLMS.Domain.Entities.QuestionEntity;

namespace OLMS.Application.Services;

public class GradeAnswerService
{
    public float CalculateGrade(List<Question> questions, Dictionary<Guid, string> answer)
    {
        float score = 0f;
        float scoreForEachQuestion = 100f / questions.Count;

        foreach (var question in questions)
        {
            if (answer.TryGetValue(question.Id, out var studentAnswer))
            {
                if (question.IsCorrect(studentAnswer))
                {
                    score += scoreForEachQuestion;
                }
            }
        }

        return score;
    }
}
