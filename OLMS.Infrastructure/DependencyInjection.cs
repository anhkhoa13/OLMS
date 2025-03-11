using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;
using OLMS.Infrastructure.Database;
using OLMS.Infrastructure.Database.Repositories;
//using OLMS.Infrastructure.Repositories;

namespace OLMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
        services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        //services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        return services;
    }
}
