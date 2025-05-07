import React, { useState, useEffect } from "react";
import CourseSection from "./CourseSection";

import {
  DndContext,
  closestCenter,
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import {
  arrayMove,
  SortableContext,
  verticalListSortingStrategy,
  useSortable,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function SortableSection({ section, index, ...props }) {
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({
      id: section.id,
    });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  return (
    <div ref={setNodeRef} style={style} {...attributes}>
      <CourseSection
        section={section}
        index={index}
        dragHandleProps={listeners}
        {...props}
      />
    </div>
  );
}

function CourseContent({
  activeSection,
  setActiveSection,
  isEditMode,
  courseId,
}) {
  const [showAddForm, setShowAddForm] = useState(false);
  const [newSectionTitle, setNewSectionTitle] = useState("");
  const [sections, setSections] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [refreshFlag, setRefreshFlag] = useState(false);

  const [hasOrderChanged, setHasOrderChanged] = useState(false);
  const sensors = useSensors(
    useSensor(PointerSensor),
    useSensor(KeyboardSensor)
  );
  const handleDragEnd = (event) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const oldIndex = sections.findIndex((s) => s.id === active.id);
    const newIndex = sections.findIndex((s) => s.id === over.id);

    const newSections = arrayMove(sections, oldIndex, newIndex).map(
      (section, index) => ({
        ...section,
        order: index,
      })
    );

    setSections(newSections);
    setHasOrderChanged(true);
  };
  const handleSaveOrder = async () => {
    try {
      const updates = sections.map((section) => ({
        sectionId: section.id,
        newOrder: section.order,
      }));

      console.log(updates);

      const response = await fetch(`${API_URL}/api/section/order`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          courseId,
          sections: updates,
        }),
      });

      if (!response.ok) throw new Error("Failed to save order");

      alert("Order saved successfully!");
      setHasOrderChanged(false);
    } catch (error) {
      alert("Error saving order: " + error.message);
    }
  };

  // Fetch sections when courseId changes
  useEffect(() => {
    const fetchSections = async () => {
      try {
        const response = await fetch(
          `${API_URL}/api/section/course?courseId=${courseId}`
        );

        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        console.log(data);
        const orderedSections = [...data].sort((a, b) => a.order - b.order);
        setSections(orderedSections);
        setError(null);
      } catch (err) {
        console.log(err);
        setError(err.message);
      } finally {
        setLoading(false);
      }
      setLoading(false);
    };

    if (courseId) {
      fetchSections();
    }
  }, [courseId, refreshFlag]);

  async function handleAddSection(e) {
    e.preventDefault();

    if (!newSectionTitle.trim()) return;

    console.log(sections.length);
    try {
      const response = await fetch(
        "https://localhost:7212/api/course/section/create",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            courseId: courseId,
            title: newSectionTitle,
            order: sections.length,
          }),
        }
      );

      if (!response.ok) {
        // Optionally handle error response here
        const errorData = await response.json();
        alert(errorData.message || "Failed to add section");
        return;
      }
      setRefreshFlag((prev) => !prev);
      setNewSectionTitle("");
      setShowAddForm(false);
    } catch (err) {
      alert("Network error: " + err.message);
    }
  }
  if (loading) {
    return <div className="p-4 text-gray-600">Loading sections...</div>;
  }

  if (error) {
    return (
      <div className="p-4 text-red-600">Error loading sections: {error}</div>
    );
  }

  return (
    <div>
      <h2 className="text-xl font-bold text-gray-800 mb-6">Course Content</h2>

      {/* Add Section Button and Form */}
      {isEditMode && (
        <div className="mb-6">
          {!showAddForm ? (
            <button
              onClick={() => setShowAddForm(true)}
              className="bg-[#6f8f54] hover:bg-[#5e7d4a] mr-8 text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200 cursor-pointer"
            >
              + Add Section
            </button>
          ) : (
            <form
              onSubmit={handleAddSection}
              className="flex items-center gap-3 my-2"
            >
              <input
                type="text"
                value={newSectionTitle}
                onChange={(e) => setNewSectionTitle(e.target.value)}
                placeholder="Section title"
                className="px-3 py-2 border w-full border-gray-300 rounded-lg focus:ring-2 focus:ring-[#6f8f54] focus:border-[#6f8f54]"
                autoFocus
              />
              <button
                type="submit"
                className="bg-[#6f8f54] cursor-pointer hover:bg-[#5e7d4a] text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200"
              >
                Add
              </button>
              <button
                type="button"
                onClick={() => {
                  setShowAddForm(false);
                  setNewSectionTitle("");
                }}
                className="py-2 px-4 cursor-pointer rounded-lg text-gray-600 hover:bg-gray-100 border"
              >
                Cancel
              </button>
            </form>
          )}
          {hasOrderChanged && (
            <button
              onClick={handleSaveOrder}
              className="bg-[#6f8f54] hover:bg-[#5e7d4a] cursor-pointer text-white font-medium py-2 px-4 rounded-lg transition-colors"
            >
              Save Order
            </button>
          )}
        </div>
      )}
      {isEditMode ? (
        <DndContext
          sensors={sensors}
          collisionDetection={closestCenter}
          onDragEnd={handleDragEnd}
        >
          <SortableContext
            items={sections}
            strategy={verticalListSortingStrategy}
          >
            <div className="space-y-4">
              {sections.map((section, index) => (
                <SortableSection
                  key={section.id}
                  section={section}
                  index={index}
                  activeSection={activeSection}
                  setActiveSection={setActiveSection}
                  isEditMode={isEditMode}
                  courseId={courseId}
                  onRefresh={setRefreshFlag}
                />
              ))}
            </div>
          </SortableContext>
        </DndContext>
      ) : (
        <div className="space-y-4">
          {sections.map((section, index) => (
            <CourseSection
              key={index}
              section={section}
              index={index}
              activeSection={activeSection}
              setActiveSection={setActiveSection}
              isEditMode={isEditMode}
              courseId={courseId}
            />
          ))}
        </div>
      )}
    </div>
  );
}

export default CourseContent;
