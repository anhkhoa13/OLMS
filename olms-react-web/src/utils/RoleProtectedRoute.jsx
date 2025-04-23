import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

const RoleProtectedRoute = ({ allowedRoles }) => {
  const { isAuthenticated, userRole, loading } = useAuth();
  const location = useLocation();

  // Show loading state while authentication is being checked
  if (loading) {
    return <div>Loading...</div>;
  }

  // First check if user is authenticated at all
  if (!isAuthenticated) {
    // Redirect to login page, preserving the intended destination
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Then check if user has the required role
  if (!allowedRoles.includes(userRole)) {
    // Redirect to unauthorized page
    return <Navigate to="/unauthorized" replace />;
  }

  // If authenticated and authorized, render the protected content
  return <Outlet />;
};

export default RoleProtectedRoute;
