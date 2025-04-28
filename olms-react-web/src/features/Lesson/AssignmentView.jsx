import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import downloadBase64File from "../../utils/ConvertToFile";
import FileUpload from "../../components/FileUpload"; // Adjust path as needed

const API_URL = import.meta.env.VITE_BACKEND_URL;

function formatDate(isoString) {
  const date = new Date(isoString);
  return date.toLocaleString(undefined, {
    year: "numeric",
    month: "short",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
}

function formatAssignmentType(type) {
  return type === "Exercise" ? "Practical Exercise" : "Quiz";
}

function AssignmentView() {
  const { assignmentId } = useParams();
  const [assignment, setAssignment] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchAssignment = async () => {
      try {
        const response = await fetch(
          `${API_URL}/api/assignment/${assignmentId}`
        );
        if (!response.ok)
          throw new Error(`HTTP error! status: ${response.status}`);
        const data = await response.json();
        setAssignment(data);
        setError(null);
      } catch (err) {
        setError(err.message);
        setAssignment(null);
      } finally {
        setLoading(false);
      }
    };
    if (assignmentId) fetchAssignment();
  }, [assignmentId]);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-green-600"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen flex items-center justify-center text-red-500">
        Error: {error}
      </div>
    );
  }

  if (!assignment) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        No assignment found
      </div>
    );
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-[#b9d6a142] py-8 px-2">
      <div className="w-full max-w-6xl bg-white rounded-2xl shadow-lg border border-gray-100 p-12 flex flex-col md:flex-row gap-12 min-h-[80vh]">
        {/* Assignment Details (Left) */}
        <div className="flex-1 flex flex-col justify-between">
          <section>
            <h2 className="text-2xl font-bold text-[#6f8f54] mb-6">
              Assignment Details
            </h2>
            <div className="mb-4">
              <label className="block text-gray-500 font-semibold mb-1">
                Title
              </label>
              <div className="text-2xl font-bold text-gray-800 mb-2">
                {assignment.title}
              </div>
            </div>
            <div className="mb-4">
              <label className="block text-gray-500 font-semibold mb-1">
                Description
              </label>
              <div className="text-gray-700">{assignment.description}</div>
            </div>
            <div className="mb-4 flex flex-wrap gap-4">
              <div>
                <label className="block text-gray-500 font-semibold mb-1">
                  Start Date
                </label>
                <div className="bg-green-50 border border-green-200 rounded-lg px-4 py-2 font-medium text-green-800">
                  {formatDate(assignment.startDate)}
                </div>
              </div>
              <div>
                <label className="block text-gray-500 font-semibold mb-1">
                  Due Date
                </label>
                <div className="bg-red-100 border-2 border-red-400 rounded-lg px-4 py-2 font-bold text-red-700 text-lg shadow">
                  {formatDate(assignment.dueDate)}
                </div>
              </div>
            </div>
            <div className="mb-4 grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label className="block text-gray-500 font-semibold mb-1">
                  Type
                </label>
                <div className="font-medium">
                  {formatAssignmentType(assignment.type)}
                </div>
              </div>
              <div>
                <label className="block text-gray-500 font-semibold mb-1">
                  Late Submission
                </label>
                <div className="font-medium">
                  {assignment.allowLateSubmission ? "Allowed" : "Not Allowed"}
                </div>
              </div>
              <div>
                <label className="block text-gray-500 font-semibold mb-1">
                  Attempts
                </label>
                <div className="font-medium">
                  {assignment.numberOfAttempts === 0
                    ? "Unlimited"
                    : assignment.numberOfAttempts}
                </div>
              </div>
            </div>
          </section>
          {/* Submission Box */}
          <section className="mt-8">
            <h2 className="text-2xl font-bold text-[#6f8f54] mb-4">
              Assignment Submission
            </h2>
            <FileUpload assignmentId={assignment.id} />
          </section>
        </div>

        {/* Attachments (Right) */}
        <div className="w-full md:w-96">
          <h2 className="text-2xl font-bold text-[#6f8f54] mb-4">
            Attachments
          </h2>
          {assignment.attachments.length === 0 ? (
            <div className="text-gray-500 italic">No attachments</div>
          ) : (
            <ul className="space-y-3">
              {assignment.attachments.map((file) => (
                <li
                  key={file.id}
                  className="flex items-center justify-between bg-gray-50 rounded-lg px-4 py-2 border border-gray-200"
                >
                  <div className="flex items-center gap-3">
                    <span className="inline-flex items-center justify-center w-8 h-8 bg-[#b9d6a1bb] text-[#6f8f54] rounded-full">
                      <svg
                        className="w-5 h-5"
                        fill="none"
                        stroke="currentColor"
                        strokeWidth={2}
                        viewBox="0 0 24 24"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          d="M12 4v16m8-8H4"
                        />
                      </svg>
                    </span>
                    <span className="font-medium text-gray-700">
                      {file.name}
                    </span>
                    <span className="text-xs text-gray-400">
                      {file.size ? `(${(file.size / 1024).toFixed(1)} KB)` : ""}
                    </span>
                  </div>
                  <a
                    href={`data:${file.type};base64,${file.data}`}
                    download={file.name}
                    className="text-[#6f8f54] hover:text-[#798b69bb] text-sm font-semibold"
                    onClick={(e) => {
                      e.preventDefault();
                      downloadBase64File(file.data, file.type, file.name);
                    }}
                  >
                    Download
                  </a>
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>
    </div>
  );
}

export default AssignmentView;
