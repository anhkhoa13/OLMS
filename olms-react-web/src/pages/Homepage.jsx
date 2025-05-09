import React from "react";
import { Link } from "react-router-dom";
import Courses from "../features/Course/Courses";
import bgImage from "../assets/images/bg_main.png";

const HomePage = () => {
  // const [quizCode, setQuizCode] = useState("");

  // const handleJoinQuiz = () => {
  //   if (quizCode.trim()) {
  //     // Placeholder logic — replace with real navigation/handler
  //     alert(`Joining quiz with code: ${quizCode}`);
  //   }
  // };

  return (
    <div className="min-h-screen">
      {/* Hero Section */}
      <div className="relative h-[80vh] flex items-center justify-center overflow-hidden">
        {/* Background image with blur effect */}
        <div
          className="absolute inset-0 z-0"
          style={{
            backgroundImage: `url(${bgImage})`,
            backgroundSize: "cover",
            backgroundPosition: "center",
            backgroundRepeat: "no-repeat",
            filter: "blur(2px)",
            opacity: "0.8",
            transform: "scale(1.1)", // Slightly scale up to avoid blur edges
          }}
        ></div>

        {/* Optional semi-transparent overlay (reduced opacity) */}
        <div className="absolute inset-0 bg-opacity-10 z-10"></div>

        {/* Content */}
        <div className="text-center px-4 md:px-8 lg:px-16 relative z-20">
          <h1 className="text-4xl md:text-6xl font-bold text-black mb-4 drop-shadow-lg">
            Welcome to Our OLMS Platform
          </h1>
          <p className="text-lg md:text-2xl text-black max-w-2xl mx-auto">
            Unlock your language potential with our personalized and interactive
            courses.
          </p>
        </div>
      </div>

      {/* Why Use Our System Section */}
      <div className="py-20 px-4 md:px-8 lg:px-16 bg-gray-50">
        <h2 className="text-3xl md:text-5xl font-extrabold text-black-600 text-center mb-12">
          Why Choose Our Platform?
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 text-center">
          <div className="p-6 bg-white rounded-2xl shadow-md hover:shadow-lg hover:shadow-[#89b46c] transition">
            <div className="text-[#89b46c] text-4xl mb-4">🎯</div>
            <h3 className="text-2xl font-bold text-[#89b46c] mb-2">
              Interactive Learning
            </h3>
            <p className="text-gray-700">
              Engage with multimedia content, quizzes, and real-time feedback to
              boost your understanding.
            </p>
          </div>
          <div className="p-6 bg-white rounded-2xl shadow-md hover:shadow-lg hover:shadow-[#89b46c] transition">
            <div className="text-[#89b46c] text-4xl mb-4">👩‍🏫</div>
            <h3 className="text-2xl font-bold text-[#89b46c] mb-2">
              Expert Instructors
            </h3>
            <p className="text-gray-700">
              Learn from certified professionals with years of teaching
              experience in language education.
            </p>
          </div>
          <div className="p-6 bg-white rounded-2xl shadow-md hover:shadow-lg hover:shadow-[#89b46c] transition">
            <div className="text-[#89b46c] text-4xl mb-4">📱</div>
            <h3 className="text-2xl font-bold text-[#89b46c] mb-2">
              Flexible Access
            </h3>
            <p className="text-gray-700">
              Study anytime, anywhere with our mobile-friendly and intuitive
              platform.
            </p>
          </div>
        </div>
      </div>

      {/* Courses Section */}
      <Courses isEnroll={false} maxNoDisplay={3} title="OUR POPULAR COURSES" />

      {/* Quiz Section */}
    </div>
  );
};

export default HomePage;
