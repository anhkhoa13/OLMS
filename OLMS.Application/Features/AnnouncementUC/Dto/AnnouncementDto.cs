﻿public class AnnouncementDto {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CourseId { get; set; }
}