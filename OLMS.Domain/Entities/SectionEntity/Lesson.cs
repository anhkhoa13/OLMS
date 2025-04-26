using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

public class Lesson : Entity {
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string VideoUrl { get; private set; }
    public Guid SectionId { get; private set; }

    private readonly List<Attachment> _attachments = [];
    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

    private Lesson() { } // Required for EF Core

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

    // Methods to manage attachments
    public void AddAttachment(Attachment attachment) {
        if (attachment == null)
            throw new ArgumentNullException(nameof(attachment));

        _attachments.Add(attachment);
    }

    public void RemoveAttachment(Attachment attachment) {
        if (attachment == null)
            throw new ArgumentNullException(nameof(attachment));

        _attachments.Remove(attachment);
    }

    public void ClearAttachments() {
        _attachments.Clear();
    }
}
