import React, { useState, useEffect } from "react";
import FileUpload from "../../components/FileUpload";
import { useAuth } from "../../contexts/AuthContext";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function ExerciseForm({
  sectionId,
  onClose,
  nextOrder,
  onSuccess,
  isEditing,
  exerciseId,
}) {
  const { currentUser } = useAuth();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [startDate, setStartDate] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [allowLateSubmission, setAllowLateSubmission] = useState(true);
  const [numberOfAttempts, setNumberOfAttempts] = useState(1);
  const [attachments, setAttachments] = useState([]);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // Fetch assignment data when in edit mode
  useEffect(() => {
    if (isEditing && exerciseId) {
      const fetchAssignment = async () => {
        setLoading(true);
        try {
          const response = await fetch(
            `${API_URL}/api/assignment?assignmentId=${exerciseId}`
          );

          if (!response.ok) throw new Error("Failed to fetch assignment");
          const data = await response.json();

          setTitle(data.title);
          setDescription(data.description);
          setStartDate(data.startDate.slice(0, 16));
          setDueDate(data.dueDate.slice(0, 16));
          setAllowLateSubmission(data.allowLateSubmission);
          setNumberOfAttempts(data.numberOfAttempts);
          setAttachments(data.attachments || []);
        } catch (err) {
          setError(err.message);
        } finally {
          setLoading(false);
        }
      };
      fetchAssignment();
    }
  }, [isEditing, exerciseId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    const assignmentData = {
      ...(isEditing && { assignmentId: exerciseId }),
      title,
      description,
      startDate: new Date(startDate).toISOString(),
      dueDate: new Date(dueDate).toISOString(),
      allowLateSubmission,
      numberOfAttempts: Number(numberOfAttempts),
      sectionId,
      instructorId: currentUser.id,
      attachments,
      order: nextOrder,
    };

    try {
      const url = isEditing
        ? `${API_URL}/api/assignment/update`
        : `${API_URL}/api/assignment/create`;

      const method = isEditing ? "PUT" : "POST";

      const response = await fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(assignmentData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Operation failed");
      }

      alert(
        isEditing
          ? "Assignment updated successfully!"
          : "Assignment created successfully!"
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
    return <div className="p-4 text-center">Loading assignment data...</div>;
  if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

  return (
    <form className="space-y-6" onSubmit={handleSubmit}>
      <div className="grid grid-cols-1 gap-6 md:grid-cols-2">
        {/* Title */}
        <div className="md:col-span-2">
          <label className="block font-medium mb-1">Exercise Title *</label>
          <input
            required
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-[#6f8f54]"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Enter exercise title"
          />
        </div>

        {/* Description */}
        <div className="md:col-span-2">
          <label className="block font-medium mb-1">Description *</label>
          <textarea
            required
            className="w-full border rounded-lg px-4 py-2 h-32 focus:ring-2 focus:ring-[#6f8f54]"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Describe the exercise"
          />
        </div>

        {/* Dates */}
        <div>
          <label className="block font-medium mb-1">Start Date *</label>
          <input
            required
            type="datetime-local"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-[#6f8f54]"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
          />
        </div>

        <div>
          <label className="block font-medium mb-1">Due Date *</label>
          <input
            required
            type="datetime-local"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-[#6f8f54]"
            value={dueDate}
            onChange={(e) => setDueDate(e.target.value)}
          />
        </div>

        {/* Settings */}
        <div className="flex items-center space-x-3">
          <input
            type="checkbox"
            id="allowLate"
            className="w-5 h-5 text-[#6f8f54] rounded focus:ring-[#6f8f54]"
            checked={allowLateSubmission}
            onChange={(e) => setAllowLateSubmission(e.target.checked)}
          />
          <label htmlFor="allowLate" className="font-medium">
            Allow late submission
          </label>
        </div>

        <div>
          <label className="block font-medium mb-1">Attempts Allowed *</label>
          <input
            required
            type="number"
            min="1"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-[#6f8f54]"
            value={numberOfAttempts}
            onChange={(e) => setNumberOfAttempts(e.target.value)}
          />
        </div>

        {/* Attachments */}
        <div className="md:col-span-2">
          <label className="block font-medium mb-1">Attachments</label>
          <FileUpload
            onFilesChange={setAttachments}
            value={attachments}
            existingFiles={attachments}
          />
        </div>
      </div>

      {/* Form Actions */}
      <div className="flex justify-end gap-4">
        <button
          type="button"
          onClick={onClose}
          className="px-6 py-2 text-gray-600 hover:text-gray-800 font-medium"
          disabled={isSubmitting}
        >
          Cancel
        </button>
        <button
          type="submit"
          className="bg-[#6f8f54] text-white px-6 py-2 rounded-lg font-medium hover:bg-[#5e7d4a] transition-colors disabled:opacity-50"
          disabled={isSubmitting}
        >
          {isSubmitting
            ? "Saving..."
            : isEditing
            ? "Update Exercise"
            : "Save Exercise"}
        </button>
      </div>
    </form>
  );
}

export default ExerciseForm;
