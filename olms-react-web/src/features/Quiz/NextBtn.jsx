function NextBtn({ dispatch, answer, index, numQuestions }) {
  if (answer === null) return;

  const isLast = index === numQuestions - 1;

  return (
    <div className="">
      <button
        className="bg-[#89b46c] text-gray-700 font-semibold px-6 py-2 rounded-xl shadow-md hover:bg-[#76a25b] transition cursor-pointer"
        onClick={() => dispatch({ type: isLast ? "FINISH" : "NEXT_QUESTION" })}
      >
        {isLast ? "Finish" : "Next"}
      </button>
    </div>
  );
}

export default NextBtn;
