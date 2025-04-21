import { DotLottieReact } from "@lottiefiles/dotlottie-react";
import { UserPlusIcon } from "@heroicons/react/24/solid";
import { Link } from "react-router-dom";

function Signup() {
  return (
    <div className="flex items-center justify-center p-4 mt-4 mb-4">
      <div className="w-full max-w-5xl flex bg-white h-100">
        {/* Left side - Animation */}
        <div className="w-1/2 flex items-center justify-center p-4">
          <DotLottieReact
            src="https://lottie.host/8f909684-69fe-494c-8889-530849977796/c3tqofKscj.lottie"
            loop
            autoplay
            className="w-full h-full"
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

          <form className="space-y-4">
            <div className="grid grid-cols-2 gap-4">
              <div>
                <input
                  type="text"
                  placeholder="First Name"
                  className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
                  required
                />
              </div>
              <div>
                <input
                  type="text"
                  placeholder="Last Name"
                  className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
                  required
                />
              </div>
            </div>

            <div>
              <input
                type="email"
                placeholder="Email Address"
                className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
                required
              />
            </div>

            <div>
              <input
                type="password"
                placeholder="Password (min 8 characters)"
                className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
                minLength="8"
                required
              />
            </div>

            <div>
              <input
                type="password"
                placeholder="Confirm Password"
                className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
                required
              />
            </div>

            <div className="flex items-center mb-2">
              <input
                type="checkbox"
                id="terms"
                className="mr-2 rounded text-[#89b46c] focus:ring-[#89b46c]"
                required
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

            <button
              type="submit"
              className="w-full text-white bg-[#89b46c] border-2 border-[#89b46c] px-6 py-3 rounded-xl flex items-center justify-center gap-2 hover:bg-white hover:text-[#89b46c] transition-colors duration-300 font-medium"
            >
              <UserPlusIcon className="w-5 h-5" />
              Register Now
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
