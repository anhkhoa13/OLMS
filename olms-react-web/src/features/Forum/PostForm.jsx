import { useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
const API_URL = import.meta.env.VITE_BACKEND_URL;

function PostForm({ forum, setForum, courseId }) {
  const [title, setTitle] = useState("");
  const [body, setBody] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [formError, setFormError] = useState("");
  const { currentUser } = useAuth();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setFormError("");

    if (!title.trim() || !body.trim()) {
      setFormError("Please fill in both title and content");
      return;
    }

    setIsSubmitting(true);
    try {
      const response = await fetch(`${API_URL}/api/forum/post/create`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          title,
          body,
          forumId: forum.id,
          userId: currentUser.id,
        }),
      });

      if (!response.ok) throw new Error("Failed to create post");
      alert("Create post successfully");
      //Refresh forum data
      const updatedForum = await fetch(
        `${API_URL}/api/forum/course/${courseId}`
      ).then((res) => res.json());
      setForum(updatedForum);

      // Clear form
      setTitle("");
      setBody("");
    } catch (err) {
      setFormError(err.message);
    } finally {
      setIsSubmitting(false);
    }
  };
  return (
    <div className="mt-12 bg-white rounded-xl shadow-sm border border-gray-200 p-6">
      <h2 className="text-2xl font-semibold text-[#6f8f54] mb-6">
        Create New Post
      </h2>

      {formError && (
        <div className="mb-4 p-3 bg-red-50 text-red-700 rounded-lg">
          {formError}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div className="mb-4">
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Post Title"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#6f8f54] focus:border-[#6f8f54]"
            disabled={isSubmitting}
          />
        </div>

        <div className="mb-6">
          <textarea
            value={body}
            onChange={(e) => setBody(e.target.value)}
            placeholder="Write your post content..."
            rows="4"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#6f8f54] focus:border-[#6f8f54]"
            disabled={isSubmitting}
          ></textarea>
        </div>

        <button
          type="submit"
          disabled={isSubmitting}
          className="bg-[#6f8f54] cursor-pointer text-white px-6 py-3 rounded-lg font-medium hover:bg-[#5e7d4a] transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
        >
          {isSubmitting ? "Posting..." : "Create Post"}
        </button>
      </form>
    </div>
  );
}

export default PostForm;
