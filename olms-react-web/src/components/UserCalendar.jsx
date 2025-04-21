import { useState } from "react";
import { Calendar, dateFnsLocalizer } from "react-big-calendar";
import format from "date-fns/format";
import parse from "date-fns/parse";
import startOfWeek from "date-fns/startOfWeek";
import getDay from "date-fns/getDay";
import enUS from "date-fns/locale/en-US";
import "react-big-calendar/lib/css/react-big-calendar.css";

// Add custom styles for hover effect
import "../assets/css/calendarStyles.css";

const locales = {
  "en-US": enUS,
};

const localizer = dateFnsLocalizer({
  format,
  parse,
  startOfWeek,
  getDay,
  locales,
});

function UserCalendar({ initialEvents = [] }) {
  const [events, setEvents] = useState(initialEvents);
  const [view, setView] = useState("month");
  const [selectedDay, setSelectedDay] = useState(null);

  const handleNavigate = (date) => {
    console.log("Navigating to:", date);
  };

  const handleViewChange = (newView) => {
    setView(newView);
    console.log("Changed view to:", newView);
  };

  const handleEventSelection = (event) => {
    console.log("Event selected:", event);
  };

  const handleAddEvent = () => {
    const newEvent = {
      title: "New Event",
      start: new Date(),
      end: new Date(),
    };
    setEvents([...events, newEvent]);
  };

  const handleDayClick = (slotInfo) => {
    setSelectedDay(slotInfo.start); // Set the selected day to show its details
  };

  return (
    <div className="flex flex-col justify-center items-center min-h-screen bg-gray-100">
      <button
        onClick={handleAddEvent}
        className="bg-blue-500 text-white px-4 py-2 rounded-full mb-4 self-start"
      >
        Add Event
      </button>
      <div className="w-full max-w-7xl bg-white rounded-xl shadow-lg p-4">
        <h2 className="text-2xl font-semibold text-gray-800 mb-4">
          Your Calendar
        </h2>
        <div className="relative h-full" style={{ height: 500 }}>
          <Calendar
            localizer={localizer}
            events={events}
            startAccessor="start"
            endAccessor="end"
            onNavigate={handleNavigate}
            onView={handleViewChange}
            onSelectEvent={handleEventSelection}
            onSelectSlot={handleDayClick} // Handle day cell clicks
            view={view}
            selectable={true} // Allow slot selection
            style={{
              "--rbc-today-bg": "#89b46c33",
              "--rbc-selected-bg": "#89b46c",
            }}
          />
        </div>
      </div>

      {/* Modal to display details of the clicked day */}
      {selectedDay && (
        <div className="fixed inset-0 bg-gray-800 bg-opacity-50 flex justify-center items-center">
          <div className="bg-white p-6 rounded-xl shadow-xl max-w-sm w-full">
            <h3 className="text-xl font-semibold mb-2">Day Details</h3>
            <p className="text-gray-700">
              You clicked on: {format(selectedDay, "MMMM dd, yyyy")}
            </p>
            <button
              onClick={() => setSelectedDay(null)}
              className="bg-red-500 text-white px-4 py-2 rounded-full mt-4"
            >
              Close
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default UserCalendar;
