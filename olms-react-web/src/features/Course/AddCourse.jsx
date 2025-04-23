import React, { useState } from "react";

function AddCourse({ isOpen, onClose, onSubmit }) {
  const [courseData, setCourseData] = useState({
    title: "",
    description: "",
  });

  const handleChange = (e) => {
    const { id, value } = e.target;
    setCourseData({
      ...courseData,
      [id.replace("course", "").toLowerCase()]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(courseData);
    setCourseData({ title: "", description: "" }); // Reset form
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 flex justify-center items-center z-50">
      {/* Separate backdrop div with opacity */}
      <div
        className="absolute inset-0 bg-black opacity-30"
        onClick={onClose}
      ></div>

      {/* Modal content */}
      <div className="bg-white rounded-lg p-6 w-96 shadow-lg relative z-10">
        <h3 className="text-xl font-semibold mb-4">Add New Course</h3>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-gray-700 mb-2" htmlFor="courseTitle">
              Course Title
            </label>
            <input
              id="courseTitle"
              type="text"
              className="w-full border border-gray-300 rounded px-3 py-2"
              placeholder="Enter course title"
              value={courseData.title}
              onChange={handleChange}
              required
            />
          </div>
          <div className="mb-4">
            <label
              className="block text-gray-700 mb-2"
              htmlFor="courseDescription"
            >
              Description
            </label>
            <textarea
              id="courseDescription"
              className="w-full border border-gray-300 rounded px-3 py-2"
              placeholder="Enter course description"
              value={courseData.description}
              onChange={handleChange}
            />
          </div>
          <div className="flex justify-end">
            <button
              type="button"
              onClick={onClose}
              className="mr-4 px-4 py-2 rounded bg-gray-300 hover:bg-gray-400"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-4 py-2 rounded bg-[#89b46c] text-white hover:bg-[#6f8f54]"
            >
              Add
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default AddCourse;
