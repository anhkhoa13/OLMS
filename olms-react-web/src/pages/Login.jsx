import { DotLottieReact } from "@lottiefiles/dotlottie-react";
import { ArrowRightEndOnRectangleIcon } from "@heroicons/react/24/solid";
import { Link } from "react-router-dom";

function Login() {
  return (
    <div className="flex items-center justify-center p-4 mt-4 mb-4">
      <div className="w-full max-w-5xl flex bg-white h-125">
        {/* left */}
        <div className="w-1/2 flex items-center justify-center p-4">
          <DotLottieReact
            src="https://lottie.host/8f909684-69fe-494c-8889-530849977796/c3tqofKscj.lottie"
            loop
            autoplay
          />
        </div>
        {/* right */}
        <div className="w-1/2 p-8">
          <h2 className="text-3xl font-bold mb-2">Login</h2>
          <p className="text-gray-600 mb-6">
            Please enter your email and password to log in
          </p>
          <form>
            <div className="mb-6">
              <input
                type="text"
                placeholder="Email or Phone Number"
                className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
              />
            </div>
            <div className="mb-6">
              <input
                type="password"
                placeholder="Password"
                className="w-full border-0 border-b-2 border-gray-300 focus:border-[#89b46c] focus:outline-none py-2 placeholder-gray-400"
              />
            </div>

            <div className="mb-4 flex justify-between items-center">
              <button
                type="submit"
                className="text-white bg-[#89b46c] border-2 px-6 py-2 rounded-xl flex items-center gap-2 hover:bg-white hover:text-[#89b46c] hover:border-[#89b46c]-2"
              >
                <ArrowRightEndOnRectangleIcon className="w-6 h-6" />
                Login
              </button>
              <a href="#" className="hover:underline text-sm text-[#89b46c]">
                Forgot password?
              </a>
            </div>
          </form>
          <p className="text-gray-600 text-sm">
            Don't have an account?{" "}
            <Link
              key="signup"
              to="/signup"
              className="text-[#95C475] hover:underline"
            >
              Sign up now
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
}

export default Login;
