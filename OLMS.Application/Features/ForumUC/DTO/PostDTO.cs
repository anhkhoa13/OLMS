public class PostDto {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int VoteScore { get; set; }
    public Guid ForumId { get; set; }
    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    public List<VoteDto> Votes { get; set; } = new List<VoteDto>();
}