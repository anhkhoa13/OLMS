import React from "react";
import CourseCard from "../components/CourseCard";

const courses = [
  {
    code: "ENG101",
    title: "English Speaking",
    description: "Master fluent English communication.",
    instructor: {
      id: "1a2b3c",
      name: "Steven James",
      avatar: "https://i.pravatar.cc/48?u=1",
    },
  },
  {
    code: "HIN201",
    title: "Hindi Speaking",
    description: "Speak Hindi confidently and clearly.",
    instructor: {
      id: "4d5e6f",
      name: "Michael Eller",
      avatar: "https://i.pravatar.cc/48?u=22",
    },
  },
  {
    code: "URD305",
    title: "Urdu Speaking",
    description: "Learn the grace and structure of Urdu.",
    instructor: {
      id: "7g8h9i",
      name: "Charles Ferrell",
      avatar: "",
    },
  },
  {
    code: "URD305",
    title: "Urdu Speaking",
    description: "Learn the grace and structure of Urdu.",
    instructor: {
      id: "7g8h9i",
      name: "Charles Ferrell",
      avatar: "",
    },
  },
];

function Courses({ isEnrolled, maxNoDisplay = null, title = "All Courses" }) {
  const displayedCourses =
    maxNoDisplay !== null ? courses.slice(0, maxNoDisplay) : courses;

  return (
    <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16">
      <h2 className="text-4xl font-extrabold text-left mb-12 text-black">
        {title}
      </h2>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10 bg-white p-8 rounded-2xl shadow-lg">
        {displayedCourses.map((course, index) => (
          <CourseCard
            key={index}
            course={course}
            className="w-full"
            isEnrolled={isEnrolled}
          />
        ))}
      </div>
    </div>
  );
}

export default Courses;
