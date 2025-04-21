import React from "react";

const Loader = ({ size = "md", fullscreen = false }) => {
  const sizes = {
    sm: "h-4 w-4 border-2",
    md: "h-8 w-8 border-4",
    lg: "h-12 w-12 border-4",
  };

  const spinnerSize = sizes[size] || sizes["md"];

  return (
    <div
      className={`flex items-center justify-center ${
        fullscreen ? "fixed inset-0 bg-white z-50" : ""
      }`}
    >
      <div
        className={`rounded-full border-t-transparent border-blue-500 animate-spin ${spinnerSize}`}
        role="status"
        aria-label="Loading..."
      ></div>
    </div>
  );
};

export default Loader;
