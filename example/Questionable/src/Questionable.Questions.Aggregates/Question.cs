using System;
using System.Collections.Generic;
using DomainModeling.Core;

namespace Questionable.Questions.Aggregates
{
    public class Question : AggregateRoot
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public bool HasAcceptedAnswer { get; set; }
        public Answer AcceptedAnswer { get; set; }
        public IList<Answer> Answers { get; set; } = new List<Answer>();
        public IList<Guid> LikedUsers { get; set; } = new List<Guid>();
        public IList<string> Tags { get; set; } = new List<string>();

        public class Answer
        {
            public Guid AnswerId { get; set; }
            public Guid UserId { get; set; }
            public string Description { get; set; }
        }
    }
}
