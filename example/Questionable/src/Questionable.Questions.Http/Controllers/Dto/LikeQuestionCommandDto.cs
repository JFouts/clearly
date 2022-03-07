using Microsoft.AspNetCore.Mvc;

namespace Questionable.Questions.Http.Controllers.Dto;

public class LikeQuestionCommandDto
{
    [FromRoute]
    public Guid? QuestionId { get; set; }

    [FromRoute]
    public Guid? UserId { get; set; }

    [FromBody]
    public LikeQuestionCommandBodyDto? Body { get; set; }

    public class LikeQuestionCommandBodyDto
    {
        public DateTime? OccurredAtUtc { get; set; }
    }
}
