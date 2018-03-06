using System;
using Microsoft.AspNetCore.Mvc;

namespace Questionable.Questions.Http.Controllers.Dto
{
    public class AcceptAnswerCommandDto
    {
        [FromRoute]
        public Guid? QuestionId { get; set; }

        [FromBody]
        public AcceptAnswerCommandBodyDto Body { get; set; }

        public class AcceptAnswerCommandBodyDto
        {
            public DateTime? OccurredAtUtc { get; set; }

            public Guid? UserId { get; set; }

            public Guid? AnswerId { get; set; }
        }
    }
}
