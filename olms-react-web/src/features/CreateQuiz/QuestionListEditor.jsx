import QuestionEditor from "./QuestionEditor";

function QuestionListEditor({ questions, setQuestions }) {
  const addQuestion = () => {
    setQuestions([
      ...questions,
      {
        id: Date.now() + Math.random(),
        type: "MultipleChoice",
        content: "",
        options: ["", ""],
        correctOptionIndex: 0,
        correctAnswer: null,
      },
    ]);
  };

  const updateQuestion = (idx, updated) => {
    const copy = [...questions];
    copy[idx] = updated;
    setQuestions(copy);
  };

  const deleteQuestion = (idx) => {
    setQuestions(questions.filter((_, i) => i !== idx));
  };

  return (
    <div className="mt-6">
      <div className="flex justify-between items-center mb-4">
        <h3 className="text-xl font-semibold text-black">Questions</h3>
        <button
          className="bg-[#89b46c] text-white px-4 py-2 rounded hover:bg-[#6f8f54] transition-colors duration-300"
          type="button"
          onClick={addQuestion}
        >
          Add Question
        </button>
      </div>
      {questions.map((q, idx) => (
        <QuestionEditor
          key={q.id}
          question={q}
          onChange={(updated) => updateQuestion(idx, updated)}
          onDelete={() => deleteQuestion(idx)}
        />
      ))}
    </div>
  );
}

export default QuestionListEditor;
