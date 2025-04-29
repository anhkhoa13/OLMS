import { useState, useEffect } from "react";
import FileUpload from "../../components/FileUpload";

const API_URL = import.meta.env.VITE_BACKEND_URL;

export default function LessonForm({
  sectionId,
  onClose,
  nextOrder,
  onSuccess,
  isEditing,
  lessonId,
}) {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [videoUrl, setVideoUrl] = useState("");
  const [attachments, setAttachments] = useState([]);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // Fetch lesson data when in edit mode
  useEffect(() => {
    if (isEditing && lessonId) {
      const fetchLesson = async () => {
        setLoading(true);
        try {
          const response = await fetch(
            `${API_URL}/api/lesson?lessonId=${lessonId}`
          );
          if (!response.ok) throw new Error("Failed to fetch lesson");
          const data = await response.json();

          setTitle(data.title);
          setContent(data.content);
          setVideoUrl(data.videoUrl);
          setAttachments(data.attachments || []);
        } catch (err) {
          setError(err.message);
        } finally {
          setLoading(false);
        }
      };
      fetchLesson();
    }
  }, [isEditing, lessonId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    const lessonData = {
      ...(isEditing && { lessonId: lessonId }),
      title,
      content,
      videoUrl,
      sectionId,
      attachments,
      order: nextOrder,
    };

    try {
      const url = isEditing
        ? `${API_URL}/api/lesson/update`
        : `${API_URL}/api/lesson/create`;

      const method = isEditing ? "PUT" : "POST";

      const response = await fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(lessonData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Operation failed");
      }

      alert(
        isEditing
          ? "Lesson updated successfully!"
          : "Lesson created successfully!"
      );
      if (onSuccess) onSuccess();
      onClose();
    } catch (error) {
      console.error("Submission error:", error);
      alert(error.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  if (loading)
    return <div className="p-4 text-center">Loading lesson data...</div>;
  if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      {/* Title field */}
      <div>
        <label className="block font-medium mb-1">Lesson Title *</label>
        <input
          required
          className="w-full border rounded px-3 py-2"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Enter lesson title"
        />
      </div>

      {/* Content field */}
      <div>
        <label className="block font-medium mb-1">Content *</label>
        <textarea
          required
          className="w-full border rounded px-3 py-2 h-32"
          value={content}
          onChange={(e) => setContent(e.target.value)}
          placeholder="Enter lesson content"
        />
      </div>

      {/* Video URL field */}
      <div>
        <label className="block font-medium mb-1">Video URL</label>
        <input
          type="url"
          className="w-full border rounded px-3 py-2"
          value={videoUrl}
          onChange={(e) => setVideoUrl(e.target.value)}
          placeholder="https://example.com/video"
        />
      </div>

      {/* Attachments */}
      <div>
        <label className="block font-medium mb-1">Attachments</label>
        <FileUpload
          onFilesChange={setAttachments}
          value={attachments}
          existingFiles={attachments}
        />
      </div>

      {/* Submit button */}
      <div className="flex justify-end gap-3">
        <button
          type="button"
          onClick={onClose}
          className="px-4 py-2 text-gray-600 hover:text-gray-800"
          disabled={isSubmitting}
        >
          Cancel
        </button>
        <button
          type="submit"
          className="bg-[#6f8f54] text-white px-4 py-2 rounded hover:bg-[#5e7d4a] disabled:opacity-50"
          disabled={isSubmitting}
        >
          {isSubmitting
            ? "Saving..."
            : isEditing
            ? "Update Lesson"
            : "Save Lesson"}
        </button>
      </div>
    </form>
  );
}
