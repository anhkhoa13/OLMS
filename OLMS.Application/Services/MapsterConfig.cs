using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuestionEntity;
using Mapster;

namespace OLMS.Application.Services;
public class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Question, QuestionDto>
            .NewConfig()
            .Map(dest => dest.Type, src => src.GetType().Name)
            .AfterMapping((src, dest) =>
            {
                if (src is MultipleChoiceQuestion mcq)
                {
                    dest.Options = mcq.Options;
                }
                else
                {
                    dest.Options = new List<string>();
                }

                if (src is ShortAnswerQuestion saq)
                {
                    dest.CorrectAnswer = saq.CorrectAnswer;
                }
                else
                {
                    dest.CorrectAnswer = null;
                }
            });
    }
}


