import { useState } from "react";
import axios from "axios";
import { useAuth } from "../../contexts/AuthContext";
const API_URL = import.meta.env.VITE_BACKEND_URL;

function Posts({ forum, onRefresh }) {
  const [newComment, setNewComment] = useState({}); // { [postId]: commentText }
  const [submitting, setSubmitting] = useState(false);
  const [deletingPostId, setDeletingPostId] = useState(null);

  const { currentUser, userRole } = useAuth();

  const handleAddComment = async (postId) => {
    setSubmitting(true);
    try {
      await axios.post(`${API_URL}/api/post/addcomment`, {
        postId: postId,
        userId: currentUser.id,
        content: newComment[postId],
      });

      setNewComment((prev) => ({ ...prev, [postId]: "" }));
      onRefresh((prev) => prev + 1);
      console.log("Comment added successfully");
    } catch (error) {
      const errorMsg = error.response?.data?.message || error.message;
      console.error("Comment submission error:", errorMsg);
    } finally {
      setSubmitting(false);
    }
  };

  const handleDeletePost = async (postId) => {
    if (!window.confirm("Are you sure you want to delete this post?")) return;
    setDeletingPostId(postId);
    try {
      await axios.delete(`${API_URL}/api/post/delete/${postId}`);
      onRefresh((prev) => prev + 1);
    } catch (error) {
      alert(error.response?.data?.message || "Failed to delete post");
    } finally {
      setDeletingPostId(null);
    }
  };

  return (
    <div className="space-y-8">
      {forum.posts.map((post) => (
        <div
          key={post.id}
          className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden relative"
        >
          {/* Delete Button (show for post author or instructor/admin) */}
          {(currentUser?.id === post.userId ||
            userRole === "Instructor" ||
            userRole === "Admin") && (
            <button
              onClick={() => handleDeletePost(post.id)}
              className="absolute top-4 right-4 p-2 text-red-600 hover:bg-red-50 rounded-full transition-colors cursor-pointer"
              title="Delete Post"
              disabled={deletingPostId === post.id}
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

          <div className="p-6">
            <div className="flex gap-6">
              {/* Voting Controls */}

              {/* Post Content */}
              <div className="flex-1">
                <h2 className="text-2xl font-semibold text-gray-800 mb-3">
                  {post.title}
                </h2>
                <p className="text-gray-600 leading-relaxed mb-6">
                  {post.body}
                </p>

                {/* Comments Section */}
                <div className="border-t border-gray-100 pt-6">
                  <h3 className="text-sm font-semibold text-[#6f8f54] uppercase tracking-wide mb-4">
                    {post.comments.length} Comments
                  </h3>
                  <div className="space-y-4">
                    {post.comments.map((comment) => (
                      <div
                        key={comment.id}
                        className="pl-4 border-l-4 border-[#6f8f54] bg-gray-50 rounded-r-lg p-4"
                      >
                        <p className="text-gray-700 mb-2">{comment.content}</p>
                      </div>
                    ))}
                  </div>
                </div>

                <div className="mt-6 pl-4 border-l-4 border-[#6f8f54]">
                  <form
                    onSubmit={(e) => {
                      e.preventDefault();
                      handleAddComment(post.id);
                    }}
                  >
                    <textarea
                      value={newComment[post.id] || ""}
                      onChange={(e) =>
                        setNewComment((prev) => ({
                          ...prev,
                          [post.id]: e.target.value,
                        }))
                      }
                      placeholder="Write a comment..."
                      className="w-full p-3 text-sm border border-gray-200 rounded-lg focus:ring-2 focus:ring-[#6f8f54] focus:border-[#6f8f54]"
                      rows="3"
                    />
                    <div className="mt-3 flex justify-end">
                      <button
                        type="submit"
                        disabled={submitting || !newComment[post.id]?.trim()}
                        className="px-5 py-2 text-sm font-medium bg-[#6f8f54] text-white rounded-lg hover:bg-[#5c7a45] disabled:opacity-50 disabled:cursor-not-allowed"
                      >
                        {submitting ? "Posting..." : "Post Comment"}
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}

export default Posts;
