using System;
using Questionable.Queries.Models;

namespace Questionable.Queries.QuestionSearch
{
    public class PopularQuestion
    {
        public Guid QuestionId { get; }
        public string Description { get; }
        public string Title { get; }
        public long Likes { get; }
        public long NumberOfAnswers { get; }
        public UserSummary User { get; }

        internal PopularQuestion(Question question)
        {
            QuestionId = question.Id;
            Description = question.Description;
            Title = question.Title;
            Likes = question.Likes;
            NumberOfAnswers = question.Answers.Count;
            User = new UserSummary(question.UserId);
        }

        public class UserSummary
        {
            public string Fullname { get; }
            public string Title { get; }
            public string AvatarUrl { get; }

            public UserSummary(Guid userId)
            {
                Fullname = "Shiba Inu";
                Title = "Jr. Software Developer";
                AvatarUrl = string.Empty;
            }
        }
    }
}