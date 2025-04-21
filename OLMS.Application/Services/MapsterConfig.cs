using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuestionEntity;
using Mapster;
using OLMS.Domain.Entities.QuizEntity;

namespace OLMS.Application.Services;
public class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Question, QuestionDto>
            .NewConfig()
            .Map(dest => dest.QuestionId, src => src.Id)
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
        TypeAdapterConfig<Quiz, QuizDto>
            .NewConfig()
            .Map(dest => dest.Code, src => src.Code);
    }
}


