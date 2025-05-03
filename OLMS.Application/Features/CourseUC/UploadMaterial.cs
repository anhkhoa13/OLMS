//using MediatR;
//using OLMS.Domain.Entities;
//using OLMS.Domain.Repositories;
//using OLMS.Domain.ExerciseAttempt;

//using static OLMS.Domain.ExerciseAttempt.ExerciseAttempt;
//using static OLMS.Domain.ExerciseAttempt.UserError;
//using static OLMS.Domain.ExerciseAttempt.CourseError;

//using Microsoft.AspNetCore.Http;

//namespace OLMS.Application.Features.CourseUC;
//public record UploadMaterialCommand(
//    Guid UserId,
//    Guid ForumId,
//    IFormFile File
//) : IRequest<ExerciseAttempt>;

//public class UploadMaterialCommandHandler : IRequestHandler<UploadMaterialCommand, ExerciseAttempt>
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly ICourseRepository _courseRepository;
//    private readonly IUserRepository _instuctorRepository;
//    private readonly IMaterialRepository _materialRepository;

//    public UploadMaterialCommandHandler(
//        IUnitOfWork unitOfWork,
//        ICourseRepository courseRepository,
//        IMaterialRepository materialRepository,
//        IUserRepository userRepository)
//    {
//        _unitOfWork = unitOfWork;
//        _courseRepository = courseRepository;
//        _instuctorRepository = userRepository;
//        _materialRepository = materialRepository;
//    }

//    public async Task<ExerciseAttempt> Handle(UploadMaterialCommand request, CancellationToken cancellationToken)
//    {
//        //if (!await _instuctorRepository.IsUserExist(request.UserId, cancellationToken)) return UserNotFound;

//        //var course = await _courseRepository.GetByIdAsync(request.ForumId);
//        //if (course == null) return CourseNotFound;

//        //var file = request.File;
//        //byte[] fileData;

//        //using (var memoryStream = new MemoryStream())
//        //{
//        //    await file.CopyToAsync(memoryStream, cancellationToken);
//        //    fileData = memoryStream.ToArray();
//        //}

//        //var material = new Material(Guid.NewGuid(), file.FileName, file.ContentType, file.Length, fileData, request.UserId);
//        //course.UploadMaterial(material);

//        //await _materialRepository.AddAsync(material, cancellationToken);
//        //_courseRepository.Update(course);
//        //await _unitOfWork.SaveChangesAsync(cancellationToken);

//        return Success();
//    }
//}