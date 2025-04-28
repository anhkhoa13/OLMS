function Posts(forum, handleVote) {
  return (
    <div className="space-y-8">
      {forum.forum.posts.map((post) => (
        <div
          key={post.id}
          className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden"
        >
          <div className="p-6">
            <div className="flex gap-6">
              {/* Voting Controls */}
              <div className="flex flex-col items-center gap-3">
                <button
                  onClick={() => handleVote(post.id, "up")}
                  className="text-gray-400 hover:text-[#6f8f54] transition-colors p-2 rounded-full hover:bg-gray-100"
                >
                  <svg
                    className="w-6 h-6"
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M5 15l7-7 7 7"
                    />
                  </svg>
                </button>
                <span className="font-semibold text-[#6f8f54] text-lg">
                  {post.voteScore}
                </span>
                <button
                  onClick={() => handleVote(post.id, "down")}
                  className="text-gray-400 hover:text-red-500 transition-colors p-2 rounded-full hover:bg-gray-100"
                >
                  <svg
                    className="w-6 h-6"
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M19 9l-7 7-7-7"
                    />
                  </svg>
                </button>
              </div>

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
                        <div className="text-xs text-gray-500 font-medium">
                          {new Date(comment.createdAt).toLocaleDateString(
                            "en-US",
                            {
                              year: "numeric",
                              month: "short",
                              day: "numeric",
                            }
                          )}
                        </div>
                      </div>
                    ))}
                  </div>
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
