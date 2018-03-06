using System;
using Questionable.Queries.Models;

namespace Questionable.Queries.QuestionSearch
{
    public class PopularQuestion
    {
        public Guid QuestionId { get; }
        public string Title { get; }
        public long Likes { get; }

        internal PopularQuestion(Question question)
        {
            QuestionId = question.Id;
            Title = question.Title;
            Likes = question.Likes;
        }
    }
}