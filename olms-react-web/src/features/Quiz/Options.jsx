import React from "react";

function Options({ label, index, selected, onSelect }) {
  return (
    <label
      className={`block p-4 rounded-xl border cursor-pointer transition-all duration-200 ${
        selected
          ? "border-[#89b46c] bg-[#f1f9ee]"
          : "border-gray-300 hover:border-[#89b46c] hover:bg-[#f8fdf5]"
      }`}
    >
      <input
        type="radio"
        value={index}
        checked={selected}
        onChange={() => onSelect(index)}
        className="hidden"
      />
      {label}
    </label>
  );
}

export default Options;
