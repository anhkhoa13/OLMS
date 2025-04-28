import { Outlet, useParams } from "react-router-dom";
import CourseSidebar from "../features/Course/CourseSidebarNav/CourseSidebar";
import NavBar from "./Navbar";
import Footer from "./Footer";

const courseContentData = {
  id: "123",
  title: "React for Beginners",
  description: "React for Beginners",
  instructor: "Dr. John Paul",
  status: "In progress",
  sections: [
    {
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
function CourseLayout() {
  const { courseId } = useParams();
  return (
    <div className="flex flex-col h-screen">
      <NavBar />
      <CourseSidebar course={courseContentData} courseId={courseId} />
      <main className="flex-grow relative bg-gray-100">
        <Outlet />
      </main>
      <Footer />
    </div>
  );
}

export default CourseLayout;
