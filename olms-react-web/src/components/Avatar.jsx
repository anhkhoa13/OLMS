import defaultAvatar from "../assets/images/defaultAvatar.jpg";

function Avatar({ name, image = null }) {
  // Check if name is null, undefined, or empty string
  const isNameEmpty = !name || name.trim() === "";

  // Only calculate initial if name is not empty
  const initial = isNameEmpty ? "?" : name.charAt(0).toUpperCase();

  // Simple hashing function to generate a color from the name
  const hashCode = (str) => {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
      hash = (hash << 5) - hash + str.charCodeAt(i);
      hash = hash & hash; // Convert to 32bit integer
    }
    return hash;
  };

  // Convert hash code into a specific color
  const getColorFromHash = (hash) => {
    const colors = [
      "bg-red-500",
      "bg-blue-500",
      "bg-green-500",
      "bg-yellow-500",
      "bg-purple-500",
      "bg-pink-500",
      "bg-indigo-500",
      "bg-teal-500",
    ];
    const colorIndex = Math.abs(hash) % colors.length; // Make sure the index is within bounds
    return colors[colorIndex];
  };

  // Get color based on the name (use a default string if name is empty)
  const color = getColorFromHash(hashCode(isNameEmpty ? "default" : name));

  return (
    <div className="w-10 h-10">
      {/* If image is provided, use it */}
      {image ? (
        <img
          src={image}
          alt={name || "User"}
          className="w-full h-full rounded-full object-cover"
        />
      ) : isNameEmpty ? (
        // If name is empty and no image is provided, use default avatar
        <img
          src={defaultAvatar}
          alt="Default User"
          className="w-full h-full rounded-full object-cover"
        />
      ) : (
        // Otherwise use the initial with background color
        <div
          className={`w-full h-full ${color} text-white font-semibold rounded-full flex items-center justify-center`}
        >
          {initial}
        </div>
      )}
    </div>
  );
}

export default Avatar;
