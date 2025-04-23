import { useEffect, useReducer } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";

import Loader from "../../components/Loader";
import Error from "../../components/Error";
import QuizDetails from "./QuizDetails";
import Question from "./Question";
import Progress from "./Progress";
import Timer from "./Timer";
import NavBtn from "./NavBtn";
import FinishedDetails from "./FinishedDetails";

const API_URL = import.meta.env.VITE_BACKEND_URL;

const studentId = "52B9A8B0-98C4-4206-81E6-BCB0EEC628E9"; //currently loggin (test)

const initState = {
  quiz: null,
  status: "loading",
  error: null,
  hasStarted: false,
  index: 0,
  secondsRemaining: null,
  userAnswers: [],
  attemptId: null,
};

function reducer(state, action) {
  switch (action.type) {
    case "SET_QUIZ":
      return {
        ...state,
        quiz: action.payload,
        status: "ready",
        userAnswers: action.payload.questions.map((question) => ({
          questionId: question.questionId,
          answer: null, // Initialize with null or empty string, depending on your use case
        })),
      };
    case "SET_ERROR":
      return { ...state, error: action.payload, status: "error" };
    case "START": {
      // Only set status to 'active' if no error occurred
      if (state.status === "error") {
        return { ...state, status: "error" }; // Keep the error status if it was an error
      }
      const [hours, minutes, seconds] = state.quiz.timeLimit
        .split(":")
        .map(Number);
      const totalSeconds = hours * 3600 + minutes * 60 + seconds;
      return {
        ...state,
        status: "active",
        secondsRemaining: totalSeconds,
        hasStarted: true,
      };
    }
    case "SET_ATTEMPT":
      return { ...state, attemptId: action.payload };
    case "ANSWER_QUESTION": {
      const { index, answer } = action.payload;
      // Find the corresponding questionId from quiz.questions using the index
      const questionId = state.quiz.questions[index].questionId;

      // Check if the answer already exists for this question
      const updatedAnswers = [...state.userAnswers];
      const existingAnswerIndex = updatedAnswers.findIndex(
        (ans) => ans.questionId === questionId
      );

      if (existingAnswerIndex !== -1) {
        // If an answer for this question already exists, update it
        updatedAnswers[existingAnswerIndex].answer = answer;
      } else {
        // Otherwise, add the new answer with questionId and answer
        updatedAnswers.push({ questionId, answer });
      }

      return { ...state, userAnswers: updatedAnswers };
    }
    case "PREVIOUS_QUESTION":
      return { ...state, index: state.index - 1 };
    case "NEXT_QUESTION":
      return { ...state, index: state.index + 1 };
    case "FINISH":
      return { ...state, status: "finished" };
    case "TICK":
      return {
        ...state,
        secondsRemaining: state.secondsRemaining - 1,
        status: state.secondsRemaining <= 0 ? "finished" : state.status,
      };
    default:
      throw new Error(`Action unknown: ${action.type}`);
  }
}

function Quiz() {
  const { code } = useParams();

  const [
    {
      quiz,
      status,
      error,
      index,
      secondsRemaining,
      userAnswers,
      hasStarted,
      attemptId,
    },
    dispatch,
  ] = useReducer(reducer, initState);
  const numQuestions = quiz?.questions?.length || 0;

  useEffect(() => {
    if (!code) return;

    const controller = new AbortController(); // 👈 create controller
    const signal = controller.signal;

    const fetchQuiz = async () => {
      try {
        const response = await fetch(
          `${API_URL}/api/quiz/code/${code}`,
          { signal } // 👈 pass signal to fetch
        );
        if (!response.ok) {
          const text = await response.text(); // safely read response even if not JSON
          throw new Error(`Error ${response.status}: ${text}`);
        }
        const data = await response.json();
        console.log(data);
        dispatch({ type: "SET_QUIZ", payload: data });
      } catch (err) {
        if (err.name === "AbortError") {
          console.log("Fetch aborted");
        } else {
          dispatch({ type: "SET_ERROR", payload: err.message });
          console.error(err);
        }
      }
    };

    fetchQuiz();

    return () => {
      controller.abort(); // 👈 cleanup on unmount or code change
    };
  }, [code]);

  useEffect(() => {
    if (hasStarted) {
      try {
        axios
          .post(`${API_URL}/api/quiz/attempts/start`, {
            StudentId: studentId,
            QuizId: quiz.quizId,
          })
          .then((res) => {
            const returnedAttempt = res.data.attemptId;

            if (returnedAttempt?.isSuccess && returnedAttempt?.value) {
              dispatch({
                type: "SET_ATTEMPT",
                payload: returnedAttempt.value,
              });
              dispatch({
                type: "START", // Start the timer only if the attempt starts successfully
              });
            } else {
              console.error(
                "Failed to start attempt:",
                returnedAttempt?.error?.errorMessage
              );
              dispatch({
                type: "SET_ERROR",
                payload:
                  returnedAttempt?.error?.errorMessage || "Unknown error",
              });
            }
          })
          .catch((err) => {
            console.error("Error starting quiz attempt:", err);
            dispatch({ type: "SET_ERROR", payload: err.message });
          });
      } catch (err) {
        dispatch({ type: "SET_ERROR", payload: err.message });
      }
    }
  }, [hasStarted, quiz?.quizId]);

  return (
    <div className="my-6 ">
      {status === "loading" && <Loader fullscreen />}
      {status === "error" && <Error message={error} />}
      {status === "ready" && (
        <QuizDetails quiz={quiz} onStart={() => dispatch({ type: "START" })} />
      )}
      {status === "active" && (
        <>
          <Progress index={index + 1} numQuestions={numQuestions} />
          <Question
            question={quiz.questions[index]}
            userAnswer={userAnswers[index]?.answer}
            onAnswer={(answer) =>
              dispatch({
                type: "ANSWER_QUESTION",
                payload: { index, answer },
              })
            }
          />
          <footer className="w-full max-w-2xl mx-auto px-4 py-6 flex justify-between items-center">
            <NavBtn
              dispatch={dispatch}
              index={index}
              numQuestions={numQuestions}
              direction="previous"
            />
            <Timer dispatch={dispatch} secondsRemaining={secondsRemaining} />
            <NavBtn
              dispatch={dispatch}
              index={index}
              numQuestions={numQuestions}
              direction="next"
            />
          </footer>
        </>
      )}
      {status === "finished" && (
        <FinishedDetails attemptId={attemptId} userAnswers={userAnswers} />
      )}
    </div>
  );
}

export default Quiz;
