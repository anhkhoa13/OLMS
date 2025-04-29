import React, { useEffect, useState } from "react";
import Posts from "./Posts";
import PostForm from "./PostForm";

const API_URL = import.meta.env.VITE_BACKEND_URL;

const Forum = ({ courseId }) => {
  const [forum, setForum] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const token = localStorage.getItem("token");

  const [refresh, setRefresh] = useState(0);

  useEffect(() => {
    const fetchForum = async () => {
      try {
        const response = await fetch(
          `${API_URL}/api/forum/course/${courseId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        if (!response.ok) throw new Error("Failed to fetch forum");
        const data = await response.json();
        setForum(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchForum();
  }, [courseId, refresh]);

  // const handleVote = async (postId, voteType) => {
  //   try {
  //     const response = await fetch(`/api/posts/${postId}/vote`, {
  //       method: "POST",
  //       headers: { "Content-Type": "application/json" },
  //       body: JSON.stringify({ voteType }),
  //     });

  //     if (!response.ok) throw new Error("Vote failed");

  //     setForum((prev) => ({
  //       ...prev,
  //       posts: prev.posts.map((post) =>
  //         post.id === postId
  //           ? {
  //               ...post,
  //               voteScore:
  //                 voteType === "up" ? post.voteScore + 1 : post.voteScore - 1,
  //             }
  //           : post
  //       ),
  //     }));
  //   } catch (err) {
  //     console.error("Vote error:", err);
  //   }
  // };

  if (loading) return <div className="p-4 text-gray-500">Loading forum...</div>;
  if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

  return (
    <div className="min-h-screen bg-gray-50 py-8 px-4 sm:px-6 lg:px-8">
      <div className="max-w-4xl mx-auto">
        {/* Forum Header */}
        <div className="mb-10 p-6 bg-white rounded-xl shadow-sm border border-gray-200">
          <h1 className="text-4xl font-bold text-[black] tracking-tight">
            {forum.title}
          </h1>
        </div>

        {/* Posts List */}
        <Posts forum={forum} onRefresh={setRefresh} />

        {/* New Post Form */}
        <PostForm forum={forum} setForum={setForum} courseId={courseId} />
      </div>
    </div>
  );
};

export default Forum;
