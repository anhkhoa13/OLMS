import { LessonIcon, QuizIcon, ExerciseIcon } from "../../../components/Icons";
import { Link } from "react-router-dom";

export function CourseContentItem({
  item,
  index,
  type,
  courseId,
  onEdit,
  isEditMode,
}) {
  let icon = null;
  let subtitle = null;

  const getLink = () => {
    if (item.type === "lesson") {
      return `/courses/${courseId}/lesson/${item.id}`;
    }
    if (item.type === "quiz") {
      var code = item.id.slice(0, 6);
      return `/courses/${courseId}/quiz/${code}`;
    }
    if (item.type === "exercise") {
      return `/courses/${courseId}/assignment/${item.id}`;
    }
    return `/course/${item.id}`;
  };

  if (type === "lesson") {
    icon = <LessonIcon />;
    subtitle = "Lesson";
  } else if (type === "quiz") {
    icon = <QuizIcon />;
    subtitle = "quiz";
  } else if (type === "exercise") {
    icon = <ExerciseIcon />;
    subtitle = "exercise";
  }

  return (
    <div className="flex items-center justify-between gap-4 p-3 mb-2 mr-4 bg-white rounded-lg shadow-sm border border-gray-100 hover:border-blue-200 hover:shadow transition-all duration-200">
      <Link to={getLink()} className="flex-1 flex items-center">
        {icon}
        <div className="ml-3">
          <div className="font-medium text-gray-800">
            {index}. {item.title}
          </div>
          <div className="text-sm text-gray-500">
            {subtitle}
            {type === "assignment" && item.dueDate && (
              <> • Due: {new Date(item.dueDate).toLocaleDateString()}</>
            )}
          </div>
        </div>
      </Link>

      {isEditMode && (
        <button
          onClick={() => onEdit(item)}
          className="bg-[#89b46c] hover:bg-[#6f8f54] text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200 cursor-pointer"
        >
          Edit
        </button>
      )}
    </div>
  );
}

export default CourseContentItem;
