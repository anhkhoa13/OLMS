export default function Modal({ open, onClose, title, children }) {
  if (!open) return null;

  return (
    <div className="fixed inset-0 z-60 flex items-start justify-center bg-[#58626b77] bg-opacity-60 p-4">
      <div className="bg-[#f6faf3] rounded-lg shadow-lg flex flex-col max-w-screen-md w-full relative my-8 max-h-[90vh]">
        {/* Header with fixed position */}
        <div className="px-8 pt-6 pb-4 border-b border-gray-100 shadow z-50">
          <div className="flex justify-between items-start">
            {title && (
              <h3 className="text-2xl font-bold text-[#6f8f54] mr-4">
                {title}
              </h3>
            )}
            <button
              onClick={onClose}
              className="text-gray-400 hover:text-gray-700 text-3xl font-light leading-none cursor-pointer"
              aria-label="Close"
            >
              &times;
            </button>
          </div>
        </div>

        {/* Scrollable content area */}
        <div className="flex-1 overflow-y-auto px-8 py-6">{children}</div>

        {/* Optional footer if needed later */}
        {/* <div className="px-8 py-4 border-t border-gray-100">
            // Add footer content here
          </div> */}
      </div>
    </div>
  );
}
