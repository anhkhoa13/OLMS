// components/CourseContent.jsx
import React from "react";

// ContentItem component for rendering different types of content
function ContentItem({ item }) {
  switch (item.type) {
    case "lesson":
      return (
        <div
          className="flex items-center p-3 mb-2 bg-white rounded-lg shadow-sm border border-gray-100 hover:border-blue-200 hover:shadow transition-all duration-200"
          key={item.title}
        >
          <div className="bg-blue-100 text-blue-600 p-2 rounded-full mr-3">
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
                d="M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z"
              />
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
              />
            </svg>
          </div>
          <div>
            <div className="font-medium text-gray-800">{item.title}</div>
            <div className="text-sm text-gray-500">Video • {item.duration}</div>
          </div>
        </div>
      );
    case "quiz":
      return (
        <div
          className="flex items-center p-3 mb-2 bg-white rounded-lg shadow-sm border border-gray-100 hover:border-green-200 hover:shadow transition-all duration-200"
          key={item.title}
        >
          <div className="bg-green-100 text-green-600 p-2 rounded-full mr-3">
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
                d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
              />
            </svg>
          </div>
          <div>
            <div className="font-medium text-gray-800">{item.title}</div>
            <div className="text-sm text-gray-500">
              {item.questions} questions
            </div>
          </div>
        </div>
      );
    case "announcement":
      return (
        <div
          className="flex items-center p-3 mb-2 bg-white rounded-lg shadow-sm border border-gray-100 hover:border-red-200 hover:shadow transition-all duration-200"
          key={item.title}
        >
          <div className="bg-red-100 text-red-600 p-2 rounded-full mr-3">
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
                d="M11 5.882V19.24a1.76 1.76 0 01-3.417.592l-2.147-6.15M18 13a3 3 0 100-6M5.436 13.683A4.001 4.001 0 017 6h1.832c4.1 0 7.625-1.234 9.168-3v14c-1.543-1.766-5.067-3-9.168-3H7a3.988 3.988 0 01-1.564-.317z"
              />
            </svg>
          </div>
          <div>
            <div className="font-medium text-gray-800">{item.title}</div>
            <div className="text-sm text-gray-500">Posted: {item.date}</div>
          </div>
        </div>
      );
    default:
      return null;
  }
}

// CourseSection component for rendering a collapsible section
function CourseSection({ section, index, activeSection, setActiveSection }) {
  return (
    <div
      className={`border rounded-lg overflow-hidden transition-all duration-200 ${
        activeSection === index
          ? "border-blue-200 shadow-md"
          : "border-gray-200 hover:border-blue-100 hover:shadow-sm"
      }`}
    >
      <button
        className="w-full flex justify-between items-center p-4 bg-gray-50 focus:outline-none"
        onClick={() => setActiveSection(activeSection === index ? null : index)}
      >
        <h3 className="font-semibold text-gray-800 text-left flex items-center">
          <span className="bg-blue-100 text-blue-600 rounded-full w-6 h-6 inline-flex items-center justify-center mr-2 text-sm">
            {index + 1}
          </span>
          {section.title}
        </h3>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className={`h-5 w-5 text-gray-500 transition-transform duration-200 ${
            activeSection === index ? "transform rotate-180" : ""
          }`}
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M19 9l-7 7-7-7"
          />
        </svg>
      </button>

      <div
        className={`overflow-hidden transition-all duration-300 ${
          activeSection === index ? "max-h-screen p-4" : "max-h-0"
        }`}
      >
        {section.items.map((item, itemIndex) => (
          <ContentItem key={`${item.type}-${itemIndex}`} item={item} />
        ))}
      </div>
    </div>
  );
}

function CourseContent({ courseSections, activeSection, setActiveSection }) {
  return (
    <div>
      <h2 className="text-xl font-bold text-gray-800 mb-6">Course Content</h2>
      <div className="space-y-4">
        {courseSections.map((section, index) => (
          <CourseSection
            key={index}
            section={section}
            index={index}
            activeSection={activeSection}
            setActiveSection={setActiveSection}
          />
        ))}
      </div>
    </div>
  );
}

export default CourseContent;
