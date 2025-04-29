// Updated CourseNavBar.jsx
function CourseNavBar({ activeTab, setActiveTab }) {
  const tabs = [
    { id: "content", label: "Content", icon: "book" },
    { id: "announcements", label: "Announcements", icon: "megaphone" },
    { id: "forum", label: "Forum", icon: "chat" },
  ];

  const getIcon = (iconName) => {
    const iconProps = {
      className: "h-5 w-5 mr-2",
      fill: "none",
      viewBox: "0 0 24 24",
      stroke: "currentColor",
      strokeWidth: 2,
    };

    switch (iconName) {
      case "book":
        return (
          <svg xmlns="http://www.w3.org/2000/svg" {...iconProps}>
            <path d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
          </svg>
        );
      case "megaphone":
        return (
          <svg xmlns="http://www.w3.org/2000/svg" {...iconProps}>
            <path d="M11 5.882V19.24a1.76 1.76 0 01-3.417.592l-2.147-6.15M18 13a3 3 0 100-6M5.436 13.683A4.001 4.001 0 017 6h1.832c4.1 0 7.625-1.234 9.168-3v14c-1.543-1.766-5.067-3-9.168-3H7a3.988 3.988 0 01-1.564-.317z" />
          </svg>
        );
      case "chat":
        return (
          <svg xmlns="http://www.w3.org/2000/svg" {...iconProps}>
            <path d="M17 8h2a2 2 0 012 2v6a2 2 0 01-2 2h-2v4l-4-4H9a1.994 1.994 0 01-1.414-.586m0 0L11 14h4a2 2 0 002-2V6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2v4l.586-.586z" />
          </svg>
        );
      default:
        return null;
    }
  };

  return (
    <div className="flex border-b border-gray-200">
      {tabs.map((tab) => (
        <button
          key={tab.id}
          className={`flex-1 py-4 px-6 text-center font-medium text-sm focus:outline-none transition-all duration-200 cursor-pointer ${
            activeTab === tab.id
              ? "text-blue-600 border-b-2 border-blue-600 bg-blue-50/50"
              : "text-gray-500 hover:text-gray-700 hover:bg-gray-50"
          }`}
          onClick={() => setActiveTab(tab.id)}
        >
          <div className="flex items-center justify-center">
            {getIcon(tab.icon)}
            {tab.label}
          </div>
        </button>
      ))}
    </div>
  );
}

export default CourseNavBar;
