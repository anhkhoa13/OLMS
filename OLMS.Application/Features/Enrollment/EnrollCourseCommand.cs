using MediatR;
using System;

namespace OLMS.Application.Feature.Enrollment
{
    public record EnrollCourseCommand(Guid StudentId, string CourseCode) : IRequest;
}
