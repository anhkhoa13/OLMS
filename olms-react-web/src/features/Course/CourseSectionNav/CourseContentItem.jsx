// ContentItem: Renders a lesson, quiz, or exercise
import { LessonIcon, QuizIcon, ExerciseIcon } from "../../../components/Icons";
import { Link } from "react-router-dom";

export function CourseContentItem({ item, index, type, courseId }) {
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
    <Link
      to={getLink()}
      className="flex items-center cursor-pointer p-3 mb-2 mr-4 bg-white rounded-lg shadow-sm border border-gray-100 hover:border-blue-200 hover:shadow transition-all duration-200"
      key={item.id}
    >
      {icon}
      <div>
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
  );
}

export default CourseContentItem;
