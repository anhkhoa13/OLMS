using OLMS.Domain.Entities.CourseAggregate;

namespace OLMS.Domain.Entities;

public class MaterialCourse
{
    public Guid CourseId { get; set; }
    public Guid MaterialId { get; set; }
    public Material Material { get; set; } = null!;
    public Course Course { get; set; } = null!;

    public MaterialCourse(Guid courseId, Guid materialId) 
    {
        CourseId = courseId;
        MaterialId = materialId;
    }
}