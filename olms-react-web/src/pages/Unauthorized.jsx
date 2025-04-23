import { useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

function Unauthorized() {
  const navigate = useNavigate();
  const { userRole } = useAuth();

  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100 p-4">
      <div className="bg-white p-8 rounded-lg shadow-md max-w-md w-full text-center">
        <h1 className="text-2xl font-bold text-red-600 mb-4">Access Denied</h1>
        <p className="text-gray-700 mb-6">
          You don't have permission to access this page. This area is restricted
          to users with higher privileges.
        </p>
        <p className="text-gray-600 mb-6">
          Your current role: <span className="font-semibold">{userRole}</span>
        </p>
        <div className="space-y-3">
          <button
            onClick={() => navigate(-1)}
            className="w-full bg-gray-200 hover:bg-gray-300 text-gray-800 font-medium py-2 px-4 rounded transition duration-200"
          >
            Go Back
          </button>
          <button
            onClick={() => navigate("/")}
            className="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded transition duration-200"
          >
            Go to Home Page
          </button>
        </div>
      </div>
    </div>
  );
}
export default Unauthorized;
