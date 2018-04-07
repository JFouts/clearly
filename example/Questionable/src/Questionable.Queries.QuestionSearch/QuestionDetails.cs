using System;
using System.Collections.Generic;
using System.Linq;
using Questionable.Queries.Models;

namespace Questionable.Queries.QuestionSearch
{
    public class QuestionDetails
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public long Likes { get; }
        public IEnumerable<AnswerDetails> Answers { get; }
        public UserDetails User { get; }
        public IEnumerable<string> SubjectTags { get; }

        public QuestionDetails(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Description = question.Description;
            Likes = question.Likes;
            Answers = question.Answers.Select(x => new AnswerDetails(x));
            User = new UserDetails(question.UserId);
            SubjectTags = question.Tags;
        }

        public class AnswerDetails
        {
            public Guid Id { get; }
            public string Description { get; }
            public bool Accepted { get; }
            public UserDetails User { get; }

            public AnswerDetails(Question.Answer answer)
            {
                Id = answer.AnswerId;
                Description = answer.Description;
                Accepted = answer.Accepted;
                User = new UserDetails(answer.UserId);
            }
        }

        public class UserDetails
        {
            public Guid Id { get; }
            public string Fullname { get; }
            public string Title { get; }
            public string AvatarUrl { get; }

            public UserDetails(Guid userId)
            {
                Id = userId;
                Fullname = "Shiba Inu";
                Title = "Jr. Software Developer";
                AvatarUrl = string.Empty;
            }
        }
    }
}