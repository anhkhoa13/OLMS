using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;


namespace OLMS.Application.Feature.Enrollment
{
    public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        //private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollCourseCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ICourseRepository courseRepository)
            //IEnrollmentRepository enrollmentRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            //_enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
        }

        public async Task Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.StudentId);

            if (user is null)
            {
                throw new InvalidOperationException("User not found");
            }

            if (user is not Student student)
            {
                throw new InvalidOperationException("Not a student");
            }

            var course = await _courseRepository.GetByCodeAsync(request.CourseCode);

            if (course == null)
            {
                throw new KeyNotFoundException("Invalid course code.");
            }

            course.EnrollStudent(student);



            //var existingEnrollment = await _enrollmentRepository.GetByStudentAndCourseAsync(request.StudentId, course.Id);
            //if (existingEnrollment != null)
            //{
            //    throw new InvalidOperationException("Student is already enrolled in this course.");
            //}


            //var enrollment = new OLMS.Domain.Entities.Enrollment(request.StudentId, course.Id);
            //await _enrollmentRepository.AddAsync(enrollment);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            _courseRepository.Update(course);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //var enrollmentId = student.Id.ToString() + course.Id.ToString(); 

            //return enrollmentId;
        }
    }
}
