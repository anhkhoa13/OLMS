import { DotLottieReact } from "@lottiefiles/dotlottie-react";
import { ArrowRightEndOnRectangleIcon } from "@heroicons/react/24/solid";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { useState } from "react";
import axios from "axios";
import { useAuth } from "../contexts/AuthContext";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function Login() {
  const navigate = useNavigate();
  // const location = useLocation();
  const { login } = useAuth();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [apiError, setApiError] = useState("");

  // Get the redirect path from location state or default to dashboard
  // const from = location.state?.from?.pathname || "/dashboard";

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      username: "",
      password: "",
      rememberMe: false,
    },
  });

  const onSubmit = async (data) => {
    setIsSubmitting(true);
    setApiError("");

    try {
      // Prepare login data
      const loginData = {
        username: data.username,
        password: data.password,
      };

      // Call login API
      const response = await axios.post(`${API_URL}/api/auth/login`, loginData);

      // Check if response contains token
      if (response.data && response.data.token) {
        // Use the login function from AuthContext
        const userData = login(response.data.token);

        // If remember me is checked, set a longer expiration
        if (data.rememberMe) {
          // This is handled by the token itself, but you could
          // store a flag to refresh the token automatically
          localStorage.setItem("rememberMe", "true");
        }

        console.log("Login successful:", userData);

        // Redirect to the page the user was trying to access, or dashboard
        // navigate(from, { replace: true });
        navigate("/dashboard", { replace: true });
      } else {
        throw new Error("No token received from server");
      }
    } catch (error) {
      console.error("Login error:", error);

      // Handle different types of errors
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        setApiError(
          error.response.data.message ||
            "Invalid username or password. Please try again."
        );
      } else if (error.request) {
        // The request was made but no response was received
        setApiError("No response from server. Please try again later.");
      } else {
        // Something happened in setting up the request that triggered an Error
        setApiError("An error occurred. Please try again.");
      }
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="flex items-center justify-center p-4 mt-4 mb-4">
      <div className="w-full max-w-6xl flex bg-white h-160">
        {/* left */}
        <div className="w-1/2 flex items-center justify-center p-4">
          <DotLottieReact
            src="https://lottie.host/8f909684-69fe-494c-8889-530849977796/c3tqofKscj.lottie"
            loop
            autoplay
            className="w-full h-160"
          />
        </div>
        {/* right */}
        <div className="w-1/2 p-8">
          <h2 className="text-3xl font-bold mb-2">Login</h2>
          <p className="text-gray-600 mb-6">
            Please enter your username and password to log in
          </p>

          {apiError && (
            <div className="mb-4 p-3 bg-red-100 text-red-700 rounded-md text-sm">
              {apiError}
            </div>
          )}

          <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
            <div>
              <input
                type="text"
                placeholder="Username"
                className={`w-full border-0 border-b-2 ${
                  errors.username ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 px-3 placeholder-gray-400`}
                {...register("username", {
                  required: "Username is required",
                })}
              />
              {errors.username && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.username.message}
                </p>
              )}
            </div>

            <div>
              <input
                type="password"
                placeholder="Password"
                className={`w-full border-0 border-b-2 ${
                  errors.password ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 px-3 placeholder-gray-400`}
                {...register("password", {
                  required: "Password is required",
                })}
              />
              {errors.password && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.password.message}
                </p>
              )}
            </div>

            <div className="flex items-center">
              <input
                type="checkbox"
                id="rememberMe"
                className="mr-2 rounded text-[#89b46c] focus:ring-[#89b46c]"
                {...register("rememberMe")}
              />
              <label htmlFor="rememberMe" className="text-sm text-gray-600">
                Remember me
              </label>
            </div>

            <div className="flex justify-between items-center">
              <button
                type="submit"
                disabled={isSubmitting}
                className={`text-white bg-[#89b46c] border-2 border-[#89b46c] px-6 py-3 rounded-xl flex items-center gap-2 hover:bg-white hover:text-[#89b46c] transition-colors duration-300 ${
                  isSubmitting ? "opacity-70 cursor-not-allowed" : ""
                }`}
              >
                {isSubmitting ? (
                  "Logging in..."
                ) : (
                  <>
                    <ArrowRightEndOnRectangleIcon className="w-5 h-5" />
                    Login
                  </>
                )}
              </button>
              <a href="#" className="hover:underline text-sm text-[#89b46c]">
                Forgot password?
              </a>
            </div>
          </form>

          <div className="mt-6">
            <p className="text-gray-600 text-sm">
              Don't have an account?{" "}
              <Link
                key="signup"
                to="/signup"
                className="text-[#89b46c] font-medium hover:underline"
              >
                Sign up now
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;
