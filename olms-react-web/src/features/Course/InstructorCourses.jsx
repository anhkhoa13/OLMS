import React from "react";

const InstructorCourses = () => {
  const courses = [
    {
      code: "279800",
      title: "Introduction to C++",
      description: "A beginner-friendly course on C# fundamentals.",
    },
    {
      code: "AF1044",
      title: "Introduction to C++",
      description: "A beginner-friendly course on C# fundamentals.",
    },
    {
      code: "EA6698",
      title: "Introduction to C++",
      description: "A beginner-friendly course on C# fundamentals.",
    },
  ];

  return (
    <div className="max-w-md mx-auto bg-white rounded-xl shadow-md overflow-hidden md:max-w-2xl">
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">Instructor Courses</h1>
        <ul className="space-y-4">
          {courses.map((course) => (
            <li
              key={course.code}
              className="border border-gray-200 rounded-lg p-4 hover:shadow-lg transition-shadow duration-300"
            >
              <h2 className="text-xl font-bold text-gray-900">
                {course.title}
              </h2>
              <p className="text-gray-700">{course.description}</p>
              <p className="text-sm text-gray-500 mt-2">
                Course Code: {course.code}
              </p>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default InstructorCourses;
