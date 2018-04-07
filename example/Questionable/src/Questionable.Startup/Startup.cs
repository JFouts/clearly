using System;
using System.Reflection;
using AutoMapper;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;
using DomainModeling.EventRepository.EventStore.AspNetCore;
using DomainModeling.EventSourcing;
using DomainModeling.EventSourcing.AspNetCore;
using DomainModeling.EventSubscription.EventStore.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Questionable.Queries.Denormalizers.QuestionDetails;
using Questionable.Queries.Models;
using Questionable.Queries.QuestionSearch;
using Questionable.Questions.Commands.CommandHandlers;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Events.EventHandlers;
using Questionable.Questions.Events.Events;
using Questionable.Questions.Http.Mappings;
using Repository.Memory;
using Repositoy.Core;

namespace Questionable.Startup
{
    public class Startup : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ConfigureMvc(services);
            ConfigureAggregates(services);
            ConfigureDomainEvents(services);
            ConfigureCommands(services);
            ConfigureMappings(services);

            //TODO: Move out
            services.AddEventStore();
            services.AddScoped<QuestionDetailDenormalizer>();
            services.AddSingleton<ICommandFactory, ValidatedCommandFactory>();
            services.AddScoped<IQuestionSearcher, QuestionSearcher>();
            services.AddScoped<IRepository<Question>, MemoryRepository<Question>>();
            services.AddScoped<IQuery<Question>, MemoryRepository<Question>>();
            services.AddSingleton(typeof(MemoryDatabase<>));

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }

        private static void ConfigureMvc(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddSubscriptions()
                .AddCors()
                .AddApplicationPart(Assembly.Load("Questionable.Questions.Http"))
                .AddApplicationPart(Assembly.Load("Questionable.Queries.Http"))
                .AddApplicationPart(Assembly.Load("Questionable.Queries.Denormalizers.QuestionDetails"));
        }

        private static void ConfigureAggregates(IServiceCollection services)
        {
            

            services.AddAggregate<Questions.Aggregates.Question>()
                .AddEvent<QuestionAskedEvent>()
                .AddEvent<QuestionTaggedEvent>()
                .AddEvent<QuestionLikedEvent>()
                .AddEvent<QuestionAnsweredEvent>()
                .AddEvent<AnswerAcceptedEvent>();
        }

        private static void ConfigureDomainEvents(IServiceCollection services)
        {
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionAskedEvent>, QuestionAskedHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionTaggedEvent>, QuestionTaggedHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionLikedEvent>, QuestionLikedHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionAnsweredEvent>, QuestionAnsweredHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, AnswerAcceptedEvent>, AnswerAcceptedHandler>();
        }

        private static void ConfigureCommands(IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<AskQuestionCommand>, AskQuestionCommandHandler>();
            services.AddScoped<ICommandHandler<LikeQuestionCommand>, LikeQuestionCommandHandler>();
            services.AddScoped<ICommandHandler<AnswerQuestionCommand>, AnswerQuestionCommandHandler>();
            services.AddScoped<ICommandHandler<AcceptAnswerCommand>, AcceptAnswerCommandHandler>();
        }

        private static void ConfigureMappings(IServiceCollection services)
        {
            services.AddScoped<IMapper>(x => new Mapper(CreateMapperConfiguration()));
        }

        private static IConfigurationProvider CreateMapperConfiguration()
        {
            return new MapperConfiguration(y => y.AddProfile<HttpCommandMappingProfile>());
        }
    }
}
