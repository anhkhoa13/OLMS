// ContentItem: Renders a lesson, quiz, or exercise
import { LessonIcon, QuizIcon, ExerciseIcon } from "../../../components/Icons";

export function CourseContentItem({ item, index, type }) {
  let icon = null;
  let subtitle = null;

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
    <div
      className="flex items-center p-3 mb-2 mr-4 bg-white rounded-lg shadow-sm border border-gray-100 hover:border-blue-200 hover:shadow transition-all duration-200"
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
    </div>
  );
}

export default CourseContentItem;
