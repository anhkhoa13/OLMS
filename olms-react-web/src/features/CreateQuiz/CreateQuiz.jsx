import React, { useState, useReducer } from "react";
import axios from "axios";
import QuizInfoForm from "./QuizInfoForm";
import QuestionListEditor from "./QuestionListEditor";
import Error from "../../components/Error";

const API_URL = import.meta.env.VITE_BACKEND_URL;
const instructorId = "C93A3362-77BD-4EAC-BC09-4DD67E2776F5";

const initialQuizInfo = {
  instructorId: instructorId,
  title: "",
  description: "",
  startTime: "",
  endTime: "",
  isTimeLimited: false,
  timeLimit: "",
  numberOfAttempts: 1,
};

function quizInfoReducer(state, action) {
  switch (action.type) {
    case "SET_FIELD":
      return { ...state, [action.field]: action.value, instructorId };
    case "RESET":
      return { ...initialQuizInfo };
    default:
      throw new Error("Unknown action type: " + action.type);
  }
}

function CreateQuiz() {
  const [quizInfo, dispatchQuizInfo] = useReducer(
    quizInfoReducer,
    initialQuizInfo
  );
  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState(null);
  const [successMsg, setSuccessMsg] = useState(null);

  const handleQuizInfoChange = (e) => {
    const { name, value, type, checked } = e.target;
    dispatchQuizInfo({
      type: "SET_FIELD",
      field: name,
      value: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async () => {
    if (!window.confirm("Are you sure you want to submit this quiz?")) {
      return;
    }
    setErrorMsg(null);
    setSuccessMsg(null);
    setLoading(true);

    try {
      const quizJson = {
        instructorId: quizInfo.instructorId,
        title: quizInfo.title,
        description: quizInfo.description,
        startTime: quizInfo.startTime,
        endTime: quizInfo.endTime,
        isTimeLimited: quizInfo.isTimeLimited,
        timeLimit: quizInfo.isTimeLimited
          ? quizInfo.timeLimit.length === 5
            ? `00:${quizInfo.timeLimit}`
            : quizInfo.timeLimit
          : null,
        numberOfAttempts: Number(quizInfo.numberOfAttempts),
      };

      const createRes = await axios.post(
        `${API_URL}/api/quiz/create`,
        quizJson
      );
      const quizId = createRes.data?.quizId || createRes.data || null;

      if (!quizId) {
        throw new Error("Quiz creation did not return a quizId.");
      }

      const questionsJson = {
        QuizId: quizId,
        Questions: questions.map((q) => ({
          type: q.type,
          content: q.content,
          options: q.type === "MultipleChoice" ? q.options : null,
          correctOptionIndex:
            q.type === "MultipleChoice" ? q.correctOptionIndex : null,
          correctAnswer: q.type === "ShortAnswer" ? q.correctAnswer : null,
        })),
      };

      await axios.post(`${API_URL}/api/quiz/add-questions`, questionsJson);

      setSuccessMsg("Quiz and questions created successfully!");
      dispatchQuizInfo({ type: "RESET" }); // <-- Reset quiz info
      setQuestions([]); // <-- Clear all questions
      window.alert("Quiz and questions created successfully!");
    } catch (error) {
      let message = "Something went wrong. Please try again later.";
      if (axios.isAxiosError(error)) {
        if (error.response) {
          // message =
          //   error.response.data?.message ||
          //   error.response.data?.error ||
          //   JSON.stringify(error.response.data) ||
          //   `Server error: ${error.response.status}`;
          message = error.message;
        } else if (error.request) {
          message = "No response from server. Please check your network.";
        } else {
          message = error.message;
        }
      } else if (error instanceof Error) {
        message = error.message;
      }

      setErrorMsg(message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-[#f6faf3] py-10">
      <div className="max-w-3xl mx-auto p-8 bg-white rounded-lg shadow-lg">
        <h2 className="text-3xl font-bold mb-6 text-black">
          Create a New Quiz
        </h2>
        {errorMsg && <Error message={errorMsg} />}
        {successMsg && (
          <div className="mb-6 p-4 bg-green-100 border border-green-300 rounded-lg text-green-800 text-center font-semibold">
            {successMsg}
          </div>
        )}
        <QuizInfoForm value={quizInfo} onChange={handleQuizInfoChange} />
        <QuestionListEditor questions={questions} setQuestions={setQuestions} />
        <button
          className="mt-6 w-full bg-[#89b46c] text-white font-semibold py-3 rounded-lg hover:bg-[#6f8f54] transition-colors duration-300"
          type="button"
          onClick={handleSubmit}
          disabled={questions.length === 0 || loading}
        >
          {loading ? "Submitting..." : "Submit"}
        </button>
      </div>
    </div>
  );
}

export default CreateQuiz;
