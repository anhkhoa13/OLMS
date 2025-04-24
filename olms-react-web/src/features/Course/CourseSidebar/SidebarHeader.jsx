import React, { useContext } from "react";
import { SidebarContext } from "./SidebarContext";

const SidebarHeader = ({ course }) => {
  const { expanded } = useContext(SidebarContext);

  return (
    <div className="p-4 border-b border-gray-200">
      {expanded ? (
        <>
          <h2 className="text-lg font-semibold text-gray-800 truncate">
            {course.title}
          </h2>
          <p className="text-sm text-gray-600 mt-1">{course.instructor}</p>
        </>
      ) : (
        <div className="w-8 h-8 rounded-full bg-purple-600 text-white flex items-center justify-center font-bold">
          {course.title.charAt(0)}
        </div>
      )}
    </div>
  );
};

export default SidebarHeader;
