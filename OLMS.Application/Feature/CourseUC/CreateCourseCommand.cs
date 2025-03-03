using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OLMS.Domain.Entities;

namespace OLMS.Application.Feature.CourseUC;

public record CreateCourseCommand(Guid Id,
                                string Title,
                                string Description,
                                Guid InstructorId) : IRequest<Guid>
{
}
