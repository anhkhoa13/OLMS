import React, { useState } from "react";
import { useLocation } from "react-router-dom";

import bgImage from "../../assets/images/bg_main_2.png";
import CourseViewHeader from "./CourseViewHeader";
import CourseNavBar from "./CourseSectionNav/CourseNavBar";
import CourseContent from "./CourseSectionNav/CourseContent";
import Forum from "../Forum/Forum";

function CourseView() {
  const location = useLocation();
  const courseData = location.state?.courseData;
  const [activeTab, setActiveTab] = useState("content");
  const [activeSection, setActiveSection] = useState(null);

  return (
    <>
      <div className="min-h-screen bg-gray-50">
        {/* Header */}
        <CourseViewHeader bgImage={bgImage} courseData={courseData} />
        {/* Main content */}
        <div className="container mx-auto px-4 md:px-6 mt-10">
          <div className="bg-white rounded-xl shadow-lg border border-gray-100 overflow-hidden">
            {/* Navigation tabs */}
            <CourseNavBar activeTab={activeTab} setActiveTab={setActiveTab} />

            {/* Content area */}
            <div className="p-6">
              {activeTab === "content" ? (
                <CourseContent
                  activeSection={activeSection}
                  setActiveSection={setActiveSection}
                  courseId={courseData.id}
                />
              ) : (
                <Forum courseId={courseData.id} />
              )}
            </div>
          </div>
        </div>
      </div>
      {/* <div className="flex relative">
        <CourseSidebar course={courseContentData} />
      </div> */}
    </>
  );
}

export default CourseView;
