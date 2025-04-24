import React, { useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import bgImage from "../../assets/images/bg_main_2.png";

// Import our new components
import CourseHeader from "./CourseHeader";
import CourseNavBar from "./CourseNavBar";
import CourseContent from "./CourseContent";
import Forum from "../Forum/Forum";

import CourseSidebar from "./CourseSidebar/CourseSidebar";

function CourseView() {
  const { code } = useParams();
  const location = useLocation();
  const courseData = location.state?.courseData;
  const [activeTab, setActiveTab] = useState("content");
  const [activeSection, setActiveSection] = useState(null);

  // Mock data for course sections
  const courseSections = [
    {
      title: "Introduction to the Course",
      items: [
        { type: "lesson", title: "Welcome to the Course", duration: "10 min" },
        { type: "quiz", title: "Pre-assessment Quiz", questions: 5 },
        {
          type: "announcement",
          title: "Course Schedule Update",
          date: "April 20, 2025",
        },
      ],
    },
    {
      title: "Core Concepts",
      items: [
        { type: "lesson", title: "Fundamental Principles", duration: "25 min" },
        { type: "lesson", title: "Key Terminology", duration: "15 min" },
        { type: "quiz", title: "Concepts Check", questions: 10 },
      ],
    },
    {
      title: "Advanced Topics",
      items: [
        { type: "lesson", title: "Advanced Applications", duration: "30 min" },
        {
          type: "announcement",
          title: "Guest Lecture Announcement",
          date: "May 5, 2025",
        },
        { type: "quiz", title: "Final Assessment", questions: 15 },
      ],
    },
  ];
  const courseContentData = {
    id: "123",
    title: "React for Beginners",
    instructor: "Jane Doe",
    sections: [
      {
        id: "section1",
        title: "Introduction to React",
        items: [
          {
            id: "item1",
            title: "Welcome to the Course",
            type: "video",
            duration: "5:30",
          },
          {
            id: "item2",
            title: "Setting Up Your Environment",
            type: "video",
            duration: "12:45",
          },
          { id: "item3", title: "Quiz: React Basics", type: "quiz" },
        ],
      },
      {
        id: "section2",
        title: "React Components",
        items: [
          {
            id: "item4",
            title: "Functional Components",
            type: "video",
            duration: "15:20",
          },
          {
            id: "item5",
            title: "Class Components",
            type: "video",
            duration: "18:10",
          },
          { id: "item6", title: "Component Assignment", type: "assignment" },
        ],
      },
    ],
  };

  return (
    <>
      <div className="min-h-screen bg-gray-50">
        {/* Header */}
        <CourseHeader bgImage={bgImage} courseData={courseData} code={code} />
        {/* Main content */}
        <div className="container mx-auto px-4 md:px-6 mt-10">
          <div className="bg-white rounded-xl shadow-lg border border-gray-100 overflow-hidden">
            {/* Navigation tabs */}
            <CourseNavBar activeTab={activeTab} setActiveTab={setActiveTab} />

            {/* Content area */}
            <div className="p-6">
              {activeTab === "content" ? (
                <CourseContent
                  courseSections={courseSections}
                  activeSection={activeSection}
                  setActiveSection={setActiveSection}
                />
              ) : (
                <Forum />
              )}
            </div>
          </div>
        </div>
      </div>
      <div className="flex relative">
        <CourseSidebar course={courseContentData} />
      </div>
    </>
  );
}

export default CourseView;
