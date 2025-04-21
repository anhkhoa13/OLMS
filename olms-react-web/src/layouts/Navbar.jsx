// import { UserCircleIcon, UserIcon } from "@heroicons/react/16/solid";
import { useState } from "react";
import Logo from "../components/Logo";
import DropDownLink from "../components/DropDownLink";
import Avatar from "../components/Avatar";
import { Link } from "react-router-dom";

const UserLogin = [
  { name: "Login", link: "/login" },
  { name: "Sign up", link: "/signup" },
];

function NavBar() {
  const [tabOpen, setTabOpen] = useState(null);

  return (
    <nav className="bg-white shadow-md px-6 py-4 flex items-center justify-between z-50">
      {/* Left side: Logo */}
      <div className="flex items-center space-x-3">
        <Logo className="" />
      </div>

      {/* Right side: Links */}
      <div className="flex items-center space-x-10 text-gray-600 font-medium">
        <Link
          key="home"
          to="/about"
          className="px-6 py-3 hover:bg-gray-100 cursor-pointer transition-all duration-200"
        >
          © About
        </Link>
        <Link
          key="contact"
          to="/contact"
          className="px-6 py-3 hover:bg-gray-100 cursor-pointer transition-all duration-200"
        >
          📱 Contact
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
          icon={<Avatar name={"Hoang"} />}
          title="Người dùng"
          items={UserLogin}
          tabOpen={tabOpen}
          setTabOpen={setTabOpen}
          isAlignLeft={false}
          dropDownWidth="w-36"
        />
      </div>
    </nav>
  );
}

export default NavBar;
