import { useNavigate } from "react-router-dom";
import { CourseContentItem } from "./CourseContentItem";

function CourseSection({
  section,
  index,
  activeSection,
  setActiveSection,
  isEditMode,
}) {
  const navigate = useNavigate();

  function handleEditSection() {
    navigate("/editSection", { state: { sectionId: section.id } });
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
      className={`border rounded-lg overflow-hidden transition-all duration-200 ${
        activeSection === index
          ? "border-blue-200 shadow-md"
          : "border-gray-200 hover:border-blue-100 hover:shadow-sm"
      }`}
    >
      <div className="flex items-center justify-between p-4 bg-gray-50">
        <button
          className="flex items-center flex-1 text-left focus:outline-none"
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
          <button
            className="ml-4 px-3 py-1 rounded-md bg-[#94be70] text-white font-medium text-sm hover:bg-[#6f8f54] transition cursor-pointer"
            onClick={() => handleEditSection()}
            type="button"
          >
            Edit
          </button>
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
          activeSection === index ? " p-4" : "max-h-0"
        }`}
      >
        {orderedItems.map((item, index) => (
          <CourseContentItem
            key={`${item.type}-${item.id}`}
            index={index + 1}
            item={item}
            type={item.type}
          />
        ))}
      </div>
    </div>
  );
}

export default CourseSection;
