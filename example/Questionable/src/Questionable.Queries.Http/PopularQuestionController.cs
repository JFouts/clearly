using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Questionable.Queries.QuestionSearch;

namespace Questionable.Queries.Http;

[Route("query/popularQuestion")]
public class PopularQuestionController : ControllerBase
{
    private readonly IQuestionSearcher _questionSearcher;

    public PopularQuestionController(IQuestionSearcher questionSearcher)
    {
        _questionSearcher = questionSearcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetPopularQuestions([FromQuery] int skip = 0, [FromQuery] int take = 100)
    {
        if (ValidateModelState())
            return BadRequest();

        return Ok(await _questionSearcher.GetMostPopularQuestions(skip, take));

        bool ValidateModelState()
        {
            return skip < 0 || take > 100;
        }
    }
}
