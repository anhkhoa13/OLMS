import { useEffect, useState } from "react";
import axios from "axios";

const API_URL = import.meta.env.VITE_BACKEND_URL;

function FinishedDetails({ attemptId, userAnswers }) {
  const [score, setScore] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    if (!attemptId || !userAnswers || userAnswers.length === 0) return;

    // Directly use userAnswers without mapping to quiz questions
    const formattedAnswers = userAnswers.map((ans) => ({
      QuestionId: ans.questionId, // Ensure this matches your state structure
      Answer: String(ans.answer), // Explicitly convert to string
    }));

    const payload = {
      attemptId: attemptId,
      answers: formattedAnswers,
    };
    console.log(payload);

    axios
      .post(`${API_URL}/api/quiz/attempts/submit`, payload)
      .then((res) => {
        setScore(res.data.score); // Extract the score
      })
      .catch((err) => {
        const message = err.response?.data?.error || "An error occurred.";
        setError(message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [attemptId, userAnswers]);

  if (loading) {
    return (
      <div className="text-center text-gray-600 text-lg mt-10">
        Submitting your quiz... please wait.
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-center text-red-500 text-lg mt-10">{error}</div>
    );
  }

  return (
    <div className="flex flex-col items-center justify-center p-8 rounded-xl shadow-lg bg-white max-w-md mx-auto mt-16">
      <h2 className="text-2xl font-bold text-green-600 mb-4">
        Quiz Submitted!
      </h2>
      <p className="text-gray-700 text-lg">Your score:</p>
      <div className="text-4xl font-extrabold text-green-700 mt-2">
        {score.toFixed(2)}%
      </div>
    </div>
  );
}

export default FinishedDetails;
