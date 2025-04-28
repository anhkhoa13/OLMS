import React, { useState, useEffect } from "react";
import CourseCard from "../../components/CourseCard";
import { useAuth } from "../../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import AddCourse from "./AddCourse";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function Courses({ isEnroll, maxNoDisplay = null, title = "All Courses" }) {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [refreshTrigger, setRefreshTrigger] = useState(0);
  const navigate = useNavigate();

  const { isAuthenticated, userRole, currentUser } = useAuth();

  const [isModalOpen, setIsModalOpen] = useState(false);

  function handleInstructorViewCourse(course) {
    navigate(`/courses/${course.id}/view`, {
      state: { courseData: course },
    });
  }
  function handleInstructorEditCourse(course) {
    navigate(`/courses/edit?code=${course.code}`, {
      state: { courseData: course },
    });
  }

  const handleAddCourse = async (courseData) => {
    const courseJson = {
      ...courseData,
      instructorId: currentUser.id,
    };

    try {
      const response = await axios.post(
        `${API_URL}/api/course/create`,
        courseJson
      );
      if (response.data) {
        alert("Add course successfully");
        setRefreshTrigger((prev) => prev + 1);
      }
    } catch (err) {
      setError("Failed to add course: " + (err.message || "Unknown error"));
    }
  };

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        setLoading(true);
        let response;

        if (userRole === "Instructor") {
          if (isEnroll) {
            response = await axios.get(
              `${API_URL}/api/instructor/courses?instructorId=${currentUser.id}`
            );
          } else {
            response = await axios.get(`${API_URL}/api/course`);
          }
        } else if (userRole === "Student") {
          if (isEnroll) {
            response = await axios.get(
              `${API_URL}/api/student/courses?StudentId=${currentUser.id}`
            );
          } else {
            //TODO: should be changed to get courses of that student who havent enrolled
            response = await axios.get(`${API_URL}/api/course`);
          }
        } else {
          if (!isAuthenticated) {
            // API endpoint for instructors
            response = await axios.get(`${API_URL}/api/course`);
          } else throw new Error("Unknown user role");
        }

        // Check if the response has the expected structure
        if (response.data && response.data.courses) {
          setCourses(response.data.courses);
        } else {
          throw new Error("Unexpected response format");
        }

        setLoading(false);
      } catch (err) {
        setError(err.message || "Failed to fetch courses");
        setLoading(false);
      }
    };

    fetchCourses();
  }, [userRole, currentUser, refreshTrigger]);

  // Filter courses based on maxNoDisplay prop
  const displayedCourses =
    maxNoDisplay !== null ? courses.slice(0, maxNoDisplay) : courses;

  if (loading) {
    return (
      <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16">
        <div className="bg-white p-8 rounded-2xl shadow-lg text-center">
          <p className="text-lg text-gray-600">Loading courses...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16">
        <div className="bg-white p-8 rounded-2xl shadow-lg text-center">
          <p className="text-lg text-red-600">Error: {error}</p>
        </div>
      </div>
    );
  }
  if (userRole === "Student" || !isAuthenticated || !isEnroll) {
    return (
      <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16">
        <h2 className="text-4xl font-extrabold text-left mb-12 text-[black]">
          {title}
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10 bg-white p-8 rounded-2xl shadow-lg">
          {displayedCourses.map((course, index) => (
            <CourseCard
              key={index}
              course={course}
              className="w-full"
              isEnrolled={isEnroll}
            />
          ))}
        </div>
      </div>
    );
  }
  if (userRole === "Instructor" && isEnroll) {
    return (
      <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16 relative">
        <h2 className="text-4xl font-extrabold text-left mb-6 text-[black]">
          {title}
        </h2>

        <div className="bg-white p-8 rounded-2xl shadow-lg">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between mb-6">
            <button
              onClick={() => setIsModalOpen(true)}
              className="mt-4 md:mt-0 bg-[#89b46c] text-white px-4 py-2 rounded-lg hover:bg-[#6f8f54] transition-colors cursor-pointer"
            >
              Add Course
            </button>
          </div>

          {displayedCourses.length > 0 ? (
            <ul className="space-y-4">
              {displayedCourses.map((course, index) => (
                <li
                  key={index}
                  className="border border-gray-200 rounded-lg p-5 hover:shadow-lg transition-shadow duration-300 flex flex-col md:flex-row justify-between"
                >
                  <div>
                    <h2 className="text-xl font-bold text-gray-900">
                      {course.title}
                    </h2>
                    <p className="text-gray-700 mt-1">{course.Description}</p>
                    <p className="text-sm text-gray-500 mt-2">
                      Course Code: {course.code}
                    </p>
                  </div>
                  <div className="mt-4 md:mt-0 flex items-center">
                    <button
                      className="bg-[#89b46c] text-white px-4 py-2 rounded-lg hover:bg-[#6f8f54] transition-colors mr-2 cursor-pointer"
                      onClick={() => handleInstructorEditCourse(course)}
                    >
                      Edit
                    </button>
                    <button
                      className="bg-gray-200 text-gray-800 px-4 py-2 rounded-lg hover:bg-gray-300 transition-colors cursor-pointer"
                      onClick={() => handleInstructorViewCourse(course)}
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

        {/* Use the AddCourse component */}
        <AddCourse
          isOpen={isModalOpen}
          onClose={() => setIsModalOpen(false)}
          onSubmit={handleAddCourse}
        />
      </div>
    );
  }

  // Default return if neither student nor instructor
  return (
    <div className="bg-gray-100 py-16 px-4 md:px-8 lg:px-16">
      <div className="bg-white p-8 rounded-2xl shadow-lg text-center">
        <p className="text-lg text-gray-600">
          No courses available for your role.
        </p>
      </div>
    </div>
  );
}

export default Courses;
