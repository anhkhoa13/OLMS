import React, { useState, useEffect } from "react";
import CourseSection from "./CourseSection";

const API_URL = import.meta.env.VITE_BACKEND_URL;

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
        setSections(data);
        setError(null);
      } catch (err) {
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
          }),
        }
      );

      if (!response.ok) {
        // Optionally handle error response here
        const errorData = await response.json();
        alert(errorData.message || "Failed to add section");
        return;
      }
      setRefreshFlag((prev) => !prev); // Toggle the flag to trigger useEffect

      // setSections([
      //   ...sections,
      //   // Use the new section from the backend if returned, otherwise use local
      //   newSection.title ? newSection : { title: newSectionTitle, content: [] },
      // ]);
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
              className="bg-[#6f8f54] hover:bg-[#5e7d4a] text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200 cursor-pointer"
            >
              + Add Section
            </button>
          ) : (
            <form
              onSubmit={handleAddSection}
              className="flex items-center gap-3 mt-2"
            >
              <input
                type="text"
                value={newSectionTitle}
                onChange={(e) => setNewSectionTitle(e.target.value)}
                placeholder="Section title"
                className="px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#6f8f54] focus:border-[#6f8f54]"
                autoFocus
              />
              <button
                type="submit"
                className="bg-[#6f8f54] hover:bg-[#5e7d4a] text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200"
              >
                Add
              </button>
              <button
                type="button"
                onClick={() => {
                  setShowAddForm(false);
                  setNewSectionTitle("");
                }}
                className="py-2 px-4 rounded-lg text-gray-600 hover:bg-gray-100"
              >
                Cancel
              </button>
            </form>
          )}
        </div>
      )}

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
    </div>
  );
}

export default CourseContent;
