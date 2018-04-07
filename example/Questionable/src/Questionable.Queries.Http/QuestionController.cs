using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Questionable.Queries.QuestionSearch;

namespace Questionable.Queries.Http
{
    [Route("query/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionSearcher _questionSearcher;

        public QuestionController(IQuestionSearcher questionSearcher)
        {
            _questionSearcher = questionSearcher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion([FromRoute] Guid id)
        {
            return Ok(await _questionSearcher.GetQuestion(id));
        }
    }
}
