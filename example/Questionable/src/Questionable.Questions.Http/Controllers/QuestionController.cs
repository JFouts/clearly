using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using DomainModeling.Core.Exceptions;
using DomainModeling.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Http.Controllers.Dto;

namespace Questionable.Questions.Http.Controllers
{
    [Route("api/command/questions/")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class QuestionController : ControllerBase
    {
        private readonly ICommandHandler<AskQuestionCommand> _askQuestionCommandHandler;
        private readonly ICommandHandler<LikeQuestionCommand> _likeQuestionCommandHandler;
        private readonly ICommandHandler<AnswerQuestionCommand> _answerQuestionCommandHandler;
        private readonly ICommandHandler<AcceptAnswerCommand> _acceptAnswerCommandHandler;
        private readonly ICommandFactory _commandFactory;
        private readonly IMapper _mapper;

        // TODO: Too many dependencies
        public QuestionController(IMapper mapper, ICommandFactory commandFactory, ICommandHandler<AskQuestionCommand> askQuestionCommandHandler, ICommandHandler<LikeQuestionCommand> likeQuestionCommandHandler, ICommandHandler<AnswerQuestionCommand> answerQuestionCommandHandler, ICommandHandler<AcceptAnswerCommand> acceptAnswerCommandHandler)
        {
            _mapper = mapper;
            _commandFactory = commandFactory;
            _askQuestionCommandHandler = askQuestionCommandHandler;
            _likeQuestionCommandHandler = likeQuestionCommandHandler;
            _answerQuestionCommandHandler = answerQuestionCommandHandler;
            _acceptAnswerCommandHandler = acceptAnswerCommandHandler;
        }

        [HttpPut("{questionId}")]
        public async Task<IActionResult> AskQuestionAsync(AskQuestionCommandDto request)
        {
            try
            {
                // TODO: Still not the best
                var command = _commandFactory.Create(() => _mapper.Map<AskQuestionCommand>(request));
                await _askQuestionCommandHandler.ExecuteAsync(command);
            }
            catch (InvalidModelStateException ex)
            {
                // TODO: This could be done in middleware
                return BadRequest(ex.ValidationErrors);
            }

            Debug.Assert(request.QuestionId != null, "request.QuestionId != null");

            return Accepted(new GenericResourceLocationResponseDto
            {
                Id = request.QuestionId.Value,
                Location = $"/questions/{request.QuestionId}"
            });
        }

        [HttpPut("{questionId}/likes/{userId}")]
        public async Task<IActionResult> LikeQuestionAsync(LikeQuestionCommandDto request)
        {
            try
            {
                var command = _commandFactory.Create(() => _mapper.Map<LikeQuestionCommand>(request));
                await _likeQuestionCommandHandler.ExecuteAsync(command);
            }
            catch (InvalidModelStateException ex)
            {
                return BadRequest(ex.ValidationErrors);
            }

            Debug.Assert(request.UserId != null, "request.UserId != null");

            return Accepted(new GenericResourceLocationResponseDto
            {
                Id = request.UserId.Value,
                Location = $"/questions/{request.QuestionId}/likes/{request.UserId}"
            });
        }

        [HttpPut("{questionId}/answers/{answerId}")]
        public async Task<IActionResult> AnswerQuestionAsync(AnswerQuestionCommandDto request)
        {
            try
            {
                var command = _commandFactory.Create(() => _mapper.Map<AnswerQuestionCommand>(request));
                await _answerQuestionCommandHandler.ExecuteAsync(command);
            }
            catch (InvalidModelStateException ex)
            {
                return BadRequest(ex.ValidationErrors);
            }

            Debug.Assert(request.AnswerId != null, "request.AnswerId != null");

            return Accepted(new GenericResourceLocationResponseDto
            {
                Id = request.AnswerId.Value,
                Location = $"/questions/{request.QuestionId}/answers/{request.AnswerId}"
            });
        }

        [HttpPut("{questionId}/acceptedAnswer")]
        public async Task<IActionResult> AcceptAnswerAsync(AcceptAnswerCommandDto request)
        {
            try
            {
                var command = _commandFactory.Create(() => _mapper.Map<AcceptAnswerCommand>(request));
                await _acceptAnswerCommandHandler.ExecuteAsync(command);
            }
            catch (InvalidModelStateException ex)
            {
                return BadRequest(ex.ValidationErrors);
            }

            Debug.Assert(request.QuestionId != null, "request.QuestionId != null");

            return Accepted(new GenericResourceLocationResponseDto
            {
                Id = request.QuestionId.Value,
                Location = $"/questions/{request.QuestionId}/acceptedAnswer"
            });
        }
    }
}
