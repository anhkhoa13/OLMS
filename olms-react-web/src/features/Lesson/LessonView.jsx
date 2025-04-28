import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import YouTube from "react-youtube";
import downloadBase64File from "../../utils/ConvertToFile";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function getYouTubeId(url) {
  const match = url.match(
    /(?:youtube\.com\/.*v=|youtu\.be\/)([a-zA-Z0-9_-]{11})/
  );
  return match ? match[1] : null;
}

function LessonView() {
  const { lessonId } = useParams();
  const [lesson, setLesson] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  console.log(lessonId);

  useEffect(() => {
    const fetchLesson = async () => {
      try {
        const response = await fetch(
          `${API_URL}/api/lesson?lessonId=${lessonId}`
        );

        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        setLesson(data);
        setError(null);
      } catch (err) {
        setError(err.message);
        setLesson(null);
      } finally {
        setLoading(false);
      }
    };

    if (lessonId) {
      fetchLesson();
    }
  }, [lessonId]);

  if (loading) {
    return <div className="text-center p-8">Loading lesson...</div>;
  }

  if (error) {
    return <div className="text-red-500 p-8">Error: {error}</div>;
  }

  if (!lesson) {
    return <div className="p-8">No lesson found</div>;
  }

  const { title, content, videoUrl, attachments = [] } = lesson;
  const youTubeId = videoUrl ? getYouTubeId(videoUrl) : null;

  return (
    <div className="max-w-8xl mx-auto bg-white rounded-lg shadow flex flex-col md:flex-row">
      {/* Video Section */}
      {videoUrl && youTubeId && (
        <div className="w-full md:w-2/3">
          <div className="aspect-video w-full">
            <YouTube
              videoId={youTubeId}
              className="w-full h-full"
              opts={{
                width: "100%",
                height: "100%",
                playerVars: { autoplay: 0 },
              }}
            />
          </div>
        </div>
      )}

      {/* Information Section */}
      <div className="w-full md:w-1/3 flex flex-col justify-start p-10">
        <h2 className="text-3xl font-bold mb-2">{title}</h2>
        <p className="text-gray-700 mb-6">{content}</p>

        {attachments.length > 0 && (
          <div>
            <h3 className="text-lg font-semibold mb-2">Attachments</h3>
            <ul>
              {attachments.map((att, idx) => (
                <li key={idx} className="mb-1">
                  <a
                    href={`data:${att.type};base64,${att.data}`}
                    download={att.name}
                    className="text-blue-600 underline hover:text-blue-800"
                    onClick={(e) => {
                      e.preventDefault();
                      downloadBase64File(att.data, att.type, att.name);
                    }}
                  >
                    {att.name} ({Math.round(att.size / 1024)} KB)
                  </a>
                </li>
              ))}
            </ul>
          </div>
        )}
      </div>
    </div>
  );
}

export default LessonView;
