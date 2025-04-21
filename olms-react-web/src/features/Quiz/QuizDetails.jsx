import React from "react";
import { CalendarDays, Timer, Info, Repeat } from "lucide-react";
import { format } from "date-fns";

function QuizDetails({ quiz, onStart }) {
  if (!quiz) return null;

  const {
    title,
    description,
    startTime,
    endTime,
    isTimeLimited,
    timeLimit,
    numberOfAttempts,
    code: { value: quizCode },
    questions,
  } = quiz;

  return (
    <div className="bg-white shadow-md rounded-lg p-6 max-w-xl mx-auto space-y-4">
      <h2 className="text-2xl font-bold text-[#333]">{title}</h2>

      <p className="text-gray-700">{description}</p>

      <div className="text-sm space-y-2 text-gray-600">
        <div className="flex items-center gap-2">
          <Info className="w-4 h-4 text-blue-500" />
          <span>
            <strong>Quiz Code:</strong> {quizCode}
          </span>
        </div>

        <div className="flex items-center gap-2">
          <CalendarDays className="w-4 h-4 text-green-500" />
          <span>
            <strong>Start:</strong> {format(new Date(startTime), "PPpp")} —{" "}
            <strong>End:</strong> {format(new Date(endTime), "PPpp")}
          </span>
        </div>

        <div className="flex items-center gap-2">
          <Timer className="w-4 h-4 text-red-500" />
          <span>
            {isTimeLimited
              ? `Time-limited: ${timeLimit} minutes`
              : "No time limit"}
          </span>
        </div>

        <div className="flex items-center gap-2">
          <Repeat className="w-4 h-4 text-purple-500" />
          <span>
            <strong>Attempts Allowed:</strong> {numberOfAttempts}
          </span>
        </div>

        <div className="text-sm">
          <strong>Total Questions:</strong> {questions.length}
        </div>
      </div>

      <button
        onClick={onStart}
        className="mt-4 w-full bg-[#89b46c] hover:bg-[#76a259] text-white py-2 px-4 rounded transition"
      >
        Start Quiz
      </button>
    </div>
  );
}

export default QuizDetails;
