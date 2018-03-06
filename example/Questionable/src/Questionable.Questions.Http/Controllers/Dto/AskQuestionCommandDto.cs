using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Questionable.Questions.Http.Controllers.Dto
{
    public class AskQuestionCommandDto
    {
        [FromRoute]
        public Guid? QuestionId { get; set; }

        [FromBody]
        public AskQuestionCommandBodyDto Body { get; set; }

        public class AskQuestionCommandBodyDto
        {
            public DateTime? OccurredAtUtc { get; set; }
            public Guid? UserId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public IEnumerable<string> SubjectTags { get; set; } = new List<string>();
        }
    }
}
