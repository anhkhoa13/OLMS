import React, { useState, useEffect } from "react";
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

function CreateQuiz({
  sectionId,
  onClose,
  nextOrder,
  onSuccess,
  isEditing,
  quizId,
}) {
  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState(null);
  const [successMsg, setSuccessMsg] = useState(null);

  const { currentUser } = useAuth();

  function minutesToHHMMSS(minutes) {
    const hrs = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hrs.toString().padStart(2, "0")}:${mins
      .toString()
      .padStart(2, "0")}:00`;
  }
  const TimeSpanToMinutes = (timeString) => {
    const [hours, minutes] = timeString.split(":");
    return parseInt(hours) * 60 + parseInt(minutes);
  };

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
  const isGuid = (id) => {
    const guidRegex =
      /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
    return guidRegex.test(id);
  };

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
          ? minutesToHHMMSS(Number(data.timeLimit))
          : null,
        numberOfAttempts: Number(data.numberOfAttempts),
        order: nextOrder,
        sectionId,
      };

      if (isEditing) {
        // Update quiz
        await axios.put(`${API_URL}/api/quiz/update/${quizId}`, {
          ...quizJson,
          QuizId: quizId,
        });
        console.log(questions);

        // Update questions
        const questionsUpdatePayload = {
          QuizId: quizId,
          Questions: questions
            .filter((q) => q.id && isGuid(q.id)) // Only existing questions
            .map((q) => ({
              QuestionId: q.id,
              type: q.type,
              content: q.content,
              options: q.type === "MultipleChoice" ? q.options : null,
              correctOptionIndex:
                q.type === "MultipleChoice" ? q.correctOptionIndex : null,
              correctAnswer: q.type === "ShortAnswer" ? q.correctAnswer : null,
            })),
        };

        if (questionsUpdatePayload.Questions.length > 0) {
          await axios.put(
            `${API_URL}/api/quiz/update-questions`,
            questionsUpdatePayload
          );
        }

        //Add new questions
        const newQuestions = questions.filter((q) => q.id && !isGuid(q.id));
        if (newQuestions.length > 0) {
          await axios.post(`${API_URL}/api/quiz/add-questions`, {
            QuizId: quizId,
            Questions: newQuestions.map((q) => ({
              type: q.type,
              content: q.content,
              options: q.type === "MultipleChoice" ? q.options : null,
              correctOptionIndex:
                q.type === "MultipleChoice" ? q.correctOptionIndex : null,
              correctAnswer: q.type === "ShortAnswer" ? q.correctAnswer : null,
            })),
          });
        }
      } else {
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
      }

      onClose();
      if (onSuccess) onSuccess(); // Trigger refresh
      reset(); // <-- Reset quiz info form
      setQuestions([]); // <-- Clear all questions
      window.alert("Quiz and questions updated successfully!");
    } catch (error) {
      console.log(error);
      let message = "Something went wrong. Please try again later.";
      if (axios.isAxiosError(error)) {
        if (error.response) {
          message = error.response.data.code;
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

  useEffect(() => {
    const fetchQuizData = async () => {
      if (!isEditing) return;

      try {
        const shortQuizId = quizId.substring(0, 6);
        const response = await axios.get(
          `${API_URL}/api/quiz/code/${shortQuizId}`
        );
        const quizData = response.data;

        // Convert TimeSpan to minutes
        const timeLimitMinutes = quizData.timeLimit
          ? Math.floor(TimeSpanToMinutes(quizData.timeLimit))
          : 0;

        // Reset form with existing values
        reset({
          title: quizData.title,
          description: quizData.description,
          startTime: quizData.startTime,
          endTime: quizData.endTime,
          isTimeLimited: quizData.isTimeLimited,
          timeLimit: timeLimitMinutes.toString(),
          numberOfAttempts: quizData.numberOfAttempts,
        });

        // Set existing questions
        const formattedQuestions = quizData.questions.map((q) => ({
          id: q.questionId,
          type: q.type,
          content: q.content,
          options: q.options || [],
          correctOptionIndex: q.correctOptionIndex,
          correctAnswer: q.correctAnswer,
        }));
        setQuestions(formattedQuestions);
      } catch (error) {
        console.error("Error fetching quiz data:", error);
        setErrorMsg("Failed to load quiz data");
      }
    };

    fetchQuizData();
  }, [isEditing, quizId, reset]);

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
