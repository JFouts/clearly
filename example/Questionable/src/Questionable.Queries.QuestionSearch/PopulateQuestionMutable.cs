using System;

namespace Questionable.Queries.QuestionSearch
{
    internal class PopulateQuestionMutable
    {
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public int Votes { get; set; }
    }
}