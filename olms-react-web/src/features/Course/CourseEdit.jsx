import React, { useState, useRef } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import axios from "axios";
import CourseContent from "./CourseSectionNav/CourseContent";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function CourseEdit() {
  const location = useLocation();
  const navigate = useNavigate();
  const courseData = location.state?.courseData;

  const [title, setTitle] = useState(courseData?.title || "");
  const [description, setDescription] = useState(courseData?.description || "");
  const [saving, setSaving] = useState(false);
  const [activeSection, setActiveSection] = useState(null);

  const formRef = useRef();

  // Save handler for both course and sections
  const handleSave = async (e) => {
    if (e) e.preventDefault();
    setSaving(true);
    try {
      // Combine course and section updates in one API call (adjust as needed)
      await axios.put(`${API_URL}/api/course/update`, {
        courseId: courseData.id,
        title,
        description,
      });

      alert("Course and sections updated successfully!");
      navigate(-1);
    } catch (error) {
      alert(
        `Error updating course: ${
          error.response?.data?.message || error.message
        }`
      );
    } finally {
      setSaving(false);
    }
  };

  const handleCancel = () => {
    navigate(-1);
  };

  return (
    <div className="flex flex-col md:flex-row gap-8 mt-8 px-4 max-w-6xl mx-auto">
      {/* Left Column: Main Content */}
      <div className="flex-1 min-w-0">
        <div className="bg-white shadow-2xl rounded-xl p-8 md:p-12 mb-8">
          <h1 className="text-3xl font-extrabold mb-8 text-gray-800 text-center">
            Edit Course
          </h1>
          <form ref={formRef} className="space-y-8" onSubmit={handleSave}>
            <div>
              <label className="block text-lg text-gray-700 font-semibold mb-3">
                Course Title
              </label>
              <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
                placeholder="Enter course title"
                className="w-full px-5 py-4 border-2 border-gray-200 rounded-lg text-lg focus:outline-none focus:ring-2 focus:ring-blue-400 transition"
              />
            </div>
            <div>
              <label className="block text-lg text-gray-700 font-semibold mb-3">
                Course Description
              </label>
              <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                required
                rows={10}
                placeholder="Write a detailed course description..."
                className="w-full px-5 py-4 border-2 border-gray-200 rounded-lg text-lg resize-y min-h-[200px] focus:outline-none focus:ring-2 focus:ring-blue-400 transition"
              />
            </div>
          </form>
        </div>
        <div className="bg-gray-50 shadow-lg rounded-xl p-8 md:p-12 mb-8">
          <CourseContent
            activeSection={activeSection}
            setActiveSection={setActiveSection}
            isEditMode={true}
            courseId={courseData.id}
          />
        </div>
      </div>

      {/* Right Column: Fixed Buttons */}
      <div className="w-full md:w-64 flex-shrink-0">
        <div className="sticky top-8 z-20">
          <div className="flex flex-col gap-4 bg-white shadow-lg rounded-xl px-6 py-8">
            <button
              type="button"
              onClick={handleCancel}
              className="px-6 py-3 rounded-lg bg-gray-200 text-gray-700 font-semibold hover:bg-gray-300 transition cursor-pointer"
              disabled={saving}
            >
              Cancel
            </button>
            <button
              type="button"
              onClick={() => formRef.current.requestSubmit()}
              className="px-6 py-3 rounded-lg bg-[#89b46c] text-white font-bold hover:bg-[#6f8f54] transition cursor-pointer"
              disabled={saving}
            >
              {saving ? "Saving..." : "Save"}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CourseEdit;
