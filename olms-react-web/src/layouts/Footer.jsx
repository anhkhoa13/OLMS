import Logo from "../components/Logo";

function Footer() {
  return (
    <footer className="bg-gray-200 text-lime px-4 md:px-8 lg:px-16 py-10">
      <div className="max-w-7xl mx-auto grid grid-cols-1 md:grid-cols-3 gap-8">
        {/* Logo / Brand */}
        <div className="flex flex-col items-start">
          <Logo />
        </div>

        {/* Contact Info */}
        <div>
          <h3 className="text-xl font-bold mb-3">Support</h3>
          <ul className="space-y-2 text-lime/90">
            <li>
              📞 Hotline: <span className="font-semibold">1800 6868</span>
            </li>
            <li>📧 Email: support@olms.edu</li>
            <li>⏰ Mon – Fri: 9am – 6pm</li>
          </ul>
        </div>

        {/* Additional Links */}
        <div>
          <h3 className="text-xl font-bold mb-3">Quick Links</h3>
          <ul className="space-y-2 text-black/90">
            <li>
              <a href="#" className="hover:underline">
                About Us
              </a>
            </li>
            <li>
              <a href="#" className="hover:underline">
                Contact
              </a>
            </li>
            <li>
              <a href="#" className="hover:underline">
                Terms of Service
              </a>
            </li>
          </ul>
        </div>
      </div>

      <div className="border-t border-white/30 mt-10 pt-6 text-center text-sm text-black/70">
        © {new Date().getFullYear()} OLMS. All rights reserved.
      </div>
    </footer>
  );
}

export default Footer;
