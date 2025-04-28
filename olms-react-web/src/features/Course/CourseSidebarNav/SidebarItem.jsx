import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { SidebarContext } from "./SidebarContext";
import { LessonIcon, QuizIcon, ExerciseIcon } from "../../../components/Icons";

const SidebarItem = ({ item, index, courseId }) => {
  const { expanded } = useContext(SidebarContext);

  // Determine icon based on item type and assignmentType
  const getIcon = () => {
    if (item.type === "lesson") return <LessonIcon />;
    if (item.type === "quiz") return <QuizIcon />;
    if (item.type === "exercise") return <ExerciseIcon />;
    return null;
  };
  // Determine link based on type
  const getLink = () => {
    if (item.type === "lesson") {
      return `lesson/${item.id}`; // Use path parameter instead of query
    }
    if (item.type === "quiz") {
      var code = item.id.slice(0, 6);
      return `quiz/${code}`;
    }
    if (item.type === "exercise") {
      return `assignment/${item.id}`;
    }
    return `/course/${courseId}/${item.id}`;
  };

  return (
    <Link
      to={getLink()}
      className={`flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200 transition-colors duration-200 relative ${item.type}`}
    >
      <div className="text-base">{getIcon()}</div>

      {expanded ? (
        <div className="ml-3 flex justify-between items-center w-full">
          <span className="text-sm truncate">
            {index}. {item.title}
          </span>
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
