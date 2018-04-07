using System;
using System.Collections.Generic;

namespace Questionable.Queries.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public long Likes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public Guid UserId { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public class Answer
        {
            public Guid AnswerId { get; set; }
            public Guid UserId { get; set; }
            public bool Accepted { get; set; }
            public string Description { get; set; }
        }
    }
}
