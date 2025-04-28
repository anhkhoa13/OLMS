import React, { useState } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { useAuth } from "../../contexts/AuthContext";

import axios from "axios";
import QuizInfoForm from "./QuizInfoForm";
import QuestionListEditor from "./QuestionListEditor";
import Error from "../../components/Error";
import * as Yup from "yup";

const API_URL = import.meta.env.VITE_BACKEND_URL;

// Yup schema
const quizInfoSchema = Yup.object().shape({
  title: Yup.string().required("Quiz title is required"),
  description: Yup.string().required("Description is required"),
  startTime: Yup.string().required("Start time is required"),
  endTime: Yup.string().required("End time is required"),
  isTimeLimited: Yup.boolean(),
  timeLimit: Yup.string().when("isTimeLimited", {
    is: true,
    then: (schema) => schema.required("Time limit is required"),
    otherwise: (schema) => schema.notRequired(),
  }),
  numberOfAttempts: Yup.number()
    .typeError("Number of attempts is required")
    .min(1, "Number of attempts must be at least 1")
    .required("Number of attempts is required"),
});

function CreateQuiz({ sectionId, onClose, nextOrder, onSuccess }) {
  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState(null);
  const [successMsg, setSuccessMsg] = useState(null);

  const { currentUser } = useAuth();

  const {
    register,
    handleSubmit,
    watch,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(quizInfoSchema),
    defaultValues: {
      title: "",
      description: "",
      startTime: "",
      endTime: "",
      isTimeLimited: false,
      timeLimit: "",
      numberOfAttempts: 1,
    },
    mode: "onBlur",
  });

  const onSubmit = async (data) => {
    if (!window.confirm("Are you sure you want to submit this quiz?")) {
      return;
    }
    if (questions.length <= 0) {
      alert("⚠️ Warning: Please add at least one question before proceeding.");
    }

    setErrorMsg(null);
    setSuccessMsg(null);
    setLoading(true);

    try {
      const quizJson = {
        instructorId: currentUser.id,
        ...data,
        timeLimit: data.isTimeLimited
          ? data.timeLimit.length === 5
            ? `00:${data.timeLimit}`
            : data.timeLimit
          : null,
        numberOfAttempts: Number(data.numberOfAttempts),
        order: nextOrder,
        sectionId,
      };
      console.log(quizJson);

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
      onClose();
      if (onSuccess) onSuccess(); // Trigger refresh
      reset(); // <-- Reset quiz info form
      setQuestions([]); // <-- Clear all questions
      window.alert("Quiz and questions created successfully!");
    } catch (error) {
      let message = "Something went wrong. Please try again later.";
      if (axios.isAxiosError(error)) {
        if (error.response) {
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
        <form onSubmit={handleSubmit(onSubmit)}>
          <QuizInfoForm register={register} errors={errors} watch={watch} />
          <QuestionListEditor
            questions={questions}
            setQuestions={setQuestions}
          />
          <button
            className="mt-6 w-full bg-[#89b46c] text-white font-semibold py-3 rounded-lg hover:bg-[#6f8f54] transition-colors duration-300"
            type="submit"
            // disabled={questions.length === 0 || loading}
            disabled={loading}
          >
            {loading ? "Submitting..." : "Submit"}
          </button>
        </form>
      </div>
    </div>
  );
}

export default CreateQuiz;
