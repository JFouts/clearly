using Microsoft.AspNetCore.Mvc;

namespace Questionable.Questions.Http.Controllers.Dto;

public class AnswerQuestionCommandDto
{
    [FromRoute]
    public Guid? QuestionId { get; set; }

    [FromRoute]
    public Guid? AnswerId { get; set; }

    [FromBody]
    public AnswerQuestionCommandBodyDto? Body { get; set; }

    public class AnswerQuestionCommandBodyDto
    {
        public DateTime? OccurredAtUtc { get; set; }

        public Guid? UserId { get; set; }

        public string? Description { get; set; }
    }
}
