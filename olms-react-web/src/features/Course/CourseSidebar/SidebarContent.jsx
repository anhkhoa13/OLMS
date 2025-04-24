import React, { useContext } from "react";
import SidebarItem from "./SidebarItem";
import { SidebarContext } from "./SidebarContext";

const SidebarContent = ({ course }) => {
  const { expanded } = useContext(SidebarContext);

  if (!expanded) return null;

  return (
    <div className="py-2">
      <div className="sidebar-sections">
        {course.sections.map((section, index) => (
          <div key={section.id} className="mb-4">
            <h3 className="px-4 py-2 text-sm font-semibold text-gray-700">
              Section {index + 1}: {section.title}
            </h3>
            <div className="section-items">
              {section.items.map((item) => (
                <SidebarItem key={item.id} item={item} sectionId={section.id} />
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default SidebarContent;
