function Progress({ index, numQuestions }) {
  return (
    <header className="w-full flex flex-col items-center justify-center gap-2 py-4">
      <progress
        className="w-3/4 h-3 appearance-none [&::-webkit-progress-bar]:rounded-full [&::-webkit-progress-value]:bg-[#89b46c] [&::-webkit-progress-value]:rounded-full"
        max={numQuestions}
        value={index}
      />
      <p className="text-sm text-gray-700">
        Question <strong>{index}</strong> / {numQuestions}
      </p>
    </header>
  );
}

export default Progress;
