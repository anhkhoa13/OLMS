import React from "react";
import Options from "./Options";

function Question({ question, selectedOption, onSelect }) {
  const { content, options } = question;

  return (
    <div className="max-w-2xl mx-auto bg-white p-6 rounded-2xl shadow-md space-y-6">
      <h3 className="text-xl font-semibold text-gray-800">{content}</h3>

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        {options.map((option, index) => (
          <Options
            key={index}
            label={option}
            index={index}
            selected={selectedOption === index}
            onSelect={onSelect}
          />
        ))}
      </div>
    </div>
  );
}

export default Question;
