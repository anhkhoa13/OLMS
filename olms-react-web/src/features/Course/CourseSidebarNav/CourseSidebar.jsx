import React, { useState, useEffect, useRef } from "react";
import { SidebarContext } from "./SidebarContext";
import SidebarHeader from "./SidebarHeader";
import SidebarContent from "./SidebarContent";
import SidebarToggle from "./SidebarToggle";

const NAVBAR_HEIGHT = 72;
const API_URL = import.meta.env.VITE_BACKEND_URL;

// const courseSectionNavBar = {
//   title: "Ultimate React Course",
//   description:
//     "Master modern React from beginner to advanced! Next.js, Context API, React Query, Redux, Tailwind, advanced patterns",
//   status: "In progress",
//   instructor: "Dr. John Paul",
//   sections: [
//     {
//       title: "React fundamentals",
//       lessons: [
//         {
//           id: "lesson1",
//           title: "Review Javascript",
//         },
//         {
//           id: "lesson2",
//           title: "Useful resources",
//         },
//       ],
//       assignments: [
//         {
//           id: "assignment1",
//           title: "Assignment 1",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Quiz",
//         },
//         {
//           id: "assignment2",
//           title: "Assignment 2",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Exercise",
//         },
//       ],
//       orders: ["lesson1", "assignment1", "lesson2", "assignment2"],
//     },
//     {
//       title: "Components, useState, and props",
//       lessons: [
//         {
//           id: "Lesson3",
//           title: "Thinking in React: State Management",
//         },
//         {
//           id: "Lesson4",
//           title: "Components, Composition, and Reuseability",
//         },
//       ],
//       assignments: [
//         {
//           id: "F6B2A8",
//           title: "Assignment 3",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Quiz",
//         },
//         {
//           id: "assignment4",
//           title: "Assignment 4",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Exercise",
//         },
//       ],
//       orders: ["lesson3, lesson4", "F6B2A8", "assignment4"],
//     },
//     {
//       title: "Advance - custom hooks, refs, more state ...",
//       assignments: [],
//       lessons: [
//         {
//           id: "lesson5",
//           title: "The Advanced useReducer Hook",
//         },
//       ],
//       orders: ["lesson5"],
//     },
//   ],
// };

function CourseSidebar({ courseId }) {
  const [expanded, setExpanded] = useState(false);
  const [navbarVisible, setNavbarVisible] = useState(true);
  const sidebarRef = useRef(null);
  const [sections, setSections] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
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
        console.log("Course content", data);
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
  }, [courseId]);

  // Close sidebar when clicking outside
  useEffect(() => {
    const handleClickOutside = (e) => {
      if (
        expanded &&
        sidebarRef.current &&
        !sidebarRef.current.contains(e.target)
      ) {
        setExpanded(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [expanded]);

  // Existing scroll handler
  useEffect(() => {
    const handleScroll = () => {
      setNavbarVisible(window.scrollY <= NAVBAR_HEIGHT);
    };
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  const toggleSidebar = () => {
    setExpanded((prev) => !prev);
  };
  if (loading) {
    return <div className="p-4 text-gray-600">Loading sections...</div>;
  }

  if (error) {
    return (
      <div className="p-4 text-red-600">Error loading sections: {error}</div>
    );
  }

  return (
    <SidebarContext.Provider value={{ expanded }}>
      <div
        ref={sidebarRef}
        className={`fixed outline-1 outline-[#6f8f54] right-0 h-screen bg-gray-50 shadow-md transition-all duration-300 ease-in-out z-51 overflow-y-auto ${
          expanded ? "w-72" : "w-0"
        } ${navbarVisible ? "top-18" : "top-0"}`}
      >
        {/* <SidebarHeader course={sections} /> */}
        <SidebarContent sections={sections} />
        <SidebarToggle onToggle={toggleSidebar} expanded={expanded} />
      </div>
    </SidebarContext.Provider>
  );
}

export default CourseSidebar;
