// components/CourseHeader.jsx
import React from "react";

function CourseHeader({ bgImage, courseData, code }) {
  return (
    <div
      className="relative bg-cover bg-center h-64 md:h-80 flex items-center"
      style={{ backgroundImage: `url(${bgImage})` }}
    >
      <div className="absolute inset-0 bg-gradient-to-r from-lime-900/70 to-indigo-900/50"></div>
      <div className="relative z-10 container mx-auto px-4 md:px-6">
        <div className="bg-white/90 backdrop-blur-sm rounded-xl p-6 max-w-2xl shadow-lg border border-gray-100">
          <h1 className="text-2xl md:text-3xl font-bold text-gray-900 mb-2">
            {courseData?.title || `Course: ${code}`}
          </h1>
          <p className="mb-2 text-gray-700">
            <span className="font-semibold">Code:</span>{" "}
            {courseData?.code || code}
          </p>
          <p className="mb-2 text-gray-700">
            <span className="font-semibold">Description:</span>{" "}
            {courseData?.description || "No description available"}
          </p>
          {courseData?.instructor && (
            <p className="text-gray-700">
              <span className="font-semibold">Instructor:</span>{" "}
              {courseData.instructor.name}
            </p>
          )}
        </div>
      </div>
    </div>
  );
}

export default CourseHeader;
