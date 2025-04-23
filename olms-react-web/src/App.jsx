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

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<Homepage />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/courses" element={<Courses isEnrolled={true} />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/createQuiz" element={<CreateQuiz />} />
          <Route path="/quiz/:code" element={<Quiz />} />
          <Route path="/createCourse" element={<CreateCourse />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
