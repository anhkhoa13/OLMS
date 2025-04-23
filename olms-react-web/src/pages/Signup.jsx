import { DotLottieReact } from "@lottiefiles/dotlottie-react";
import { UserPlusIcon } from "@heroicons/react/24/solid";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { useState } from "react";
import axios from "axios";

const API_URL = import.meta.env.VITE_BACKEND_URL;

// Role enum values matching the backend
const Role = {
  Admin: 0,
  Instructor: 1,
  Student: 2,
};

function Signup() {
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [apiError, setApiError] = useState("");

  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
  } = useForm({
    defaultValues: {
      username: "",
      fullName: "",
      email: "",
      age: "",
      password: "",
      confirmPassword: "",
      role: Role.Student.toString(), // Default to Student
      terms: false,
    },
  });

  // Get the current value of password for comparison
  const password = watch("password");

  const onSubmit = async (data) => {
    setIsSubmitting(true);
    setApiError("");

    try {
      // Prepare the data for the API according to RegisterUserCommand
      const signupData = {
        username: data.username,
        password: data.password,
        fullName: data.fullName,
        email: data.email,
        age: parseInt(data.age, 10), // Convert string to integer
        role: parseInt(data.role, 10), // Convert string to integer
      };

      // Call your signup API
      const response = await axios.post(
        `${API_URL}/api/auth/register`,
        signupData
      );

      // Handle successful signup
      console.log("Signup successful:", response.data);

      // Redirect to login page
      navigate("/login");
    } catch (error) {
      // Handle API errors
      console.error("Signup error:", error);
      setApiError(
        error.response?.data?.message ||
          "An error occurred during registration. Please try again."
      );
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="flex items-center justify-center p-4 mt-4 mb-4">
      <div className="w-full max-w-6xl flex bg-white h-180">
        {/* Left side - Animation */}
        <div className="w-1/2 flex items-center justify-center p-4">
          <DotLottieReact
            src="https://lottie.host/8f909684-69fe-494c-8889-530849977796/c3tqofKscj.lottie"
            loop
            autoplay
            className="w-full h-160"
          />
        </div>

        {/* Right side - Form */}
        <div className="w-1/2 p-8 flex flex-col justify-center">
          <div className="mb-4">
            <h2 className="text-3xl font-bold text-gray-800 mb-2 mt-2">
              Create Account
            </h2>
            <p className="text-gray-600">Join our learning community today</p>
          </div>

          {apiError && (
            <div className="mb-4 p-3 bg-red-100 text-red-700 rounded-md text-sm">
              {apiError}
            </div>
          )}

          <form className="space-y-4" onSubmit={handleSubmit(onSubmit)}>
            <div>
              <input
                type="text"
                placeholder="Username"
                className={`w-full border-0 border-b-2 ${
                  errors.username ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400`}
                {...register("username", {
                  required: "Username is required",
                  minLength: {
                    value: 3,
                    message: "Username must be at least 3 characters",
                  },
                  pattern: {
                    value: /^[a-zA-Z0-9_]+$/,
                    message:
                      "Username can only contain letters, numbers, and underscores",
                  },
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
                type="text"
                placeholder="Full Name"
                className={`w-full border-0 border-b-2 ${
                  errors.fullName ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400`}
                {...register("fullName", {
                  required: "Full name is required",
                  minLength: {
                    value: 2,
                    message: "Full name must be at least 2 characters",
                  },
                })}
              />
              {errors.fullName && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.fullName.message}
                </p>
              )}
            </div>

            <div>
              <input
                type="email"
                placeholder="Email Address"
                className={`w-full border-0 border-b-2 ${
                  errors.email ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400`}
                {...register("email", {
                  required: "Email address is required",
                  pattern: {
                    value: /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/,
                    message: "Please enter a valid email address",
                  },
                })}
              />
              {errors.email && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.email.message}
                </p>
              )}
            </div>

            <div>
              <input
                type="number"
                placeholder="Age"
                className={`w-full border-0 border-b-2 ${
                  errors.age ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400`}
                {...register("age", {
                  required: "Age is required",
                  min: {
                    value: 13,
                    message: "You must be at least 13 years old",
                  },
                  max: {
                    value: 120,
                    message: "Please enter a valid age",
                  },
                  valueAsNumber: true,
                })}
              />
              {errors.age && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.age.message}
                </p>
              )}
            </div>

            <div>
              <input
                type="password"
                placeholder="Password (min 8 characters)"
                className={`w-full border-0 border-b-2 ${
                  errors.password ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400`}
                {...register("password", {
                  required: "Password is required",
                  minLength: {
                    value: 8,
                    message: "Password must be at least 8 characters",
                  },
                  pattern: {
                    value:
                      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$/,
                    message:
                      "Password must contain at least one uppercase letter, one lowercase letter, and one number",
                  },
                })}
              />
              {errors.password && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.password.message}
                </p>
              )}
            </div>

            <div>
              <input
                type="password"
                placeholder="Confirm Password"
                className={`w-full border-0 border-b-2 ${
                  errors.confirmPassword ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400`}
                {...register("confirmPassword", {
                  required: "Please confirm your password",
                  validate: (value) =>
                    value === password || "The passwords do not match",
                })}
              />
              {errors.confirmPassword && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.confirmPassword.message}
                </p>
              )}
            </div>

            <div>
              <select
                className={`w-full border-0 border-b-2 ${
                  errors.role ? "border-red-500" : "border-gray-300"
                } focus:border-[#89b46c] focus:outline-none py-2 text-gray-600`}
                {...register("role", {
                  required: "Please select a role",
                })}
              >
                <option value={Role.Student}>Student</option>
                <option value={Role.Instructor}>Instructor</option>
                {/* Typically we don't allow users to register as Admin, but included for completeness */}
                {/* <option value={Role.Admin}>Admin</option> */}
              </select>
              {errors.role && (
                <p className="mt-1 text-xs text-red-500">
                  {errors.role.message}
                </p>
              )}
            </div>

            <div className="flex items-center mb-2">
              <input
                type="checkbox"
                id="terms"
                className={`mr-2 rounded ${
                  errors.terms ? "border-red-500" : "border-gray-300"
                } text-[#89b46c] focus:ring-[#89b46c]`}
                {...register("terms", {
                  required: "You must agree to the Terms and Privacy Policy",
                })}
              />
              <label htmlFor="terms" className="text-sm text-gray-600">
                I agree to the{" "}
                <a href="#" className="text-[#89b46c] hover:underline">
                  Terms
                </a>{" "}
                and{" "}
                <a href="#" className="text-[#89b46c] hover:underline">
                  Privacy Policy
                </a>
              </label>
            </div>
            {errors.terms && (
              <p className="mt-1 text-xs text-red-500">
                {errors.terms.message}
              </p>
            )}

            <button
              type="submit"
              disabled={isSubmitting}
              className={`w-full text-white bg-[#89b46c] border-2 border-[#89b46c] px-6 py-3 rounded-xl flex items-center justify-center gap-2 hover:bg-white hover:text-[#89b46c] transition-colors duration-300 font-medium ${
                isSubmitting ? "opacity-70 cursor-not-allowed" : ""
              }`}
            >
              {isSubmitting ? (
                "Processing..."
              ) : (
                <>
                  <UserPlusIcon className="w-5 h-5" />
                  Register Now
                </>
              )}
            </button>
          </form>

          <div className="mt-2 text-center">
            <p className="text-gray-600 text-sm">
              Already have an account?{" "}
              <Link
                key={"login"}
                to="/login"
                className="text-[#89b46c] font-medium hover:underline"
              >
                Log in here
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Signup;
