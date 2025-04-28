import { useRef } from "react";

// Utility function to convert a file to base64 (you can also move this to a utils file)
function convertFileToBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      const base64Data = reader.result.split(",")[1];
      resolve(base64Data);
    };
    reader.onerror = (error) => reject(error);
  });
}

/**
 * FileUpload component
 * @param {Object} props
 * @param {Function} props.onFilesChange - Callback with array of { name, data } objects
 * @param {Array} props.value - Current attachments array (optional)
 */
export default function FileUpload({ onFilesChange, value = [] }) {
  const inputRef = useRef();

  const handleFileChange = async (e) => {
    const files = Array.from(e.target.files);
    const newAttachments = await Promise.all(
      files.map(async (file) => ({
        name: file.name,
        data: await convertFileToBase64(file),
      }))
    );
    onFilesChange([...value, ...newAttachments]);
    inputRef.current.value = ""; // Reset file input for re-uploading same file if needed
  };

  return (
    <div className="border-2 border-dashed border-gray-300 rounded-lg p-4 text-center">
      <input
        type="file"
        multiple
        onChange={handleFileChange}
        className="hidden"
        id="file-upload"
        ref={inputRef}
      />
      <label
        htmlFor="file-upload"
        className="cursor-pointer text-[#6f8f54] hover:text-[#5e7d4a]"
      >
        Click to upload files
      </label>
      <div className="mt-2 text-sm text-gray-500">
        {value.map((file, index) => (
          <div key={index} className="text-sm text-gray-600">
            {file.name}
          </div>
        ))}
      </div>
    </div>
  );
}
