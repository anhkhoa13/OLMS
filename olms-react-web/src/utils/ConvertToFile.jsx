export default function downloadBase64File(base64, mimeType, fileName) {
  // Decode base64 to binary
  const byteCharacters = atob(base64);
  const byteNumbers = new Array(byteCharacters.length);
  for (let i = 0; i < byteCharacters.length; i++) {
    byteNumbers[i] = byteCharacters.charCodeAt(i);
  }
  const byteArray = new Uint8Array(byteNumbers);

  // Create blob
  const blob = new Blob([byteArray], { type: mimeType });

  // Create object URL
  const url = URL.createObjectURL(blob);

  // Create and trigger download link
  const link = document.createElement("a");
  link.href = url;
  link.download = fileName;
  document.body.appendChild(link);
  link.click();

  // Cleanup
  document.body.removeChild(link);
  URL.revokeObjectURL(url);
}

export const convertFileToBase64 = (file) => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsArrayBuffer(file);
    reader.onload = () => resolve(new Uint8Array(reader.result));
    reader.onerror = (error) => reject(error);
  });
};

// Usage example:
// const attachment = {
//   name: "sample.pdf",
//   type: "application/pdf",
//   data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
//   size: 585,
// };
