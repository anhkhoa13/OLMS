import { useState, useEffect } from "react";
import { CourseContentItem } from "../Course/CourseSectionNav/CourseContentItem";
import Modal from "../../components/Modal";
import LessonForm from "../Lesson/LessonForm";
import ExerciseForm from "../Lesson/ExerciseForm";
import CreateQuiz from "../CreateQuiz/CreateQuiz";
import { useLocation } from "react-router-dom";
import axios from "axios";

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
  useSortable,
  verticalListSortingStrategy,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";

const SortableCourseContentItem = ({ item, ...props }) => {
  // Use the actual sectionItem ID from the items array
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({ id: item?.sectionItemId || "" });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  return (
    <div ref={setNodeRef} style={style} {...attributes}>
      <CourseContentItem {...props} item={item} dragHandleProps={listeners} />
    </div>
  );
};

const API_URL = import.meta.env.VITE_BACKEND_URL;

function SectionEdit() {
  const [section, setSection] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showAddMenu, setShowAddMenu] = useState(false);
  const [modalType, setModalType] = useState(null);
  const [refreshKey, setRefreshKey] = useState(0);
  const [isEditing, setIsEditing] = useState(false);
  const [currentItem, setCurrentItem] = useState(null);

  const location = useLocation();
  const sectionId = location.state?.sectionId;
  const courseId = location.state?.courseId;

  const [items, setItems] = useState([]);
  const [hasOrderChanged, setHasOrderChanged] = useState(false);

  const sensors = useSensors(
    useSensor(PointerSensor),
    useSensor(KeyboardSensor)
  );

  // Fetch section data on mount or when sectionId changes
  useEffect(() => {
    async function fetchSection() {
      setLoading(true);
      setError(null);
      try {
        const response = await fetch(
          `${API_URL}/api/section?sectionId=${sectionId}`
        );
        if (!response.ok) {
          throw new Error(`Error: ${response.status}`);
        }
        const data = await response.json();
        setSection(data);
      } catch (err) {
        setError(err.message);
        setSection(null);
      } finally {
        setLoading(false);
      }
    }
    fetchSection();
  }, [sectionId, refreshKey]);

  useEffect(() => {
    if (section?.sectionItems) {
      const sortedItems = [...section.sectionItems].sort(
        (a, b) => a.order - b.order
      );
      setItems(sortedItems);
    }
  }, [section]);

  const handleDragEnd = (event) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const oldIndex = items.findIndex((s) => s.id === active.id);
    const newIndex = items.findIndex((s) => s.id === over.id);

    const newSections = arrayMove(items, oldIndex, newIndex).map(
      (section, index) => ({
        ...section,
        order: index,
      })
    );

    setItems(newSections);
    setHasOrderChanged(true);
  };

  const handleSaveOrder = async () => {
    try {
      const orderUpdates = items.map((item, index) => ({
        sectionItemId: item.id,
        newOrder: index,
      }));
      console.log(orderUpdates);

      await axios.put(`${API_URL}/api/section/item/order`, {
        sectionId: section.id,
        items: orderUpdates,
      });

      // Refresh the section data after successful update
      const response = await fetch(
        `${API_URL}/api/section?sectionId=${sectionId}`
      );
      const updatedSection = await response.json();
      setSection(updatedSection);

      setHasOrderChanged(false);
      alert("Order saved successfully");
    } catch (error) {
      alert("Error saving order: " + error.message);
    }
  };

  const handleRefresh = () => setRefreshKey((prev) => prev + 1);

  const handleEditItem = (item) => {
    setCurrentItem(item);
    setIsEditing(true);
    setModalType(item.type);
  };
  const handleDeleteItem = async (item) => {
    try {
      let endpoint = "";
      if (item.type === "quiz") {
        // const code = item.id.slice(0, 6);
        endpoint = `${API_URL}/api/quiz/${item.id}`;
      } else if (item.type === "exercise") {
        endpoint = `${API_URL}/api/exercise/${item.id}`;
      } else if (item.type === "lesson") {
        endpoint = `${API_URL}/api/lesson/${item.id}`;
      } else {
        console.error("Unknown item type:", item.type);
        return;
      }

      const response = await axios.delete(endpoint);

      if (response.status === 200) {
        alert(`${item.type} deleted successfully`);
        // Optionally refresh or update state here
      } else {
        console.error(`Failed to delete ${item.type}`);
      }
    } catch (error) {
      console.error("Error deleting item:", error);
    }
  };

  // Create maps for quick lookup by id (case-insensitive)
  const lessonsMap = Object.fromEntries(
    (section?.lessons || []).map((l) => [l.id.toLowerCase(), l])
  );
  const assignmentsMap = Object.fromEntries(
    (section?.assignments || []).map((a) => [a.id.toLowerCase(), a])
  );

  const orderedItems = items
    .map((sectionItem) => {
      const id = sectionItem.itemId.toLowerCase();
      let item = null;

      if (sectionItem.itemType === "Lesson") {
        item = lessonsMap[id];
        return { ...item, type: "lesson" };
      }

      if (sectionItem.itemType === "Assignment") {
        item = assignmentsMap[id];
        return { ...item, type: item.type?.toLowerCase() };
      }

      return null;
    })
    .filter(Boolean);

  function handleAdd(type) {
    setModalType(type);
    setShowAddMenu(false);
  }

  function closeModal() {
    setModalType(null);
  }
  // Modify the modal rendering logic
  const getModalTitle = () => {
    if (isEditing) {
      return `Edit ${
        currentItem?.type?.charAt(0).toUpperCase() + currentItem?.type?.slice(1)
      }`;
    }
    return `Add ${modalType?.charAt(0).toUpperCase() + modalType?.slice(1)}`;
  };

  if (loading) {
    return (
      <div className="p-8 text-center text-gray-500">Loading section...</div>
    );
  }

  if (error || !section) {
    return (
      <div className="p-8 text-center text-red-500">
        {error || "No section data found."}
      </div>
    );
  }

  return (
    <div className="max-w-3xl mx-auto p-8 bg-white rounded-xl shadow-md my-8">
      <div className="flex items-center justify-between mb-6">
        <div>
          <h2 className="text-3xl font-bold text-[#6f8f54] mb-1">
            {section.title}
          </h2>
          <div className="text-gray-500 text-sm">Section ID: {section.id}</div>
        </div>
        {hasOrderChanged && (
          <button
            onClick={handleSaveOrder}
            className="bg-[#6f8f54] hover:bg-[#5e7d4a] cursor-pointer text-white font-medium py-2 px-4 rounded-lg transition-colors"
          >
            Save Order
          </button>
        )}
      </div>

      {/* Ordered List using CourseContentItem */}
      {/* <div className="space-y-2">
        {orderedItems.map((item, index) => (
          <CourseContentItem
            key={item.id}
            item={item}
            index={index + 1}
            type={item.type}
            courseId={courseId}
            isEditMode={true}
            onEdit={handleEditItem}
            onDelete={handleDeleteItem}
          />
        ))}
      </div> */}
      <DndContext
        sensors={sensors}
        collisionDetection={closestCenter}
        onDragEnd={handleDragEnd}
      >
        <SortableContext
          items={items.map((i) => i.id)}
          strategy={verticalListSortingStrategy}
        >
          <div className="space-y-2">
            {items.map((sectionItem, index) => {
              const item = orderedItems[index];
              return (
                <SortableCourseContentItem
                  key={sectionItem.id} // Use sectionItem.id instead of item.id
                  item={{ ...item, sectionItemId: sectionItem.id }}
                  index={index + 1}
                  type={item?.type}
                  courseId={courseId}
                  onEdit={handleEditItem}
                  onDelete={handleDeleteItem}
                  isEditMode={true}
                />
              );
            })}
          </div>
        </SortableContext>
      </DndContext>
      {/* Add Item Button */}
      <div className="flex justify-end mb-6">
        <div className="relative">
          <button
            onClick={() => setShowAddMenu((v) => !v)}
            className="bg-[#89b46c] hover:bg-[#6f8f54]  cursor-pointer text-white px-5 py-2 rounded-lg font-medium shadow transition-colors"
          >
            + Add Item
          </button>
          {showAddMenu && (
            <div className="absolute right-0 mt-2 w-40 bg-white border border-gray-200 rounded-lg shadow-lg z-10">
              <button
                onClick={() => handleAdd("lesson")}
                className="block cursor-pointer w-full text-left px-4 py-2 hover:bg-gray-100 text-gray-700"
              >
                Lesson
              </button>
              <button
                onClick={() => handleAdd("exercise")}
                className="block cursor-pointer w-full text-left px-4 py-2 hover:bg-gray-100 text-gray-700"
              >
                Exercise
              </button>
              <button
                onClick={() => handleAdd("quiz")}
                className="block cursor-pointer w-full text-left px-4 py-2 hover:bg-gray-100 text-gray-700"
              >
                Quiz
              </button>
            </div>
          )}
        </div>
      </div>

      {/* Modal */}
      <Modal
        open={!!modalType || isEditing}
        onClose={() => {
          closeModal();
          setIsEditing(false);
          setCurrentItem(null);
        }}
        title={getModalTitle()}
      >
        {modalType === "lesson" && (
          <LessonForm
            sectionId={section.id}
            onClose={() => {
              closeModal();
              setIsEditing(false);
              setCurrentItem(null);
            }}
            nextOrder={orderedItems.length}
            onSuccess={handleRefresh}
            isEditing={isEditing} // Add editing mode prop
            lessonId={currentItem?.id}
          />
        )}
        {modalType === "exercise" && (
          <ExerciseForm
            sectionId={section.id}
            onClose={() => {
              closeModal();
              setIsEditing(false);
              setCurrentItem(null);
            }}
            nextOrder={orderedItems.length}
            onSuccess={handleRefresh}
            isEditing={isEditing}
            exerciseId={currentItem?.id}
          />
        )}
        {modalType === "quiz" && (
          <CreateQuiz
            sectionId={section.id}
            onClose={() => {
              closeModal();
              setIsEditing(false);
              setCurrentItem(null);
            }}
            nextOrder={orderedItems.length}
            onSuccess={handleRefresh}
            isEditing={isEditing}
            quizId={currentItem?.id}
          />
        )}
      </Modal>
    </div>
  );
}

export default SectionEdit;
