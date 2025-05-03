import React from "react";

const SidebarToggle = ({ onToggle, expanded }) => {
  return (
    <button
      className={`fixed top-1/2 transform -translate-y-1/2 z-50 w-8 h-8 rounded-full bg-[#b9d6a1] border border-gray-200 shadow-md flex items-center justify-center cursor-pointer focus:outline-none transition-all duration-300 ${
        expanded ? "right-[calc(18rem-1rem)]" : "right-[calc(0rem+0.5rem)]"
      }`}
      onClick={onToggle}
      aria-label={expanded ? "Collapse sidebar" : "Expand sidebar"}
    >
      {!expanded ? (
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="h-4 w-4 text-gray-600"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M15 19l-7-7 7-7"
          />
        </svg>
      ) : (
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="h-4 w-4 text-gray-600"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M9 5l7 7-7 7"
          />
        </svg>
      )}
    </button>
  );
};

export default SidebarToggle;
