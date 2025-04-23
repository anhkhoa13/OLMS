import { Reac, useState } from "react";
import Avatar from "./Avatar";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { Loader } from "lucide-react";

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
            // Add any auth headers if needed, e.g.:
            // 'Authorization': `Bearer ${currentUser.token}`
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
    console.log("handleView");
  }

  return (
    <div className="w-full bg-gradient-to-br from-blue-50 to-purple-50 p-6 rounded-xl border border-gray-200 hover:shadow-md transition-all duration-300 hover:-translate-y-1">
      {/* // <div className="bg-white text-black rounded-2xl shadow-md hover:shadow-xl transition-all duration-300 flex flex-col justify-between"> */}
      <div className="p-6">
        <h3 className="text-2xl font-bold mb-2 text-[#6d8f56]">{title}</h3>
        <p className="text-sm text-gray-600 mb-2">{description}</p>
        <p className="text-sm text-gray-600 mb-4">
          <b>Course code:</b> {code}
        </p>
        <div className="flex items-center gap-4 mt-auto">
          {/* <img
            src={instructor.avatar || "https://via.placeholder.com/48"}
            alt={instructor.name}
            className="w-12 h-12 rounded-full object-cover border"
          /> */}
          <Avatar name={instructor.name} />
          <div>
            <p className="font-semibold">{instructor.name}</p>
            <p className="text-xs text-gray-500">Instructor</p>
          </div>
        </div>
      </div>
      <button
        className="bg-lime-500 hover:bg-lime-600 text-black text-center py-3 font-semibold text-lg rounded-b-2xl cursor-pointer w-full"
        onClick={isEnrolled ? handleView : handleEnroll}
      >
        {isEnrolled ? isLoading ? <Loader /> : "View" : "Enroll Now"}
      </button>
    </div>
  );
};

export default CourseCard;
