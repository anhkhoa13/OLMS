import React, { useState } from "react";
import { SidebarContext } from "./SidebarContext";
import SidebarHeader from "./SidebarHeader";
import SidebarContent from "./SidebarContent";
import SidebarToggle from "./SidebarToggle";

const CourseSidebar = ({ course }) => {
  const [expanded, setExpanded] = useState(false);

  const toggleSidebar = () => {
    setExpanded((prev) => !prev);
  };

  return (
    <SidebarContext.Provider value={{ expanded }}>
      <div
        className={`fixed top-0 left-0 h-screen bg-gray-50 shadow-md transition-all duration-300 ease-in-out z-10 overflow-y-auto ${
          expanded ? "w-72" : "w-0"
        }`}
      >
        <SidebarHeader course={course} />
        <SidebarContent course={course} />
        <SidebarToggle onToggle={toggleSidebar} expanded={expanded} />
      </div>
    </SidebarContext.Provider>
  );
};

export default CourseSidebar;
