using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;


namespace OLMS.Application.Feature.Enrollment
{
    public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollCourseCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
        }

        public async Task<Guid> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {

            var student = await _userRepository.GetByIdAsync(request.StudentId);
            if (student == null || student.Role != Role.Student)
            {
                throw new InvalidOperationException("User is not a valid student.");
            }


            var course = (await _courseRepository.GetAllAsync())
                         .FirstOrDefault(c => c.Code.Value == request.CourseCode);

            if (course == null)
            {
                throw new KeyNotFoundException("Invalid course code.");
            }


            var existingEnrollment = await _enrollmentRepository.GetByStudentAndCourseAsync(request.StudentId, course.Id);
            if (existingEnrollment != null)
            {
                throw new InvalidOperationException("Student is already enrolled in this course.");
            }


            var enrollment = new OLMS.Domain.Entities.Enrollment(request.StudentId, course.Id);
            await _enrollmentRepository.AddAsync(enrollment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return enrollment.Id;
        }
    }
}
