// components/CourseForum.jsx
import React, { useState, useEffect } from "react";
import DiscussionList from "./DiscussionList";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function Forum({ courseId, currentUser }) {
  const [discussions, setDiscussions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [content, setContent] = useState("");
  const [documentUrl, setDocumentUrl] = useState("");

  // Fetch existing discussions
  useEffect(() => {
    const fetchDiscussions = async () => {
      try {
        const response = await fetch(`${API_URL}/api/discussions/${courseId}`);
        if (!response.ok) throw new Error("Failed to fetch discussions");
        const data = await response.json();
        setDiscussions(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchDiscussions();
  }, [courseId]);

  // Handle new post submission
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`${API_URL}/api/discussions`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          courseId,
          creatorId: currentUser.id,
          content,
          documentUrl: documentUrl || null,
        }),
      });

      if (!response.ok) throw new Error("Failed to create post");

      const newDiscussion = await response.json();
      setDiscussions([...discussions, newDiscussion]);
      setShowForm(false);
      setContent("");
      setDocumentUrl("");
    } catch (err) {
      setError(err.message);
    }
  };

  if (loading) return <div className="p-4 text-gray-600">Loading forum...</div>;
  if (error) return <div className="p-4 text-red-600">Error: {error}</div>;

  return (
    <div>
      <h2 className="text-xl font-bold text-gray-800 mb-6">Course Forum</h2>
      <div className="bg-gray-50 rounded-lg p-6 border border-gray-200">
        <p className="text-gray-600 mb-6">
          Welcome to the course forum! Here you can discuss topics with your
          peers and instructors.
        </p>

        {/* Discussions List */}
        <DiscussionList discussions={discussions} />

        {/* New Post Form */}
        {showForm ? (
          <form
            onSubmit={handleSubmit}
            className="bg-white p-4 rounded-lg border border-gray-300"
          >
            <textarea
              className="w-full p-2 border rounded mb-3"
              placeholder="Write your post..."
              value={content}
              onChange={(e) => setContent(e.target.value)}
              required
            />
            <input
              type="url"
              className="w-full p-2 border rounded mb-3"
              placeholder="Document URL (optional)"
              value={documentUrl}
              onChange={(e) => setDocumentUrl(e.target.value)}
            />
            <div className="flex gap-2">
              <button
                type="submit"
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              >
                Post
              </button>
              <button
                type="button"
                onClick={() => setShowForm(false)}
                className="bg-gray-200 text-gray-700 px-4 py-2 rounded hover:bg-gray-300"
              >
                Cancel
              </button>
            </div>
          </form>
        ) : (
          <button
            onClick={() => setShowForm(true)}
            className="bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200 inline-flex items-center"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5 mr-2"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M12 6v6m0 0v6m0-6h6m-6 0H6"
              />
            </svg>
            Create New Post
          </button>
        )}
      </div>
    </div>
  );
}

export default Forum;
