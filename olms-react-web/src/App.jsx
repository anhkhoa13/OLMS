import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./layouts/Layout";
import Homepage from "./pages/Homepage";
import Login from "./pages/Login";
import About from "./pages/About";
import Contact from "./pages/Contact";
import Courses from "./pages/Courses";
import Signup from "./pages/Signup";
import Dashboard from "./pages/Dashboard.jsx";
import Quiz from "./features/Quiz/Quiz";
import CreateQuiz from "./features/CreateQuiz/CreateQuiz.jsx";
import CreateCourse from "./features/Course/CreateCourse.jsx";
import { AuthProvider } from "./contexts/AuthContext";
import RoleProtectedRoute from "./utils/RoleProtectedRoute.jsx";
import Unauthorized from "./pages/Unauthorized.jsx";

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route element={<Layout />}>
            {/* Public routes */}
            <Route path="/" element={<Homepage />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact" element={<Contact />} />
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
              <Route path="/courses" element={<Courses isEnrolled={true} />} />
            </Route>

            {/* Instructor-only routes */}
            <Route
              element={<RoleProtectedRoute allowedRoles={["Instructor"]} />}
            >
              <Route path="/createQuiz" element={<CreateQuiz />} />
              <Route path="/createCourse" element={<CreateCourse />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
