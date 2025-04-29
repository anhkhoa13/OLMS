using Microsoft.EntityFrameworkCore;

using OLMS.Domain.Entities;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.ProgressAggregate;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;

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
    public DbSet<Progress> Progresses { get; set; }

    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<StudentResponse> StudentAnswers { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<SectionItem> SectionItems { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    public DbSet<Forum> Forums { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Announcement> Announcements { get; set; }


    public DbSet<QuizCourse> QuizCourses { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException("Database configuration is missing.");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
}

