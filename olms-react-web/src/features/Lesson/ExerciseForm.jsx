import { useState } from "react";
import FileUpload from "../../components/FileUpload";
import { useAuth } from "../../contexts/AuthContext";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function ExerciseForm({ sectionId, onClose, nextOrder, onSuccess }) {
  const { currentUser } = useAuth(); // Get instructor ID from context
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [startDate, setStartDate] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [allowLateSubmission, setAllowLateSubmission] = useState(true);
  const [numberOfAttempts, setNumberOfAttempts] = useState(1);
  const [attachments, setAttachments] = useState([]);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    const exerciseData = {
      title,
      description,
      startDate: new Date(startDate).toISOString(),
      dueDate: new Date(dueDate).toISOString(),
      allowLateSubmission,
      numberOfAttempts: Number(numberOfAttempts),
      sectionId,
      instructorId: currentUser.id, // Assuming user context contains instructor ID
      attachments,
      order: nextOrder,
    };

    try {
      const response = await fetch(`${API_URL}/api/assignment/create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(exerciseData),
      });

      if (!response.ok) throw new Error("Failed to create exercise");
      alert("Create assignment successfully!");
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
          <FileUpload onFilesChange={setAttachments} value={attachments} />
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
          {isSubmitting ? "Saving..." : "Save Exercise"}
        </button>
      </div>
    </form>
  );
}

export default ExerciseForm;
