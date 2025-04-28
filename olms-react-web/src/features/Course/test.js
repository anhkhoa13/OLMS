// // just to display for user to navigate (get)
// const courseSectionNavBar = {
//   title: "Ultimate React Course",
//   description:
//     "Master modern React from beginner to advanced! Next.js, Context API, React Query, Redux, Tailwind, advanced patterns",
//   status: "In progress",
//   instructor: "Dr. John Paul",
//   sections: [
//     {
//       title: "Section 1: React fundamentals",
//       lessons: [
//         {
//           id: "lesson1",
//           title: "Review Javascript",
//         },
//         {
//           id: "lesson2",
//           title: "Useful resources",
//         },
//       ],
//       assignments: [
//         {
//           id: "assignment1",
//           title: "Assignment 1",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Quiz",
//         },
//         {
//           id: "assignment2",
//           title: "Assignment 2",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Exercise",
//         },
//       ],
//       orders: ["lesson1", "assignment1", "lesson2", "assignment2"],
//     },
//     {
//       title: "Section 2: components, useState, and props",
//       lessons: [
//         {
//           id: "Lesson3",
//           title: "Thinking in React: State Management",
//         },
//         {
//           id: "Lesson4",
//           title: "Components, Composition, and Reuseability",
//         },
//       ],
//       assignments: [
//         {
//           id: "assignment3",
//           title: "Assignment 3",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Quiz",
//         },
//         {
//           id: "assignment4",
//           title: "Assignment 4",
//           dueDate: "2025-04-25T13:45:00Z",
//           assignmentType: "Exercise",
//         },
//       ],
//       orders: ["lesson3, lesson4", "assignment3", "assignment4"],
//     },
//     {
//       title: "Section 3: Advance - custom hooks, refs, more state ...",
//       assignments: [],
//       lessons: [
//         {
//           id: "lesson5",
//           title: "The Advanced useReducer Hook",
//         },
//       ],
//       orders: ["lesson5"],
//     },
//   ],
//   // for student only
//   progress: {
//     completedLesson: [
//       {
//         id: "",
//       },
//     ],
//   },
// };

// // when click on sidebar to view details of lesson (get)
// const lessons1 = {
//   id: "lessons1",
//   title: "Review Javascript",
//   description: "",
//   videoUrl: "https://www.youtube.com/watch?v=W6NZfCO5SIk",
//   attachments: [],
// };
// const lessons2 = {
//   id: "lessons2",
//   title: "Useful resources",
//   description: "",
//   videoUrl: null,
//   attachments: [
//     {
//       name: "sample.pdf",
//       type: "application/pdf",
//       data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
//       size: 585,
//     },
//   ],
// };

// const assignment = {
//   title: "Midterm assignment 1",
//   description:
//     "In this assignment, you will build a small React application that demonstrates your understanding of components, props, state, and event handling. The goal is to create a functional and interactive user interface—for example, a to-do list, a counter app, or a simple form. Your app should follow best practices in component structure and demonstrate clean, readable code.",
//   dueDate: "2025-04-25T13:45:00Z",
//   attachments: [
//     {
//       name: "OSBook.pdf",
//       type: "application/pdf",
//       data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
//       size: 585,
//     },
//     {
//       name: "Exe_2.pdf",
//       type: "application/pdf",
//       data: "JVBERi0xLjQKMSAwIG9iago8PCAvVHlwZSAvQ2F0YWxvZyAvUGFnZXMgMiAwIFIgPj4KZW5kb2JqCjIgMCBvYmoKPDwgL1R5cGUgL1BhZ2VzIC9LaWRzIFszIDAgUl0gL0NvdW50IDEgPj4KZW5kb2JqCjMgMCBvYmoKPDwgL1R5cGUgL1BhZ2UgL1BhcmVudCAyIDAgUiAvTWVkaWFCb3ggWzAgMCAyMDAgMjAwXSAvQ29udGVudHMgNCAwIFIgL1Jlc291cmNlcyA8PCAvRm9udCA8PCAvRjEgNSAwIFIgPj4gPj4gPj4KZW5kb2JqCjQgMCBvYmoKPDwgL0xlbmd0aCA0NCA+PgpzdHJlYW0KQlQgL0YxIDI0IFRmIDUwIDE1MCBUZCAoSGVsbG8sIFBERiEpIFRqIEVUCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PCAvVHlwZSAvRm9udCAvU3VidHlwZSAvVHlwZTEgL0Jhc2VGb250IC9IZWx2ZXRpY2EgPj4KZW5kb2JqCnhyZWYKMCA2CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxMCAwMDAwMCBuIAowMDAwMDAwMDYxIDAwMDAwIG4gCjAwMDAwMDAxMTYgMDAwMDAgbiAKMDAwMDAwMDIxMSAwMDAwMCBuIAowMDAwMDAwMjc2IDAwMDAwIG4gCnRyYWlsZXIKPDwgL1NpemUgNiAvUm9vdCAxIDAgUiA+PgpzdGFydHhyZWYKMzQxCiUlRU9G",
//       size: 585,
//     },
//   ],
// };

// // post
// const danhDauHoanThanh = {
//   studentId: "",
//   lessonId: "",
// };

// // when click on to view details of assignment (instructor) (get)
// const instructorAssignment = {
//   title: "",
//   description: "",
//   startDate: "",
//   dueDate: "",
//   assignmentType: "Execise",
//   attachments: [
//     {
//       name: "",
//       type: "",
//       data: "", //byte[]
//       size: "",
//     },
//   ],
//   assignmentAttemps: [
//     {
//       assignmentAttempId: "",
//       studentId: "",
//       studentName: "",
//       submittedAt: "",
//       status: "",
//       answers: [
//         {
//           name: "",
//           type: "",
//           data: "", //byte[]
//           size: "",
//         },
//       ],
//       isScored: false,
//     },
//   ],
// };
// // when click on sidebar to view assignment
// const studentAssignment = {
//   title: "",
//   description: "",
//   startDate: "",
//   dueDate: "",
//   assignmentType: "Execise",
//   attachments: [
//     {
//       name: "",
//       type: "",
//       data: "", //byte[]
//       size: "",
//     },
//   ],
// };
// // chi nop bai cho type: exercise assignmentAttempt (post)
// const nopBai = {
//   studentId: "",
//   assignmentId: "",
//   assignmentType: "Exercise",
//   answers: [
//     {
//       name: "",
//       type: "",
//       data: "", //byte[]
//       size: "",
//     },
//   ],
// };

// // when instructor chấm bài assignment (post)
// const ChamBai = {
//   assignmentAttemptId: "",
//   score: 43,
// };

// const announcementCourse = [
//   {
//     id: "",
//     title: "",
//     message: "",
//     postAt: "",
//   },
// ];

// const fetchjson = {
//   forum: {
//     posts: [
//       {
//         userId: "",
//         title: "",
//         body: "",
//         voteScore: 5,
//         comments: [
//           {
//             userId: "",
//             commentsId: "",
//             messages: "",
//           },
//         ],
//       },
//     ],
//   },
// };
