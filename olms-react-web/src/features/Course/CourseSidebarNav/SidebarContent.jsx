import React, { useContext, useState } from "react";
import { SidebarContext } from "./SidebarContext";
import SidebarItem from "./SidebarItem";
import { ChevronDownIcon, ChevronUpIcon } from "@heroicons/react/24/outline";

const SidebarContent = ({ sections }) => {
  const { expanded } = useContext(SidebarContext);
  const [sectionExpanded, setSectionExpanded] = useState({});

  const toggleSection = (index) => {
    setSectionExpanded((prev) => ({
      ...prev,
      [index]: !prev[index],
    }));
  };

  if (!expanded) return null;

  return (
    <div className="py-2">
      {sections.map((section, index) => {
        const lessonsMap = Object.fromEntries(
          (section.lessons || []).map((l) => [l.id.toLowerCase(), l])
        );
        const assignmentsMap = Object.fromEntries(
          (section.assignments || []).map((a) => [a.id.toLowerCase(), a])
        );

        const orderedItems = (section.sectionItems || [])
          .sort((a, b) => a.order - b.order)
          .map((sectionItem) => {
            const id = sectionItem.itemId.toLowerCase();
            let item = null;

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

        console.log(orderedItems);

        return (
          <div key={section.title} className="mb-4">
            <button
              onClick={() => toggleSection(index)}
              className="w-full px-4 py-2 flex items-center justify-between hover:bg-gray-200 transition-colors cursor-pointer"
            >
              <h2 className="text-lg font-semibold text-gray-700 text-left">
                Section {index + 1}: {section.title}
              </h2>
              {sectionExpanded[index] ? (
                <ChevronUpIcon className="h-5 w-5 text-gray-600 ml-2" />
              ) : (
                <ChevronDownIcon className="h-5 w-5 text-gray-600 ml-2" />
              )}
            </button>

            {sectionExpanded[index] && (
              <div className="section-items">
                {orderedItems.map((item, idx) => (
                  <React.Fragment key={`${item.type}-${item.id}`}>
                    <SidebarItem
                      item={item}
                      index={idx + 1}
                      sectionId={section.title}
                    />
                    {idx < orderedItems.length - 1 && (
                      <div className="border-t border-gray-200 mx-4" />
                    )}
                  </React.Fragment>
                ))}
              </div>
            )}
          </div>
        );
      })}
    </div>
  );
};

export default SidebarContent;
