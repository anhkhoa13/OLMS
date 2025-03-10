using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; private set; }
    public int CorrectOptionIndex { get; private set; }

    protected override QuestionType Type => QuestionType.MultipleChoice;

    public MultipleChoiceQuestion(Guid id, string content, List<string> options, int correctOptionIndex) : base(id, content)
    {
        Options = options;
        CorrectOptionIndex = correctOptionIndex;
    }

    public bool IsCorrect(int selectedOptionIndex) => selectedOptionIndex == CorrectOptionIndex;
}
