using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Clearly.Core;
using Clearly.Core.ValidationAnnotations;

namespace Questionable.Questions.Commands.Commands
{
    public class AskQuestionCommand : Command
    {
        public Guid QuestionId { get; }

        public DateTime OccurredAtUtc { get; }

        public Guid UserId { get; }

        [Required]
        [NonEmpty]
        [StringLength(120)]
        public string Title { get; }

        [Required]
        [NonEmpty]
        [Markdown]
        public string Description { get; }

        [Required]
        public IEnumerable<QuestionSubjectTag> SubjectsTags { get; }

        public AskQuestionCommand(Guid questionId, DateTime occurredAtUtc, Guid userId, string title, string description, IEnumerable<QuestionSubjectTag> subjectTags)
        {
            QuestionId = questionId;
            OccurredAtUtc = occurredAtUtc;
            UserId = userId;
            Title = title;
            Description = description;
            SubjectsTags = subjectTags;
        }

        public class QuestionSubjectTag
        {
            [Required]
            [Alphanumeric]
            public string SubjectTag { get; }

            public QuestionSubjectTag(string subjectTag)
            {
                SubjectTag = subjectTag;
            }
        }
    }
}
