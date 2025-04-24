function DiscussionList(discussions) {
  return (
    <div className="mb-6 space-y-4">
      {discussions.map((discussion) => (
        <div
          key={discussion.id}
          className="p-4 bg-white rounded-lg border border-gray-200"
        >
          <div className="flex items-center justify-between mb-2">
            <span className="font-medium text-gray-800">
              {discussion.creatorId}
            </span>
            <span className="text-sm text-gray-500">
              {new Date(discussion.createdAt).toLocaleDateString()}
            </span>
          </div>
          <p className="text-gray-700 mb-2">{discussion.content}</p>
          {discussion.documentUrl && (
            <a
              href={discussion.documentUrl}
              className="text-blue-600 hover:underline text-sm"
            >
              View attached document
            </a>
          )}
        </div>
      ))}
    </div>
  );
}

export default DiscussionList;
