import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { SidebarContext } from "./SidebarContext";

const SidebarItem = ({ item, sectionId }) => {
  const { expanded } = useContext(SidebarContext);

  // Determine icon based on item type
  const getIcon = (type) => {
    switch (type) {
      case "video":
        return "🎬";
      case "quiz":
        return "📝";
      case "assignment":
        return "📋";
      default:
        return "📄";
    }
  };

  return (
    <Link
      to={`/course/${sectionId}/${item.id}`}
      className={`flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200 transition-colors duration-200 relative ${item.type}`}
    >
      <div className="text-base">{getIcon(item.type)}</div>

      {expanded ? (
        <div className="ml-3 flex justify-between items-center w-full">
          <span className="text-sm truncate">{item.title}</span>
          {item.duration && (
            <span className="text-xs text-gray-500 ml-2">{item.duration}</span>
          )}
        </div>
      ) : (
        <div className="absolute left-16 ml-2 bg-gray-800 text-white text-xs py-1 px-2 rounded opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-opacity duration-200 whitespace-nowrap">
          <span>{item.title}</span>
          {item.duration && <span className="ml-2">({item.duration})</span>}
        </div>
      )}
    </Link>
  );
};

export default SidebarItem;
