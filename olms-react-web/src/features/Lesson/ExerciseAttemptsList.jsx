import React, { useEffect, useState } from "react";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function formatDateTime(dateStr) {
  const d = new Date(dateStr);
  return d.toLocaleString();
}

function downloadBase64File(base64, type, fileName) {
  const link = document.createElement("a");
  link.href = `data:application/octet-stream;base64,${base64}`;
  link.download = fileName;
  link.click();
}

export function ExerciseAttemptsList({ exerciseId }) {
  const [attempts, setAttempts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchAttempts() {
      setLoading(true);
      try {
        const res = await fetch(`${API_URL}/api/instructor/getExerciseList`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ exerciseId }),
        });
        const data = await res.json();
        setAttempts(data);
      } catch (e) {
        console.log(e);
        setAttempts([]);
      }
      setLoading(false);
    }
    if (exerciseId) fetchAttempts();
  }, [exerciseId]);

  if (loading) {
    return (
      <div className="flex justify-center items-center py-8">
        <span className="text-gray-500">Loading submissions...</span>
      </div>
    );
  }

  if (!attempts.length) {
    return (
      <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-6 text-center text-yellow-700 font-medium">
        No submissions yet for this exercise.
      </div>
    );
  }

  return (
    <div className="overflow-x-auto">
      <table className="min-w-full bg-white border border-gray-200 rounded-lg shadow-sm">
        <thead className="bg-[#f4f9f2]">
          <tr>
            <th className="px-4 py-3 text-left font-semibold text-[#6f8f54]">
              Student
            </th>
            <th className="px-4 py-3 text-left font-semibold text-[#6f8f54]">
              Score
            </th>
            <th className="px-4 py-3 text-left font-semibold text-[#6f8f54]">
              Submitted At
            </th>
            <th className="px-4 py-3 text-left font-semibold text-[#6f8f54]">
              Status
            </th>
            <th className="px-4 py-3 text-left font-semibold text-[#6f8f54]">
              Attachments
            </th>
          </tr>
        </thead>
        <tbody>
          {attempts.map((attempt, idx) => (
            <tr
              key={attempt.studentId}
              className={idx % 2 === 0 ? "bg-white" : "bg-gray-50"}
            >
              <td className="px-4 py-3 font-medium text-gray-800">
                {attempt.studentName}
              </td>
              <td className="px-4 py-3 text-gray-700">{attempt.score}</td>
              <td className="px-4 py-3 text-gray-700">
                {formatDateTime(attempt.submitAt)}
              </td>
              <td className="px-4 py-3">
                <span className="inline-block px-3 py-1 rounded-full text-xs font-semibold bg-blue-50 text-blue-700 border border-blue-200">
                  {attempt.status && Object.keys(attempt.status).length > 0
                    ? JSON.stringify(attempt.status)
                    : "Pending"}
                </span>
              </td>
              <td className="px-4 py-3">
                <div className="flex flex-wrap gap-2">
                  {attempt.attachments && attempt.attachments.length > 0 ? (
                    attempt.attachments.map((file) => (
                      <button
                        key={file.id}
                        className="flex items-center gap-1 px-3 py-1 bg-[#e6f2d7] hover:bg-[#b9d6a1] text-[#6f8f54] rounded-lg text-xs font-semibold shadow"
                        onClick={() =>
                          downloadBase64File(file.data, file.type, file.name)
                        }
                        type="button"
                      >
                        <svg
                          className="w-4 h-4 mr-1"
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
                        {file.name}
                      </button>
                    ))
                  ) : (
                    <span className="text-gray-400 italic">No files</span>
                  )}
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
