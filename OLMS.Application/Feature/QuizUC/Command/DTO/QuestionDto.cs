using OLMS.Domain.Entities.QuizEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.QuizUC.Command.DTO;
public class QuestionDto
{
    public Guid QuestionId { get; set; }
    public string Content { get; set; }
    public QuestionType Type { get; set; }
    public List<string> Options { get; set; }
}
