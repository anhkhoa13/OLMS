using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLMS.Domain.Repositories;
using OLMS.Infrastructure.Database;
using OLMS.Infrastructure.Database.Repositories;


namespace OLMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TestConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStudentRepository, StudentReposiroty>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IProgressRepository, ProgressRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IForumRepository, ForumRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();


        services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
        services.AddScoped<ISectionItemRepository, SectionItemRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        //services.AddScoped<IMaterialRepository, MaterialRepository>();

        //services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        return services;
    }
}
