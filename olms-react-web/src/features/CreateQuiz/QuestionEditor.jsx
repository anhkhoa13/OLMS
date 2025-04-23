function QuestionEditor({ question, onChange, onDelete }) {
  const handleTypeChange = (e) => {
    const type = e.target.value;
    if (type === "MultipleChoice") {
      onChange({
        ...question,
        type,
        options:
          question.options && question.options.length >= 2
            ? question.options
            : ["", ""],
        correctOptionIndex: 0,
        correctAnswer: null,
      });
    } else {
      onChange({
        ...question,
        type,
        options: null,
        correctOptionIndex: null,
        correctAnswer: "",
      });
    }
  };

  const handleContentChange = (e) => {
    onChange({ ...question, content: e.target.value });
  };

  // Multiple Choice Option Handlers
  const handleOptionChange = (idx, value) => {
    const newOptions = [...question.options];
    newOptions[idx] = value;
    onChange({ ...question, options: newOptions });
  };

  const addOption = () => {
    if (question.options.length < 4) {
      onChange({ ...question, options: [...question.options, ""] });
    }
  };

  const removeOption = (idx) => {
    const newOptions = question.options.filter((_, i) => i !== idx);
    let correctOptionIndex = question.correctOptionIndex;
    if (idx === correctOptionIndex) correctOptionIndex = 0;
    if (correctOptionIndex > idx) correctOptionIndex -= 1;
    onChange({ ...question, options: newOptions, correctOptionIndex });
  };

  const handleCorrectOptionChange = (idx) => {
    onChange({ ...question, correctOptionIndex: idx });
  };

  // Short Answer Handler
  const handleCorrectAnswerChange = (e) => {
    onChange({ ...question, correctAnswer: e.target.value });
  };

  return (
    <div className="border border-[#89b46c] p-4 rounded-lg mb-4 bg-white shadow-md">
      <div className="flex justify-between items-center mb-2">
        <strong className="text-black">Question</strong>
        <button
          type="button"
          className="text-red-500 hover:text-red-700 font-semibold"
          onClick={onDelete}
        >
          Delete
        </button>
      </div>
      <select
        value={question.type}
        onChange={handleTypeChange}
        className="mb-2 w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
      >
        <option value="MultipleChoice">Multiple Choice</option>
        <option value="ShortAnswer">Short Answer</option>
      </select>
      <input
        value={question.content}
        onChange={handleContentChange}
        placeholder="Question text"
        className="mb-2 w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
      />
      {question.type === "MultipleChoice" && (
        <div>
          <strong className="text-black">Options:</strong>
          {question.options.map((opt, idx) => (
            <div key={idx} className="flex items-center mb-1">
              <input
                type="text"
                value={opt}
                onChange={(e) => handleOptionChange(idx, e.target.value)}
                placeholder={`Option ${idx + 1}`}
                className="mr-2 flex-grow px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
              />
              <label className="mr-2 flex items-center space-x-1 text-black">
                <input
                  type="radio"
                  name={`correctOption${question.id}`}
                  checked={question.correctOptionIndex === idx}
                  onChange={() => handleCorrectOptionChange(idx)}
                  className="text-[#89b46c]"
                />
                <span>Correct</span>
              </label>
              {question.options.length > 2 && (
                <button
                  type="button"
                  className="text-red-400 hover:text-red-600 font-semibold"
                  onClick={() => removeOption(idx)}
                >
                  Remove
                </button>
              )}
            </div>
          ))}
          {question.options.length < 4 && (
            <button
              type="button"
              className="mt-2 bg-[#89b46c] text-white px-3 py-1 rounded hover:bg-[#6f8f54] transition-colors duration-300"
              onClick={addOption}
            >
              Add Option
            </button>
          )}
        </div>
      )}
      {question.type === "ShortAnswer" && (
        <input
          value={question.correctAnswer}
          onChange={handleCorrectAnswerChange}
          placeholder="Correct answer"
          className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
        />
      )}
    </div>
  );
}

export default QuestionEditor;
