import React, { useEffect, useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
import axios from "axios";

function AnnouncementView({ courseId }) {
  const [announcements, setAnnouncements] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [creating, setCreating] = useState(false);
  const [createError, setCreateError] = useState(null);
  const { userRole } = useAuth();

  const API_URL = import.meta.env.VITE_BACKEND_URL;

  useEffect(() => {
    const fetchAnnouncements = async () => {
      try {
        const response = await fetch(
          `${API_URL}/api/announcement/course/${courseId}`
        );
        if (!response.ok) throw new Error("Failed to fetch announcements");
        const data = await response.json();
        setAnnouncements(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    if (courseId) fetchAnnouncements();
  }, [courseId]);

  const handleDeleteAnnouncement = async (announcementId) => {
    if (
      !window.confirm(
        "Are you sure you want to delete this announcement? This action cannot be undone."
      )
    )
      return;

    try {
      await axios.delete(
        `${API_URL}/api/announcement/delete/${announcementId}`
      );
      setAnnouncements((prev) => prev.filter((a) => a.id !== announcementId));
    } catch (err) {
      alert(
        err.response?.data?.error?.message || "Failed to delete announcement"
      );
    }
  };

  const handleCreateAnnouncement = async () => {
    setCreateError(null);
    if (!title.trim() || !content.trim()) {
      setCreateError("Title and content are required.");
      return;
    }

    setCreating(true);
    try {
      const response = await fetch(`${API_URL}/api/announcement`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          // Add authorization header if needed
          // "Authorization": `Bearer ${currentUser.token}`
        },
        body: JSON.stringify({
          title: title.trim(),
          content: content.trim(),
          courseId,
        }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(
          errorData.error?.message || "Failed to create announcement"
        );
      }

      // Refresh announcements after successful creation
      const refreshedResponse = await fetch(
        `${API_URL}/api/announcement/course/${courseId}`
      );
      const refreshedData = await refreshedResponse.json();
      setAnnouncements(refreshedData);

      // Clear form fields
      setTitle("");
      setContent("");
    } catch (err) {
      setCreateError(err.message);
    } finally {
      setCreating(false);
    }
  };

  return (
    <div className="p-4 max-w-3xl mx-auto">
      <h2 className="text-2xl font-bold mb-6 text-gray-800">
        Course Announcements
      </h2>

      {(userRole === "Instructor" || userRole === "Admin") && (
        <div className="mt-8 p-6 bg-white border border-gray-200 rounded-lg shadow-sm mb-4">
          <h3 className="text-lg font-semibold mb-4">
            Create New Announcement
          </h3>
          {createError && (
            <div className="mb-4 p-3 bg-red-50 text-red-700 rounded-md">
              {createError}
            </div>
          )}
          <input
            type="text"
            placeholder="Announcement Title"
            className="w-full mb-4 p-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            disabled={creating}
          />
          <textarea
            placeholder="Announcement Content"
            className="w-full mb-4 p-2 border border-gray-300 rounded-md h-32 resize-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            value={content}
            onChange={(e) => setContent(e.target.value)}
            disabled={creating}
          />
          <button
            className="cursor-pointer px-6 py-2 bg-[#6f8f54] text-white rounded-md hover:bg-[#4d633a] transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            onClick={handleCreateAnnouncement}
            disabled={creating}
          >
            {creating ? (
              <>
                <span className="animate-pulse">Creating...</span>
                <i className="ml-2 fas fa-spinner fa-spin"></i>
              </>
            ) : (
              "Publish Announcement"
            )}
          </button>
        </div>
      )}

      {loading ? (
        <div className="text-center py-4">
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600 mx-auto"></div>
          <p className="mt-2 text-gray-600">Loading announcements...</p>
        </div>
      ) : error ? (
        <div className="p-4 bg-red-50 rounded-lg">
          <p className="text-red-600">Error: {error}</p>
        </div>
      ) : announcements.length === 0 ? (
        <div className="p-4 bg-gray-50 rounded-lg">
          <p className="text-gray-500 text-center">
            No announcements available for this course.
          </p>
        </div>
      ) : (
        <div className="space-y-4">
          {announcements.map((announcement) => (
            <div
              key={announcement.id}
              className="bg-white rounded-lg shadow-sm border border-gray-100 p-4 hover:shadow-md transition-shadow relative"
            >
              {(userRole === "Instructor" || userRole === "Admin") && (
                <button
                  onClick={() => handleDeleteAnnouncement(announcement.id)}
                  className="absolute top-4 right-4 p-2 text-red-600 hover:bg-red-50 rounded-full transition-colors cursor-pointer"
                  title="Delete Announcement"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                    />
                  </svg>
                </button>
              )}

              <h3 className="text-lg font-semibold text-gray-800 mb-2 pr-8">
                {announcement.title}
              </h3>
              <div className="flex items-center justify-start text-sm">
                <span className="text-gray-400">
                  Posted:{" "}
                  {new Date(announcement.createdAt).toLocaleDateString(
                    "en-US",
                    {
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                      hour: "2-digit",
                      minute: "2-digit",
                    }
                  )}
                </span>
              </div>
              <p className="mt-3 text-gray-600 whitespace-pre-wrap">
                {announcement.content}
              </p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default AnnouncementView;
