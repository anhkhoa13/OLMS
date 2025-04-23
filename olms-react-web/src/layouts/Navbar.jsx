import { UserCircleIcon, UserIcon } from "@heroicons/react/16/solid";
import { useState } from "react";
import { Link } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import { ArrowRightOnRectangleIcon } from "@heroicons/react/24/outline";
import Logo from "../components/Logo";
import DropDownLink from "../components/DropDownLink";
import Avatar from "../components/Avatar";

const userLogin = [
  { name: "Login", link: "/login" },
  { name: "Sign up", link: "/signup" },
];

function NavBar() {
  const { logout, isAuthenticated, currentUser, userRole } = useAuth();
  const navigate = useNavigate();
  const [tabOpen, setTabOpen] = useState(null);

  const handleLogout = () => {
    logout();
    navigate("/"); // Redirect to home page after logout
  };

  // Check if user has Instructor or Admin role
  const canCreateContent = userRole === "Instructor" || userRole === "Admin";

  // Create base menu items that all authenticated users can see
  let menuItems = [{ name: "Profile", link: "/profile" }];

  // Add instructor/admin specific menu items if user has appropriate role
  if (canCreateContent) {
    menuItems = [
      ...menuItems,
      { name: "Create Quiz", link: "/createQuiz" },
      { name: "Create Course", link: "/createCourse" },
    ];
  }

  // Add logout option for all authenticated users
  menuItems.push({
    name: "Logout",
    link: "#",
    onClick: handleLogout,
    icon: <ArrowRightOnRectangleIcon className="w-4 h-4 ml-2" />,
  });

  // Final user menu items based on authentication status
  const userMenuItems = isAuthenticated ? menuItems : userLogin;

  return (
    <nav className="bg-white shadow-md px-6 py-4 flex items-center justify-between z-50">
      {/* Left side: Logo */}
      <div className="flex items-center space-x-3">
        <Logo className="" />
      </div>

      {/* Right side: Links */}
      <div className="flex items-center space-x-10 text-gray-600 font-medium">
        <Link
          key="explore"
          to="/explore"
          className="px-6 py-3 hover:bg-gray-100 cursor-pointer transition-all duration-200"
        >
          ⭐ Explore
        </Link>
        <Link
          key="dashboard"
          to="/dashboard"
          className="px-6 py-3 hover:bg-gray-100 cursor-pointer transition-all duration-200"
        >
          📅 Calendar
        </Link>
        <Link
          key="courses"
          to="/courses"
          className="px-6 py-3 hover:bg-gray-100 cursor-pointer transition-all duration-200"
        >
          💻 My Courses
        </Link>
        <DropDownLink
          icon={<Avatar name={isAuthenticated ? currentUser?.fullName : ""} />}
          title="Người dùng"
          items={userMenuItems}
          tabOpen={tabOpen}
          setTabOpen={setTabOpen}
          isAlignLeft={false}
          dropDownWidth="w-40"
        />
      </div>
    </nav>
  );
}

export default NavBar;
