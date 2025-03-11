﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; private set; }
    public int CorrectOptionIndex { get; private set; }
    public override QuestionType Type { get; protected set; } = QuestionType.MultipleChoice;

    public MultipleChoiceQuestion(Guid id, string content, List<string> options, int correctOptionIndex, Guid quizId) 
        : base(id, content, quizId)
    {
        Options = options;
        CorrectOptionIndex = correctOptionIndex;
    }

    public bool IsCorrect(int selectedOptionIndex) => selectedOptionIndex == CorrectOptionIndex;
}
