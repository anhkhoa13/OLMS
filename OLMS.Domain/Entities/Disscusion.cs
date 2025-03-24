namespace OLMS.Domain.Entities
{
    public class Discussion
    {
        public string Id { get; set; }  // Format: CourseID-D1, CourseID-D2, etc.
        public string CourseId { get; set; }
        public string CreatorId { get; set; }
        public string Content { get; set; }
        public string? DocumentUrl { get; set; }
        public int Votes { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
