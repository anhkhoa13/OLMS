import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./layouts/Layout";
import Homepage from "./pages/Homepage";
import Explore from "./pages/Explore";
import Login from "./pages/Login";
import Courses from "./features/Course/Courses.jsx";
import Signup from "./pages/Signup";
import Dashboard from "./pages/Dashboard.jsx";
import Quiz from "./features/Quiz/Quiz";
import CreateQuiz from "./features/CreateQuiz/CreateQuiz.jsx";
import AddCourse from "./features/Course/AddCourse.jsx";
import { AuthProvider } from "./contexts/AuthContext";
import RoleProtectedRoute from "./utils/RoleProtectedRoute.jsx";
import Unauthorized from "./pages/Unauthorized.jsx";
import CourseView from "./features/Course/CourseView.jsx";

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route element={<Layout />}>
            {/* Public routes */}
            <Route path="/" element={<Homepage />} />
            <Route path="/explore" element={<Explore />} />
            <Route path="/login" element={<Login />} />
            <Route path="/signup" element={<Signup />} />
            <Route path="/unauthorized" element={<Unauthorized />} />

            {/* Routes for all authenticated users */}
            <Route
              element={
                <RoleProtectedRoute
                  allowedRoles={["Student", "Instructor", "Admin"]}
                />
              }
            >
              <Route path="/quiz/:code" element={<Quiz />} />
              <Route path="/dashboard" element={<Dashboard />} />
              {/* <Route path="/profile" element={<Profile />} /> */}
              <Route
                path="/courses"
                element={<Courses isEnroll={true} title="Your courses" />}
              />
              <Route path="/courses/:courseCode" element={<CourseView />} />
            </Route>

            {/* Instructor-only routes */}
            <Route
              element={<RoleProtectedRoute allowedRoles={["Instructor"]} />}
            >
              <Route path="/createQuiz" element={<CreateQuiz />} />
              <Route path="/createCourse" element={<AddCourse />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
