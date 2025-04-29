using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

public class Lesson : Entity {
    public string Title { get;  set; } = default!;
    public string Content { get;  set; } = default!;
    public string VideoUrl { get;  set; } = default!;
    public Guid SectionId { get; private set; }

    private readonly List<LessonAttachment> _lessonAttachment = [];
    public IReadOnlyCollection<LessonAttachment> LessonAttachments => _lessonAttachment;

    private Lesson() : base(){ } // Required for EF Core

    public Lesson(Guid id, string title, string content, string videoUrl, Guid sectionId) : base(id) {
        Title = title;
        Content = content;
        VideoUrl = videoUrl;
        SectionId = sectionId;
    }

    public static Lesson Create(string title, string content, string videoUrl, Guid sectionId) {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.", nameof(content));
        if (string.IsNullOrWhiteSpace(videoUrl))
            throw new ArgumentException("VideoUrl cannot be empty.", nameof(videoUrl));
        if (sectionId == Guid.Empty)
            throw new ArgumentException("SectionId must be a valid GUID.", nameof(sectionId));

        // Optionally, add more checks (e.g., valid URL format for videoUrl)
        if (!Uri.TryCreate(videoUrl, UriKind.Absolute, out var uriResult))
            throw new ArgumentException("VideoUrl must be a valid URL.", nameof(videoUrl));

        return new Lesson(Guid.NewGuid(), title, content, videoUrl, sectionId);
    }

    public void AddAttachment(LessonAttachment attachment) {
        if (attachment == null)
            throw new ArgumentNullException(nameof(attachment));

        _lessonAttachment.Add(attachment);
    }

    public void RemoveAttachment(LessonAttachment attachment) {
        if (attachment == null)
            throw new ArgumentNullException(nameof(attachment));

        _lessonAttachment.Remove(attachment);
    }

    public void ClearAttachments() {
        _lessonAttachment.Clear();
    }
}
