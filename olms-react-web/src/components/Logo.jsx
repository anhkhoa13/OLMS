import { Link } from "react-router-dom";
import logo from "../assets/images/logo.png"; // Adjust path as needed

function Logo({ className = "" }) {
  return (
    <Link to="/" className={`block ${className}`}>
      <img
        src={logo}
        alt="Learn2 Logo"
        className="h-16 w-auto object-contain"
      />
    </Link>
  );
}

export default Logo;
