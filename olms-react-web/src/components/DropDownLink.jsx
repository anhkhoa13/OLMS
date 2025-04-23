import React from "react";
import { Link } from "react-router-dom";

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

  const handleItemClick = (item) => {
    // If the item has a custom onClick handler, use it
    if (item.onClick) {
      item.onClick();
    }

    // Close the dropdown
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
        {items.map((item) =>
          item.onClick ? (
            // For items with custom onClick handlers (like logout)
            <button
              key={item.name}
              onClick={() => handleItemClick(item)}
              className="block w-full text-left px-6 py-3 text-base text-gray-700 hover:bg-gray-100 flex items-center justify-between"
            >
              <span>{item.name}</span>
              {item.icon && item.icon}
            </button>
          ) : (
            // For regular navigation links
            <Link
              key={item.name}
              to={item.link}
              onClick={() => handleItemClick(item)}
              className="block px-6 py-3 text-base text-gray-700 hover:bg-gray-100"
            >
              {item.name}
            </Link>
          )
        )}
      </div>
    </div>
  );
}

export default DropDownLink;
