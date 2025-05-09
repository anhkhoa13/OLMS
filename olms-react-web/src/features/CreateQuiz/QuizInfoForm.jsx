import React from "react";

function QuizInfoForm({ register, errors, watch }) {
  const isTimeLimited = watch("isTimeLimited");

  return (
    <div className="space-y-4 bg-white p-6 rounded-lg shadow-md border border-gray-200 mb-8">
      <h3 className="text-2xl font-bold mb-2 text-black">Quiz Details</h3>
      <div>
        <input
          {...register("title")}
          placeholder="Quiz Title"
          className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
        />
        {errors.title && (
          <div className="text-red-600 text-sm">{errors.title.message}</div>
        )}
      </div>
      <div>
        <textarea
          {...register("description")}
          placeholder="Description"
          className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
        />
        {errors.description && (
          <div className="text-red-600 text-sm">
            {errors.description.message}
          </div>
        )}
      </div>
      <div className="flex space-x-4">
        <div className="flex-1">
          <input
            {...register("startTime")}
            type="datetime-local"
            className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
          />
          {errors.startTime && (
            <div className="text-red-600 text-sm">
              {errors.startTime.message}
            </div>
          )}
        </div>
        <div className="flex-1">
          <input
            {...register("endTime")}
            type="datetime-local"
            className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
          />
          {errors.endTime && (
            <div className="text-red-600 text-sm">{errors.endTime.message}</div>
          )}
        </div>
      </div>
      <label className="inline-flex items-center space-x-2 text-black">
        <input
          type="checkbox"
          {...register("isTimeLimited")}
          className="form-checkbox h-5 w-5 text-[#89b46c]"
        />
        <span>Time Limited? (minutes)</span>
      </label>
      {isTimeLimited && (
        <div>
          {/* <input
            {...register("timeLimit")}
            type="time"
            step="1"
            placeholder="Time Limit (hh:mm:ss)"
            className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
          /> */}
          <input
            type="number"
            min="0"
            placeholder="Minutes"
            {...register("timeLimit")}
            className="w-1/2 px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
          />
          {errors.timeLimit && (
            <div className="text-red-600 text-sm">
              {errors.timeLimit.message}
            </div>
          )}
        </div>
      )}
      <div>Number of attempts</div>
      <div>
        <input
          {...register("numberOfAttempts")}
          type="number"
          min={1}
          className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#89b46c] text-black"
        />
        {errors.numberOfAttempts && (
          <div className="text-red-600 text-sm">
            {errors.numberOfAttempts.message}
          </div>
        )}
      </div>
    </div>
  );
}

export default QuizInfoForm;
