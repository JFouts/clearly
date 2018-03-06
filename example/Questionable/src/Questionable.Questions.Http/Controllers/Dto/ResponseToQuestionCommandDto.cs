using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Questionable.Commands.Commands
{
    public class ResponseToQuestionCommandDto
    {
        [Required]
        [FromRoute]
        public Guid? QuestionId { get; set; }
        
        [Required]
        [FromRoute]
        public Guid? ResponseId { get; set; }

        [Required]
        [FromBody]
        public ResponseToQuestionCommandBody Body { get; set; }
    }

    public class ResponseToQuestionCommandBody
    {

        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public string Response { get; set; }

        [Required]
        public DateTime? OccuredAtUtc { get; set; }
    }
}