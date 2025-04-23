import React from "react";
import Options from "./Options";

function Question({ question, userAnswer, onAnswer }) {
  const { content, options, type } = question;

  return (
    <div className="max-w-2xl mx-auto bg-white p-6 rounded-2xl shadow-md space-y-6">
      <h3 className="text-xl font-semibold text-gray-800">{content}</h3>

      {type === "MultipleChoiceQuestion" && (
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          {options.map((option, index) => (
            <Options
              key={index}
              label={option}
              index={index}
              selected={userAnswer === index} // 'userAnswer' should be the index of the selected option
              onSelect={() => onAnswer(index)} // Return the option index as the answer
            />
          ))}
        </div>
      )}

      {type === "ShortAnswerQuestion" && (
        <input
          type="text"
          className="w-full border rounded-lg p-3"
          placeholder="Type your answer..."
          value={userAnswer || ""}
          onChange={(e) => onAnswer(e.target.value)} // Return the value as the answer
        />
      )}
    </div>
  );
}

export default Question;
