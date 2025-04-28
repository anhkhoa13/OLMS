public class ForumDto {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid CourseId { get; set; }
    public List<PostDto> Posts { get; set; } = new List<PostDto>();
}