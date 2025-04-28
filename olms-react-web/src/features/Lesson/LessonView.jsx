import React from "react";
import YouTube from "react-youtube";
import downloadBase64File from "../../utils/ConvertToFile";

const lessons2 = {
  id: "lessons2",
  title: "Useful resources",
  description: "Things that you dont know you may need in this journey",
  videoUrl: "https://www.youtube.com/watch?v=W6NZfCO5SIk",
  //   videoUrl: null,
  attachments: [
    {
      name: "sample.pdf",
      type: "application/pdf",
      data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
      size: 585,
    },
  ],
};

function getYouTubeId(url) {
  const match = url.match(
    /(?:youtube\.com\/.*v=|youtu\.be\/)([a-zA-Z0-9_-]{11})/
  );
  return match ? match[1] : null;
}

function LessonView() {
  const { title, description, videoUrl, attachments } = lessons2;
  const youTubeId = videoUrl ? getYouTubeId(videoUrl) : null;

  return (
    <div className="max-w-8xl mx-auto bg-white rounded-lg shadow flex flex-col md:flex-row">
      {/* Video Section - Left, Large */}
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

      {/* Information Section - Right */}
      <div className="w-full md:w-1/3 flex flex-col justify-start p-10">
        <h2 className="text-3xl font-bold mb-2">{title}</h2>
        <p className="text-gray-700 mb-6">{description}</p>

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
      </div>
    </div>
  );
}

export default LessonView;
