import React from "react";

const assignment = {
  title: "Midterm assignment 1",
  description:
    "In this assignment, you will build a small React application that demonstrates your understanding of components, props, state, and event handling. The goal is to create a functional and interactive user interface-for example, a to-do list, a counter app, or a simple form. Your app should follow best practices in component structure and demonstrate clean, readable code.",
  dueDate: "2025-04-25T13:45:00Z",
  attachments: [
    {
      name: "OSBook.pdf",
      type: "application/pdf",
      data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
      size: 585,
    },
    {
      name: "Exe_2.pdf",
      type: "application/pdf",
      data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
      size: 585,
    },
  ],
};

function formatDate(isoString) {
  const date = new Date(isoString);
  return date.toLocaleString(undefined, {
    year: "numeric",
    month: "short",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
}

function AssignmentView() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-50 to-indigo-100 py-8 px-2">
      <div className="max-w-xl w-full bg-white rounded-2xl shadow-lg border border-gray-100 p-8">
        <div className="mb-6">
          <h1 className="text-2xl font-bold text-indigo-700 mb-2">
            {assignment.title}
          </h1>
          <p className="text-gray-600">{assignment.description}</p>
        </div>
        <div className="flex items-center gap-2 mb-6">
          <span className="inline-block px-3 py-1 bg-indigo-100 text-indigo-700 text-xs font-semibold rounded-full">
            Due: {formatDate(assignment.dueDate)}
          </span>
        </div>
        <div>
          <h2 className="text-lg font-semibold text-gray-800 mb-2">
            Attachments
          </h2>
          <ul className="space-y-2">
            {assignment.attachments.map((file) => (
              <li
                key={file.name}
                className="flex items-center justify-between bg-gray-50 rounded-lg px-4 py-2 border border-gray-200"
              >
                <div className="flex items-center gap-3">
                  <span className="inline-flex items-center justify-center w-8 h-8 bg-indigo-200 text-indigo-700 rounded-full">
                    <svg
                      className="w-5 h-5"
                      fill="none"
                      stroke="currentColor"
                      strokeWidth={2}
                      viewBox="0 0 24 24"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M12 4v16m8-8H4"
                      />
                    </svg>
                  </span>
                  <span className="font-medium text-gray-700">{file.name}</span>
                  <span className="text-xs text-gray-400">
                    ({(file.size / 1024).toFixed(1)} KB)
                  </span>
                </div>
                <a
                  href={`data:${file.type};base64,${file.data}`}
                  download={file.name}
                  className="text-indigo-600 hover:text-indigo-900 text-sm font-semibold"
                >
                  Download
                </a>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
}

export default AssignmentView;
