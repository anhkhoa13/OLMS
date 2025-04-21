import AcademicPlanner from "../components/AcademicPlanner";
import UserCalendar from "../components/UserCalendar";

function Dashboard() {
  return (
    <div className="flex flex-col justify-center min-h-screen bg-gray-100 px-32">
      <AcademicPlanner />
      <UserCalendar />
    </div>
  );
}

export default Dashboard;
