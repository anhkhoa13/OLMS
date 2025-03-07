using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public abstract class Quiz : Entity
{
    public string Title { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsTimeLimited { get; private set; }

    public Quiz(Guid id, string title, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
        IsTimeLimited = isTimeLimited;
    }
}
