import React from "react";
import { AlertTriangle } from "lucide-react";

function Error({ message = "Something went wrong. Please try again later." }) {
  return (
    <div className="flex flex-col items-center justify-center text-center p-6 bg-red-100 border border-red-300 rounded-lg text-red-800">
      <AlertTriangle className="w-8 h-8 mb-2" />
      <h2 className="text-lg font-semibold">Error</h2>
      <p className="text-sm">{message}</p>
    </div>
  );
}

export default Error;
