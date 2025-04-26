//using MediatR;
//using OLMS.Domain.Entities;
//using OLMS.Domain.Repositories;
//using OLMS.Domain.Result;

//using static OLMS.Domain.Result.Result;
//using static OLMS.Domain.Result.UserError;
//using static OLMS.Domain.Result.CourseError;

//using Microsoft.AspNetCore.Http;

//namespace OLMS.Application.Features.CourseUC;
//public record UploadMaterialCommand(
//    Guid UserId,
//    Guid CourseId,
//    IFormFile File
//) : IRequest<Result>;

//public class UploadMaterialCommandHandler : IRequestHandler<UploadMaterialCommand, Result>
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

//    public async Task<Result> Handle(UploadMaterialCommand request, CancellationToken cancellationToken)
//    {
//        //if (!await _instuctorRepository.IsUserExist(request.UserId, cancellationToken)) return UserNotFound;

//        //var course = await _courseRepository.GetByIdAsync(request.CourseId);
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