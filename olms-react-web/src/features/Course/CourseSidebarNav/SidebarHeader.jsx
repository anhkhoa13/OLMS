import React, { useContext } from "react";
import { SidebarContext } from "./SidebarContext";

const SidebarHeader = ({ course }) => {
  const { expanded } = useContext(SidebarContext);

  return (
    <div className="p-4 border-b border-gray-200">
      {expanded && (
        <>
          <h2 className="text-lg font-bold text-gray-800 truncate">
            {course.title}
          </h2>
          <p className="text-sm text-gray-600 mt-1">{course.instructor}</p>
        </>
      )}
    </div>
  );
};

export default SidebarHeader;
