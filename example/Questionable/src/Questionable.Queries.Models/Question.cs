using System;
using System.Collections.Generic;

namespace Questionable.Queries.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public int Likes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Answer> Answers { get; } = new List<Answer>();

        public class Answer
        {
            public Guid AnswerId { get; set; }
            public bool Accepted { get; set; }
        }
    }
}
