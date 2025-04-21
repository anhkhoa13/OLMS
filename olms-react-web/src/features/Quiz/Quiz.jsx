import { useEffect, useReducer } from "react";
import { useParams } from "react-router-dom";
import Loader from "../../components/Loader";
import Error from "../../components/Error";
import QuizDetails from "./QuizDetails";
import Question from "./Question";
import Progress from "./Progress";
import Timer from "./Timer";
import NextBtn from "./NextBtn";
import FinishedDetails from "./FinishedDetails";

const initState = {
  quiz: null,
  status: "loading",
  error: null,
  index: 0,
  secondsRemaining: null,
};

function reducer(state, action) {
  switch (action.type) {
    case "SET_QUIZ":
      return { ...state, quiz: action.payload, status: "ready" };
    case "SET_ERROR":
      return { ...state, error: action.payload, status: "error" };
    case "START": {
      const [hours, minutes, seconds] = state.quiz.timeLimit
        .split(":")
        .map(Number);
      const totalSeconds = hours * 3600 + minutes * 60 + seconds;
      return {
        ...state,
        status: "active",
        secondsRemaining: totalSeconds,
      };
    }
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

  const [{ quiz, status, error, index, secondsRemaining }, dispatch] =
    useReducer(reducer, initState);
  const numQuestions = quiz?.questions?.length || 0;

  useEffect(() => {
    if (!code) return;

    const fetchQuiz = async () => {
      try {
        const response = await fetch(
          `https://localhost:7212/api/quiz/code/${code}`
        );
        if (!response.ok) throw new Error("Quiz not found");
        const data = await response.json();
        console.log(data);
        dispatch({ type: "SET_QUIZ", payload: data });
      } catch (err) {
        //setError(err.message || "Something went wrong");
        dispatch({ type: "SET_ERROR", payload: err.message });
        console.error(err);
      } finally {
        //setLoading(false);
      }
    };

    fetchQuiz();
  }, [code]);

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
            onNext={() => dispatch({ type: "NEXT_QUESTION" })}
          />
          <footer className="w-full max-w-2xl mx-auto px-4 py-6 flex justify-between items-center">
            <Timer dispatch={dispatch} secondsRemaining={secondsRemaining} />
            <NextBtn
              dispatch={dispatch}
              numQuestions={numQuestions}
              index={index}
            />
          </footer>
        </>
      )}
      {status === "finished" && <FinishedDetails />}
    </div>
  );
}

export default Quiz;
