import { Reac, useState } from "react";
import Avatar from "./Avatar";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { Loader } from "lucide-react";
import cardBg from "../assets/images/card_2.png";

const API_URL = import.meta.env.VITE_BACKEND_URL;

const CourseCard = ({ course, isEnrolled }) => {
  const { code, title, description, instructor } = course;
  const [isLoading, setIsLoading] = useState(false); // Add loading state
  const { currentUser, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  async function handleEnroll() {
    console.log("handleEnroll");
    if (!isAuthenticated) {
      navigate("/login");
      return;
    }
    try {
      setIsLoading(true);
      const enrollmentData = {
        studentId: currentUser.id,
        courseCode: code,
      };
      const response = await axios.post(
        `${API_URL}/api/student/enroll`,
        enrollmentData,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log("Enrollment successful:", response.data);
      alert(response.data.message || "Successfully enrolled in the course!");
      window.location.reload(); // Or use a more elegant state update approach
    } catch (error) {
      console.error("Enrollment failed:", error);
      if (error.response) {
        const errorMessage =
          error.response.data.message || "Failed to enroll in the course";
        alert(`Error: ${errorMessage}`);
      } else if (error.request) {
        alert("Network error. Please check your connection and try again.");
      } else {
        alert("An unexpected error occurred. Please try again later.");
      }
    } finally {
      setIsLoading(false);
    }
  }

  function handleView() {
    navigate(`/courses/${course.id}/view`, {
      state: { courseData: course },
    });
  }

  return (
    <div
      className="relative w-full rounded-xl border border-gray-200 overflow-hidden hover:shadow-lg transition-all duration-300 hover:-translate-y-1 flex flex-col"
      style={{
        backgroundImage: `url(${cardBg})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
      }}
    >
      {/* Overlay */}
      <div className="absolute inset-0 bg-gradient-to-br from-black/60 to-black/40 z-0" />

      {/* Content */}
      <div className="relative z-10 p-4 flex flex-col min-h-[180px] flex-1">
        <h2 className="text-3xl font-bold mb-1 text-white drop-shadow">
          {title}
        </h2>
        <p className="text-xs text-gray-100 mb-1 drop-shadow line-clamp-2">
          {description}
        </p>
        <p className="text-xs text-gray-200 mb-2">
          <b>Code:</b> {code}
        </p>
        <div className="mt-auto">
          <div className="flex items-center gap-2 mt-auto">
            <Avatar name={instructor.name} />
            <div>
              <p className="font-semibold text-white text-sm">
                {instructor.name}
              </p>
            </div>
          </div>
        </div>
      </div>

      {/* Button */}
      <button
        className="relative z-10 bg-lime-500 hover:bg-lime-600 text-black text-center py-2 font-semibold text-base cursor-pointer w-full transition-colors duration-200"
        onClick={isEnrolled ? handleView : handleEnroll}
      >
        {isEnrolled ? isLoading ? <Loader /> : "View" : "Enroll Now"}
      </button>
    </div>
  );
};

export default CourseCard;
