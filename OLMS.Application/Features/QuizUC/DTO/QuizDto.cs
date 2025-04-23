using OLMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Features.QuizUC.DTO;
public class QuizDto
{
    public Guid QuizId { get; set; }
    public Code Code { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsTimeLimited { get; set; }
    public int NumberOfAttempts { get; set; }
    public TimeSpan? TimeLimit { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}
