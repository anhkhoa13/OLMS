// src/contexts/AuthContext.jsx
import { createContext, useState, useEffect, useContext } from "react";
import { jwtDecode } from "jwt-decode"; // You'll need to install this: npm install jwt-decode
import axios from "axios";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [currentUser, setCurrentUser] = useState(null);
  const [userRole, setUserRole] = useState(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Check if user is logged in on initial load
    const initAuth = () => {
      const token = localStorage.getItem("token");
      if (token) {
        try {
          // Verify and decode the token
          const decodedToken = jwtDecode(token);
          console.log("Decoded token:", decodedToken);

          // Check if token is expired
          const currentTime = Date.now() / 1000;
          if (decodedToken.exp && decodedToken.exp < currentTime) {
            // Token is expired
            logout();
            return;
          }

          // Extract user info from token
          const userData = {
            id: decodedToken.nameid, // ClaimTypes.NameIdentifier
            NamedNodeMap: decodedToken.name, // ClaimTypes.Name
            role: decodedToken.role, // ClaimTypes.Role
          };

          setCurrentUser(userData);
          setUserRole(userData.role);
          setIsAuthenticated(true);

          // Set up axios auth header for all future requests
          setupAxiosInterceptors(token);
        } catch (error) {
          console.error("Error parsing auth token:", error);
          logout();
        }
      }
      setLoading(false);
    };

    initAuth();
  }, []);

  const login = (token) => {
    try {
      // Decode the token
      const decodedToken = jwtDecode(token);
      console.log(token);

      // Extract user info
      const userData = {
        id: decodedToken.nameid, // ClaimTypes.NameIdentifier
        fullName: decodedToken.unique_name, // ClaimTypes.Name
        role: decodedToken.role, // ClaimTypes.Role
      };

      // Save token to localStorage
      localStorage.setItem("token", token);

      // Update state
      setCurrentUser(userData);
      setUserRole(userData.role);
      setIsAuthenticated(true);

      // Set up axios auth header
      setupAxiosInterceptors(token);

      return userData;
    } catch (error) {
      console.error("Error during login:", error);
      throw new Error("Authentication failed");
    }
  };

  const logout = () => {
    // Clear localStorage
    localStorage.removeItem("token");

    // Reset state
    setCurrentUser(null);
    setUserRole(null);
    setIsAuthenticated(false);

    // Clear axios auth header
    setupAxiosInterceptors(null);
  };

  const setupAxiosInterceptors = (token) => {
    if (token) {
      // Set default auth header for all requests
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
      // Remove auth header
      delete axios.defaults.headers.common["Authorization"];
    }
  };

  return (
    <AuthContext.Provider
      value={{
        currentUser,
        userRole,
        isAuthenticated,
        loading,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);

export default AuthContext;
