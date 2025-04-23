import React from "react";
import { Link } from "react-router";

function DropDownLink({
  title,
  items,
  tabOpen,
  setTabOpen,
  icon = null,
  isAlignLeft = true,
  dropDownWidth = "w-64",
}) {
  const toggleDropdown = () => {
    setTabOpen((prev) => (prev === title ? null : title));
  };

  const handleLinkClick = () => {
    setTabOpen(null);
  };

  return (
    <div className="relative inline-block text-left ms-auto pr-16">
      <div
        onClick={toggleDropdown}
        className={`flex items-center justify-center text-base font-medium text-gray-700 px-6 py-3 hover:bg-gray-100 cursor-pointer transition-all duration-200 relative
          ${
            tabOpen === title
              ? "after:absolute after:left-6 after:right-6 after:bottom-0 after:h-1 after:bg-green-200 after:rounded-full"
              : "after:absolute after:left-6 after:right-6 after:bottom-0 after:h-0 after:bg-green-200 after:rounded-full"
          }
        `}
      >
        {icon ? icon : title}
      </div>

      <div
        className={`absolute ${
          isAlignLeft ? "left" : "right"
        }-0 mt-3 ${dropDownWidth} bg-white border-2 border-gray-100 rounded-lg shadow-lg z-10 transition-all duration-200 origin-top
          ${
            tabOpen === title
              ? "opacity-100 scale-100"
              : "opacity-0 scale-95 pointer-events-none"
          }
        `}
      >
        {items.map((item) => (
          <Link
            key={item.name}
            to={item.link}
            onClick={handleLinkClick}
            className="block px-6 py-3 text-base text-gray-700 hover:bg-gray-100"
          >
            {item.name}
          </Link>
        ))}
      </div>
    </div>
  );
}

export default DropDownLink;
