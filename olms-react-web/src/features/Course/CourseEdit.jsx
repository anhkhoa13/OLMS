import React, { useState } from "react";
import { useLocation, useParams, useNavigate } from "react-router-dom";
import CourseContent from "./CourseSectionNav/CourseContent";

const courseSectionNavBar = {
  title: "Ultimate React Course",
  description:
    "Master modern React from beginner to advanced! Next.js, Context API, React Query, Redux, Tailwind, advanced patterns",
  status: "In progress",
  sections: [
    {
      id: "6737371B-2028-46E9-4D4E-08DD85A8CFAC",
      title: "Section 1: React fundamentals",
      lessons: [
        {
          id: "lesson1",
          title: "Review Javascript",
        },
        {
          id: "lesson2",
          title: "Useful resources",
        },
      ],
      assignments: [
        {
          id: "assignment1",
          title: "Assignment 1",
          dueDate: "2025-04-25T13:45:00Z",
          assignmentType: "Quiz",
        },
        {
          id: "assignment2",
          title: "Assignment 2",
          dueDate: "2025-04-25T13:45:00Z",
          assignmentType: "Exercise",
        },
      ],
      orders: ["lesson1", "assignment1", "lesson2", "assignment2"],
    },
    {
      id: "id2",
      title: "Section 2: components, useState, and props",
      lessons: [
        {
          id: "Lesson3",
          title: "Thinking in React: State Management",
        },
        {
          id: "Lesson4",
          title: "Components, Composition, and Reuseability",
        },
      ],
      assignments: [
        {
          id: "assignment3",
          title: "Assignment 3",
          dueDate: "2025-04-25T13:45:00Z",
          assignmentType: "Quiz",
        },
        {
          id: "assignment4",
          title: "Assignment 4",
          dueDate: "2025-04-25T13:45:00Z",
          assignmentType: "Exercise",
        },
      ],
      orders: ["lesson3, lesson4", "assignment3", "assignment4"],
    },
    {
      id: "id3",
      title: "Section 3: Advance - custom hooks, refs, more state ...",
      assignments: [],
      lessons: [
        {
          id: "lesson5",
          title: "The Advanced useReducer Hook",
        },
      ],
      orders: ["lesson5"],
    },
  ],
  // for student only
  progress: {
    completedLesson: [
      {
        id: "",
      },
    ],
  },
};

function CourseEdit() {
  const { code } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const courseData = location.state?.courseData;

  const [title, setTitle] = useState(courseData?.title || "");
  const [description, setDescription] = useState(courseData?.description || "");
  const [saving, setSaving] = useState(false);
  const [activeSection, setActiveSection] = useState(null);

  const handleSave = (e) => {
    e.preventDefault();
    setSaving(true);
    setTimeout(() => {
      console.log("Saved data:", { code, title, description });
      setSaving(false);
      navigate(-1);
    }, 1000);
  };

  const handleCancel = () => {
    navigate(-1);
  };

  return (
    <div className="flex flex-col md:flex-row gap-8 mt-16 px-4 max-w-6xl mx-auto">
      {/* Left Column: Main Content */}
      <div className="flex-1 min-w-0">
        <div className="bg-white shadow-2xl rounded-xl p-8 md:p-12 mb-8">
          <h1 className="text-3xl font-extrabold mb-8 text-gray-800 text-center">
            Edit Course
          </h1>
          <form className="space-y-8">
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
        <div className="bg-gray-50 shadow-lg rounded-xl p-8 md:p-12">
          <CourseContent
            courseSections={courseSectionNavBar.sections}
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
              onClick={handleSave}
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
