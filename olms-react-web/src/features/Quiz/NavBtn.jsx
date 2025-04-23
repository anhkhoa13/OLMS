function NavBtn({ dispatch, index, numQuestions, direction }) {
  const isFirst = index === 0;
  const isLast = index === numQuestions - 1;

  // Don't render the "previous" button on the first question
  if (isFirst && direction === "previous") return null;

  const actionType =
    direction === "next"
      ? isLast
        ? "FINISH"
        : "NEXT_QUESTION"
      : "PREVIOUS_QUESTION";

  const buttonText =
    direction === "next" ? (isLast ? "Submit" : "Next") : "Previous";

  return (
    <div>
      <button
        className="bg-[#89b46c] text-gray-700 font-semibold px-6 py-2 rounded-xl shadow-md hover:bg-[#76a25b] transition cursor-pointer"
        onClick={() => dispatch({ type: actionType })}
      >
        {buttonText}
      </button>
    </div>
  );
}

export default NavBtn;
