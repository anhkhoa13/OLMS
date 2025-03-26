using Microsoft.EntityFrameworkCore;

using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Entities.QuizEntity;

namespace OLMS.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<UserBase> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }
    //public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<Material> Materials { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException("Database configuration is missing.");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        //// Định nghĩa Table-Per-Type (TPT)
        //modelBuilder.Entity<UserBase>().ToTable("User");
        //modelBuilder.Entity<Student>().ToTable("Student"); // Bảng Student mở rộng từ UserBase
        //modelBuilder.Entity<Instructor>().ToTable("Instructor"); // Bảng Instructor mở rộng từ UserBase

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
}

