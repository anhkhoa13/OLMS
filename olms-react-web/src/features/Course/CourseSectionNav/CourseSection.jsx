import { useNavigate } from "react-router-dom";
import { CourseContentItem } from "./CourseContentItem";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function CourseSection({
  section,
  index,
  activeSection,
  setActiveSection,
  isEditMode,
  courseId,
  dragHandleProps,
  onRefresh,
}) {
  const navigate = useNavigate();

  function handleEditSection() {
    navigate("/editSection", {
      state: { sectionId: section.id, courseId: courseId },
    });
  }

  async function handleDeleteSection() {
    try {
      const response = await fetch(
        `${API_URL}/api/section/delete/${section.id}`,
        {
          method: "DELETE",
        }
      );
      if (response.ok) {
        onRefresh((prev) => !prev);
        alert("Section deleted successfully");
        // Optionally, refresh the list or update state here
      } else {
        alert("Failed to delete section");
      }
    } catch (error) {
      console.error("Error deleting section:", error);
    }
  }

  const lessonsMap = Object.fromEntries(
    (section.lessons || []).map((l) => [l.id.toLowerCase(), l])
  );
  // console.log("Lesson map = ", lessonsMap);
  const assignmentsMap = Object.fromEntries(
    (section.assignments || []).map((a) => [a.id.toLowerCase(), a])
  );
  // console.log("assignmentsMap = ", assignmentsMap);

  // Debug: Log original section items
  // console.log("Original sectionItems:", section.sectionItems);

  const orderedItems = (section.sectionItems || [])
    .sort((a, b) => a.order - b.order)
    .map((sectionItem) => {
      const id = sectionItem.itemId.toLowerCase();
      let item = null;

      // Debug: Log current processing
      // console.log(`Processing sectionItem:`, sectionItem);
      // console.log(`Looking for ID: ${id} in ${sectionItem.itemType}`);

      if (sectionItem.itemType === "Lesson") {
        item = lessonsMap[id];
        // console.log(`Lesson found:`, !!item);
        if (item) {
          return { ...item, type: "lesson", itemType: "lesson" };
        }
      } else if (sectionItem.itemType === "Assignment") {
        item = assignmentsMap[id];
        // console.log(`Assignment found:`, !!item);
        if (item) {
          return {
            ...item,
            type: item.type?.toLowerCase() || "assignment",
            itemType: "assignment",
          };
        }
      }

      console.warn("Missing item for sectionItem:", sectionItem);
      return null;
    })
    .filter(Boolean);

  return (
    <div
      className={`relative border rounded-lg overflow-hidden transition-all duration-200 ${
        activeSection === index
          ? "border-blue-200 shadow-md"
          : "border-gray-200 hover:border-blue-100 hover:shadow-sm"
      }`}
    >
      {isEditMode && !(activeSection === index) && (
        <button
          {...dragHandleProps}
          className="absolute left-2 top-1/2 -translate-y-1/2 cursor-move p-2 bg-gray-100 rounded-md text-gray-600 hover:bg-gray-200 hover:text-gray-800 transition-opacity opacity-100"
          title="Drag to reorder"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-6 w-6"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            aria-hidden="true"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M4 8h16M4 16h16"
            />
          </svg>
        </button>
      )}

      <div className="flex items-center justify-between p-4 bg-gray-50">
        <button
          className="flex items-center flex-1 text-left focus:outline-none cursor-pointer pl-2"
          onClick={() =>
            setActiveSection(activeSection === index ? null : index)
          }
        >
          <span className="bg-blue-100 text-blue-600 rounded-full w-6 h-6 inline-flex items-center justify-center mr-2 text-sm">
            {index + 1}
          </span>
          <h3 className="font-semibold text-gray-800">{section.title}</h3>
        </button>

        {isEditMode && (
          <div className="flex gap-2 ml-4">
            <button
              className="px-3 py-1 rounded-md bg-[#94be70] text-white font-medium text-sm hover:bg-[#6f8f54] transition cursor-pointer"
              onClick={handleEditSection}
              type="button"
            >
              Edit
            </button>
            <button
              className="px-3 py-1 rounded-md bg-red-600 text-white font-medium text-sm hover:bg-red-700 transition cursor-pointer"
              onClick={() => {
                if (
                  window.confirm(
                    "Are you sure you want to delete this section? This will delete all the items. Action cannot be undone."
                  )
                ) {
                  handleDeleteSection();
                }
              }}
              type="button"
            >
              Delete
            </button>
          </div>
        )}

        <svg
          xmlns="http://www.w3.org/2000/svg"
          className={`h-5 w-5 text-gray-500 ml-2 transition-transform duration-200 ${
            activeSection === index ? "transform rotate-180" : ""
          }`}
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          onClick={() =>
            setActiveSection(activeSection === index ? null : index)
          }
          style={{ cursor: "pointer" }}
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M19 9l-7 7-7-7"
          />
        </svg>
      </div>

      <div
        className={`overflow-hidden transition-all duration-300 ${
          activeSection === index ? "p-4" : "max-h-0"
        }`}
      >
        {orderedItems.map((item, index) => (
          <CourseContentItem
            key={`${item.type}-${item.id}`}
            index={index + 1}
            item={item}
            type={item.type}
            courseId={courseId}
          />
        ))}
      </div>
    </div>
  );
}

export default CourseSection;
