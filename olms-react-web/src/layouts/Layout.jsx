import { Outlet } from "react-router";
import Navbar from "./Navbar";
import Footer from "./Footer";

// GeneralLayout.jsx
function Layout() {
  return (
    <div className="flex flex-col h-screen">
      <Navbar />
      <main className="flex-grow relative bg-gray-100">
        <Outlet />
      </main>
      <Footer />
    </div>
  );
}

export default Layout;
