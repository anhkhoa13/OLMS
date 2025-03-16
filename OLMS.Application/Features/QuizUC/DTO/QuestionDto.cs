using OLMS.Domain.Entities.QuizEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Features.QuizUC.DTO;
public class QuestionDto
{
    public Guid QuestionId { get; set; }
    public required string Content { get; set; }
    public required string Type { get; set; }
    public List<string> Options { get; set; } = new();
}
