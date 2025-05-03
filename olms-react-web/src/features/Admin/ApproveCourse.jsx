import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import axios from "axios";
import { useAuth } from "../../contexts/AuthContext";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function ApproveCourse() {
  const [displayedCourses, setDisplayedCourses] = useState([]);
  const navigate = useNavigate();
  const { userRole, currentUser } = useAuth();

  const [title] = useState("Approve Courses"); // Added title state

  useEffect(() => {
    const fetchAdminCourses = async () => {
      try {
        const response = await axios.get(`${API_URL}/api/admin/courses`);
        console.log(response.data);
        setDisplayedCourses(response.data);
      } catch (error) {
        console.error("Failed to fetch courses:", error);
      }
    };

    fetchAdminCourses();
  }, []); // Added empty dependency array

  const handleAdminApproveCourse = async (course) => {
    if (userRole !== "Admin") {
      alert("You are not admin!!!!");
      return;
    }

    try {
      await axios.post(`${API_URL}/api/admin/approveCourse`, {
        CourseId: course.id,
        AdminId: currentUser.id,
      });

      // Refresh the course list after approval
      const updatedResponse = await axios.get(`${API_URL}/api/admin/courses`);
      setDisplayedCourses(updatedResponse.data);

      alert("Course approved successfully!");
    } catch (error) {
      console.error("Approval failed:", error);
      alert(
        `Approval failed: ${error.response?.data?.message || error.message}`
      );
    }
  };

  const handleViewCourse = (course) => {
    navigate(`/courses/${course.id}/view`, {
      state: { courseData: course },
    });
  };

  return (
    <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16 relative">
      <h2 className="text-4xl font-extrabold text-left mb-6 text-[black]">
        {title}
      </h2>

      <div className="bg-white p-8 rounded-2xl shadow-lg">
        {displayedCourses.length > 0 ? (
          <ul className="space-y-4">
            {displayedCourses.map((course, index) => (
              <li
                key={index}
                className="border border-gray-200 rounded-xl p-6 hover:shadow-lg transition-shadow duration-300 flex flex-col md:flex-row md:items-center justify-between gap-6 bg-white"
              >
                {/* Left: Course Info */}
                <div className="flex-1 min-w-0">
                  <h2 className="text-2xl font-semibold text-gray-900 truncate">
                    {course.title}
                  </h2>
                  <p className="text-gray-700 mt-1 line-clamp-2">
                    {course.Description}
                  </p>
                  <p className="text-sm text-gray-500 mt-2">
                    Course Code: {course.code}
                  </p>
                </div>

                {/* Middle: Status */}
                <div className="flex items-center md:justify-center mt-3 md:mt-0">
                  <span
                    className={`
                      px-3 py-1 rounded-full text-xs font-semibold
                      ${
                        course.status === "Enrolling"
                          ? "bg-green-100 text-green-700"
                          : course.status === "Pending"
                          ? "bg-yellow-100 text-yellow-700"
                          : "bg-gray-100 text-red-500"
                      }
                    `}
                  >
                    {course.status}
                  </span>
                </div>

                {/* Right: Actions */}
                <div className="flex items-center mt-4 md:mt-0 gap-2">
                  <button
                    className="bg-[#FF4C4C] text-white px-4 py-2 rounded-lg hover:bg-[#884a4a] transition-colors cursor-pointer"
                    onClick={() => handleAdminApproveCourse(course)}
                  >
                    Approve
                  </button>
                  <button
                    className="bg-gray-200 text-gray-800 px-4 py-2 rounded-lg hover:bg-gray-300 transition-colors cursor-pointer"
                    onClick={() => handleViewCourse(course)}
                  >
                    View
                  </button>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-center text-gray-500">No courses found.</p>
        )}
      </div>
    </div>
  );
}

export default ApproveCourse;
