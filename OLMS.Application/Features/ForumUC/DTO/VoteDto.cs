using OLMS.Domain.Entities.ForumAggregate.PostAggregate;

public class VoteDto {
    public Guid Id { get; set; }
    public VoteType Type { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}