function Avatar({ name, image = null }) {
  const initial = name?.charAt(0)?.toUpperCase() || "?";

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

  // Get color based on the name
  const color = getColorFromHash(hashCode(name || ""));

  return (
    <div className="w-10 h-10">
      {image ? (
        <img
          src={image}
          alt={name}
          className="w-full h-full rounded-full object-cover"
        />
      ) : (
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
