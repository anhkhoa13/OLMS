import React from "react";
import Avatar from "./Avatar";

const CourseCard = ({ course, isEnrolled }) => {
  const { title, description, instructor } = course;

  return (
    <div className="w-full bg-gradient-to-br from-blue-50 to-purple-50 p-6 rounded-xl border border-gray-200 hover:shadow-md transition-all duration-300 hover:-translate-y-1">
      {/* // <div className="bg-white text-black rounded-2xl shadow-md hover:shadow-xl transition-all duration-300 flex flex-col justify-between"> */}
      <div className="p-6">
        <h3 className="text-2xl font-bold mb-2">{title}</h3>
        <p className="text-sm text-gray-600 mb-4">{description}</p>
        <div className="flex items-center gap-4 mt-auto">
          {/* <img
            src={instructor.avatar || "https://via.placeholder.com/48"}
            alt={instructor.name}
            className="w-12 h-12 rounded-full object-cover border"
          /> */}
          <Avatar name={instructor.name} image={instructor.avatar} />
          <div>
            <p className="font-semibold">{instructor.name}</p>
            <p className="text-xs text-gray-500">Instructor</p>
          </div>
        </div>
      </div>
      <div className="bg-lime-400 hover:bg-lime-500 text-black text-center py-3 font-semibold text-lg rounded-b-2xl">
        {isEnrolled ? "View" : "Enroll Now"}
      </div>
    </div>
  );
};

export default CourseCard;
