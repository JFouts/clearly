using AutoMapper;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Http.Controllers.Dto;

namespace Questionable.Questions.Http.Mappings
{
    public class HttpCommandMappingProfile : Profile
    {
        public HttpCommandMappingProfile()
        {
            CreateMap<AskQuestionCommandDto, AskQuestionCommand>()
                .ForCtorParam("questionId", x => x.MapFrom(y => y.QuestionId))
                .ForCtorParam("occurredAtUtc", x => x.MapFrom(y => y.Body.OccurredAtUtc))
                .ForCtorParam("userId", x => x.MapFrom(y => y.Body.UserId))
                .ForCtorParam("title", x => x.MapFrom(y => y.Body.Title))
                .ForCtorParam("description", x => x.MapFrom(y => y.Body.Description))
                .ForCtorParam("subjectTags", x => x.MapFrom(y => y.Body.SubjectTags));

            CreateMap<string, AskQuestionCommand.QuestionSubjectTag>()
                .ConvertUsing(x => new AskQuestionCommand.QuestionSubjectTag(x));

            CreateMap<LikeQuestionCommandDto, LikeQuestionCommand>()
                .ForCtorParam("questionId", x => x.MapFrom(y => y.QuestionId))
                .ForCtorParam("occurredAtUtc", x => x.MapFrom(y => y.Body.OccurredAtUtc))
                .ForCtorParam("userId", x => x.MapFrom(y => y.UserId));

            CreateMap<AnswerQuestionCommandDto, AnswerQuestionCommand>()
                .ForCtorParam("questionId", x => x.MapFrom(y => y.QuestionId))
                .ForCtorParam("occurredAtUtc", x => x.MapFrom(y => y.Body.OccurredAtUtc))
                .ForCtorParam("answerId", x => x.MapFrom(y => y.AnswerId))
                .ForCtorParam("userId", x => x.MapFrom(y => y.Body.UserId))
                .ForCtorParam("description", x => x.MapFrom(y => y.Body.Description));

            CreateMap<AcceptAnswerCommandDto, AcceptAnswerCommand>()
                .ForCtorParam("questionId", x => x.MapFrom(y => y.QuestionId))
                .ForCtorParam("occurredAtUtc", x => x.MapFrom(y => y.Body.OccurredAtUtc))
                .ForCtorParam("answerId", x => x.MapFrom(y => y.Body.AnswerId))
                .ForCtorParam("userId", x => x.MapFrom(y => y.Body.UserId));
        }
    }
}
