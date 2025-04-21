import React, { useState } from "react";

const mockData = [
  {
    date: "Tuesday, April 22, 2025",
    tasks: [
      {
        time: "12:00",
        title: "Report 1",
        description: "Due assignment · Mobile Programming (2+1)_ Group 02FIE",
      },
      {
        time: "13:04",
        title: "Assignment 7: To-do List App",
        description:
          "Submission deadline · Mobile Programming (2+1)_ Group 02FIE",
      },
    ],
  },
  {
    date: "Wednesday, April 23, 2025",
    tasks: [
      {
        time: "13:39",
        title: "Image Loader Project",
        description:
          "Submission deadline · Mobile Programming (2+1)_ Group 02FIE",
      },
    ],
  },
  {
    date: "Friday, April 25, 2025",
    tasks: [
      {
        time: "07:00",
        title: "Submit Presentation Feedback (Round 3)",
        description: "Due assignment · Software Design Patterns_ Group 01FIE",
      },
      {
        time: "07:00",
        title: "Submit Practical Exercise 05 (Group at home)",
        description: "Due assignment · Software Design Patterns_ Group 01FIE",
      },
    ],
  },
];

function AcademicPlanner() {
  const [searchTerm, setSearchTerm] = useState("");
  const [sortOrder, setSortOrder] = useState("asc");

  const parseDate = (dateString) => {
    return new Date(dateString);
  };

  const filteredAndSortedData = [...mockData]
    .map((day) => ({
      ...day,
      tasks: day.tasks.filter(
        (task) =>
          task.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
          task.description.toLowerCase().includes(searchTerm.toLowerCase())
      ),
    }))
    .filter((day) => day.tasks.length > 0)
    .sort((a, b) =>
      sortOrder === "asc"
        ? parseDate(a.date) - parseDate(b.date)
        : parseDate(b.date) - parseDate(a.date)
    );

  return (
    <div className="p-4 bg-white rounded-lg mt-6">
      <div className="flex flex-wrap gap-4 mb-6 items-center">
        <select
          value={sortOrder}
          onChange={(e) => setSortOrder(e.target.value)}
          className="border border-gray-300 px-3 py-1 rounded text-sm"
        >
          <option value="asc">Sort by date ↑</option>
          <option value="desc">Sort by date ↓</option>
        </select>
        <select
          value={""}
          onChange={""}
          className="border border-gray-300 px-3 py-1 rounded text-sm"
        >
          <option value="asc">Due</option>
          <option value="desc">Done</option>
        </select>

        <input
          type="text"
          placeholder="Search by activity type or name"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="border border-gray-300 px-3 py-1 rounded text-sm flex-1 min-w-[200px]"
        />
      </div>

      {filteredAndSortedData.map((day, idx) => (
        <div key={idx} className="mb-6">
          <h2 className="text-lg font-semibold text-gray-800 mb-2">
            {day.date}
          </h2>
          <ul className="divide-y divide-gray-200">
            {day.tasks.map((task, taskIdx) => (
              <li key={taskIdx} className="py-3">
                <div className="text-sm font-medium text-blue-600 mb-1">
                  {task.time} - {task.title}
                </div>
                <div className="text-sm text-gray-700">{task.description}</div>
              </li>
            ))}
          </ul>
        </div>
      ))}
    </div>
  );
}

export default AcademicPlanner;
