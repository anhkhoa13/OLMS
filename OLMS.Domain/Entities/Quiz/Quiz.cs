using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public abstract class Quiz : Entity, IAggregateRoot
{
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public DateTime StartTime { get; protected set; }
    public DateTime EndTime { get; protected set; }
    public bool IsTimeLimited { get; protected set; }

    public Quiz(Guid id, string title, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
        IsTimeLimited = isTimeLimited;
    }
}
