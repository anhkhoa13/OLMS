import { useState } from "react";
import FileUpload from "../../components/FileUpload";

const API_URL = import.meta.env.VITE_BACKEND_URL;

export default function LessonForm({
  sectionId,
  onClose,
  nextOrder,
  onSuccess,
}) {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [videoUrl, setVideoUrl] = useState("");
  const [attachments, setAttachments] = useState([]);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    const lessonData = {
      title,
      content,
      videoUrl,
      sectionId,
      attachments,
      order: nextOrder,
    };

    console.log(lessonData);

    try {
      const response = await fetch(`${API_URL}/api/lesson/create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(lessonData),
      });

      if (!response.ok) throw new Error("Failed to create lesson");
      alert("Add lesson successfully");
      if (onSuccess) onSuccess(); // Trigger refresh
      onClose();
    } catch (error) {
      console.error("Submission error:", error);
      alert(error.message);
    } finally {
      setIsSubmitting(false);
    }
  };

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
        <FileUpload onFilesChange={setAttachments} value={attachments} />
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
          {isSubmitting ? "Saving..." : "Save Lesson"}
        </button>
      </div>
    </form>
  );
}
