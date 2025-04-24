// components/CourseNavBar.jsx
import React from "react";

function CourseNavBar({ activeTab, setActiveTab }) {
  return (
    <div className="flex border-b border-gray-200">
      <button
        className={`flex-1 py-4 px-6 text-center font-medium text-sm focus:outline-none transition-all duration-200 cursor-pointer ${
          activeTab === "content"
            ? "text-blue-600 border-b-2 border-blue-600 bg-blue-50/50"
            : "text-gray-500 hover:text-gray-700 hover:bg-gray-50"
        }`}
        onClick={() => setActiveTab("content")}
      >
        <div className="flex items-center justify-center">
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
              d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253"
            />
          </svg>
          Content
        </div>
      </button>
      <button
        className={`flex-1 py-4 px-6 text-center font-medium text-sm focus:outline-none transition-all duration-200 cursor-pointer ${
          activeTab === "forum"
            ? "text-blue-600 border-b-2 border-blue-600 bg-blue-50/50"
            : "text-gray-500 hover:text-gray-700 hover:bg-gray-50"
        }`}
        onClick={() => setActiveTab("forum")}
      >
        <div className="flex items-center justify-center">
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
              d="M17 8h2a2 2 0 012 2v6a2 2 0 01-2 2h-2v4l-4-4H9a1.994 1.994 0 01-1.414-.586m0 0L11 14h4a2 2 0 002-2V6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2v4l.586-.586z"
            />
          </svg>
          Forum
        </div>
      </button>
    </div>
  );
}

export default CourseNavBar;
