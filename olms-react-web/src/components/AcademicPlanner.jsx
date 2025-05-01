import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import { QuizIcon, ExerciseIcon } from "../components/Icons";
import { useAuth } from "../contexts/AuthContext";

function AcademicPlanner() {
  const [data, setData] = useState([]);
  const [sortMode, setSortMode] = useState("course"); // 'course' or 'date'
  const [sortOrder, setSortOrder] = useState("asc");
  const [searchTerm, setSearchTerm] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isSorting, setIsSorting] = useState(false);
  const { currentUser } = useAuth();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          `https://localhost:7212/api/student/${currentUser.id}/dashboard`
        );
        setData(response.data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const handleSortChange = (e) => {
    setIsSorting(true);
    setSortMode(e.target.value);
    setTimeout(() => setIsSorting(false), 300);
  };

  function getLink(type, courseId, itemId) {
    console.log(type, courseId, itemId);
    if (type === "lesson") {
      return `/courses/${courseId}/lesson/${itemId}`;
    }
    if (type === "quiz") {
      var code = itemId.slice(0, 6);
      return `/courses/${courseId}/quiz/${code}`;
    }
    if (type === "exercise") {
      return `/courses/${courseId}/assignment/${itemId}`;
    }
    return `/course/${itemId}`;
  }

  const parseDate = (dateString) => new Date(dateString);

  // Flatten all assignments across courses
  const allAssignments = data.flatMap((course) =>
    course.assignments.map((assignment) => ({
      ...assignment,
      courseId: course.courseId,
      courseTitle: course.title,
      courseCode: course.code,
    }))
  );
  const allAnnouncements = data.flatMap((course) =>
    (course.announcements || []).map((ann) => ({
      ...ann,
      courseTitle: course.title,
      courseCode: course.code,
      courseId: course.courseId,
    }))
  );

  // Group assignments by date
  const groupByDate = (assignments) => {
    const grouped = {};
    assignments.forEach((assignment) => {
      const date = parseDate(assignment.dueDate).toLocaleDateString("en-US", {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "numeric",
      });
      if (!grouped[date]) grouped[date] = [];
      grouped[date].push(assignment);
    });
    return grouped;
  };

  // Sort functions
  const sortAssignments = (a, b) =>
    sortOrder === "asc"
      ? parseDate(a.dueDate) - parseDate(b.dueDate)
      : parseDate(b.dueDate) - parseDate(a.dueDate);

  const filteredData = allAssignments.filter(
    (assignment) =>
      assignment.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      assignment.courseTitle.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const sortedData = [...filteredData].sort(sortAssignments);

  // Grouped data for different sort modes
  const groupedData =
    sortMode === "course"
      ? data
          .sort((a, b) =>
            sortOrder === "asc"
              ? a.title.localeCompare(b.title)
              : b.title.localeCompare(a.title)
          )
          .map((course) => ({
            ...course,
            assignments: course.assignments.sort(sortAssignments),
          }))
      : Object.entries(groupByDate(sortedData)).map(([date, tasks]) => ({
          date,
          tasks: tasks.sort(sortAssignments).map((task) => ({
            ...task,
            // Explicitly include courseId (already present but shown for clarity)
            courseId: task.courseId,
            courseTitle: task.courseTitle,
            courseCode: task.courseCode,
          })),
        }));

  console.log(groupedData);

  if (loading) return <div className="p-4 text-center">Loading...</div>;
  if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

  return (
    <div className="p-4 bg-white rounded-lg mt-6">
      <div className="flex flex-wrap gap-4 mb-6 items-center">
        <select
          value={sortMode}
          onChange={(e) => handleSortChange(e)}
          className="border border-gray-300 px-3 py-1 rounded text-sm"
        >
          <option value="course">Sort by Course</option>
          <option value="date">Sort by Date</option>
        </select>

        <select
          value={sortOrder}
          onChange={(e) => setSortOrder(e.target.value)}
          className="border border-gray-300 px-3 py-1 rounded text-sm"
        >
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>

        <input
          type="text"
          placeholder="Search by course or assignment"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="border border-gray-300 px-3 py-1 rounded text-sm flex-1 min-w-[200px]"
        />
      </div>

      <div
        className={`transition-opacity duration-300 ${
          isSorting ? "opacity-50" : "opacity-100"
        }`}
      >
        {sortMode === "course"
          ? groupedData.map((course) => (
              <div
                key={course.courseId}
                className="mb-8 transition-all duration-300 transform hover:scale-[1.005]"
              >
                <h2 className="text-xl font-semibold text-gray-800 mb-4">
                  {course.title} ({course.code})
                </h2>
                <ul className="divide-y divide-gray-200">
                  {course.assignments.map((assignment) => (
                    <li
                      key={assignment.id}
                      className="py-3 group hover:bg-gray-50 transition-colors duration-200"
                    >
                      <div className="flex items-center justify-between">
                        <div className="flex items-center space-x-3">
                          {assignment.type === "quiz" ? (
                            <QuizIcon className="w-5 h-5 text-blue-500" />
                          ) : (
                            <ExerciseIcon className="w-5 h-5 text-green-500" />
                          )}
                          <div>
                            <div className="text-sm font-medium text-blue-600 mb-1">
                              {assignment.title} · {assignment.type}
                            </div>
                            <div className="text-sm text-gray-700">
                              Due{" "}
                              {new Date(assignment.dueDate).toLocaleString(
                                "en-US"
                              )}
                            </div>
                          </div>
                        </div>
                        <Link
                          to={getLink(
                            assignment.type,
                            course.courseId,
                            assignment.id
                          )}
                          className="opacity-0 group-hover:opacity-100 transition-opacity duration-200 
                                 px-3 py-1 bg-[#b9d6a1bb] hover:bg-[#6f8f54] rounded-lg text-sm mr-4"
                        >
                          View
                        </Link>
                      </div>
                    </li>
                  ))}
                </ul>
              </div>
            ))
          : groupedData.map((group) => (
              <div
                key={group.date}
                className="mb-6 transition-all duration-300 transform hover:scale-[1.005]"
              >
                <h2 className="text-lg font-semibold text-gray-800 mb-2">
                  {group.date}
                </h2>
                <ul className="divide-y divide-gray-200">
                  {group.tasks.map((assignment) => (
                    <li
                      key={assignment.id}
                      className="py-3 group hover:bg-gray-50 transition-colors duration-200"
                    >
                      <div className="flex items-center justify-between">
                        <div className="flex items-center space-x-3">
                          {assignment.type === "quiz" ? (
                            <QuizIcon className="w-5 h-5 text-blue-500" />
                          ) : (
                            <ExerciseIcon className="w-5 h-5 text-green-500" />
                          )}
                          <div>
                            <div className="text-sm font-medium text-blue-600 mb-1">
                              {parseDate(assignment.dueDate).toLocaleTimeString(
                                [],
                                {
                                  hour: "2-digit",
                                  minute: "2-digit",
                                }
                              )}{" "}
                              - {assignment.title} ({assignment.courseCode})
                            </div>
                            <div className="text-sm text-gray-700">
                              {assignment.type} · {assignment.courseTitle}
                            </div>
                          </div>
                        </div>
                        <Link
                          to={getLink(
                            assignment.type,
                            assignment.courseId,
                            assignment.id
                          )}
                          className="opacity-0 group-hover:opacity-100 transition-opacity duration-200 
                                 px-3 py-1 bg-[#b9d6a1bb] hover:bg-[#6f8f54] rounded-lg text-sm mr-4"
                        >
                          View
                        </Link>
                      </div>
                    </li>
                  ))}
                </ul>
              </div>
            ))}
      </div>

      <div className="mt-10">
        <h2 className="text-xl font-bold text-gray-800 mb-4">Announcements</h2>
        <div className="bg-white rounded-lg shadow p-4">
          {allAnnouncements.length === 0 ? (
            <div className="text-gray-400 text-sm">No announcements.</div>
          ) : (
            <ul className="space-y-4">
              {allAnnouncements
                .sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt))
                .map((ann) => (
                  <li key={ann.id} className="pb-2 border-b last:border-b-0">
                    <div className="flex justify-between items-center">
                      <div>
                        <div className="font-semibold text-gray-800">
                          {ann.title}
                        </div>
                        <div className="text-gray-700">{ann.content}</div>
                        <div className="text-xs text-gray-400">
                          {" "}
                          post on &nbsp;
                          {new Date(ann.createdAt).toLocaleString()}
                        </div>
                      </div>
                      <div className="ml-4 text-xs text-gray-500 italic">
                        {ann.courseTitle} ({ann.courseCode})
                      </div>
                    </div>
                  </li>
                ))}
            </ul>
          )}
        </div>
      </div>
    </div>
  );
}

export default AcademicPlanner;
